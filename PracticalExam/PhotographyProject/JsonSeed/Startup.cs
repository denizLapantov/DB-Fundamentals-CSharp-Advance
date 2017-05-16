using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Newtonsoft.Json;
using Photography.Data;
using Photography.Model;

namespace JsonSeed
{
    public class Startup
    {
        private const string LensPath = "../../../datasets/lenses.json";
        private const string CamerasParh = "../../../datasets/cameras.json";
        private const string PhotograogersPath = "../../../datasets/photographers.json";

        static void Main(string[] args)
        {
            var context = new PhotographyContext();

         ImportLenses(context);
         ImportCameras(context);
         ImportPhotographers(context);

        }

        private static void ImportPhotographers(PhotographyContext context)
        {
            var json = File.ReadAllText(PhotograogersPath);
            var photographersEntity = JsonConvert.DeserializeObject<IEnumerable<PhotographerDto>>(json);

            foreach (var photographer in photographersEntity)
            {
                if (photographer.FirstName == null || photographer.LastName == null || photographer.Phone == null)
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }


                var newPhotograph = new Photographer
                {
                    FirstName = photographer.FirstName,
                    LastName = photographer.LastName,
                    Phone = photographer.Phone
                };

                try
                {
                    context.Photographers.Add(newPhotograph);
                    context.SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {
                    context.Photographers.Remove(newPhotograph);
                }

                Console.WriteLine($"Successfully imported {photographer.FirstName} {photographer.LastName} | Lenses: {photographer.Lenses.Count}");
            }

            context.SaveChanges();
        

    
}

        private static void ImportCameras(PhotographyContext context)
        {
            var json = File.ReadAllText(CamerasParh);
            var camerasEntity = JsonConvert.DeserializeObject<IEnumerable<CameraDto>>(json);

            foreach (var camera in camerasEntity)
            {
                if (camera.Make == null || camera.Model == null || camera.Type == null || camera.MinIso < 100)
                   
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }

                if (camera.Type == "DSLR")
                {


                    var newdslrCamera = new DslrCamera
                    {
                        Make = camera.Make,
                        Model = camera.Model,
                        MinIso = camera.MinIso,
                        MaxIso = camera.MaxIso,
                        IsFullFrame = camera.IsFullFrame,
                        MaxShutterSpeed = camera.MaxShutterSpeed,

                    };

                    try
                    {
                        context.Cameras.Add(newdslrCamera);
                        context.SaveChanges();

                    }
                    catch (DbEntityValidationException db)
                    {
                        context.Cameras.Remove(newdslrCamera);
                        context.SaveChanges();
                    }

                    Console.WriteLine($"Successfully imported {camera.Type} {camera.Make} {camera.Model}");
                }
                else
                {
                    var mirrorlesCamera = new MirrorlessCamera()
                    {
                        Make = camera.Make,
                        Model = camera.Model,
                        MinIso = camera.MinIso,
                        MaxIso = camera.MaxIso,
                        IsFullFrame = camera.IsFullFrame,
                        MaxVideoResolution = camera.MaxVideoResolution,
                        MaxFrameRate = camera.MaxFrameRate

                    };

                    try
                    {
                        context.Cameras.Add(mirrorlesCamera);
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException db)
                    {
                        context.Cameras.Remove(mirrorlesCamera);
                        Console.WriteLine($"Successfully imported {camera.Type} {camera.Make} {camera.Model}");
                    }
                }
                context.SaveChanges();
            }
        }

        private static void ImportLenses(PhotographyContext context)
        {
            var jsoon = File.ReadAllText(LensPath);
            var lensesEntity = JsonConvert.DeserializeObject<IEnumerable<LensDto>>(jsoon);


            foreach (var lens in lensesEntity)
            {
                if (lens.Make == null || lens.CompatibleWith == null)
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }


                var lensEntityAsJson = new Lens
                {
                    Make = lens.Make,
                    FocalLenght = lens.FocalLength,
                    MaxAperture = lens.MaxAperture,
                    CompatibleWith = lens.CompatibleWith
                };


                try
                {
                    context.Lenses.Add(lensEntityAsJson);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    context.Lenses.Remove(lensEntityAsJson);
                    context.SaveChanges();
                }
               
                Console.WriteLine($"Successfully imported {lens.Make} {lens.FocalLength}mm f{lens.MaxAperture}.");
            }
          
        }
    }
}
