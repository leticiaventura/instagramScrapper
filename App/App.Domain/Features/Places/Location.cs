using App.Domain.Features.Base;
using App.Domain.Models;
using System;
using System.Globalization;

namespace App.Domain.Features.Places
{
    public class Location : Entity
    {
        public string Crosstreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int VenueId { get; set; }

        public Location()
        {

        }

        public Location(RawLocation rawLocation)
        {
            Crosstreet = rawLocation.CrossStreet;
            City = rawLocation.City;
            State = rawLocation.State;
            Postalcode = rawLocation.PostalCode;
            Country = rawLocation.Country;
            Lat = rawLocation.Lat;
            Lng = rawLocation.Lng;
        }

        public double CalculateDistance (Location l2)
        {
            var l1 = this;

            //Haversine formula
            const double EarthRadius = 6371000; //metros
            double distance = 0;
            double lat1 = double.Parse(l1.Lat, CultureInfo.InvariantCulture);
            double lat2 = double.Parse(l2.Lat, CultureInfo.InvariantCulture);
            double lon1 = double.Parse(l1.Lng, CultureInfo.InvariantCulture);
            double lon2 = double.Parse(l2.Lng, CultureInfo.InvariantCulture);

            double radianLat1 = ToRadians(lat1);
            double radianLat2 = ToRadians(lat2);

            double dLat = radianLat1 - radianLat2;
            double dLon = ToRadians(lon1 - lon2);


            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                    + Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                    * Math.Cos(radianLat1) * Math.Cos(radianLat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));

            distance = EarthRadius * c;
            return distance;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
