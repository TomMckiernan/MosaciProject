using MongoDB.Driver;
using System.IO;
using System.Linq;

namespace ImageFileIndexService
{
    public class ImageFileIndex
    {
        MongoClient client;
        IMongoDatabase database;

        public ImageFileIndex()
        {
            client = new MongoClient();
            database = client.GetDatabase("MosaicDatabase");
        }

        public ImageFileIndexResponse ReadImageFileIndex(string indexedLocation)
        {
            return new MongoImageFileIndex().Read(database, indexedLocation);
        }

        public void AnalyseImageFileIndex(string indexedLocation)
        {

            // Store all files in indexed location
            var indexedDirectory = new DirectoryInfo(indexedLocation);
            // Returns filenames in all sub directories as well
            var files = indexedDirectory.GetFiles("*.jpg", SearchOption.AllDirectories).ToList();

            // Get exisiting files in location
            var existingFiles = ReadImageFileIndex(indexedLocation).Files.ToList();

           // Get new files


           // Get updated files
           // Will need to get more back from initial file search

           // Get deleted files

        }
    }
}
