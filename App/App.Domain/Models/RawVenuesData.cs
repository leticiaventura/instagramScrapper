using System;
using Newtonsoft.Json;

namespace App.Domain.Models
{
    public partial class RawVanuesData
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("venues")]
        public RawVenue[] Venues { get; set; }
    }

    public partial class RawVenue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public RawLocation Location { get; set; }

        [JsonProperty("categories")]
        public RawCategory[] Categories { get; set; }

        [JsonProperty("referralId")]
        public string ReferralId { get; set; }

        [JsonProperty("hasPerk")]
        public bool HasPerk { get; set; }
    }

    public partial class RawCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pluralName")]
        public string PluralName { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }
    }

    public partial class Icon
    {
        [JsonProperty("prefix")]
        public Uri Prefix { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }

    public partial class RawLocation
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }

        [JsonProperty("labeledLatLngs")]
        public LabeledLatLng[] LabeledLatLngs { get; set; }

        [JsonProperty("distance")]
        public long Distance { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("formattedAddress")]
        public string[] FormattedAddress { get; set; }

        [JsonProperty("crossStreet")]
        public string CrossStreet { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
    }

    public partial class LabeledLatLng
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}

