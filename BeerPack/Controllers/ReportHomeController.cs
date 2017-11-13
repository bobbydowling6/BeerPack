using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//.Net has a bunch of built-in stuff for talking to SQL - make sure you add these namespaces
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using BeerPack.Models;
using System.Data.Common;

namespace BeerPack.Reports.Controllers
{
    public class ReportHomeController : Controller
    {
        // GET: Reports/ReportIndex
        public ActionResult Index(string selectedState = "California")
        {
            //Let's work with ADO.Net - High Ceremony, but high-level of control

            SalesReportModel model = new SalesReportModel();

            //Look up a connection string by name out of web.config
            string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Open the doorway to the server
                connection.Open();

                //Commands are used to send a statement to SQL
                DbCommand quantityCommand = connection.CreateCommand();
                quantityCommand.CommandText = "SELECT * FROM vw_BillingStates";
                //If you're doing an update, delete, or insert, just send the query over:
                //command.ExecuteNonQuery()   

                //Execute scalar is used if your query gives back one single value (e.g. one row with one columns)
                //command.ExecuteScalar()

                //Use execute reader to read data in, record by record
                List<string> states = new List<string>();
                using (DbDataReader reader = quantityCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        states.Add(reader.GetString(0));
                    }
                }


                SqlCommand command1 = connection.CreateCommand();
                quantityCommand.CommandText = @"select top 5 ProductID, SUM(LineTotal) from salesorderdetail JOIN SalesOrderHeader
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN[Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                         WHERE Address.StateProvince = '" + selectedState + "' group by ProductID";
                SqlCommand command2 = connection.CreateCommand();
                quantityCommand.CommandText = @"select top 5 ProductID, SUM(OrderQty) from salesorderdetail JOIN SalesOrderHeader 
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN [Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                        WHERE Address.StateProvince = '" + selectedState + "' group by ProductID";

                List<string> Quantity = new List<string>();
                model.States = states.ToArray();
                //TODO: Get these using ADO.Net
                model.TopSalesByDollar = new TopSaleByDollar[0];

                    

               
                model.TopSalesByQuantity = new TopSaleByQuantity[0];
                //Advantage of Stored Procedures : parameters cannot be used for SQL Injection attacks!
                //Other advantage - SQL will cache the execution plan, as opposed to figuring it out each time you run dynamic T-SQL
                quantityCommand.CommandText = "sp_TopSalesByQuantity";
                quantityCommand.CommandType = System.Data.CommandType.StoredProcedure;
                quantityCommand.Parameters.Add(new SqlParameter("@stateProvince", selectedState));

                List<TopSaleByQuantity> quantities = new List<TopSaleByQuantity>();
                //Another advantage of stored procedures - they're securable! (meaning I have to explicitly grant access to this stored procedure to the SalesUser login)
                //GRANT EXEC ON SalesLT.sp_TopSalesByQuantity TO SalesUser
                using (DbDataReader reader = quantityCommand.ExecuteReader())
                {
                    int totalQuantityOrdinal = reader.GetOrdinal("TotalQuantity");
                    int productNameOrdinal = reader.GetOrdinal("Name");
                    int productIdOrdinal = reader.GetOrdinal("ProductID");

                    while (reader.Read())
                    {
                        quantities.Add(new TopSaleByQuantity
                        {
                            TotalQuantity = reader.GetInt32(totalQuantityOrdinal),
                            ProductName = reader.GetString(productNameOrdinal),
                            ProductID = reader.GetInt32(productIdOrdinal)
                        });
                    }
                }

                model.TopSalesByQuantity = quantities.ToArray();
                //Make sure you close the connection when you're finished
                connection.Close();
                    }
                    return View(model);
                }
            }
        }
    


    
