using DronWeb.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DronWeb.Models.Repositories
{
    public class FiltersRepositories
    {
        //public Localization sortOrder(string sortOrder, Localization localization)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        localization.City = String.IsNullOrEmpty(sortOrder) ? String.Empty : sortOrder;

        //        var localizations = from s in context.Localizations select s;

        //        switch (sortOrder)
        //        {
        //            case "localization.City":
        //                localizations = localizations.OrderByDescending(s => s.City);
        //                break;

        //            default:
        //                localizations = localizations.OrderByDescending(s => s.City);
        //                break;
        //        }


        //    }
        //}
    }
}