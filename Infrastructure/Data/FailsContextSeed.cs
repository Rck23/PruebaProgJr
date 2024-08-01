using Core.Entities;
using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Infrastructure.Data
{
    public class FailsContextSeed
    {
        public static async Task SeedAsync(FailsContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Fails.Any())
                {

                    using (var readerFails = new StreamReader(ruta + @"/Data/Csvs/fails.csv"))
                    {
                        using (var csvFails = new CsvReader(readerFails, CultureInfo.InvariantCulture))
                        {
                            var RegisterFails = csvFails.GetRecords<Fail>();
                            
                            context.Fails.AddRange(RegisterFails);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<FailsContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
