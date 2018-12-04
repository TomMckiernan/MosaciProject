using ImageMosaicService;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFileIndexService
{
    public class ImageFileIndex
    {
        MongoClient client;
        IMongoDatabase database;

        // Default database name can be changed during testing
        public ImageFileIndex(string dbName = "MosaicDatabase")
        {
            client = new MongoClient();
            database = client.GetDatabase(dbName);
        }

        public ImageFileIndexResponse ReadAllImageFileIndex(ImageFileIndexRequest request)
        {
            return new MongoImageFileIndex().ReadAll(database, request.IndexedLocation);
        }

        public ImageFileResponse ReadImageFile(ImageFileRequest request)
        {
            return new MongoImageFileIndex().ReadImageFile(database, request.Id);
        }

        public ImageFileIndexResponse ReadAllImageFiles(ImageFilesAllRequest request)
        {
            return new MongoImageFileIndex().ReadAllImageFiles(database, request.Ids.ToList());
        }

        // Analyse all files in current directory and update collection to reflect directory
        public async Task<ImageFileIndexUpdateResponse> AnalyseImageFileIndex(ImageFileIndexRequest request)
        {
            try
            {
                await Task.Run(() => {

                    // Store all files in indexed location
                    var indexedDirectory = new DirectoryInfo(request.IndexedLocation);
                    // Returns filenames in all sub directories as well
                    var directoryFiles = indexedDirectory.GetFiles("*.png", SearchOption.AllDirectories).ToList();

                    // Get exisiting files in location
                    var existingFiles = ReadAllImageFileIndex(request).Files.ToList();

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

                return new ImageFileIndexUpdateResponse() { FilePath = request.IndexedLocation };
            }
            catch (Exception ex)
            {
                return new ImageFileIndexUpdateResponse() { FilePath = request.IndexedLocation, Error = ex.Message };
            }
            
       }

        public async Task AnalyseNewFiles(FileInfo x)
        {
            var data = GenerateMetaData(x);
            var request = new ImageFileIndexStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = x.Name,
                FilePath = x.FullName,
                LastWriteTime = x.LastWriteTime.ToString(),
                Data = new Metadata()
                {
                    AverageBL = data.AverageBL.ToArgb(),
                    AverageBR = data.AverageBR.ToArgb(),
                    AverageTL = data.AverageTL.ToArgb(),
                    AverageTR = data.AverageTR.ToArgb()
                }
            };
            var response = new MongoImageFileIndex().Insert(database, request);
     
        }

        private ImageInfo GenerateMetaData(FileInfo file)
        {
            var imageProcessing = new ImageProcessing();
            var imageInfos = new List<ImageInfo>();
            ImageInfo info;

            using (var inputBmp = imageProcessing.Resize(file.FullName))
            {
                info = imageProcessing.GetAverageColor(inputBmp, file.FullName);
            }
            return info;
        }

        public async Task AnalyseUpdatedFiles(FileInfo x)
        {
            // At time of impl can think of linq to pass FileInfo and proto to this method
            // May have to refetch element in collection and then perform a insert will newly created structure
        }

        public async Task AnalyseDeletedFiles(string id)
        {
            new MongoImageFileIndex().Delete(database, id);
        }
    }
}
