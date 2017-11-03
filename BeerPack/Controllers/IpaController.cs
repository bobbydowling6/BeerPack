using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class IpaController : Controller
    {
        // GET: Ipa
        public ActionResult List()
        {
                List<IPA> ipa = new List<IPA>();
                ipa.Add(new IPA
                {
                    ID = 1,
                    Name = "FIRESTONE Walker Brewing Co.",
                    Price = 2,
                    Description = "Union Jack IPA",
                    Image = "/images/IPA's/firestoneipa.jpg"
                });

                ipa.Add(new IPA
                {
                    ID = 2,
                    Name = "Goose Island IPA",
                    Price = 2,
                    Description = "IPA",
                    Image = "/images/IPA's/gooseislandipa.jpg"
                });

                ipa.Add(new IPA
                {
                    ID = 3,
                    Name = "Lagunitas Brewing Co.",
                    Price = 2,
                    Description = "Lagunitas IPA",
                    Image = "/images/IPA's/Lagunitas.jpg"
                });

                ipa.Add(new IPA
                {
                    ID = 4,
                    Name = "Samual Adams Rebel IPA",
                    Price = 2,
                    Description = "Rebel IPA",
                    Image = "/images/IPA's/samualadamsipa.jpg"
                });

                ipa.Add(new IPA
                {
                    ID = 5,
                    Name = "Sierra Nevada Brewing Co.",
                    Price = 2,
                    Description = "IPA",
                    Image = "/images/IPA's/sierranevada.jpg"
                });

                ipa.Add(new IPA
                {
                    ID = 6,
                    Name = "New Belgium Brewing Co",
                    Price = 2,
                    Description = "VooDoo Ranger IPA",
                    Image = "/images/Ipa's/voodoo-ranger-by-new-belgium-brewing-co.jpg"
                });
                return View(ipa);
            }
           
            public ActionResult Index(int id)
            {
                var ipa = new Models.IPA();
                if (id == 1)
                {
                    ipa.Name = "FireStone Walker Brewing Co.";
                    ipa.Description = "Union Jack IPA";
                    ipa.Price = 3;
                    ipa.Image = "/images/Ipa's/firestoneipa.jpg";
                }
                else if (id == 2)
                {
                    ipa.Name = "Goose Island IPA";
                    ipa.Description = "IPA";
                    ipa.Price = 3;
                    ipa.Image = "/images/Ipa's/gooseislandipa.jpg";
                }
                else if (id == 3)
                {
                    ipa.Name = "Lagunitas Brewing Co.";
                    ipa.Description = "Lagunitas IPA";
                    ipa.Price = 2;
                    ipa.Image = "/images/Ipa's/Lagunitas.jpg";
                }
                else if (id == 4)
                {
                    ipa.Name = "Samual Adams Rebel IPA";
                    ipa.Description = "Rebel IPA";
                    ipa.Price = 2;
                    ipa.Image = "/images/Ipa's/samualadamsipa.jpg";
                }
                else if (id == 5)
                {
                    ipa.Name = "Sierra Nevada Brewing Co.";
                    ipa.Description = "IPA";
                    ipa.Price = 2;
                    ipa.Image = "/images/Ipa's/sierranevada.jpg";
                }
                else if (id == 6)
                {
                    ipa.Name = "New Belgium Brewing Co.";
                    ipa.Description = "VooDoo Ranger IPA";
                    ipa.Price = 2;
                    ipa.Image = "/images/Ipa's/voodoo-ranger-by-new-belgium-brewing-co.jpg";
                }
                else
                {
                    return HttpNotFound("This product doesn't exist");
                }


                return View(ipa);
            }

            [HttpPost]
            public ActionResult Index(IPA model)
            {
                HttpContext.Session.Add("productName", model.Name);
                HttpContext.Session.Add("productPrice", model.Price.ToString("C"));
                HttpContext.Session.Add("productQuantity", model.Quantity.ToString());

                Response.AppendCookie(new HttpCookie("productName", model.Name));
                Response.AppendCookie(new HttpCookie("productPrice", model.Price.ToString("C")));
                Response.AppendCookie(new HttpCookie("productQuantity", model.Quantity.ToString()));

                return RedirectToAction("Index", "Cart");
            }
        }
    }