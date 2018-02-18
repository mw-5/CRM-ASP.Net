using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_web.Models.Model
{
    public class DefTblCustomers
    {
        public DefTblCustomers()
        {
            TblName = "customers";
            Cid = new ColDef() { Name = "cid", Type = ColTypes.Numeric };
            Company = new ColDef() { Name = "company", Type = ColTypes.Text };
            Address = new ColDef() { Name = "address", Type = ColTypes.Text };
            Zip = new ColDef() { Name = "zip", Type = ColTypes.Text };
            City = new ColDef() { Name = "city", Type = ColTypes.Text };
            Country = new ColDef() { Name = "country", Type = ColTypes.Text };
            ContractId = new ColDef() { Name = "contract_id", Type = ColTypes.Text };
            ContractDate = new ColDef() { Name = "contract_date", Type = ColTypes.Date };
        }

        public String TblName { get; private set; }
        public ColDef Cid { get; private set; }
        public ColDef Company { get; private set; }
        public ColDef Address { get; private set; }
        public ColDef Zip { get; private set; }
        public ColDef City { get; private set; }
        public ColDef Country { get; private set; }
        public ColDef ContractId { get; private set; }
        public ColDef ContractDate { get; private set; }
    }
}
