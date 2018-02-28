using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;


namespace ProjectAI
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Paste Link and press enter");
                string url = "http://96.74.130.169:19486/" + Console.ReadLine();
                List<RootObject> rootObject = _download_serialized_json_data_list<RootObject>(url);
                Console.WriteLine(rootObject.Count);
            }
        }
        //
        // ...

        static List<T> _download_serialized_json_data_list<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<List<T>>(json_data) : new List<T>();
            }
        }
    }

    public class RootObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Dp { get; set; }
    }
}
