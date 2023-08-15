using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pantry.Core.Database.Sqlite;

namespace Pantry.Core.Database;
public class DatabaseManager
{
    public IPantryDatabase GetPantryDatabase() { return new SqlitePantryDatabase(); }
}