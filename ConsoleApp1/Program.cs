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
using WeatherSearchDLL;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                WeatherSearchDLL.WeatherSearchDLL w = new WeatherSearchDLL.WeatherSearchDLL();
                var l = w.GetTypeList();
                int type_index = 0;
                int country_index = 0;
                int location_index = 0;
                foreach (string s in l)
                {
                    Console.Write(type_index++);
                    Console.Write(". ");
                    Console.WriteLine(s);
                }
                int key_index = 0;

                WeatherSearchDLL.WeatherSearchDLL.eInfoType type = WeatherSearchDLL.WeatherSearchDLL.eInfoType.Town;
                string country = "";
                string location = "";
                key_index = Convert.ToInt32(Console.ReadLine());
                type = (WeatherSearchDLL.WeatherSearchDLL.eInfoType)key_index;

                l = w.GetCountry(type);
                foreach (string s in l)
                {
                    Console.Write(country_index++);
                    Console.Write(". ");
                    Console.WriteLine(s);
                }

                key_index = Convert.ToInt32(Console.ReadLine());
                country = l[key_index];
                l = w.GetLocation(type, country);
                foreach (string s in l)
                {
                    Console.Write(location_index++);
                    Console.Write(". ");
                    Console.WriteLine(s);
                }

                key_index = Convert.ToInt32(Console.ReadLine());
                location = l[key_index];
                var i = w.GetWeatherInfo(type, country, location);
                Console.Write("時間:");
                Console.WriteLine(i.Time);

                Console.Write("溫度:");
                Console.WriteLine(i.C_T);

                Console.Write("體感溫度:");
                Console.WriteLine(i.C_AT);

                Console.Write("降雨機率:");
                Console.WriteLine(i.RH);

                Console.Write("時雨量:");
                Console.WriteLine(i.Rain);

                Console.Write("日出:");
                Console.WriteLine(i.Sunrise);

                Console.Write("日落:");
                Console.WriteLine(i.Sunset);
                Console.WriteLine("按任意鍵繼續，按Esc跳離");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
