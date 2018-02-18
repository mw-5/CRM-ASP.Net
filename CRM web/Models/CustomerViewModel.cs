using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM_web.Models
{
    public class CustomerViewModel
    {
        [Required]
        public int Cid { get; set; }

        public String Company { get; set; }

        public String Address { get; set; }

        public String Zip { get; set; }

        public String City { get; set; }

        public String Country { get; set; }

        public String ContractId { get; set; }

        public DateTime? ContractDate { get; set; }




        public Dictionary<ColDef, object> getMap()
        {
            DefTblCustomers tblDef = Model.Model.GetModel().TblCustomers;
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();

            map.Add(tblDef.Cid, Cid);
            map.Add(tblDef.Company, Company);
            map.Add(tblDef.Address, Address);
            map.Add(tblDef.Zip, Zip);
            map.Add(tblDef.City, City);
            map.Add(tblDef.Country, Country);
            map.Add(tblDef.ContractId, ContractId);
            map.Add(tblDef.ContractDate, ContractDate);            

            return map;
        }
    }
}