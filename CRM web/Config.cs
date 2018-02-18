using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Resources;
using System.Collections;

namespace CRM_web
{
    public class Config
    {
        public Config()
        {
            LoadConfig();
            config = this;
        }

        private static Config config;
        public static Config GetConfig()
        {
            if (config == null)
            {
                new Config();
            }
            return config;
        }

        public static String PathFileConfig { get; set; }

        private void LoadConfig()
        {
            XmlDocument xdoc = new XmlDocument();
            if (PathFileConfig == null || PathFileConfig.Equals(""))
            {
                PathFileConfig = HttpContext.Current.Server.MapPath("~/App_Start/Config.xml");
            }
            xdoc.Load(PathFileConfig);

            Language = xdoc.SelectSingleNode("//language").InnerText;

            ConnectionString = "Server=" + xdoc.SelectSingleNode("//connectionString/server").InnerText + ";"
                + "User Id=" + xdoc.SelectSingleNode("//connectionString/user").InnerText + ";"
                + "Password=" + xdoc.SelectSingleNode("//connectionString/password").InnerText + ";"
                + "Database=" + xdoc.SelectSingleNode("//connectionString/databaseName").InnerText + ";";

            // get path for file folders of customers and close application if path does not exist.
            PathCustomerFolders = xdoc.SelectSingleNode("//pathCustomerFolders").InnerText;
            if (!(PathCustomerFolders.EndsWith(@"\") || PathCustomerFolders.EndsWith(@"/")))
            {
                PathCustomerFolders += @"\";
            }
            if (!Directory.Exists(PathCustomerFolders))
            {                
                System.Diagnostics.Trace.Write(String.Format(Resources.Strings.MsgConfigPathDoesNotExist, PathCustomerFolders));
                Environment.Exit(0);
            }
        }

        public String ConnectionString { get; private set; }

        public String PathCustomerFolders { get; private set; }

        public String Language { get; private set; }
    }
}