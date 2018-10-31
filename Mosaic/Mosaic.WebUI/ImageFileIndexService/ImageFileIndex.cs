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
            var directoryFiles = indexedDirectory.GetFiles("*.jpg", SearchOption.AllDirectories).ToList();

            // Get exisiting files in location
            var existingFiles = ReadImageFileIndex(indexedLocation).Files.ToList();

            // Get new files
            var newFiles = directoryFiles.Where(f => !existingFiles.Any(f2 => f2.FilePath == f.FullName));

            // Get updated files
            var updatedFiles = directoryFiles.Where(f => existingFiles.Any(f2 => f2.FilePath == f.FullName && f.LastWriteTime.ToString() != f2.LastWriteTime));

            // Will need to get more back from initial file search

            // Get deleted files
            var deletedFiles = existingFiles.Where(f => !directoryFiles.Any(f2 => f2.FullName == f.FilePath));

            // Methods call to perform appropriate operations with these calculated lists
        }
    }
}
