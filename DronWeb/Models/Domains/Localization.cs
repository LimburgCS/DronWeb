using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DronWeb.Models.Domains
{
    

    public class Localization
    {

        
        public int Id { get; set; }

        
        public string UserId { get; set; }

        [Display(Name ="Nick")]
        [Required(ErrorMessage="Pole Nick jest wymagane")]
        public string Name { get; set; }

        [Display(Name = "Wykorzystany sprzęt")]
        [Required(ErrorMessage = "Pole wykorzystany sprzęt jest wymagane")]
        public string equipment { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Pole lokalizacja jest wymagane")]
        public string City { get; set; }

       
        public int ProvinceId { get; set; }
            
            

        [Display(Name = "Opis lokalizacji")]
        [Required(ErrorMessage = "Pole opis jest wymagane")]
        public string description { get; set; }

        [Display(Name ="Data utworzenia")]
        public DateTime CreatedDate { get; set; }


        public float latitude { get; set; }
        public float longitude { get; set; }



        public byte[] Image { get; set; }


        
        
        public Province provinces { get; set; }

        public ApplicationUser User { get; set; }




        
    }
}