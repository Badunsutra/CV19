using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        private const string data_uri = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_uri, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_strream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_strream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                yield return line.Replace("Korea,", "Korea -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(l => l.Split(','));

            int GetParse(string s)
            {
                int.TryParse(s, out int result);
                return result;
            }

            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(GetParse).ToArray();

                yield return (country_name, province, counts);
            }

        }

        static void Main(string[] args)
        {
            //WebClient web_client = new WebClient();

            HttpClient client = new HttpClient();

            //var response = client.GetAsync(data_uri).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            //var dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var russia = GetCountriesData()
                .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia.Counts, (date, count) => $"{date:dd.MM.yyyy} - {count}")));

            Console.ReadLine();
        }
    }
}
