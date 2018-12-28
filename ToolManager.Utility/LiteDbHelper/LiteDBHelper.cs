using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToolManager.Utility.LiteDbHelper
{
    using LiteDB;
    using ToolManager.Infrustructure;

    public static class LiteDBHelper<T> where T : new()
    {
        public static int Insert(String dbFileName, T model, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<T>(tableName);
                // Insert new customer document
                var value = col.Insert(model);
                return value.AsInt32;
            }
        }


        public static bool Update(String dbFileName, T model, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<T>(tableName);
                // Update a document inside a collection
                var success = col.Update(model);
                return success;
            }
        }

        public static bool Delete(String dbFileName, int docId, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var success = col.Delete(docId);
                return success;
            }
        }

        public static T FindOne(String dbFileName, Query query, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var doc = col.FindOne(query);
                return BsonHelper.BsonToObject.ConvertTo<T>(doc);
            }
        }

        public static bool Exists(String dbFileName, Query query, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var doc = col.Exists(query);
                return doc;
            }
        }

        public static BsonDocument FindBsonById(String dbFileName, int docId, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var doc = col.FindById(docId);
                return doc;
            }
        }

        public static T FindById(String dbFileName, int docId, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var doc = col.FindById(docId);
                return BsonHelper.BsonToObject.ConvertTo<T>(doc);
            }
        }

        public static IEnumerable<BsonDocument> FindBsonAll(String dbFileName, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection(tableName);
                var doc = col.FindAll();
                return doc;
            }
        }

        public static IEnumerable<T> FindAll(String dbFileName, string tableName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(dbFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<T>(tableName);
                var docs = col.FindAll();
                return docs;
            }
        }
    }
}
