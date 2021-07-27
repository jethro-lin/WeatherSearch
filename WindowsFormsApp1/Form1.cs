using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public List<Country> countries;
        public List<NatScenic> nation_scenic;
        public List<Scenery> sceneries = new List<Scenery>();
        public List<Town> towns;

        private string GetIPAddress()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);
            return externalIp.ToString();
        }

        private static string GetUserCountryByIp(string ip)
        {
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData("https://www.ip-adress.com/ip-address/ipv4/" + ip));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
            doc.Load(ms, Encoding.UTF8);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html/body/main/table/tbody/tr[4]/td");
            if (nodes.Count > 0)
                return nodes[0].InnerText;
            else
                return "";
        }

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

        private string GetCountryID()
        {
            string id = "";
            if (this.comboBox_country.SelectedItem != null)
            {
                foreach (Country c in countries)
                {
                    if (c.Name.C == this.comboBox_country.SelectedItem.ToString())
                    {
                        id = c.ID;
                        break;
                    }
                }
            }
            return id;
        }

        public Form1()
        {
            InitializeComponent();

            this.comboBox_type.Items.Add(new Type("鄉鎮市區", "Town"));
            this.comboBox_type.Items.Add(new Type("海水浴場", "Beach"));
            this.comboBox_type.Items.Add(new Type("主要港口", "Port"));
            this.comboBox_type.Items.Add(new Type("休閒漁港", "Harbors"));
            this.comboBox_type.Items.Add(new Type("海釣", "Fishing"));
            this.comboBox_type.Items.Add(new Type("單車", "Biking"));
            this.comboBox_type.Items.Add(new Type("登山", "Mountain"));
            this.comboBox_type.Items.Add(new Type("觀星", "StarView"));
            this.comboBox_type.Items.Add(new Type("棒球場", "Ballpark"));
            this.comboBox_type.Items.Add(new Type("國家公園", "NatPark"));
            this.comboBox_type.Items.Add(new Type("國家風景區", "NatScenic"));
            this.comboBox_type.Items.Add(new Type("國家森林遊樂區", "NatForest"));
            this.comboBox_type.Items.Add(new Type("農場旅遊", "Farm"));
            this.comboBox_type.Items.Add(new Type("主要水庫", "Reservoir"));
            this.comboBox_type.Items.Add(new Type("浮潛", "Snorkeling"));
            this.comboBox_type.Items.Add(new Type("衝浪", "Surfing"));

            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_County.js");
            countries = JsonConvert.DeserializeObject<List<Country>>(j);
            foreach (Country c in countries)
            {
                this.comboBox_country.Items.Add(c.Name.C);
            }

            this.comboBox_type.SelectedIndex = 0;
            /*
            string ip = GetIPAddress();
            string country = GetUserCountryByIp(ip);
            foreach (Country c in countries)
            {
                if (country != "" && c.Name.E.Contains(country))
                {
                    this.comboBox_country.SelectedItem = c.Name.C;
                    break;
                }
            }
            if (this.comboBox_country.Items.Count > 0 && this.comboBox_country.SelectedIndex == -1)
                this.comboBox_country.SelectedIndex = 0;
            */
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GetCountryID();
            int country_id = 0;
            
            Type t = (Type)this.comboBox_type.SelectedItem;

            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_" + t.link + ".js");

            this.comboBox_country.Items.Clear();
            if (this.comboBox_type.SelectedIndex == 0)
            {
                dynamic stuff = JsonConvert.DeserializeObject(j);
                foreach (Country c in countries)
                {
                    foreach (dynamic item in stuff)
                    {
                        if (c.ID == item.Path.ToString())
                        {
                            this.comboBox_country.Items.Add(c.Name.C);
                            if (c.ID == id)
                                country_id = this.comboBox_country.Items.Count - 1;
                            break;
                        }
                    }
                }
            }
            else
            {
                nation_scenic = JsonConvert.DeserializeObject<List<NatScenic>>(j);
                foreach (Country c in countries)
                {
                    foreach (NatScenic n in nation_scenic)
                    {
                        if (n.CID == c.ID)
                        {
                            this.comboBox_country.Items.Add(c.Name.C);
                            if (c.ID == id)
                                country_id = this.comboBox_country.Items.Count - 1;
                            break;
                        }
                    }
                }
            }
            this.comboBox_country.SelectedIndex = country_id;
        }

        private void comboBox_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type t = (Type)this.comboBox_type.SelectedItem;
            string j = GetJsonString("https://www.cwb.gov.tw/Data/js/info/Info_" + t.link + ".js");
            string id = GetCountryID();

            this.comboBox_town.Items.Clear();

            if (this.comboBox_type.SelectedIndex == 0)
            {
                dynamic stuff = JsonConvert.DeserializeObject(j);
                foreach (dynamic item in stuff)
                {
                    if (id == item.Path.ToString())
                    {
                        towns = JsonConvert.DeserializeObject<List<Town>>(item.First.ToString());
                        foreach (Town c in towns)
                        {
                            this.comboBox_town.Items.Add(c.Name.C);
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
                    if (n.CID == id)
                    {
                        this.comboBox_town.Items.Add(n.Name.C);
                    }
                }
            }

            if (this.comboBox_town.Items.Count != 0)
                this.comboBox_town.SelectedIndex = 0;
        }

        private void comboBox_town_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type t = (Type)this.comboBox_type.SelectedItem;
            string link = "https://www.cwb.gov.tw/Data/js/GT/TableData_GT_";
            string tid = "";

            if (this.comboBox_type.SelectedIndex == 0)
            {
                string id = "";
                foreach (Country c in countries)
                {
                    if (c.Name.C == this.comboBox_country.SelectedItem.ToString())
                    {
                        id = c.ID;
                        break;
                    }
                }

                foreach (Town c in towns)
                {
                    if (c.Name.C == ((System.Windows.Forms.ComboBox)sender).SelectedItem.ToString())
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
                    if (c.Name.C == ((System.Windows.Forms.ComboBox)sender).SelectedItem.ToString())
                    {
                        tid = c.PID;
                        break;
                    }
                }
                link = link + "R_" + t.link + ".js";
            }
            string response = GetResponseString(link);
            {
                string header1 = "var GT_Time =";
                int index1 = response.IndexOf(header1) + header1.Length;
                string header2 = "var GT =";
                int index2 = response.IndexOf(header2);

                string j = response.Substring(index1, index2 - index1 - 2);
                Time stuff = JsonConvert.DeserializeObject<Time>(j);
                this.label_time.Text = stuff.C.Replace("<br>", "\n");
            }
            {
                string header2 = "var GT =";
                int index2 = response.IndexOf(header2) + header2.Length;
                string j = response.Substring(index2, response.Length - index2 - 1);
                dynamic stuff = JsonConvert.DeserializeObject(j);

                foreach (dynamic item in stuff)
                {
                    string a = item.Path.ToString();
                    if (a == tid)
                    {
                        string temp = item.First.ToString();
                        Info infos = JsonConvert.DeserializeObject<Info>(temp);
                        this.label_temperature.Text = infos.C_T;
                        this.label_body_temperature.Text = infos.C_AT;
                        this.label_humidity.Text = infos.RH;
                        //this.label_temperature.Text = infos.F_AT;
                        this.label_precipitation.Text = infos.Rain;
                        //this.label_temperature.Text = infos.Rain;
                        this.label_sunrise.Text = infos.Sunrise;
                        this.label_sunset.Text = infos.Sunset;
                        break;
                    }
                }
            }
        }
    }

    public class Time
    {
        public string C;
        public string E;
    }

    public class CName
    {
        public string C;
        public string E;
    }

    public class Country
    {
        public string ID;
        public string Area;
        public string TID;
        public CName Name;
    }

    public class Town
    {
        public string ID;
        public string RID;
        public string Tide;
        public CName Name;
    }

    public class NatScenic
    {
        public string PID;
        public string CID;
        public string TID;
        public CName Name;
    }

    public class Info
    {
        public string C_T;
        public string C_AT;
        public string F_T;
        public string F_AT;
        public string RH;
        public string Rain;
        public string Sunrise;
        public string Sunset;
    }

    public class Scenery
    {
        public string TypeID;
        public string TypeName;

        public Scenery(string path, string v)
        {
            this.TypeID = path;
            this.TypeName = v;
        }
    }

    public class Type
    {
        public string C;
        public string link;

        public Type(string _c, string _link)
        {
            this.C = _c;
            this.link = _link;
        }
        public override string ToString()
        {
            return C;
        }
    }
}
