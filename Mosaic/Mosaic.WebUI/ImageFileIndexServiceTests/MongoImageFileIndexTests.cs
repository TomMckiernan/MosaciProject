using ImageFileIndexService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ImageFileIndexServiceTests
{
    [TestClass]
    public class MongoImageFileIndexTests
    {
        MongoClient client;
        IMongoDatabase database;

        [TestInitialize]
        public void Init()
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
                Data = new Metadata() { }
            };
        }

        [TestMethod]
        public void ImageFileIndexReadReturnNoFilesWithNoneInserted()
        {
            var collection = new MongoImageFileIndex();

            var indexedLocation = "Pictures";
            var responseRead = collection.ReadAll(database, indexedLocation);
            Assert.AreEqual(0, responseRead.Files.Count);
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsOneInsertedFiles()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);

            var indexedLocation = "Pictures";
            var responseRead = collection.ReadAll(database, indexedLocation);
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
            var responseRead = collection.ReadAll(database, indexedLocation);

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
            var responseRead = collection.ReadAll(database, indexedLocation);

            Assert.AreEqual(2, responseRead.Files.Count);
            Assert.IsTrue(responseRead.Files.Contains(request));
            Assert.IsTrue(responseRead.Files.Contains(request2));
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsErrorIfIndexedLocationNull()
        {
            var response = new MongoImageFileIndex().ReadAll(database, null);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void ImageFileIndexReadReturnsErrorIfIndexedLocationEmpty()
        {
            var indexedLocation = String.Empty;
            var response = new MongoImageFileIndex().ReadAll(database, indexedLocation);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void ReadImageFileReturnsFileWithCorrectId()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);
            var responseRead = collection.ReadImageFile(database, request.Id);

            Assert.IsTrue(responseRead.File.Equals(request));
        }

        [TestMethod]
        public void ReadImageFileReturnsErrorIfIdIsNullOrEmpty()
        {
            var collection = new MongoImageFileIndex();
            var responseRead = collection.ReadImageFile(database, string.Empty);
            Assert.IsFalse(String.IsNullOrEmpty(responseRead.Error));
        }

        [TestMethod]
        public void ReadImageFileReturnsErrorFileWithIdDoesNotExist()
        {
            var collection = new MongoImageFileIndex();
            var responseRead = collection.ReadImageFile(database, "1");
            Assert.IsFalse(String.IsNullOrEmpty(responseRead.Error));
        }

        [TestMethod]
        public void ReadAllImageFilesReturnsErrorIfIdsIsNull()
        {
            var collection = new MongoImageFileIndex();
            var responseRead = collection.ReadAllImageFiles(database, null);
            Assert.IsFalse(String.IsNullOrEmpty(responseRead.Error));
        }

        [TestMethod]
        public void ReadAllImageFilesReturnsAllFilesWhichMatchIds()
        {
            var collection = new MongoImageFileIndex();
            var request = CreateImageFileIndexStructure();
            var responseInsert = collection.Insert(database, request);
            var request2 = CreateImageFileIndexStructure(filePath: "Pictures\\SubPictures\\Image.jpg");
            var responseInsert2 = collection.Insert(database, request2);

            var ids = new List<string>() { responseInsert.File.Id, responseInsert2.File.Id };
            var responseRead = collection.ReadAllImageFiles(database, ids);

            Assert.AreEqual(2, responseRead.Files.Count);
            Assert.IsTrue(responseRead.Files.Contains(request));
            Assert.IsTrue(responseRead.Files.Contains(request2));
        }

        [TestCleanup]
        public void Cleanup()
        {
            database.DropCollection("ImageFileIndex");
        }
    }
}
