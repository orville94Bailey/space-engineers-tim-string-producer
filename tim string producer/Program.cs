using System;
using System.Collections.Generic;
using System.Linq;
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

            var drillingRig = JObject.Parse(@"{'piston':10,'drill':10,'battery':2,'programmableBlock':2,'largeContainer':2, 'largeConveyor':40, 'rotor':1, 'lightArmor':20}");

            var partsDictionary = new Dictionary<string, int>();

            foreach (var block in drillingRig)
            {
                foreach (var component in BlockToComponentDict[block.Key].Children<JProperty>())
                {
                    //Console.WriteLine(component.Name + " " + component.Value);
                    UpsertIntoDictionary(partsDictionary, component.Name, component.Value, block.Value);
                }
            }

            foreach (var item in partsDictionary)
            {
                Console.Write(item.Key + ":" + item.Value+" ");
            }

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

        static void RegisterParts(Dictionary<string,JObject> dict)
        {
            dict.Add("piston", JObject.Parse(@"{'Computer':2,'Motor':4,'LargeTube':12,'Construction':10,'SteelPlate':25}"));
            dict.Add("drill", JObject.Parse(@"{'Computer':5,'Motor':5,'LargeTube':12,'Construction':40,'SteelPlate':300}"));
            dict.Add("battery", JObject.Parse(@"{'Computer':25,'PowerCell':80,'Construction':30,'SteelPlate':80}"));
            dict.Add("programmableBlock", JObject.Parse(@"{'Computer':2,'Display':1,'Motor':1,'LargeTube':2,'Construction':4,'SteelPlate':21}"));
            dict.Add("largeContainer", JObject.Parse(@"{'Computer':8,'Display':1,'Motor':20,'SmallTube':60, 'MetalGrid':24,'Construction':80,'InteriorPlate':360}"));
            dict.Add("largeConveyor", JObject.Parse(@"{'Motor':6,'SmallTube':12,'Construction':20,'InteriorPlate':14}"));
            dict.Add("rotor", JObject.Parse(@"{'Computer':2,'Motor':4,'LargeTube':4,'Construction':10,'SteelPlate':15}"));
            dict.Add("lightArmor", JObject.Parse(@"{'SteelPlate':24}"));
        }
    }
}
