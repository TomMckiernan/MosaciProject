using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Utility;

namespace UtilityTests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void BsonDocumentInCorrectFormatReturnsCompleteIndexedLocationStructure()
        {
            var bson = new BsonDocument();
            var id = new BsonElement("_id", "0");
            var indexedLocation = new BsonElement("IndexedLocation", "IndexedLocation");
            bson.Add(id);
            bson.Add(indexedLocation);
            var result = new IndexedLocationStructure().ConvertFromBsonDocument(bson);
            Assert.AreEqual("0", result.Id);
            Assert.AreEqual("IndexedLocation", result.IndexedLocation);
        }
    }
}
