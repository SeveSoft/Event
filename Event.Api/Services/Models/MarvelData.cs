using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Event.Api.Services.Models
{
    public class Comics
    {
        [JsonProperty("Available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionUrl { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }

    public class Data
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public class Events
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionUrl { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }

    public class Item
    {
        [JsonProperty("resourceURI")]
        public string ResourceUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUrl { get; set; }

        [JsonProperty("comics")]
        public Comics Comics { get; set; }

        [JsonProperty("series")]
        public Series Series { get; set; }

        [JsonProperty("stories")]
        public Stories Stories { get; set; }

        [JsonProperty("events")]
        public Events Events { get; set; }

        [JsonProperty("urls")]
        public List<Url> Urls { get; set; }

    }

    public class MarvelData
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("attributionText")]
        public string AttributionText { get; set; }

        [JsonProperty("attributionHTML")]
        public string AttributionHtml { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Series
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionUrl { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }

    public class Stories
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionUrl { get; set; }
        
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("extension")]
        public string Extension { get; set; }
    }

    public class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string UrlValue { get; set; }
    }
}