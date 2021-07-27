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
using WeatherSearchDLL;

namespace WindowsFormsApp1
{
    public partial class WeatherSearch : Form
    {
        private WeatherSearchDLL.WeatherSearchDLL weather_search;

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

        public WeatherSearch()
        {
            InitializeComponent();
            weather_search = new WeatherSearchDLL.WeatherSearchDLL();

            string[] types = weather_search.GetTypeList();
            foreach (string type in types)
            {
                this.comboBox_type.Items.Add(type);
            }

            string[] countries = weather_search.GetCountry(WeatherSearchDLL.WeatherSearchDLL.eInfoType.Town);
            foreach (string c in countries)
            {
                this.comboBox_country.Items.Add(c);
            }

            this.comboBox_type.SelectedIndex = 0;

            /// *** 透過當前IP取得當前位置
            string ip = GetIPAddress();
            string country = GetUserCountryByIp(ip);
            this.comboBox_country.SelectedItem = weather_search.GetCountryByEnglish(country);
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherSearchDLL.WeatherSearchDLL.eInfoType type = (WeatherSearchDLL.WeatherSearchDLL.eInfoType)this.comboBox_type.SelectedIndex;
            string country_name = (this.comboBox_country.SelectedItem == null) ? "" : this.comboBox_country.SelectedItem.ToString();

            string[] countries = weather_search.GetCountry(type);
            this.comboBox_country.Items.Clear();
            foreach (string c in countries)
            {
                this.comboBox_country.Items.Add(c);
            }

            this.comboBox_country.SelectedItem = country_name;
            if (this.comboBox_country.SelectedIndex == -1)
                this.comboBox_country.SelectedIndex = 0;
        }

        private void comboBox_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherSearchDLL.WeatherSearchDLL.eInfoType type = (WeatherSearchDLL.WeatherSearchDLL.eInfoType)this.comboBox_type.SelectedIndex;
            string country_name = this.comboBox_country.SelectedItem.ToString();

            string[] locations = weather_search.GetLocation(type, country_name);
            this.comboBox_town.Items.Clear();
            foreach (string l in locations)
            {
                this.comboBox_town.Items.Add(l);
            }

            this.comboBox_town.SelectedIndex = 0;
        }

        private void comboBox_town_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherSearchDLL.WeatherSearchDLL.eInfoType type = (WeatherSearchDLL.WeatherSearchDLL.eInfoType)this.comboBox_type.SelectedIndex;
            string country_name = this.comboBox_country.SelectedItem.ToString();
            string location_name = this.comboBox_town.SelectedItem.ToString();

            WeatherSearchDLL.WeatherSearchDLL.Info info = weather_search.GetWeatherInfo(type, country_name, location_name);
            this.label_time.Text = info.Time;

            this.label_temperature.Text = info.C_T;
            this.label_body_temperature.Text = info.C_AT;
            this.label_humidity.Text = info.RH;
            this.label_precipitation.Text = info.Rain;
            this.label_sunrise.Text = info.Sunrise;
            this.label_sunset.Text = info.Sunset;
        }
    }
}
