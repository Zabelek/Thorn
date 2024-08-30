using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class FileManager
    {
        #region Variables
        private static XmlSerializer _galaxySerializer, _clusterSerializer, _starSerializer, _planetSerializer, _locationSerializer, _personSerializer, 
            _mainCharacterSerializer, _climateControllerSerializer, _dateSerializer, _itemSerializer;
        private static XmlSerializer _usableObjectSerializer;
        #endregion

        #region Methods
        public static void LoadUniverseFirstTime()
        {
            Universe.GameDate = new System.DateTime(3835, 1, 1);
            Universe.Galaxies.Add(new Galaxy("Milkiway"));
            Cluster local = new Cluster("Local", new Vector2(0,0));
            Universe.Galaxies[0].Clusters.Add(local);
            Star sol = new Star("Sol", new Vector2(0, 0));
            local.Stars.Add(sol);
            Planet earth = new Planet("Earth", 15, 1, 87);
            earth.Size = 1;
            earth.Type = Planet.PlanetType.RockyPlanet;
            sol.Planets.Add(earth);
            Location svania = new Location("Svania");
            Location svmain = new Location("Svania Main Square");
            svmain.ShortName = "Main Square";
            Location qualtekmf = new Location("QualTek Main Facility");
            qualtekmf.ShortName = "QualTek Buildings";
            Location qualtekmfe = new Location("QualTek Main Facility Entrance");
            qualtekmfe.ShortName = "QualTek Entrance";
            Location wstprocplan = new Location("Waste Processing Plant");
            wstprocplan.ShortName = "WPP";
            Location wstprocplane = new Location("Waste Processing Plant Entrance");
            wstprocplane.ShortName = "Entrance";
            Location wstprocplanfg = new Location("Waste Processing Plant Fencing Gap");
            wstprocplanfg.ShortName = "Fencing Gap";
            Location symresar = new Location("Symbiont Residential Area");
            symresar.ShortName = "Symbiont District";
            Location abdwh = new Location("Abandoned Symbiont Dwelling");
            abdwh.ShortName = "My House";
            Location zackhome = new Location("Zack's Home");
            abdwh.ShortName = "My House";
            zackhome.ShortName = "My Home";
            zackhome.IsOpenSpace = false;
            Location humtesar = new Location("Human Residential Area");
            humtesar.ShortName = "Human District";
            svania.AddLocation(svmain);
            svania.AddLocation(qualtekmf);
                qualtekmf.AddLocation(qualtekmfe);
            svania.AddLocation(wstprocplan);
                wstprocplan.AddLocation(wstprocplane);
                wstprocplan.AddLocation(wstprocplanfg);
            svania.AddLocation(symresar);
                symresar.AddLocation(abdwh);
                    abdwh.AddLocation(zackhome);
            svania.AddLocation(humtesar);
            earth.AddLocation(svania);
            zackhome.AverageTemperature = 20;
            MainCharacter zack = new MainCharacter();
            Universe.MainCharacter = zack;
            zack.FirstName = "Zachary";
            zack.LastName = "Ksaewron";
            zack.PersonBodyType = Person.BodyType.Skinny;
            zack.HomeLocation = zackhome;
            zackhome.AddPerson(zack);
        }
        public static void LoadUniverse()
        {
            _galaxySerializer = new XmlSerializer(typeof(Galaxy));
            _clusterSerializer = new XmlSerializer(typeof(Cluster));
            _starSerializer = new XmlSerializer(typeof(Star));
            _planetSerializer = new XmlSerializer(typeof(Planet));
            _locationSerializer = new XmlSerializer(typeof(Location));
            _personSerializer = new XmlSerializer(typeof(Person));
            _mainCharacterSerializer = new XmlSerializer(typeof(MainCharacter));
            _climateControllerSerializer = new XmlSerializer(typeof(ClimateController));
            _dateSerializer = new XmlSerializer(typeof(DateTime));
            _itemSerializer = new XmlSerializer(typeof(List<Item>));
            LoadDate();
            try
            {
                using (FileStream reader = File.Open("Content\\Universe\\Milkiway\\Milkiway.xml", FileMode.Open))
                {
                }
                LoadRegisteredItems();
                String basePath = "Content\\Universe\\";
                String[] galaxyDirectories = Directory.GetDirectories(basePath);
                foreach(String galaxyDirectory in galaxyDirectories)
                {
                    String[] gsubs = galaxyDirectory.Split("\\");
                    using (FileStream greader = File.Open(galaxyDirectory+"\\"+ gsubs[gsubs.Length-1]+".xml", FileMode.Open))
                    {
                        Galaxy g = (Galaxy)_galaxySerializer.Deserialize(greader);
                        Universe.Galaxies.Add(g);
                        String[] clusterDirectories = Directory.GetDirectories(galaxyDirectory);
                        foreach (String clusterDirectory in clusterDirectories)
                        {
                            String[] clsubs = clusterDirectory.Split("\\");
                            using (FileStream clreader = File.Open(clusterDirectory + "\\" + clsubs[clsubs.Length - 1] + ".xml", FileMode.Open))
                            {
                                Cluster cl = (Cluster)_clusterSerializer.Deserialize(clreader);
                                g.Clusters.Add(cl);
                                String[] starDirectories = Directory.GetDirectories(clusterDirectory);
                                foreach (String starDirectory in starDirectories)
                                {
                                    String[] stsubs = starDirectory.Split("\\");
                                    using (FileStream streader = File.Open(starDirectory + "\\" + stsubs[stsubs.Length - 1] + ".xml", FileMode.Open))
                                    {
                                        Star st = (Star)_starSerializer.Deserialize(streader);
                                        cl.Stars.Add(st);
                                        String[] planetDirectories = Directory.GetDirectories(starDirectory);
                                        foreach (String planetDirectory in planetDirectories)
                                        {
                                            String[] plsubs = planetDirectory.Split("\\");
                                            using (FileStream plreader = File.Open(planetDirectory + "\\" + plsubs[plsubs.Length - 1] + ".xml", FileMode.Open))
                                            {
                                                Planet pl = (Planet)_planetSerializer.Deserialize(plreader);
                                                st.Planets.Add(pl);
                                                pl.MainDirectory = planetDirectory + "\\";
                                                String[] locationDirectories = Directory.GetDirectories(planetDirectory);
                                                foreach (String locationDirectory in locationDirectories)
                                                {
                                                    LoadLocation(locationDirectory, pl);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(IOException e)
            {
                LoadUniverseFirstTime();
            }
        }
        public static void SaveUniverse()
        {
            String basePath = "Content\\Universe\\";
            SaveRegisteredItems();
            foreach (Galaxy g in Universe.Galaxies)
            {
                String galaxyBasePath = basePath + g.Name + "\\";
                try
                {
                    TextWriter writer = new StreamWriter(galaxyBasePath + g.Name + ".xml");
                    _galaxySerializer.Serialize(writer, g);
                    writer.Close();
                }
                catch (IOException e)
                {
                    Directory.CreateDirectory(basePath + g.Name);
                    using (System.IO.FileStream str = File.Create(galaxyBasePath + g.Name + ".xml"))
                    { }
                    TextWriter writer = new StreamWriter(galaxyBasePath + g.Name + ".xml");
                    _galaxySerializer.Serialize(writer, g);
                    writer.Close();
                }
                foreach (Cluster cl in g.Clusters)
                {
                    String clusterBasePath = galaxyBasePath + cl.Name + "\\";
                    try
                    {
                        TextWriter writer = new StreamWriter(clusterBasePath + cl.Name + ".xml");
                        _clusterSerializer.Serialize(writer, cl);
                        writer.Close();
                    }
                    catch (IOException e)
                    {
                        Directory.CreateDirectory(galaxyBasePath + cl.Name);
                        using (System.IO.FileStream str = File.Create(clusterBasePath + cl.Name + ".xml"))
                        { }
                        TextWriter writer = new StreamWriter(clusterBasePath + cl.Name + ".xml");
                        _clusterSerializer.Serialize(writer, cl);
                        writer.Close();
                    }
                    foreach (Star st in cl.Stars)
                    {
                        String starBasePath = clusterBasePath + st.Name + "\\";
                        try
                        {
                            TextWriter writer = new StreamWriter(starBasePath + st.Name + ".xml");
                            _starSerializer.Serialize(writer, st);
                            writer.Close();
                        }
                        catch (IOException e)
                        {
                            Directory.CreateDirectory(clusterBasePath + st.Name);
                            using (System.IO.FileStream str = File.Create(starBasePath + st.Name + ".xml"))
                            { }
                            TextWriter writer = new StreamWriter(starBasePath + st.Name + ".xml");
                            _starSerializer.Serialize(writer, st);
                            writer.Close();
                        }
                        foreach(Planet pl in st.Planets)
                        {
                            String planetBasePath = starBasePath + pl.Name + "\\";
                            try
                            {
                                TextWriter writer = new StreamWriter(planetBasePath + pl.Name + ".xml");
                                _planetSerializer.Serialize(writer, pl);
                                writer.Close();
                            }
                            catch (IOException e)
                            {
                                Directory.CreateDirectory(starBasePath + pl.Name);
                                using (System.IO.FileStream str = File.Create(planetBasePath + pl.Name + ".xml"))
                                { }
                                TextWriter writer = new StreamWriter(planetBasePath + pl.Name + ".xml");
                                _planetSerializer.Serialize(writer, pl);
                                writer.Close();
                            }
                            foreach(Location lo in pl.MainLocations)
                            {
                                SaveLocation(planetBasePath, lo);
                            }
                        }
                    }
                }
            }
        }
        public static void LoadLocation(String path, ILocationContainer container)
        {
            String[] subPathsubs = path.Split("\\");
            if (String.Compare(subPathsubs[subPathsubs.Length - 1], "_people") == 0)
            {
                LoadPeople(path, (Location)container);
            }
            else if (String.Compare(subPathsubs[subPathsubs.Length - 1], "_usables") == 0)
            {
                LoadUsableObjects(path, (Location)container);
            }
            else
            {
                using (FileStream reader = File.Open(path + "\\" + subPathsubs[subPathsubs.Length - 1] + ".xml", FileMode.Open))
                {
                    Location loc = (Location)_locationSerializer.Deserialize(reader);
                    container.AddLocation(loc);
                    loc.MainDirectory = path;
                    String[] ssubpaths = Directory.GetDirectories(path);
                    if (ssubpaths.Length > 0)
                    {
                        foreach (String ssubpath in ssubpaths)
                        {
                            LoadLocation(ssubpath, loc);
                        }
                    }
                }
            }
        }
        public static void SaveLocation(String basePath, Location location)
        {
            String locationBasePath = basePath + location.Name + "\\";
            try
            {
                TextWriter writer = new StreamWriter(locationBasePath + location.Name + ".xml");
                _locationSerializer.Serialize(writer, location);
                writer.Close();
            }
            catch (IOException e)
            {
                Directory.CreateDirectory(basePath + location.Name);
                using (System.IO.FileStream str = File.Create(locationBasePath + location.Name + ".xml"))
                { }
                TextWriter writer = new StreamWriter(locationBasePath + location.Name + ".xml");
                _locationSerializer.Serialize(writer, location);
                writer.Close();
            }
            if(location.HasPeople())
            {
                SavePeople(locationBasePath, location.GetPeople());
            }
            if(location.Usables.Count > 0)
            {
                SaveUsableObjects(locationBasePath, location.Usables);
            }
            if (location.Sublocations.Count > 0)
            {
                foreach (Location sloc in location.Sublocations)
                {
                    SaveLocation(locationBasePath, sloc);
                }
            }
        }
        public static void LoadPeople(String path, Location location)
        {
            String[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                try
                {
                    using (FileStream streader = File.Open(file, FileMode.Open))
                    {
                        Person person = (Person)_personSerializer.Deserialize(streader);
                        person.Inventory?.RegisterAllItems();
                        location.AddPerson(person);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    using (FileStream streader = File.Open(file, FileMode.Open))
                    {
                        Universe.MainCharacter = (MainCharacter)_mainCharacterSerializer.Deserialize(streader);
                        Universe.MainCharacter.Inventory?.RegisterAllItems();
                        location.AddPerson(Universe.MainCharacter);
                    }
                }
            }
        }
        public static void SavePeople(String path, List<Person> people)
        {
            foreach(Person person in people)
            {
                if(person.PersonID ==0)
                {
                    try
                    {
                        TextWriter writer = new StreamWriter(path + "_people\\" + person.PersonID + ".xml");
                        _mainCharacterSerializer.Serialize(writer, Universe.MainCharacter);
                        writer.Close();
                    }
                    catch (IOException ex)
                    {
                        Directory.CreateDirectory(path + "_people");
                        using (System.IO.FileStream str = File.Create(path + "_people\\" + person.PersonID + ".xml"))
                        { }
                        TextWriter writer = new StreamWriter(path + "_people\\" + person.PersonID + ".xml");
                        _mainCharacterSerializer.Serialize(writer, Universe.MainCharacter);
                        writer.Close();
                    }
                }
                else
                {
                    try
                    {
                        TextWriter writer = new StreamWriter(path + "_people\\" + person.PersonID + ".xml");
                        _personSerializer.Serialize(writer, person);
                        writer.Close();
                    }
                    catch (IOException e)
                    {
                        Directory.CreateDirectory(path + "_people");
                        using (System.IO.FileStream str = File.Create(path + "_people\\" + person.PersonID + ".xml"))
                        { }
                        TextWriter writer = new StreamWriter(path + "_people\\" + person.PersonID + ".xml");
                        _personSerializer.Serialize(writer, person);
                        writer.Close();
                    }
                }
            }
        }
        public static void SaveUsableObjects(String path, List<UsableObject> usableObjects)
        {
            foreach (UsableObject obj in usableObjects)
            {
                _usableObjectSerializer = new XmlSerializer(obj.GetType());
                try
                {
                    TextWriter writer = new StreamWriter(path + "_usables\\" + obj.Name + "\\" + obj.Name + ".xml");
                    _usableObjectSerializer.Serialize(writer, obj);
                    writer.Close();
                }
                catch (IOException ex)
                {
                    Directory.CreateDirectory(path + "_usables\\" + obj.Name + "\\");
                    using (System.IO.FileStream str = File.Create(path + "_usables\\" + obj.Name + "\\" + obj.Name + ".xml"))
                    { }
                    TextWriter writer = new StreamWriter(path + "_usables\\" + obj.Name + "\\" + obj.Name + ".xml");
                    _usableObjectSerializer.Serialize(writer, obj);
                    writer.Close();
                }
            }
        }
        public static void LoadUsableObjects(String path, Location location)
        {
            String[] files = Directory.GetDirectories(path);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            foreach (String file in files)
            {
                String[] filename1 = file.Split('\\');
                String filename = filename1[filename1.Length - 1];
                TextReader reader = new StreamReader(file+"\\"+filename+".xml");
                reader.ReadLine();
                String suba = reader.ReadLine();
                String[] suba1 = suba.Split(" ");
                String suba2 = suba1[0].Substring(1);
                String a = "Supreme_Commander_Thorn." + suba2;
                Type ty = Type.GetType(a);
                _usableObjectSerializer = new XmlSerializer(ty);
                reader.Close();
                reader.Dispose();
                TextReader sReader = new StreamReader(file + "\\" + filename + ".xml");
                var obj = _usableObjectSerializer.Deserialize(sReader);
                sReader.Close();
                sReader.Dispose();
                ((UsableObject)obj).MainDirectory = file;
                ((UsableObject)obj).ObjectButton = new CustomBoundsButton(file+"\\Image_Day.png", ((UsableObject)obj).Pos, ((UsableObject)obj).Dims, ((UsableObject)obj).ShowOptions, null);
                location.Usables.Add((UsableObject)obj);
            }
        }
        public static ClimateController LoadClimateControllerForAPlanet(Planet planet)
        {
            try
            {
                TextReader writer = new StreamReader(planet.MainDirectory + "_Climate.xml");
                ClimateController controller = (ClimateController)_climateControllerSerializer.Deserialize(writer);
                writer.Close();
                return controller;
            }
            catch(IOException ex)
            {
                return null;
            }
        }
        public static void SaveClimateController(ClimateController controller)
        {
            try
            {
                TextWriter writer = new StreamWriter(controller.ControlledPlanet.MainDirectory + "_Climate.xml");
                _climateControllerSerializer.Serialize(writer, controller);
                writer.Close();
            }
            catch (IOException ex)
            {
                using (System.IO.FileStream str = File.Create(controller.ControlledPlanet.MainDirectory + "_Climate.xml"))
                { }
                TextWriter writer = new StreamWriter(controller.ControlledPlanet.MainDirectory + "_Climate.xml");
                _climateControllerSerializer.Serialize(writer, controller);
                writer.Close();
            }
        }
        public static void LoadDate()
        {
            try
            {
                using (FileStream reader = File.Open("Content\\Universe\\_Time.xml", FileMode.Open))
                {
                    Universe.GameDate = (DateTime)_dateSerializer.Deserialize(reader);
                }
            }
            catch(IOException e)
            {
                Universe.GameDate = new DateTime(3835, 1, 1);
            }
        }
        public static void SaveDate()
        {
            try
            {
                StreamWriter writer = new StreamWriter("Content\\Universe\\_Time.xml");
                _dateSerializer.Serialize(writer, Universe.GameDate);
            }
            catch(IOException e)
            {
                using (System.IO.FileStream str = File.Create("Content\\Universe\\_Time.xml"))
                { }
                StreamWriter writer = new StreamWriter("Content\\Universe\\_Time.xml");
                _dateSerializer.Serialize(writer, Universe.GameDate);
            }
        }
        public static void SaveRegisteredItems()
        {
            try
            {
                StreamWriter writer = new StreamWriter("Content\\Universe\\_RegisteredItems.xml");
                _itemSerializer.Serialize(writer, Universe.RegisteredItems);
            }
            catch (IOException e)
            {
                using (System.IO.FileStream str = File.Create("Content\\Universe\\_RegisteredItems.xml"))
                { }
                StreamWriter writer = new StreamWriter("Content\\Universe\\_RegisteredItems.xml");
                _itemSerializer.Serialize(writer, Universe.RegisteredItems);

            }
        }
        public static void LoadRegisteredItems()
        {
            try
            {
                var xmlWriterSettings = new XmlReaderSettings();
                using (XmlReader reader = XmlReader.Create("Content\\Universe\\_RegisteredItems.xml", xmlWriterSettings))
                {
                    Universe.RegisteredItems = (List<Item>)_itemSerializer.Deserialize(reader);
                }

            }
            catch (IOException e)
            {
            }
        }
        #endregion
    }
}
