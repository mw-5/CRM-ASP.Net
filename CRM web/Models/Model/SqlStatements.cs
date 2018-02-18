using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_web.Models.Model
{
    public class SqlStatements
    {
        private String customers = "SELECT * FROM customers";
        /// <summary>
        /// Retrieves all customers
        /// </summary>
        public String Customers
        {
            get
            {
                return customers;
            }
        }

        private String contactPersonsById = "SELECT * FROM contact_persons WHERE cid = XXX";
        /// <summary>
        /// Retrieves all contact persons for a given cid. Replace XXX with cid.
        /// </summary>
        public String ContactPersonsById
        {
            get
            {
                return contactPersonsById;
            }
        }

        private String contactPersonById = "SELECT * FROM contact_persons WHERE id = XXX";
        /// <summary>
        /// Retrieves contact persons for a given id. Replace XXX with id.
        /// </summary>
        public String ContactPersonById
        {
            get
            {
                return contactPersonById;
            }
        }

        private String customerByCid = "SELECT * FROM customers WHERE cid = XXX";
        /// <summary>
        /// Retrieves customer for a given cid. Replace XXX with id.
        /// </summary>
        public String CustomerByCid
        {
            get
            {
                return customerByCid;
            }
        }


        private String notesById = "SELECT * FROM notes WHERE cid = XXX";
        /// <summary>
        /// Retrieves all notes for a given cid. Replace XXX with cid.
        /// </summary>
        public String NotesById
        {
            get
            {
                return notesById;
            }
        }

        private String noteById = "SELECT * FROM notes WHERE id = XXX";
        /// <summary>
        /// Retrieves note for a given id. Replace XXX with id.
        /// </summary>
        public String NoteById
        {
            get
            {
                return noteById;
            }
        }



        public static String DateToSql(DateTime date)
        {
            return @"timestamp '" + date.Year.ToString("0000") + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00") + " " + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00") + @"'";
        }

        public static String ConvertToSql(ColTypes type, object value)
        {
            String sql = "";

            if (value == null || value.ToString().Equals(""))
            {
                sql = "NULL";
            }
            else
            {
                switch (type)
                {
                    case ColTypes.Text:
                        sql = "'" + value.ToString().Replace("'", "") + "'";
                        break;
                    case ColTypes.Numeric:
                        sql = value.ToString();
                        break;
                    case ColTypes.Date:
                        sql = DateToSql((DateTime)value);
                        break;
                    case ColTypes.Boolean:
                        sql = value.ToString();
                        break;
                }
            }

            return sql;
        }

        public static String BuildInsert(String tblName, Dictionary<ColDef, object> map)
        {
            String columns = "", values = "";

            foreach (var kv in map)
            {
                columns += kv.Key.Name + ",";
                values += ConvertToSql(kv.Key.Type, kv.Value) + ",";
            }

            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);

            return "INSERT INTO " + tblName + "(" + columns + ") VALUES(" + values + ");";
        }

        public static String BuildUpdate(String tblName, Dictionary<ColDef, object> map, Tuple<ColDef, object> id)
        {
            String sql = "UPDATE " + tblName + " SET ";

            foreach (var kv in map)
            {
                if (!kv.Key.Name.Equals(id.Item1.Name))
                {
                    sql += kv.Key.Name + " = " + ConvertToSql(kv.Key.Type, kv.Value) + ",";
                }
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE " + id.Item1.Name + " = " + ConvertToSql(id.Item1.Type, id.Item2) + ";";

            return sql;
        }
    }




}