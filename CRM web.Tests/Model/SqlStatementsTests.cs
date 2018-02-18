using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_web.Models.Model;
using System.Collections.Generic;
using CRM_web.Tests.TestCases;
using System.IO;
using System.Reflection;

namespace CRM_web.Tests.Model
{
    [TestClass]
    public class SqlStatementsTests
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod()]
        public void DateToSqlTest()
        {
            string s = SqlStatements.DateToSql(new DateTime(2017, 12, 1, 11, 10, 9));
            Assert.AreEqual(s, @"timestamp '2017-12-01 11:10:09'");
        }

        [TestMethod()]
        public void ConvertToSqlTest()
        {
            Assert.AreEqual("True", SqlStatements.ConvertToSql(ColTypes.Boolean, true));
            Assert.AreEqual(@"timestamp '2017-01-01 00:00:00'", SqlStatements.ConvertToSql(ColTypes.Date, new DateTime(2017, 1, 1)));
            Assert.AreEqual("9", SqlStatements.ConvertToSql(ColTypes.Numeric, 9));
            Assert.AreEqual(@"'ab'", SqlStatements.ConvertToSql(ColTypes.Text, "a'b"));
            Assert.AreEqual("NULL", SqlStatements.ConvertToSql(ColTypes.Text, null));
        }

        [TestMethod()]
        public void BuildInsertTest()
        {
            DefTblCustomers def = new DefTblCustomers();
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();
            map.Add(def.Cid, 1);
            map.Add(def.Company, "test");
            String sql = SqlStatements.BuildInsert(def.TblName, map);
            Assert.AreEqual(String.Format("INSERT INTO {0}({1},{2}) VALUES(1,'test');", def.TblName, def.Cid.Name, def.Company.Name), sql);
        }

        [TestMethod()]
        public void BuildUpdateTest()
        {
            DefTblCustomers def = new DefTblCustomers();
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();
            map.Add(def.Cid, TestCustomer.cid);
            map.Add(def.Company, TestCustomer.company);
            map.Add(def.Address, TestCustomer.address);
            String sql = SqlStatements.BuildUpdate(def.TblName, map, new Tuple<ColDef, object>(def.Cid, TestCustomer.cid));
            Assert.AreEqual(String.Format("UPDATE {0} SET {1} = '{2}',{3} = '{4}' WHERE {5} = {6};", def.TblName, def.Company.Name, TestCustomer.company, def.Address.Name, TestCustomer.address, def.Cid.Name, TestCustomer.cid), sql);
        }
    }
}
