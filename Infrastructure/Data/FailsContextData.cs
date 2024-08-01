using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace Infrastructure.Data
{
    public class FailsContextData
    {
        public static async Task LoadDataAsync(FailsContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Fails!.Any())
                {
                    // llamar al archivo JSON para poner informacion 
                    var countryData = File.ReadAllText("../Infrastructure/Data/fails.json");

                    var countries = JsonConvert.DeserializeObject<List<Fail>>(countryData);

                    await context.Fails!.AddRangeAsync(countries!);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) {
                var logger = loggerFactory.CreateLogger<FailsContextData>();
                logger.LogError(e.Message);
            }
        }
    }
}
