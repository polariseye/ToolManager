using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.TypeMap
{
    using LiteDB;
    using ToolManager.Infrustructure;
    using ToolManager.Utility.LiteDbHelper;

    public class TypeMapConfigDAL
    {
        static string TABLE_NAME = "TypeMapConfig"; //table name

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(TypeMapConfig model)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                var value = col.Insert(model);
                return value.AsInt32;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(TypeMapConfig model)
        {
            return LiteDBHelper<TypeMapConfig>.Update(LocalConfig.SettingDataFileName, model, TABLE_NAME);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public int Delete(Int32 id)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                return col.Delete(tmp => tmp.Id == id);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public int DeleteByLanguage(String language)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                return col.Delete(tmp => tmp.Language == language);
            }
        }

        /// <summary>
        /// Find all list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TypeMapConfig> FindAll()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                return col.FindAll();
            }
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                return col.Count();
            }
        }

        /// <summary>
        /// Init Database
        /// </summary>
        public static void Init()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(LocalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<TypeMapConfig>(TABLE_NAME);

                if (col.Count() == 0)
                {
                    // Index document using a document property
                    // col.EnsureIndex(x => x.Name, true);
                    // col.EnsureIndex(x => x.ModulePath, true);
                }
            }
        }
    }
}
