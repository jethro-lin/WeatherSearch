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
        public List<Town> towns;

        private string GetIPAddress()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);

            Console.WriteLine(externalIp.ToString());
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

        public Form1()
        {
            InitializeComponent();

            string ip = GetIPAddress();
            string country = GetUserCountryByIp(ip);
            Console.WriteLine();

            string header = "var Info_County = ";

            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/info/Info_County.js"));// 取得城市列表
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
            doc.Load(ms, Encoding.UTF8);
            Console.WriteLine(doc.Text);
            string j = doc.Text.Substring(header.Length, doc.Text.Length - header.Length - 1);
            countries = JsonConvert.DeserializeObject<List<Country>>(j);
            foreach(Country c in countries)
            {
                this.comboBox_country.Items.Add(c.Name.C);
                if (c.Name.E.Contains(country))
                {
                    this.comboBox_country.SelectedItem = c.Name.C;
                }
            }
        }

        private void comboBox_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            string header = "var Info_Town = ";
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/info/Info_Town.js"));// 取得鄉村列表
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
            doc.Load(ms, Encoding.UTF8);
            Console.WriteLine(doc.Text);
            string j = doc.Text.Substring(header.Length, doc.Text.Length - header.Length - 1);
            dynamic stuff = JsonConvert.DeserializeObject(j);

            string id = "";
            foreach (Country c in countries)
            {
                if (c.Name.C == ((System.Windows.Forms.ComboBox)sender).SelectedItem.ToString())
                {
                    id = c.ID;
                    break;
                }
            }

            this.comboBox_town.Items.Clear();
            foreach (dynamic item in stuff)
            {
                if(id == item.Path.ToString())
                {
                    towns = JsonConvert.DeserializeObject<List<Town>>(item.First.ToString());
                    foreach (Town c in towns)
                    {
                        this.comboBox_town.Items.Add(c.Name.C);
                    }
                    break;
                }
            }
            
            if (this.comboBox_town.Items.Count != 0)
                this.comboBox_town.SelectedIndex = 0;
        }

        private void comboBox_town_SelectedIndexChanged(object sender, EventArgs e)
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

            string tid = "";
            foreach (Town c in towns)
            {
                if (c.Name.C == ((System.Windows.Forms.ComboBox)sender).SelectedItem.ToString())
                {
                    tid = c.ID;
                    break;
                }
            }

            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/GT/TableData_GT_T_"+ id + ".js"));//取得氣象資訊
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };
            doc.Load(ms, Encoding.UTF8);
            Console.WriteLine(doc.Text);
            {
                string header1 = "var GT_Time =";
                int index1 = doc.Text.IndexOf(header1) + header1.Length;
                string header2 = "var GT =";
                int index2 = doc.Text.IndexOf(header2);
            
                string j = doc.Text.Substring(index1, index2 - index1 - 2);
                Time stuff = JsonConvert.DeserializeObject<Time>(j);
                this.label_time.Text = stuff.C.Replace("<br>", "\n");
            }
            {
                string header2 = "var GT =";
                int index2 = doc.Text.IndexOf(header2) + header2.Length;
                string j = doc.Text.Substring(index2, doc.Text.Length - index2 - 1);
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
        public CName Name;
        public string RID;
        public string Tide;
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

    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
}
