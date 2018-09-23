using MongoDB.Driver;
using System;

namespace ProjectService
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");

            //Get Database and Collection  
            IMongoDatabase db = dbClient.GetDatabase("test");
            var collList = db.ListCollections().ToList();
            Console.WriteLine("The list of collections are :");
            foreach (var item in collList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
