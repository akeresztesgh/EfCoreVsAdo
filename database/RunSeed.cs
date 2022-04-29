using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class RunSeed
    {
        private readonly EfDbContext dbContext = new EfDbContext();
        public RunSeed()
        {
        }

        public async Task Run(int numAs, int numBsPerA)
        {
            if (dbContext.ARows.Any())
            {
                dbContext.BRows.RemoveRange(dbContext.BRows);
                await dbContext.SaveChangesAsync();
                dbContext.ARows.RemoveRange(dbContext.ARows);
                await dbContext.SaveChangesAsync();

                await dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('ARows', RESEED, 0)");
                await dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('BRows', RESEED, 0)");
            }

            for(var i = 1; i <= numAs; i++)
            {
                var aRow = new A()
                {
                    Name = i.ToString(),
                };
                dbContext.ARows.Add(aRow);
                for (var j = 1; j <= numBsPerA; j++)
                {
                    var bRow = new B()
                    {
                        Name = $"{i} - {j}",
                        AId = aRow.Id
                    };
                    aRow.Bs.Add(bRow);
                }                

                if (i % 100 == 0)
                {
                    Console.WriteLine($"{i} rows complete");
                    await dbContext.SaveChangesAsync();
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
