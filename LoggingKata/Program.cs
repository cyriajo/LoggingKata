using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";

            if (path.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified as an argument");
                Console.ReadLine();
                return;

            }
            Logger.Info("Log initialized");
            Logger.Info("Grabbing from path: = + path");
            var lines = File.ReadAllLines(path);

            if (lines.Length == 0)
            {
                Logger.Error("No Locations to check. Must have atleast one Location");
            }
            else if (lines.Length == 1)
            {
                Logger.Warn("Only one Location is provided");
            }

            Console.WriteLine("Initializing TacoParser");

            var parser = new TacoParser();
            var locations = lines.Select(line => parser.Parse(line));

            Console.WriteLine("Parsed Locations");

            //TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.
            //HINT:  You'll need two nested forloops
            ITrackable a = null;
            ITrackable b = null;

            double distance = 0;

            Console.WriteLine("Starting Foreach");

            foreach (var locA in locations)
            {
                var origin = new Coordinate
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };

                foreach (var locB in locations)
                {
                    var destination = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };

                    var nDist = GeoCalculator.GetDistance(origin, destination);

                    if (!(nDist > distance)) continue;
                    Console.WriteLine("Found the next furthest apart");
                    a = locA;
                    b = locB;
                    distance = nDist;
                }
            }

            Console.WriteLine("Finished foreach loops");

            if (a == null || b == null)
            {
                Logger.Error("Failed to find the furthest location");
                Console.WriteLine("Couldn't find the locations furthest apart");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"The two Taco Bells that are furthest apart are: {a.Name} and {b.Name}");
            Console.WriteLine($"These two locations are {distance} apart");
            Console.ReadLine();
        }
    }
}