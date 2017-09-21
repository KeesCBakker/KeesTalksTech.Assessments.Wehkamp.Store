using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Math;
using KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unsplasharp;
using Unsplasharp.Models;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalogGenerator
{
    class Program
    {
        private static readonly string[] noiseKeywords = "photo taken keyword original primary color with this view cuts against their deep shot high empty through".Split(' ');

        static void Main(string[] args)
        {
            var key = "b698bd6365f99a2d28ff5f2953d9de804742cf47baccb1890719397253564652";
            //var key = "e5457c652514029496f7eda5d48fbad171d94b74747f59bc2f592e1a4adc762a";
            //var key = "07fbb153f650b9a882fd916f59d3b84c694b3f6457174cdab8e3470f380c4e7c";
            //var key = "1c8949783fd75ec3a72bbcc3a668eb378f780292c04fd5803d305076dc080036";
            var destination = @"..\KeesTalksTech.Assessments.Wehkamp.Store.StoreApi\data\products.xml";

            var client = new UnsplasharpClient(key);
            var randomizer = new Random();

            List<PhotoProduct> products = new List<PhotoProduct>();

            Console.WriteLine("This tool will generate a products.xml database based on Unsplash photos.");
            Console.WriteLine("Unsplash has a rate limit of 50 request / hour.");

            string[] keywords = {
                    "lights", "cloths", "Berlin", "Paris", "computer", "phone",
                    "building", "food", "love", "smile", "music",
                    "animal", "car", "sports", "bird", "Beauty",
                    "model", "tools", "working", "wild", "water", "portrait",
                    "winter", "summer"};

            foreach (var keyword in keywords)
            {
                Console.WriteLine();
                Console.WriteLine($"Downloading '{keyword}' photos from Unsplash.com...");

                var photosTask = client.SearchPhotos(keyword, 1, 30);
                var photos = photosTask.Result;

                var photosTask2 = client.SearchPhotos(keyword, 2, 30);
                var photos2 = photosTask.Result;

                photos.AddRange(photos2);

                var processed = photos
                    //skip photos without a description
                    .Where(p => !String.IsNullOrEmpty(p.Description))
                    .Select(p => new PhotoProduct
                    {
                        Id = p.Id,
                        Name = p.Description,
                        Description = $"Photo was taken by {p.User.Name} on {p.CreatedAt}. The primary color of this photo is {p.Color}. Original is {p.Height}x{p.Width}.",
                        PictureUrl = p.Urls.Regular,
                        ThumbnailUrl = p.Urls.Small,
                        Price = new PhotoProductPrice()
                        {
                            Amount = Randomizer.GenerateRandomNumber(1, 25, 2)
                        },
                        Keywords = GetKeywords(p, keyword),
                        Orientation = GetOrientation(p)
                    })
                    .ToList();

                processed.ForEach(p => products.Add(p));

                Console.WriteLine($"{processed.Count} downloaded.");
            }

            Console.WriteLine();
            Console.WriteLine($"{products.Count} photo records downloaded.");
            Console.WriteLine();
            Console.WriteLine($"Writing products to file...");

            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, destination);

            XmlSerializationHelper.SerializeToFile(path, products);

            Console.WriteLine();
            Console.WriteLine("Ready!");
            Console.ReadLine();
        }

        private static string GetOrientation(Photo photo)
        {
            if(photo.Height > photo.Width)
            {
                return "p";
            }

            return "l";
        }


        private static string[] GetKeywords(Photo photo, string keyword)
        {
            var list = new List<string>();

            list.Add(keyword);

            photo.Description
                     .Split(",. '\"“”/:!?[]{}()_+-=;".ToArray())
                     .Where(s => s.Length >= 4)
                     .Where(s => !noiseKeywords.Contains(s.ToLower()))
                     .ToList()
                     .ForEach(s => list.Add(s));

            list.Add(photo.User.Name);
            list.Add(photo.Color);
            list.Add($"{photo.Height}x{photo.Width}");

            return list.ToArray();
        }
    }
}
