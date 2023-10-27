using DronWeb.Models.Domains;
using DronWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using PagedList;

namespace DronWeb.Models.Repositories
{
    public class LocalizationRepository
    {
        public IEnumerable<Localization> GetLocalizations(string search, string SelectedProvinces)
        {
            using (var context = new ApplicationDbContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    return context.Localizations.Where(x => x.City.Contains(search)).Include(x => x.provinces).
                        ToList();
                }
                

                return context.Localizations.Where(x=>x.City == x.City).Include(x => x.provinces).
                    AsQueryable().ToList(); // jesli chce pobierać dane z innej tabeli musi być include, wtedy w widku moge łączyć z innymi tabelami!!

                
                    
               // return context.Localizations.ToList(); //wszystkiego lokalizacje wraz z info o użytkowniku  wywolujemy tolist
            }
        }


        public List<Province> GetProvinces ()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Provinces.ToList();
            }
        }

        public Localization GetLocalization(int id) // do edycji
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Localizations
                    
                    .Include(x => x.provinces)
                    .Single(x => x.Id == id);
            }
        }

        public void Add(Localization localization, HttpPostedFileBase image)
        {
            using (var context = new ApplicationDbContext())
            {
                
                localization.CreatedDate = DateTime.Today;
                localization.Image = ConvertToBytes(image);
                context.Localizations.Add(localization);
                context.SaveChanges();
            }
        }


        public void  Update(Localization localization, HttpPostedFileBase image)
        {
            using (var context = new ApplicationDbContext())
            {
                
                    var localizationUpdate = context.Localizations
                    .Single(x => x.Id == localization.Id  /*&& x.UserId == localization.UserId*/);

                    if (localization.Image != null)
                    {
                        localization.Image = ConvertToBytes(image);
                        localizationUpdate.Image = localization.Image;
                    }


                    
                    localizationUpdate.Name = localization.Name;
                    localizationUpdate.City = localization.City;
                    localizationUpdate.ProvinceId = localization.ProvinceId;
                    localizationUpdate.equipment = localization.equipment;
                    localizationUpdate.description = localization.description;
                
                

                

                // parametry ktore moga być zmienione i zaktualizowane

                context.SaveChanges();
            }
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            
                byte[] imageBytes = null;
                BinaryReader reader = new BinaryReader(image.InputStream);
                imageBytes = reader.ReadBytes((int)image.ContentLength); //  odczytuje zawartość przesłanego pliku obrazu i zapisuje ją jako tablicę bajtów 
                return imageBytes;
            
          
                
            
           
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var localizationToDelete = context.Localizations
                    .Single(x => x.Id == id /*&& x.UserId == userId*/);
                context.Localizations.Remove(localizationToDelete);
                context.SaveChanges();
            }
        }

        
        public IEnumerable<Localization> GetSearch(int SelectedProvinces)
        {
            using (var context = new ApplicationDbContext())
            {
                

               
                    return context.Localizations.Where(a=>a.ProvinceId == SelectedProvinces).Include(x => x.provinces).ToList();
                

                



            }
        }
    }
}