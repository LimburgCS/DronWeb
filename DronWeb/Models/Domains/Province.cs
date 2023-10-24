using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DronWeb.Models.Domains
{
    public class Province
    {
        public Province()
        {
            Localization = new Collection<Localization>(); // inicjalizacja tabeli
        }

        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public ICollection<Localization> Localization { get; set; }

        

        
    }
}