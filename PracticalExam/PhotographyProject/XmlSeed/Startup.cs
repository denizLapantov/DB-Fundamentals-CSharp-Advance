using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Photography.Data;
using Photography.Model;

namespace XmlSeed
{
    public class Startup
    {
        private const string AccessoriesPath = "../../../datasets/accessories.xml";

        private const string WorkShopPaths = "../../../datasets/workshops.xml";


        static void Main(string[] args)
        {
            var context = new PhotographyContext();

            ImportAccesories(context);
            ImportWorkshop(context);
        }

        private static void ImportWorkshop(PhotographyContext context)
        {
            XDocument xDocument = XDocument.Load(WorkShopPaths);
            var workshop = xDocument.XPathSelectElements("workshops/workshop");

            foreach (var work in workshop)
            {
                var name = work.Attribute("name");
                var starrtDate = (DateTime) work.Attribute("start-date");
                var endDate = (DateTime) work.Attribute("end-date");
                var location = work.Attribute("location");
                var pricePerParticipant = (decimal) work.Attribute("price");

                if (name == null || location == null || starrtDate == null || endDate == null)
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }



                var newWorkshop = new Workshop
                {
                    Name = name.Value,
                    StartDate = starrtDate,
                    EndDate = endDate,
                    Location = work.Value,
                    PricePerParticipant = pricePerParticipant,

                };

                try
                {
                    context.Workshops.Add(newWorkshop);
                }
                catch (DbEntityValidationException ex)
                {
                    context.Workshops.Remove(newWorkshop);
                }
                

                Console.WriteLine($"Successfully imported {work.Name}");


            }
        }

        private static void ImportAccesories(PhotographyContext context)
        {
            XDocument xDocument =XDocument.Load(AccessoriesPath);
            var suppliers = xDocument.XPathSelectElements("accessories/accessory");
            var rnd = new Random();

            foreach (var supplier in suppliers)
            {
                ImportAccesorie(supplier, context,rnd);
            }

            context.SaveChanges();
        }

        private static void ImportAccesorie(XElement accesry, PhotographyContext context,Random rnd)
        {
            var name = accesry.Attribute("name");

            int accesoriesNumber = rnd.Next(1, context.Accessories.Count() + 1);


            if (name == null)
            {
                Console.WriteLine("Error. Invalid data provided");
            }

            var accespry = new Accessory
            {
                Name = name.Value,
                Owner = context.Photographers.FirstOrDefault(x => x.Id == accesoriesNumber)
            };

            context.Accessories.Add(accespry);

            Console.WriteLine($"Successfully imported {accespry.Name}");

        }
    }
}
