using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_web.Models.Model;

namespace CRM_web.Tests.TestCases
{
    public static class TestCase
    {
        public static void PopulateTestData()
        {
            CleanUp();
            InsertTestCustomer();
            InsertTestContactPerson();
            InsertTestNote();
        }

        public static void CleanUp()
        {
            DeleteTestCustomer();
        }

        public static void DeleteTestCustomer()
        {
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery("DELETE FROM contact_persons WHERE cid = " + TestCustomer.cid);
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery("DELETE FROM notes WHERE cid = " + TestCustomer.cid);
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery("DELETE FROM customers WHERE cid = " + TestCustomer.cid);
        }

        public static void DeleteTestContactPerson()
        {
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery("DELETE FROM contact_persons WHERE id = " + TestContactPerson.id);
        }

        public static void DeleteTestNote()
        {
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery("DELETE FROM notes WHERE id = " + TestNote.id);
        }

        public static void InsertTestCustomer()
        {
            DefTblCustomers def = new DefTblCustomers();
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();
            map.Add(def.Cid, TestCustomer.cid);
            map.Add(def.Company, TestCustomer.company);
            map.Add(def.Address, TestCustomer.address);
            map.Add(def.Zip, TestCustomer.zip);
            map.Add(def.City, TestCustomer.city);
            map.Add(def.Country, TestCustomer.country);
            map.Add(def.ContractId, TestCustomer.contractId);
            map.Add(def.ContractDate, TestCustomer.contractDate);
            String sql = SqlStatements.BuildInsert(def.TblName, map);
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery(sql);
        }

        public static void InsertTestContactPerson()
        {
            DefTblContactPersons def = new DefTblContactPersons();
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();
            map.Add(def.Id, TestContactPerson.id);
            map.Add(def.Cid, TestContactPerson.cid);
            map.Add(def.Forename, TestContactPerson.forename);
            map.Add(def.Surname, TestContactPerson.surname);
            map.Add(def.Gender, TestContactPerson.gender);
            map.Add(def.Email, TestContactPerson.email);
            map.Add(def.Phone, TestContactPerson.phone);
            map.Add(def.MainContact, TestContactPerson.mainContact);
            String sql = SqlStatements.BuildInsert(def.TblName, map);
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery(sql);
        }

        public static void InsertTestNote()
        {
            DefTblNotes def = new DefTblNotes();
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();
            map.Add(def.Id, TestNote.id);
            map.Add(def.Cid, TestNote.cid);
            map.Add(def.CreatedBy, TestNote.createdBy);
            map.Add(def.EntryDate, TestNote.entryDate);
            map.Add(def.Memo, TestNote.memo);
            map.Add(def.Category, TestNote.category);
            map.Add(def.Attachment, TestNote.attachment);
            String sql = SqlStatements.BuildInsert(def.TblName, map);
            CRM_web.Models.Model.Model.GetModel().ExecuteActionQuery(sql);
        }
    }

    public static class TestCustomer
    {
        public static readonly int cid = 9999;
        public static readonly String company = "testCompany";
        public static readonly String address = "testAddress";
        public static readonly String zip = "testZip";
        public static readonly String city = "testCity";
        public static readonly String country = "testCountry";
        public static readonly String contractId = "testContractId";
        public static readonly DateTime? contractDate = new DateTime(2017, 1, 1);
    }

    public static class TestContactPerson
    {
        public static readonly int id = 9999;
        public static readonly int cid = 9999;
        public static readonly String forename = "testForename";
        public static readonly String surname = "testSurname";
        public static readonly String gender = "m";
        public static readonly String email = "test@test.com";
        public static readonly String phone = "123";
        public static readonly bool mainContact = true;
    }

    public static class TestNote
    {
        public static readonly int id = 9999;
        public static readonly int cid = 9999;
        public static readonly String createdBy = "testUser";
        public static readonly DateTime? entryDate = new DateTime(2017, 1, 1);
        public static readonly String memo = "testMemo";
        public static readonly String category = "testCategory";
        public static readonly String attachment = "test.txt";
    }
}
