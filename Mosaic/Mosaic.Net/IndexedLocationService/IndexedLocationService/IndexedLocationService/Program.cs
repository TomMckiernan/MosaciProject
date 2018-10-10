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
            
            //Need to deal with starting conditions for the index location collection
            //Need to check there is only one indexed location in the collection

            //If no index location in the collection need to create default value
        }
    }
}
