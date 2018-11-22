using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerateTest
{
    using Kalman.Data;
    using Kalman.Data.SchemaObject;
    using Kalman.Database;

    class Program
    {
        static void Main(string[] args)
        {
            var db = DatabaseFactory.Create("");
            var schema = DbSchemaFactory.Create(db);
            schema.GetDatabaseList();
        }
    }
}
