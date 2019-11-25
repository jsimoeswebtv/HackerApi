using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HackerNewsApi.Models
{
    public class Story
    {
        #region Private Fields

        private int myVar;

        #endregion Private Fields

        #region Public Properties

        public List<int> kids { get; set; }

        public string by { set { postedBy = value; } }

        [JsonProperty(Order = 3)]
        public string postedBy { get; set; }

        public int descendants { get; set; }

        public int id { get; set; }

        [JsonProperty(Order = 5)]
        public int score { get; set; }

        [JsonConverter(typeof(MyDateTimeConverter))]
        [JsonProperty(PropertyName = "time", Order = 4)]
        public DateTime time { get; set; }

        [JsonProperty(Order = 1)]
        public string title { get; set; }

        public string type { get; set; }

        public string url { set { uri = value; } }

        [JsonProperty(Order = 2)]
        public string uri { get; set; }

        [JsonProperty(Order = 6)]
        public int commentCount
        {
            get { return kids.Count; }
        }

        #endregion Public Properties

        #region Public Methods

        public bool ShouldSerializekids()
        {
            // don't serialize the Manager property if an employee is their own manager
            return false;
        }

        public bool ShouldSerializedescendants()
        {
            // don't serialize the Manager property if an employee is their own manager
            return false;
        }

        public bool ShouldSerializeid()
        {
            // don't serialize the Manager property if an employee is their own manager
            return false;
        }

        public bool ShouldSerializetype()
        {
            // don't serialize the Manager property if an employee is their own manager
            return false;
        }

        #endregion Public Methods
    }

    public class MyDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = (Int64)reader.Value;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime dt = (DateTime)value;

            writer.WriteValue(dt.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss:zzz"));
        }

        #endregion Public Methods
    }
}