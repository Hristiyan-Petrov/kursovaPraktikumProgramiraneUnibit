using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaPP2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> galaxyCouter = new List<string>();
            List<String> starCouter = new List<string>();
            List<String> planetCouter = new List<string>();
            List<String> moonCouter = new List<string>();

            // tova beshe za proba v nachaloto
            //List<Money> myMoney = new List<Money>();
            //myMoney.Add(new Money { amount = 10, type = "US", stars = new List<Star>() });
            //myMoney.Add(new Money { amount = 20, type = "BGN", stars = new List<Star>() });

            //Console.WriteLine(myMoney[0].amount);

            //myMoney[0].stars.Add(new Star { name = "Earth" });
            //myMoney[0].stars.Add(new Star { name = "Earth v.2" });
            //myMoney[1].stars.Add(new Star { name = "Earth" });

            //Console.WriteLine(myMoney[0].stars[0].name);

            List<Galaxy> galaxies = new List<Galaxy>();

            string currentCommand = Console.ReadLine();
            while (currentCommand != "exit")
            {
                string task = currentCommand.Split()[0];
                switch (task)
                {
                    case "add":
                        string spaceObject = currentCommand.Split()[1];
                        switch (spaceObject)
                        {
                            case "galaxy":
                                int firstChar = currentCommand.IndexOf("[") + 1;
                                int secondChar = currentCommand.IndexOf("]");
                                int cutLength = secondChar - firstChar;
                                string name = currentCommand.Substring(firstChar, cutLength);
                                string type = currentCommand.Substring(secondChar + 2).Split()[0];
                                string age = currentCommand.Substring(secondChar + 2).Split()[1];

                                galaxies.Add(new Galaxy { name = name, type = type, age = age, stars = new List<Star>() });
                                galaxyCouter.Add(name);
                                break;

                            case "star":
                                int firstCharGalaxy = currentCommand.IndexOf("[") + 1;
                                int secondCharGalaxy = currentCommand.IndexOf("]");
                                int cutLengthGalaxy = secondCharGalaxy - firstCharGalaxy;
                                string galaxyName = currentCommand.Substring(firstCharGalaxy, cutLengthGalaxy);

                                int firstCharStar = currentCommand.LastIndexOf("[") + 1;
                                int secondCharStar = currentCommand.LastIndexOf("]");
                                int cutLengthStar = secondCharStar - firstCharStar;
                                string starName = currentCommand.Substring(firstCharStar, cutLengthStar);

                                double mass = double.Parse(currentCommand.Substring(secondCharStar + 2).Split()[0]);
                                double size = double.Parse(currentCommand.Substring(secondCharStar + 2).Split()[1]);
                                int temp = int.Parse(currentCommand.Substring(secondCharStar + 2).Split()[2]);
                                double luminosity = double.Parse(currentCommand.Substring(secondCharStar + 2).Split()[3]);
                                string starClass = "something";
                                if (temp >= 30000)
                                {
                                    starClass = "O";
                                } 
                                else if (temp <30000 && temp >= 10000)
                                {
                                    starClass = "B";
                                }
                                else if (temp < 10000 && temp >= 7500)
                                {
                                    starClass = "A";
                                }
                                else if (temp < 7500 && temp >= 6000)
                                {
                                    starClass = "F";
                                }
                                else if (temp < 6000 && temp >= 5200)
                                {
                                    starClass = "G";
                                }
                                else if (temp < 5200 && temp >= 3700)
                                {
                                    starClass = "K";
                                }
                                else if (temp < 3700 && temp >= 2400)
                                {
                                    starClass = "M";
                                }

                                int gIndex = galaxyCouter.FindIndex(x => x.Contains(galaxyName));
                                galaxies[gIndex].stars.Add(new Star { name = starName, starClass = starClass, mass = mass, size = size, temp = temp, luminosity = luminosity, planets = new List<Planet>() });
                                starCouter.Add(starName);
                                break;

                            case "planet":
                                int firstIndexStar = currentCommand.IndexOf("[") + 1;
                                int secondIndexStar = currentCommand.IndexOf("]");
                                int cutLengthStarName = secondIndexStar - firstIndexStar;
                                starName = currentCommand.Substring(firstIndexStar, cutLengthStarName);

                                int firstCharPlanet = currentCommand.LastIndexOf("[") + 1;
                                int secondCharPlanet = currentCommand.LastIndexOf("]");
                                int cutLengthPlanet = secondCharPlanet - firstCharPlanet;
                                string planetName = currentCommand.Substring(firstCharPlanet, cutLengthPlanet);

                                string planetType = currentCommand.Substring(secondCharPlanet + 2).Split()[0];
                                string supportLife = currentCommand.Substring(secondCharPlanet + 2).Split()[1];

                                string galaxyNameForIndex = "something";
                                foreach (var galaxy in galaxies)
                                {
                                    foreach (var star in galaxy.stars)
                                    {
                                        if (star.name == starName)
                                        {
                                            galaxyNameForIndex = galaxy.name;
                                        }
                                    }
                                }
                                gIndex = galaxyCouter.FindIndex(x => x.Contains(galaxyNameForIndex));

                                int sIndex = starCouter.FindIndex(x => x.Contains(starName));
                                galaxies[gIndex].stars[sIndex].planets.Add(new Planet { name = planetName, type = planetType, supportLife = supportLife, moons = new List<Moon>() });
                                planetCouter.Add(planetName);
                                break;

                            case "moon":
                                int firstIndexPlanet = currentCommand.IndexOf("[") + 1;
                                int secondIndexPlanet = currentCommand.IndexOf("]");
                                int cutLengthPlanetName = secondIndexPlanet - firstIndexPlanet;
                                planetName = currentCommand.Substring(firstIndexPlanet, cutLengthPlanetName);

                                int firstCharMoon = currentCommand.LastIndexOf("[") + 1;
                                int secondCharMoon = currentCommand.LastIndexOf("]");
                                int cutLengthMoon = secondCharMoon - firstCharMoon;
                                string moonName = currentCommand.Substring(firstCharMoon, cutLengthMoon);

                                string galaxyForMoon = "something";
                                string starForMoon = "something";
                                foreach (var galaxy in galaxies)
                                {
                                    foreach (var star in galaxy.stars)
                                    {
                                        foreach (var planet in star.planets)
                                        {
                                            if (planet.name == planetName)
                                            {
                                                galaxyForMoon = galaxy.name;
                                                starForMoon = star.name;
                                            }
                                        }
                                    }
                                }
                                gIndex = galaxyCouter.FindIndex(x => x.Contains(galaxyForMoon));
                                sIndex = starCouter.FindIndex(x => x.Contains(starForMoon));
                                int pIndex = planetCouter.FindIndex(x => x.Contains(planetName));

                                galaxies[gIndex].stars[sIndex].planets[pIndex].moons.Add(new Moon { name = moonName });
                                moonCouter.Add(moonName);
                                break;
                        }
                        break;

                    case "list":
                        spaceObject = currentCommand.Split()[1];
                        Console.WriteLine($"--- List of all researched {spaceObject}---");
                        switch (spaceObject)
                        {
                            case "galaxies":
                                foreach (var galaxy in galaxyCouter)
                                {
                                    Console.WriteLine(galaxy);
                                }
                                break;
                            case "stars":
                                foreach (var star in starCouter)
                                {
                                    Console.WriteLine(star);
                                }
                                break;
                            case "planets":
                                foreach (var planet in planetCouter)
                                {
                                    Console.WriteLine(planet);
                                }
                                break;
                            case "moons":
                                foreach (var moon in moonCouter)
                                {
                                    Console.WriteLine(moon);
                                }
                                break;
                        }
                        Console.WriteLine($"--- End of {spaceObject} list ---");
                        break;

                    case "stats":
                        Console.WriteLine("--- Stats ---");
                        Console.WriteLine($"Galaxies: {galaxyCouter.Count}");
                        Console.WriteLine($"Stars: {starCouter.Count}");
                        Console.WriteLine($"Planets: {planetCouter.Count}");
                        Console.WriteLine($"Moons: {moonCouter.Count}");
                        Console.WriteLine("--- End of stats ---");

                        break;

                    case "print":
                        int firstCharGalaxyPrint = currentCommand.IndexOf("[") + 1;
                        int secondCharGalaxyPrint = currentCommand.IndexOf("]");
                        int cutLengthGalaxyPrint = secondCharGalaxyPrint - firstCharGalaxyPrint;
                        string galaxyNamePrint = currentCommand.Substring(firstCharGalaxyPrint, cutLengthGalaxyPrint);


                        var searchedGalaxy = from g in galaxies
                                             where g.name == galaxyNamePrint
                                             select g;

                        foreach (var galaxy in searchedGalaxy)
                        {
                            Console.WriteLine($"--- Data for {galaxyNamePrint} galaxy ---");
                            Console.WriteLine($"Type: {galaxy.type}");
                            Console.WriteLine($"Age: {galaxy.age}");
                            Console.WriteLine("Stars:");
                            if (galaxy.stars.Count == 0)
                            {
                                Console.WriteLine("none");
                            }
                            foreach (var star in galaxy.stars)
                            {
                                Console.WriteLine($"\t-Name: {star.name}");
                                Console.WriteLine($"\tClass: {star.starClass} ({star.mass}, {star.size}, {star.temp}, {star.luminosity})");
                                Console.WriteLine("\tPlanets:");
                                if (star.planets.Count == 0)
                                {
                                    Console.WriteLine("\tnone");
                                }
                                foreach (var planet in star.planets)
                                {
                                    Console.WriteLine($"\t\t-Name: {planet.name}");
                                    Console.WriteLine($"\t\tType: {planet.type}");
                                    Console.WriteLine($"\t\tSupport life: {planet.supportLife}");
                                    Console.WriteLine($"\t\tMoons:");
                                    if (planet.moons.Count == 0)
                                    {
                                        Console.WriteLine("\t\tnone");
                                    }
                                    foreach (var moon in planet.moons)
                                    {
                                        Console.WriteLine($"\t\t\t{moon.name}");
                                    }
                                }
                            }
                            Console.WriteLine($"--- End of data for {galaxyNamePrint} galaxy ---");
                        }
                        break;
                }
                currentCommand = Console.ReadLine();
            }

            // lipsva kod za izpisvane na "none" pri lipsa na podobekt
        }

        class Galaxy
        {
            public string name { get; set; }
            public string type { get; set; }
            public string age { get; set; }
            public List<Star> stars { get; set; }

        }

        class Star
        {
            public string name { get; set; }
            public string starClass { get; set; }
            public double mass { get; set; }
            public double size { get; set; }
            public int temp { get; set; }
            public double luminosity { get; set; }
            public List<Planet> planets { get; set; }

        }

        class Planet
        {
            public string name { get; set; }
            public string type { get; set; }
            public string supportLife { get; set; }
            public List<Moon> moons { get; set; }

        }

        class Moon
        {
            public string name { get; set; }
        }
    }
    
}
