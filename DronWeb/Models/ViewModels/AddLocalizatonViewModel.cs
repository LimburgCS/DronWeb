using DronWeb.Models.Domains;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DronWeb.Models.ViewModels
{
    public class AddLocalizatonViewModel
    {
        public Localization Localization { get; set; }

        public List<Province> Provinces { get; set; }


        public String heading { get; set; }




        
    }
}