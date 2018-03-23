using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Threading;
using System.ComponentModel;

namespace ProjectAI
{
    class Program
    {
        private const string url = "http://96.74.130.169:19486/api/";
        private static List<RootObject> rootObject;
        //
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.CurrentThread.IsBackground = true;
                Console.WriteLine("Paste onlye model");
                string s = Console.ReadLine();
                List<RootObject> rootObject = null;
                int c = 0;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    rootObject = _download_serialized_json_data_list<RootObject>(url + s);
                    c = rootObject.Count;
                }).Start();
                Console.Write("Downloading");
                while (c==0)
                {
                    Console.Write(".");
                    Thread.Sleep(10);
                }
                Console.WriteLine();
                Console.WriteLine(rootObject.Count);
            }
        }
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
        public int MyProperty { get; set; }
        public int ka { get; set; }
    }
}
