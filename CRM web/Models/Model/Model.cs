using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


namespace CRM_web.Models.Model
{
    public class Model : ModelBase
    {
        public Model()
        {
            TblCustomers = new DefTblCustomers();
            TblNotes = new DefTblNotes();
            TblContactPersons = new DefTblContactPersons();
            Sql = new SqlStatements();
            Tasks = new List<IAsyncResult>();
        }

        private static Model model;
        public static Model GetModel()
        {
            if (model == null)
            {
                model = new Model();
            }
            return model;
        }

        public DefTblCustomers TblCustomers { get; private set; }
        public DefTblNotes TblNotes { get; private set; }
        public DefTblContactPersons TblContactPersons { get; private set; }

        public SqlStatements Sql { get; private set; }

        private DataView customers;
        public DataView Customers
        {
            get
            {
                if (customers == null)
                {
                    LoadCustomers();
                }
                return customers;
            }
            private set
            {
                customers = value;
            }
        }
        public void LoadCustomers()
        {
            LoadTable(out customers, Sql.Customers);
        }

        public DataView GetCustomer(int cid)
        {
            DataView customer;
            LoadTable(out customer, Sql.CustomerByCid.Replace("XXX", cid.ToString()));
            return customer;
        }

        private DataView contactPersons;
        public DataView ContactPersons
        {
            get
            {
                if (contactPersons == null)
                {
                    LoadContactPersons(0);
                }
                return contactPersons;
            }
            set
            {
                contactPersons = value;
            }
        }
        public void LoadContactPersons(int cid)
        {
            LoadTable(out contactPersons, Sql.ContactPersonsById.Replace("XXX", cid.ToString()));
        }

        public DataView GetContactPerson(int id)
        {
            DataView contactPerson;
            LoadTable(out contactPerson, Sql.ContactPersonById.Replace("XXX", id.ToString()));
            return contactPerson;
        }

        private DataView notes;
        public DataView Notes
        {
            get
            {
                if (notes == null)
                {
                    LoadNotes(0);
                }
                return notes;
            }
            private set
            {
                notes = value;
            }
        }
        public void LoadNotes(int cid)
        {
            LoadTable(out notes, Sql.NotesById.Replace("XXX", cid.ToString()));
        }

        public DataView GetNote(int id)
        {
            DataView note;
            LoadTable(out note, Sql.NoteById.Replace("XXX", id.ToString()));
            return note;
        }


        public void LoadAllTablesAsync()
        {
            new Task(LoadCustomers).Start();
        }

        private int cid;
        public int Cid
        {
            get
            {
                return cid;
            }
            set
            {
                cid = value;
            }
        }

        /// <summary>
        /// Used to pass id of record to be edited to a form.
        /// If new entry is desired pass null.
        /// </summary>
        public String Id4Edit { get; set; }

        public List<IAsyncResult> Tasks { get; set; }


        public void Submit(Dictionary<ColDef, object> Map, String tblName, Tuple<ColDef, Object> id, EntryMode mode)
        {
            String sql = "";

            if (mode == EntryMode.New)
            {
                foreach (var kv in Map) // remove id if exists
                {
                    if (kv.Key.Name.Equals(id.Item1.Name))
                    {
                        Map.Remove(kv.Key);
                        break;
                    }
                }
                object _id = model.ExecuteScalar("SELECT (Max(" + id.Item1.Name + ") + 1) FROM " + tblName); // generate new id
                if (_id.ToString().Equals(""))
                {
                    _id = 1;
                }
                Map.Add(id.Item1, _id);
                sql = SqlStatements.BuildInsert(tblName, Map);
            }
            else if (mode == EntryMode.Edit)
            {
                sql = SqlStatements.BuildUpdate(tblName, Map, id);
            }
      
            model.Tasks.Add(new Action<String>(s => model.ExecuteActionQuery(s)).BeginInvoke(sql, null/*p => { CRM.Pages.Navigator.UpdateTablesAsync(); }*/, null));           
        }        


    }

    public enum EntryMode
    {
        New,
        Edit
    }
}