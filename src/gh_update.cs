﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using gh_update;
//
//    var updateJson = UpdateJson.FromJson(jsonString);

namespace gh_update
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UpdateJson
    {
        [JsonProperty("url_win64")]
        public Uri UrlWin64 { get; set; }

        [JsonProperty("url_win86")]
        public Uri UrlWin86 { get; set; }

        [JsonProperty("url_winarm")]
        public Uri UrlWinarm { get; set; }

        [JsonProperty("url_linux")]
        public Uri UrlLinux { get; set; }

        [JsonProperty("url_linuxarm")]
        public Uri UrlLinuxarm { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public partial class UpdateJson
    {
        public static UpdateJson[] FromJson(string json)
        {
            return JsonConvert.DeserializeObject<UpdateJson[]>(json, gh_update.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this UpdateJson[] self)
        {
            return JsonConvert.SerializeObject(self, gh_update.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
