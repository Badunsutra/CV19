using System;
using System.Net;
using System.Net.Http;

namespace CV19Console
{
    internal class Program
    {
        private const string data_uri = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            //WebClient web_client = new WebClient();

            HttpClient client = new HttpClient();

            var response = client.GetAsync(data_uri).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }
    }
}
