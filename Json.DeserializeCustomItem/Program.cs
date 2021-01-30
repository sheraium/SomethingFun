using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Config.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json.DeserializeCustomItem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var settings = new ConfigurationBuilder<IMySettings>()
                .UseJsonFile("test.json")
                .Build();

            settings.Id = "TEST";

            var jobs = new List<IJob>
            {
                new Move(){Id = "1", Dest = "100"},
                new Transfer(){Id = "2", Dest = "200"},
            };

            var enumerable = jobs.Where(x => x.Id == "1");

            using (var file = File.CreateText("jobs.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, jobs);
            }

            using (var file = File.OpenText("jobs.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new JobItemConverter());
                var fileJobs = (List<IJob>)serializer.Deserialize(file, typeof(List<IJob>));
            }
        }
    }

    public interface IMySettings
    {
        string Id { get; set; }

        IAxisSpeed[] Speeds { get; set; }
    }

    public interface IAxisSpeed
    {
        public int Travel { get; set; }
        public int Lift { get; set; }
    }

    public interface IJob
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }

    public class Move : IJob
    {
        public string Id { get; set; }
        public string Type { get; set; } = "MOVE";
        public string Dest { get; set; }
    }

    public class Transfer : IJob
    {
        public string Id { get; set; }
        public string Type { get; set; } = "TRANSFER";
        public string Source { get; set; }
        public string Dest { get; set; }
    }

    internal class JobItemConverter : Newtonsoft.Json.Converters.CustomCreationConverter<IJob>
    {
        public override IJob Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public IJob Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("Type");

            switch (type)
            {
                case "MOVE":
                    return new Move();

                case "TRANSFER":
                    return new Transfer();
            }

            throw new ApplicationException($"The animal type {type} is not supported!");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}