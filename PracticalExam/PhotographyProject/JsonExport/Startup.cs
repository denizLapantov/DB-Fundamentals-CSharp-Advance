using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Photography.Data;

namespace JsonExport
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var context = new PhotographyContext();
            ExportedPhotographOrdered(context);
        }

        private static void ExportedPhotographOrdered(PhotographyContext context)
        {
            var orderedPhotographters = context.Photographers
                .OrderBy(x => x.FirstName)
                .ThenByDescending(x => x.LastName)
                .Select(ph => new
                {
                    ph.FirstName,
                    ph.LastName,
                    ph.Phone
                });

            var orderedAsJson = JsonConvert.SerializeObject(orderedPhotographters,Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("..//..//..//ExportedJson//orderedPhotographers.json", orderedAsJson);
        }

    }
}
