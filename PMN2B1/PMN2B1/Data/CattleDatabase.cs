using PMN2B1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMN2B1.Data
{
    public class CattleDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public CattleDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Cattle>().Wait();
        }

        public Task<List<Cattle>> GetAllCattleAsync()
        {
            return _database.Table<Cattle>().ToListAsync();
        }

        public Task<Cattle> GetCattleAsync(int id)
        {
            return _database.Table<Cattle>()
                           .Where(i => i.ID == id)
                           .FirstOrDefaultAsync();
        }

        public Task<int> SaveCattleAsync(Cattle cattle)
        {
            if (cattle.ID != 0)
            {
                return _database.UpdateAsync(cattle);
            }
            else
            {
                return _database.InsertAsync(cattle);
            }
        }

        public Task<int> DeleteCattleAsync(Cattle cattle)
        {
            return _database.DeleteAsync(cattle);
        }
    }
}
