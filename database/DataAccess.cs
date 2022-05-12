using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class DataAccess
    {
        private readonly EfDbContext dbContext = new EfDbContext();
        public DataAccess()
        {

        }

        public async Task<List<A>> GetFromEf(List<long> lst)
        {
            return await dbContext.ARows.AsNoTracking()
                                  .Include(x => x.Bs)
                                  .Where(x => lst.Contains(x.Id))
                                  .OrderBy(x => x.Id)
                                  .ToListAsync();
        }

        public async Task<List<A>> GetFromAdo(List<long> lst)
        {
            var con = new SqlConnection(EfDbContext.ConnectionString);
            var cmd = new SqlCommand(@$"SELECT A.Id, A.Name, B.Id as BId, B.Name as BName 
                                       FROM AROWS A JOIN BROWS B ON B.AID=A.ID
                                       WHERE A.Id in ({string.Join(',', lst)})
                                       ORDER BY A.Id ASC", con);

            var retLst = new List<A>();
            try
            {
                con.Open();
                using(var rdr = cmd.ExecuteReader())
                {
                    var a = new A();
                    while (await rdr.ReadAsync())
                    {
                        var aId = rdr.GetInt64(0);
                        if(a.Id != aId)
                        {
                            a = new A();
                            a.Id = aId;
                            a.Name = rdr.GetString(1);
                            retLst.Add(a);
                        }

                        var b = new B()
                        {
                            Id = rdr.GetInt64(2),
                            Name = rdr.GetString(3),
                            AId = aId,
                        };
                        a.Bs.Add(b);
                    }
                }

            }
            finally
            {
                con?.Close();
            }
            return retLst;
        }

        public async Task<long> InsertEf(string name)
        {
            var a = new A()
            {
                Name = name
            };
            dbContext.ARows.Add(a);
            await dbContext.SaveChangesAsync();
            return a.Id;
        }

        public async Task<long> InsertAdo(string name)
        {
            var con = new SqlConnection(EfDbContext.ConnectionString);
            var cmd = new SqlCommand(@$"INSERT INTO AROWS OUTPUT INSERTED.ID VALUES(@name)", con);
            cmd.Parameters.AddWithValue("name", name);

            try
            {
                con.Open();

                return (long)await cmd.ExecuteScalarAsync();
            }
            finally
            {
                con.Close();
            }

        }

        public async Task InsertManyEf(List<string> names)
        {
            var aLst = names.Select(x=> new A() { Name = x });
            await dbContext.ARows.AddRangeAsync(aLst);
            await dbContext.SaveChangesAsync();
        }

        public async Task InsertManyAdo(List<string> names)
        {
            var con = new SqlConnection(EfDbContext.ConnectionString);
            var str = new StringBuilder("INSERT INTO AROWS VALUES");
            var cmd = new SqlCommand();
            cmd.Connection = con;

            for (var i = 0; i < names.Count; i++)
            {
                str.Append($"(@name{i})");
                if(i < names.Count - 1)
                {
                    str.Append(",");
                }

                cmd.Parameters.AddWithValue($"name{i}", names[i]);
            }
            cmd.CommandText = str.ToString();

            try
            {
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                con.Close();
            }

        }
    }
}
