using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AnalyseImageFileIndex(string indexedLocation)
        {
            await Task.Run(() => {

                // Store all files in indexed location
                var indexedDirectory = new DirectoryInfo(indexedLocation);
                // Returns filenames in all sub directories as well
                var directoryFiles = indexedDirectory.GetFiles("*.jpg", SearchOption.AllDirectories).ToList();

                // Get exisiting files in location
                var existingFiles = ReadImageFileIndex(indexedLocation).Files.ToList();

                // Get new files
                var newFiles = directoryFiles.Where(f => !existingFiles.Any(f2 => f2.FilePath == f.FullName));
                var newTask = newFiles.Select(async x => await AnalyseNewFiles(x));

                // Get updated files
                var updatedFiles = directoryFiles.Where(f => existingFiles.Any(f2 => f2.FilePath == f.FullName && f.LastWriteTime.ToString() != f2.LastWriteTime));
                var updatedTask = updatedFiles.Select(async x => await AnalyseUpdatedFiles(x));

                // Get deleted files
                var deletedFiles = existingFiles.Where(f => !directoryFiles.Any(f2 => f2.FullName == f.FilePath));
                var deleteTask = deletedFiles.Select(async x => await AnalyseDeletedFiles(x.Id));

                Task.WhenAll(newTask.Concat(updatedTask.Concat(deleteTask)));
            });
       }

        public async Task AnalyseNewFiles(FileInfo x)
        {
            var request = new ImageFileIndexStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = x.Name,
                FilePath = x.FullName,
                LastWriteTime = x.LastWriteTime.ToString(),
                Metadata = GenerateMetaData()
            };
     
        }

        private string GenerateMetaData()
        {
            return String.Empty;
        }

        public async Task AnalyseUpdatedFiles(FileInfo x)
        {

        }

        public async Task AnalyseDeletedFiles(string id)
        {
            new MongoImageFileIndex().Delete(database, id);
        }
    }
}
