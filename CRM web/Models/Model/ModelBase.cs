using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRM_web.Models.Model
{
    public class ModelBase
    {
        private Config config;
        public Config Config
        {
            get
            {
                if (config == null)
                {
                    config = new Config();
                }
                return config;
            }
            set
            {
                config = value;
            }
        }

        public void LoadTable(out DataView field, String sql)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            con.ConnectionString = Config.ConnectionString;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            DataTable dt = new DataTable();
            con.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            try
            {
                dt.Load(dr);
            }
            catch (ConstraintException)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    c.AllowDBNull = true;
                }
                dt.Load(dr);
            }


            con.Close();
            field = dt.AsDataView();
        }



        public object ExecuteScalar(String sql)
        {
            NpgsqlConnection con = new NpgsqlConnection(Config.ConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            object result = null;
            try
            {
                con.Open();
                result = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                PrintError(e.Message + "\n\n" + e.StackTrace, e);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int ExecuteActionQuery(String sql)
        {
            int result = 0;
            NpgsqlConnection con = new NpgsqlConnection(Config.ConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                PrintError("Abfrage konnte nicht ausgeführt werden.", e);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public void PrintError(String additionalText, Exception e)
        {
            String msg = "";
            if (additionalText != null)
            {
                msg = additionalText + "\n\n";
            }
            msg += e.Message + "\n\nQuelle: " + e.Source + "\n\nStackTrace: " + e.StackTrace;

            System.Diagnostics.Trace.Write(msg);            
        }
        public void PrintError(Exception e)
        {
            PrintError(null, e);
        }
    }

}
