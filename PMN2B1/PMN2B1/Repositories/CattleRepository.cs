using PMN2B1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMN2B1.Repositories
{
    public class CattleRepository
    {
        SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public CattleRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Cattle>().Wait();
        }

        public async Task<List<Cattle>> GetAllCattleAsync()
        {
            try
            {
                return await conn.Table<Cattle>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Falha ao receber os dados. {0}", ex.Message);
            }

            return new List<Cattle>();
        }

        public Task<Cattle> GetCattleAsync(int id)
        {
            return conn.Table<Cattle>()
                           .Where(i => i.ID == id)
                           .FirstOrDefaultAsync();
        }

        public async Task SaveCattleAsync(Cattle cattle)
        {
            int result = 0;           
            
            try
            {
                if (string.IsNullOrEmpty(cattle.Identifier))
                    throw new Exception("Informe o identificador");

                if (cattle.ID != 0)
                {
                    result = await conn.UpdateAsync(cattle);
                }
                else
                {
                    result = await conn.InsertAsync(new Cattle 
                    { 
                        Identifier = cattle.Identifier,
                        Specie = cattle.Specie,
                        BirthDate = cattle.BirthDate,
                        Sex = cattle.Sex,
                    });
                }

            StatusMessage = string.Format("{0} registro(s) adicionados [Nome: {1})", result, cattle.Identifier);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Falha ao adicionar {0}. Erro: {1}", cattle.Identifier, ex.Message);
            }
                
        }

        public Task<int> DeleteCattleAsync(Cattle cattle)
        {
            return conn.DeleteAsync(cattle);
        }
    }
}
