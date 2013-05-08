using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DealerPortal
{

    public static class Config
    {
     
        public static string startURL;
        public static string testUserName;           // Username of selenium test user. 
        public static string testPassWord;           // Password for selenium test user. Must have admin role. 
        public static string auxUserName;            // An account for tinkering with
        public static string auxEmailAddr;           // Email address associated with auxUserName

        public static void ReadConfig(string fileName)
        {
            try
            {
                XDocument doc = XDocument.Load(fileName);

                foreach (XElement el in doc.Root.Elements())
                {
                    foreach (XElement element in el.Elements())
                    {
                        string eledata = element.Value;
                        string elename = element.Name.ToString();

                        if (elename == "baseURL")
                        {
                            startURL = eledata;
                        }
                        if (elename == "testUserName")
                        {
                            testUserName = eledata;
                        }
                        if (elename == "testPassWord")
                        {
                            testPassWord = eledata;
                        }
                        if (elename == "auxUserName")
                        {
                            auxUserName = eledata;
                        }
                        if (elename == "auxEmailAddr")
                        {
                            auxEmailAddr = eledata;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

