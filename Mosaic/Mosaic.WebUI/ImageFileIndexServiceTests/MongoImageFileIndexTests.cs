using ImageFileIndexService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace ImageFileIndexServiceTests
{
    [TestClass]
    public class MongoImageFileIndexTests
    {
        MongoClient client;
        IMongoDatabase database;

        [TestInitialize]
        public void init()
        {
            client = new MongoClient();
            database = client.GetDatabase("TestMosaicDatabase");
        }

        private ImageFileIndexStructure CreateImageFileIndexStructure(string fileName = "Image.jpg", string filePath = "Pictures\\Image.jpg",
            string metaData = "metadata")
        {
            return new ImageFileIndexStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = fileName,
                FilePath = filePath,
                LastWriteTime = DateTime.Now.ToString(),
                Metadata = metaData
            };
        }

        [TestMethod]
        public void ImageFileIndexReadReturnNoFilesWithNoneInserted()
        {
            var collection = new MongoImageFileIndex();

            var indexedLocation = "Pictures";
            var responseRead = collection.Read(database, indexedLocation);
            Assert.AreEqual(0, responseRead.Files.Count);
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsOneInsertedFiles()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);

            var indexedLocation = "Pictures";
            var responseRead = collection.Read(database, indexedLocation);
            Assert.AreEqual(1, responseRead.Files.Count);
            Assert.IsTrue(responseRead.Files.Contains(request));
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsOnlyFilesInIndexedLocation()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);
            var request2 = CreateImageFileIndexStructure(filePath:"NotIndexed\\Image.jpg");
            var responseInsert2 = collection.Insert(database, request2);

            var indexedLocation = "Pictures";
            var responseRead = collection.Read(database, indexedLocation);

            Assert.AreEqual(1, responseRead.Files.Count);
            Assert.IsTrue(responseRead.Files.Contains(request));
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsFilesInSubDirectories()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);
            var request2 = CreateImageFileIndexStructure(filePath: "Pictures\\SubPictures\\Image.jpg");
            var responseInsert2 = collection.Insert(database, request2);

            var indexedLocation = "Pictures";
            var responseRead = collection.Read(database, indexedLocation);

            Assert.AreEqual(2, responseRead.Files.Count);
            Assert.IsTrue(responseRead.Files.Contains(request));
            Assert.IsTrue(responseRead.Files.Contains(request2));

        }


        [TestCleanup]
        public void cleanup()
        {
            database.DropCollection("ImageFileIndex");
        }
    }
}
