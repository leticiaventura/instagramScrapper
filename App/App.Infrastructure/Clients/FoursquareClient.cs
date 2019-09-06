using App.Domain.Models;
using App.Infrastructure.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;

namespace App.Infrastructure.Clients
{
    public static class FoursquareClient
    {
        public static RawVanuesData GetVenues(string latLon, bool insertIntoResponseTable = false)
        {
            Uri uri = ArrangeURI(latLon);

            string objResponse;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        objResponse = reader.ReadToEnd();
                    }
                }
            }
            if (insertIntoResponseTable)
                InsertResponseInDatabase(objResponse);

            return JsonConvert.DeserializeObject<RawVanuesData>(objResponse);
        }

        private static Uri ArrangeURI(string latLon)
        {
            Uri prefix = new Uri(ConfigurationManager.AppSettings.Get("FoursquareAPI"));
            UriTemplate getVenuesTamplate = new UriTemplate("venues/search?client_id={clientId}&client_secret={clientSecret}&v={version}&ll={latLon}&intent={intent}&radius={radius}&limit={limit}");
            string intent = "browse";
            string radius = "500";
            string limit = "5000";

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("clientId", ConfigurationManager.AppSettings.Get("FoursquareClientId"));
            parameters.Add("clientSecret", ConfigurationManager.AppSettings.Get("FoursquareClientSecret"));
            parameters.Add("version", ConfigurationManager.AppSettings.Get("FoursquareVersion"));
            parameters.Add("latLon", latLon);
            parameters.Add("intent", intent);
            parameters.Add("radius", radius);
            parameters.Add("limit", limit);

            Uri uri = getVenuesTamplate.BindByName(prefix, parameters);
            return uri;
        }

        private static void InsertResponseInDatabase(string response)
        {
            const string sqlInsert = @"INSERT INTO TBFoursquare_Response (response) VALUES (@Response)";
            Db.Insert(sqlInsert, new object[] { "@response", response});
        }
    }
}
