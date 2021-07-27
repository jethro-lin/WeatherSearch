using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Data;

namespace WeatherSearchDLL
{
    public class WeatherSearchDLL
    {
        public class Info
        {
            public string Time;
            public string C_T;
            public string C_AT;
            public string F_T;
            public string F_AT;
            public string RH;
            public string Rain;
            public string Sunrise;
            public string Sunset;
        }

        private List<Country> countries = new List<Country>();
        private List<NatScenic> nation_scenic = new List<NatScenic>();
        private List<Town> towns = new List<Town>();

        public enum eInfoType
        {
            Town
            , Beach
            , Port
            , Harbors
            , Fishing
            , Biking
            , Mountain
            , StarView
            , Ballpark
            , NatPark
            , NatScenic
            , NatForest
            , Farm
            , Reservoir
            , Snorkeling
            , Surfing
        }

        public string[] GetTypeList()
        {
            return InfoType;
        }

        private string[] InfoType = { "鄉鎮市區"
                    , "海水浴場"
                    , "主要港口"
                    , "休閒漁港"
                    , "海釣"
                    , "單車"
                    , "登山"
                    , "觀星"
                    , "棒球場"
                    , "國家公園"
                    , "國家風景區"
                    , "國家森林遊樂區"
                    , "農場旅遊"
                    , "主要水庫"
                    , "浮潛"
                    , "衝浪"};

        private string[] InfoTypeLink = { "Town"
                    , "Beach"
                    , "Port"
                    , "Harbors"
                    , "Fishing"
                    , "Biking"
                    , "Mountain"
                    , "StarView"
                    , "Ballpark"
                    , "NatPark"
                    , "NatScenic"
                    , "NatForest"
                    , "Farm"
                    , "Reservoir"
                    , "Snorkeling"
                    , "Surfing"};

        private static string GetJsonString(string link)
        {
            try
            {
                WebClient client = new WebClient();
                MemoryStream ms = new MemoryStream(client.DownloadData(link));
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
                doc.Load(ms, Encoding.UTF8);
                int a = doc.Text.IndexOf('=');
                string j = doc.Text.Substring(a + 2, doc.Text.Length - a - 3);
                return j;
            }
            catch
            {
                return "";
            }
        }

        public WeatherSearchDLL()
        {
            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_County.js");
            countries = ParseJsonTo<Country>(j);
        }

        private List<T> ParseJsonTo<T>(string j)
        {
            List<T> res = JsonConvert.DeserializeObject<List<T>>(j);
            if (res == null)
                res = new List<T>();
            return res;
        }

        private object ParseJsonTo(string j)
        {
            object res = JsonConvert.DeserializeObject(j);
            if (res == null)
                res = JsonConvert.DeserializeObject("{}");
            return res;
        }

        public string GetCountryByEnglish(string english_name)
        {
            foreach (Country c in countries)
            {
                if (c.Name.E.Contains(english_name))
                    return c.Name.C;
            }
            return "";
        }

        public string[] GetCountry(eInfoType type)
        {
            List<string> res = new List<string>();

            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_" + InfoTypeLink[(int)type] + ".js");

            if (type == eInfoType.Town)
            {
                dynamic stuff = ParseJsonTo(j);
                foreach (Country c in countries)
                {
                    foreach (dynamic item in stuff)
                    {
                        if (c.ID == item.Path.ToString())
                        {
                            res.Add(c.Name.C);
                            break;
                        }
                    }
                }
            }
            else
            {
                nation_scenic = ParseJsonTo<NatScenic>(j);
                foreach (Country c in countries)
                {
                    foreach (NatScenic n in nation_scenic)
                    {
                        if (n.CID == c.ID)
                        {
                            res.Add(c.Name.C);
                            break;
                        }
                    }
                }
            }
            return res.ToArray();
        }

        public string[] GetLocation(eInfoType type, string Country)
        {
            List<string> res = new List<string>();
            string id = GetCountryID(Country);
            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_" + InfoTypeLink[(int)type] + ".js");
            if (type == eInfoType.Town)
            {
                dynamic stuff = JsonConvert.DeserializeObject(j);
                foreach (dynamic item in stuff)
                {
                    if (id == item.Path.ToString())
                    {
                        towns = JsonConvert.DeserializeObject<List<Town>>(item.First.ToString());
                        foreach (Town c in towns)
                        {
                            res.Add(c.Name.C);
                        }
                        break;
                    }
                }
            }
            else
            {
                nation_scenic = JsonConvert.DeserializeObject<List<NatScenic>>(j);
                foreach (NatScenic n in nation_scenic)
                {
                    if (n.CID == id || id == "")
                    {
                        res.Add(n.Name.C);
                    }
                }
            }
            return res.ToArray();
        }

        public Info GetWeatherInfo(eInfoType type, string Country, string Location)
        {
            string link = "https://www.cwb.gov.tw/Data/js/GT/TableData_GT_";
            string tid = "";

            if (type == eInfoType.Town)
            {
                string id = "";
                foreach (Country c in countries)
                {
                    if (c.Name.C == Country)
                    {
                        id = c.ID;
                        break;
                    }
                }

                foreach (Town c in towns)
                {
                    if (c.Name.C == Location)
                    {
                        tid = c.ID;
                        break;
                    }
                }
                link = link + "T_" + id + ".js";
            }
            else
            {
                foreach (NatScenic c in nation_scenic)
                {
                    if (c.Name.C == Location)
                    {
                        tid = c.PID;
                        break;
                    }
                }
                link = link + "R_" + InfoTypeLink[(int)type] + ".js";
            }
            string header1 = "var GT_Time =";
            string header2 = "var GT =";
            string response = GetResponseString(link);
            Info infos = new Info();
            {
                int index1 = response.IndexOf(header2) + header2.Length;
                string j = response.Substring(index1, response.Length - index1 - 1);
                dynamic stuff = JsonConvert.DeserializeObject(j);

                foreach (dynamic item in stuff)
                {
                    string a = item.Path.ToString();
                    if (a == tid)
                    {
                        string temp = item.First.ToString();
                        infos = JsonConvert.DeserializeObject<Info>(temp);
                        break;
                    }
                }
            }
            {
                int index1 = response.IndexOf(header1) + header1.Length;
                int index2 = response.IndexOf(header2);

                string j = response.Substring(index1, index2 - index1 - 2);
                Time stuff = JsonConvert.DeserializeObject<Time>(j);
                infos.Time = stuff.C.Replace("<br>", "\n");
            }
            return infos;
        }

        private static string GetResponseString(string link)
        {
            try
            {
                WebClient client = new WebClient();
                MemoryStream ms = new MemoryStream(client.DownloadData(link));
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
                doc.Load(ms, Encoding.UTF8);
                return doc.Text;
            }
            catch
            {
                return "";
            }
        }

        private string GetCountryID(string country)
        {
            string id = "";
            foreach (Country c in countries)
            {
                if (c.Name.C == country)
                {
                    id = c.ID;
                    break;
                }
            }
            return id;
        }
    }
}
