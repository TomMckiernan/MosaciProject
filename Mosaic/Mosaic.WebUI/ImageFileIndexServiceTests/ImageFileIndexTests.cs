﻿using ImageFileIndexService;
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
        public void Init()
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
                var request = new ImageFileIndexRequest() { IndexedLocation = indexedLocation };
                var service = new ImageFileIndex("TestMosaicDatabase");
                await service.AnalyseImageFileIndex(request);
            });
            var result = new MongoImageFileIndex().ReadAll(database, indexedLocation);
            Assert.AreEqual(7, result.Files.Count);
        }

        // Test png files in subdirectores are read - tested in mongo test class
        // However the initial fetch of the sub directory files is not checked

        // Test png files deleted in directory no longer appear in collection

        // Test png files updated in directory are updated in collection
        // This may be manual only 

        // Test file with same name but in different directories doesn't alter
        // existing file in collection

        [TestCleanup]
        public void Cleanup()
        {
            database.DropCollection("ImageFileIndex");
        }

    }
}
