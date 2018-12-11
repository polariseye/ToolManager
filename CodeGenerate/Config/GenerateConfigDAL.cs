using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.Config
{
    using Kalman;
    using Kalman.Database;
    using LiteDB;

    /// <summary>
    /// 生成配置文件
    /// </summary>
    public class GenerateConfigDAL
    {
        static string TABLE_NAME = "GenerateConfig"; //table name

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(GenerateConfig model)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(NormalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<GenerateConfig>(TABLE_NAME);

                var value = col.Insert(model);
                return value.AsInt32;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(GenerateConfig model)
        {
            return LiteDBHelper<GenerateConfig>.Update(model, TABLE_NAME);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(NormalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<GenerateConfig>(TABLE_NAME);

                return col.Delete(tmp => true);
            }
        }

        /// <summary>
        /// Find all list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GenerateConfig> FindAll()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(NormalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<GenerateConfig>(TABLE_NAME);

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
            using (var db = new LiteDatabase(NormalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<GenerateConfig>(TABLE_NAME);

                return col.Count();
            }
        }

        /// <summary>
        /// Init Database
        /// </summary>
        public static void Init()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(NormalConfig.SettingDataFileName))
            {
                // Get DbConnection collection
                var col = db.GetCollection<GenerateConfig>(TABLE_NAME);

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
