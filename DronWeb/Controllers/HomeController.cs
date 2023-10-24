using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DronWeb.Models.Domains;
using DronWeb.Models.Repositories;
using DronWeb.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Contexts;
using DronWeb.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Security;

namespace DronWeb.Controllers
{


    
    public class HomeController : Controller
    {

        private LocalizationRepository _localizationRepository = new LocalizationRepository();
        private readonly ApplicationDbContext _context = new ApplicationDbContext();








        public ActionResult AddLocalization(int id = 0) // widok tabelki dodawania
        {


            //var vm = PrepareAddLocalizationVm(localization);
            //return View("addLocalization", vm);

            var localization = id == 0 ? GetNewLocalization() :
                _localizationRepository.GetLocalization(id);

            var vm = PrepareAddLocalizationVm(localization);
            return View(vm);



        }

        private Localization GetNewLocalization()
        {
            return new Localization
            {
                CreatedDate = DateTime.Now,

            };
        }


        //public ActionResult Create(Localization localization) // widok tabelki dodawania
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        var vm = PrepareAddLocalizationVm(localization);
        //        return View("addLocalization", vm);
        //    }

        //    if (localization.Id != 0)
        //    {
        //        _localizationRepository.Update(localization);
        //    }

        //    return RedirectToAction("Localization");


        //}





        //public ActionResult AddLocalization(Localization localization, int id)
        //{
        //    var UserId = User.Identity.GetUserId();
        //    var table = _localizationRepository.GetLocalization(id, UserId);
        //    var vm = PrepareLocalizationVm(table, UserId);
        //    return View(vm);
        //}




        public ActionResult Localization(string search,string searchBy, string sortBy, string SelectedProvinces)
        {
            //var userId = User.Identity.GetUserId();


            ViewBag.SortCityParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.DateSortParm = sortBy == "Date" ? "date_desc" : "Date";
           // SelectedProvinces = Request.Form["SelectedProvinces"];
            var province = _context.Provinces.ToList();
            ViewBag.Provinces = new SelectList(province, "Id", "Name");
            
            var localizations = _localizationRepository.GetLocalizations(search, SelectedProvinces);
            switch (sortBy)
            {
                case "Name desc":
                    localizations = localizations.OrderByDescending(x => x.City);
                    break;
                case "Date":
                    localizations = localizations.OrderBy(x => x.CreatedDate);
                    break;

                default:
                    localizations = localizations.OrderBy(x => x.City);
                    break;
                    
            }

 

            

            return View("LocalizationTable", localizations);




        }

        [HttpPost]
        public ActionResult Search(int SelectedProvinces)
        {
            var province = _context.Provinces.ToList();

            
            ViewBag.Provinces = new SelectList(province,"Id", "Name");
            var selProvince = _localizationRepository.GetSearch(SelectedProvinces);
            
            
            return View("LocalizationTable", selProvince);
        }




        [HttpPost]
        public ActionResult AddLocalization(Localization localization, HttpPostedFileBase image)
        {
            //var userId = User.Identity.GetUserId(); // aby apka byla bezpieczna to jest ten kod
            //localization.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareAddLocalizationVm(localization);
                return View("addLocalization", vm);
            }

            
                if (localization.Id == 0)
                {

                    image = Request.Files["image"];

                    _localizationRepository.Add(localization, image);

                }
                else
                {
                      image = Request.Files["image"];
                      _localizationRepository.Update(localization, image);
                }

            return RedirectToAction("Localization");

        }
        //private object PrepareLocalizationVm(Localization localization)
        //{
        //    return new AddLocalizatonViewModel
        //    {
        //       Localization = localization,
        //       Provinces = _localizationRepository.GetProvinces()
                
        //    };

        //}

        private object PrepareAddLocalizationVm(Localization localization)
        {
            return new AddLocalizatonViewModel
            {
                
                Localization =localization,

                Provinces = _localizationRepository.GetProvinces(),

                heading = localization.Id == 0 ? "Dodawanie nowej lokalizacji" : "Edytowanie lokalizacji"


            };

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _localizationRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                // logowanie do pliku
                return Json(new { success = false, Message = exception.Message });
            }

            return Json(new { success = true });
        }

 


        public ActionResult RetriveImage(int id) // wyświetlanie zdjęcia
        {

            byte[] cover = GetImageFromDataBase(id);

            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        //private byte[] GetImageFromDataBase(int id) // to jest to samo co niżej
        //{
        //    var q = from temp in _context.Localizations where temp.Id == id select temp.Image;
        //    byte[] cover = q.First();
        //    return cover;
        //}

        private byte[] GetImageFromDataBase(int id)
        {
            var getImage = _context.Localizations.Where(x => x.Id == id).Select(x=>x.Image);
            byte[] cover = getImage.First();
            return cover;
        }




        public ActionResult Index(Localization localization)
        {

            return View();
        }



        public ActionResult Gallery()
        {
            ViewBag.Message = "Galeria";

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "SkyDron";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}