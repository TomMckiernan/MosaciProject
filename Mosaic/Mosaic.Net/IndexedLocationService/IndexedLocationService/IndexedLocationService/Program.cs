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
            var mongo = new MongoIndexedLocation();
            var response = mongo.Read(true, client);
            if (response == null)
            {
                Console.WriteLine("no results found");
            }
            else
            {
                Console.WriteLine(response.Location);
            }

            Console.ReadKey();
            var item = new IndexedLocation() { Location = "nowhere"};
            var response2 = mongo.Insert(item, client);
        }
    }
}
