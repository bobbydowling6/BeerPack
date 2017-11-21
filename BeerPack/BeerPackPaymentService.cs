﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Braintree;

namespace BeerPack
{
    public class BeerPackPaymentService
    {
        protected Braintree.BraintreeGateway gateway;
        public BeerPackPaymentService()
        {
            string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
            string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
            string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

        }

        public Braintree.Customer GetCustomer(string email)
        {

            var customerGateway = gateway.Customer;
            Braintree.CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
            query.Email.Is(email);
            var matchedCustomers = customerGateway.Search(query);
            Braintree.Customer customer = null;
            if (matchedCustomers.Ids.Count == 0)
            {
                Braintree.CustomerRequest newCustomer = new Braintree.CustomerRequest();
                newCustomer.Email = email;

                var result = customerGateway.Create(newCustomer);
                customer = result.Target;
            }
            else
            {
                customer = matchedCustomers.FirstItem;
            }
            return customer;
        }

        internal Customer UpdateCustomer(string firstName, string lastName, string id)
        {
            Braintree.CustomerRequest request = new Braintree.CustomerRequest();
            request.FirstName = firstName;
            request.LastName = lastName;
            var result = gateway.Customer.Update(id, request);
            return result.Target;
        }

        internal void DeleteAddress(string email, string id)
        {
            Customer c = GetCustomer(email);
            gateway.Address.Delete(c.Id, id);
        }

        public void AddAddress(string email, string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postalCode, string countryName)
        {
            Customer c = GetCustomer(email);

            Braintree.AddressRequest newAddress = new Braintree.AddressRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                CountryName = countryName,
                PostalCode = postalCode,
                ExtendedAddress = extendedAddress,
                Locality = locality,
                Region = region,
                StreetAddress = streetAddress
            };

            gateway.Address.Create(c.Id, newAddress);
        }

        public string AuthorizeCard(string email, decimal total, decimal tax, string trackingNumber, string addressId, string cardholderName, string cvv, string cardNumber, string expirationMonth, string expirationYear)
        {
            var customer = GetCustomer(email);
            Braintree.TransactionRequest transaction = new Braintree.TransactionRequest();
            //transaction.Amount = 1m;    //I can hard-code a dollar amount for now to test everything else
            transaction.Amount = total;
            transaction.TaxAmount = tax;
            transaction.OrderId = trackingNumber;
            transaction.CustomerId = customer.Id;
            transaction.ShippingAddressId = addressId;
            //https://developers.braintreepayments.com/reference/general/testing/ruby
            transaction.CreditCard = new Braintree.TransactionCreditCardRequest
            {
                CardholderName = cardholderName,
                CVV = cvv,
                Number = cardNumber,
                ExpirationYear = expirationYear,
                ExpirationMonth = expirationMonth
            };

            var result = gateway.Transaction.Sale(transaction);

            return result.Message;
        }
    }
}

