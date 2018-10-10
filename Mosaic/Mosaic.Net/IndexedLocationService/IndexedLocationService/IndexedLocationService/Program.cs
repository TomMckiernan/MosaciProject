using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace IndexedLocationService
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            var client = new MongoClient();
            var database = client.GetDatabase("MosaicDatabase");
            
            //var mongo = new MongoIndexedLocation();
            //var response = mongo.Read(true, database);
            //if (response == null)
            //{
            //    Console.WriteLine("no results found");
            //}
            //else
            //{
            //    Console.WriteLine(response.Location);
            //}

            //Console.ReadKey();
            //var item = new IndexedLocation() { Location = "somewhere"};
            //var response2 = mongo.Insert(item, database);
            //Console.Write(response2.IsAcknowledged);
        }
    }
}
