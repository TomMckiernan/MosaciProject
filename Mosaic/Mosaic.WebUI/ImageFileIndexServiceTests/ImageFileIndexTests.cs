using ImageFileIndexService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageFileIndexServiceTests
{
    [TestClass]
    public class ImageFileIndexTests
    {
        MongoClient client;
        IMongoDatabase database;

        [TestInitialize]
        public void init()
        {
            client = new MongoClient();
            database = client.GetDatabase("TestMosaicDatabase");
        }

        // Uses a test directory containing seven jpg images
        [TestMethod]
        public async Task AllFilesInDirectoryImportedIntoCollection()
        {
            var indexedLocation = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\Test7Images";
            await Task.Run(async () =>
            {
                // Actual test code here.
                var service = new ImageFileIndex("TestMosaicDatabase");
                await service.AnalyseImageFileIndex(indexedLocation);
            });
            var result = new MongoImageFileIndex().Read(database, indexedLocation);
            Assert.AreEqual(7, result.Files.Count);

        }

        // Test jpg files in subdirectores are read - tested in mongo test class
        // However the initial ftech of the sub directory files is not checked

        // Test jpg files deleted in directory no longer appear in collection

        // Test jpg files updated in directory are updated in collection
        // This may be manual only 

        // Test file with same name but in different directories doesn't alter
        // existing file in collection

        [TestCleanup]
        public void cleanup()
        {
            database.DropCollection("ImageFileIndex");
        }

    }
}
