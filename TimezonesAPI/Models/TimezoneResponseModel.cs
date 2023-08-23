using System;

namespace TimezonesAPI.Models
{
    public class TimezoneResponseModel
    {
        public string? id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? offset { get; set; }
        public bool? isDTS { get; set; }
        public string[] utc { get; set; }
        public int? order { get; set; }
        public string createdByUserId { get; set; }
        public string lastModifiedByUserId { get; set; }
        public string lastModifiedOnDate { get; set; }
        public string createdOnDate { get; set; }
    }
}
