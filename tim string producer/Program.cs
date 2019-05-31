using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace tim_string_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var BlockToComponentDict = new Dictionary<string, JObject>();
            RegisterParts(BlockToComponentDict);

            var drillingRig = JObject.Parse(@"{'piston':10,'drill':10,'battery':2,'programmableBlock':2,'largeContainer':2, 'largeConveyor':40, 'rotor':1, 'lightArmor':20, 'connector':7, 'largeSorter':6}");

            var adhoc = JObject.Parse(@"{
                                            'piston':0,
                                            'drill':0,
                                            'battery':0,
                                            'programmableBlock':0,
                                            'largeContainer':0,
                                            'largeConveyor':0,
                                            'rotor':0,
                                            'lightArmor':0,
                                            'connector':0,
                                            'largeSorter':0,
                                            'controlStation':0,
                                            'lgSmallReactor':0,
                                            'lgLargeIonThruster':0,
                                            'lgSmallIonThruster':0,
                                            'lgGyro':0,
                                            'lgLandingGear':0,
                                            'lgOreDetector':1
                                        }");









            var partsDictionary = new Dictionary<string, int>();

            foreach (var block in adhoc)
            {
                foreach (var component in BlockToComponentDict[block.Key].Children<JProperty>())
                {
                    //Console.WriteLine(component.Name + " " + component.Value);
                    UpsertIntoDictionary(partsDictionary, component.Name, component.Value, block.Value);
                }
            }
            Console.Write(GenerateTimString(partsDictionary));

            Console.ReadLine();
        }

        static void UpsertIntoDictionary(Dictionary<string, int> dict, string key, JToken value, JToken multiplier)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] += (value.Value<int>() * multiplier.Value<int>());
            }
            else
            {
                dict.Add(key, (value.Value<int>() * multiplier.Value<int>()));
            }
        }

        static void RegisterParts(Dictionary<string, JObject> dict)
        {
            dict.Add("piston", JObject.Parse(@"{'Computer':2,'Motor':4,'LargeTube':12,'Construction':10,'SteelPlate':25}"));
            dict.Add("drill", JObject.Parse(@"{'Computer':5,'Motor':5,'LargeTube':12,'Construction':40,'SteelPlate':300}"));
            dict.Add("battery", JObject.Parse(@"{'Computer':25,'PowerCell':80,'Construction':30,'SteelPlate':80}"));
            dict.Add("programmableBlock", JObject.Parse(@"{'Computer':2,'Display':1,'Motor':1,'LargeTube':2,'Construction':4,'SteelPlate':21}"));
            dict.Add("largeContainer", JObject.Parse(@"{'Computer':8,'Display':1,'Motor':20,'SmallTube':60, 'MetalGrid':24,'Construction':80,'InteriorPlate':360}"));
            dict.Add("largeConveyor", JObject.Parse(@"{'Motor':6,'SmallTube':12,'Construction':20,'InteriorPlate':14}"));
            dict.Add("rotor", JObject.Parse(@"{'Computer':2,'Motor':4,'LargeTube':4,'Construction':10,'SteelPlate':15}"));
            dict.Add("lightArmor", JObject.Parse(@"{'SteelPlate':24}"));
            dict.Add("connector", JObject.Parse(@"{'Computer':20,'Motor':8,'SmallTube':12,'Construction':40,'SteelPlate':150}"));
            dict.Add("largeSorter", JObject.Parse(@"{'Motor':2,'Computer':20,'SmallTube':50,'Construction':120,'InteriorPlate':50}"));
            dict.Add("controlStation", JObject.Parse(@"{'Motor':2,'Computer':100,'Display':10,'Construction':20,'InteriorPlate':20}"));
            dict.Add("lgLargeIonThruster", JObject.Parse(@"{'SteelPlate':150,'LargeTube':40,'Thrust':960,'Construction':100}"));
            dict.Add("lgSmallIonThruster", JObject.Parse(@"{'SteelPlate':25,'LargeTube':8,'Thrust':80,'Construction':60}"));
            dict.Add("lgSmallReactor", JObject.Parse(@"{'SteelPlate':80,'LargeTube':8,'MetalGrid':4,'Construction':40, 'Reactor':100, 'Motor':6, 'Computer':25}"));
            dict.Add("lgGyro", JObject.Parse(@"{'SteelPlate':600,'LargeTube':4,'MetalGrid':50,'Construction':40, 'Motor':4, 'Computer':5}"));
            dict.Add("lgLandingGear", JObject.Parse(@"{'SteelPlate':150, 'Construction':20, 'Motor':6}"));
            dict.Add("lgOreDetector", JObject.Parse(@"{'SteelPlate':50, 'Construction':40, 'Motor':5, 'Computer':25, 'Detector':20}"));

        }

        static string GenerateTimString(Dictionary<string, int> dict)
        {
            var sb = new StringBuilder();

            foreach (var item in dict)
            {
                if (item.Value != 0)
                {
                    sb.Append(item.Key + ":" + item.Value + " ");
                }
            }

            return sb.ToString();
        }
    }
}
