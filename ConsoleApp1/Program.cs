using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb webClient = new HtmlWeb(); //建立htmlweb
                                               //處理C# 連線 HTTPS 網站發生驗證失敗導致基礎連接已關閉
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
            SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            webClient.PreRequest += request =>
            {
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument { OptionUseIdAttribute = true };

            WebClient client = new WebClient();
            //https://www.cwb.gov.tw/Data/js/info/Info_County.js?v=20200415

            //MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/GT/TableData_GT_T_65.js"));//?T=2021071402-4&_=1626248396342
            //MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/fcst/W50_Data.js"));// 取得城市列表
            MemoryStream ms = new MemoryStream(client.DownloadData("https://www.cwb.gov.tw/Data/js/info/Info_Town.js"));// 取得鄉村列表
            doc.Load(ms, Encoding.UTF8);
            
            Console.WriteLine(doc.Text);

            /// WebBrowser w = new WebBrowser();
            /// w.DocumentText = doc.Text;
            /// w.Document.InvokeScript("ChangeTID");

            doc = webClient.Load("https://www.cwb.gov.tw/Data/js/GT/TableData_GT_T_65.js?T=2021071415-4&_=1626248396342"); //載入網址資料
            Console.WriteLine(doc.Text);
            HtmlNode.ElementsFlags.Remove("option");
            doc.LoadHtml(doc.Text);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes(@"/html/body/div[2]/main/div/div[2]/div[1]/div[1]/form/fieldset/div/div[1]/div[2]/div/div[1]/label/select/option"))
            {
                Console.WriteLine("Value=" + node.Attributes["value"].Value);
                Console.WriteLine("InnerText=" + node.InnerText);
                Console.WriteLine();
            }
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine(doc.Text);
            //HtmlNodeCollection mtchrslts = doc.DocumentNode.SelectNodes("/html/body/div[2]/main/div/div[1]/table/tbody/tr/td[1]/span[1]");
            HtmlNodeCollection mtchrslts = doc.DocumentNode.SelectNodes("/html/body/div[2]/main/div/div[2]/div[1]/div[1]/form/fieldset/div/div[1]/div[2]/div/div[1]/label/select/option[2]");
            mtchrslts = doc.DocumentNode.SelectNodes("/html/body/div[3]/main/div[1]/div[2]/div[2]/div/div/ol/li[1]/a/span[2]/span[1]/i[1]");
            var a = doc.ParseExecuting;


            /// HttpClient httpClient = new HttpClient();
            /// 
            /// string url = "https://www.cwb.gov.tw/V8/C/W/Town/Town.html?TID=6500200";
            /// HttpResponseMessage responseMessage = await httpClient.GetAsync(url); //發送請求
            /// 
            /// //檢查回應的伺服器狀態StatusCode是否是200 OK
            /// if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            /// {
            ///     string responseResult = responseMessage.Content.ReadAsStringAsync().Result;//取得內容
            /// 
            ///     Console.WriteLine(responseResult);
            /// }


            int i = 0;
        }
    }
}
