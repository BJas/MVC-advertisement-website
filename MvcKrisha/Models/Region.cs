using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcKrisha.Models
{
    public class Region
    {
        public int RegionID { get; set; }

        public string City { get; set; }

        public virtual ICollection<Flat> Flats { get; set; }
    }
}