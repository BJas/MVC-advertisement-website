using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcKrisha.Models
{
    public class Flat
    {
        public int ID { get; set; }
        public int Floor { get; set; }
        public int FloorAll { get; set; }
        public int Room { get; set; }
        public int Price { get; set; }
        public double Area { get; set; }

        public string State { get; set; }

        public string Type { get; set; }

        public int BuildTime { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Image { get; set; }
        public string PhoneNumb { get; set; }
        public int RegionID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual Region Region { get; set; }
    }
}
