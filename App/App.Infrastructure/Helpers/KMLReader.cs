using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Infrastructure.Helpers
{
    public static class KMLReader
    {
        public static IList<String> GetCoordiates()
        {
            List<string> coordinates = new List<string>();
            string path = ConfigurationManager.AppSettings.Get("KMLFile");
            XmlDocument file = new XmlDocument();
            file.Load(path);

            XmlNode document = file.DocumentElement.FirstChild;
            foreach (XmlNode placemark in document.ChildNodes)
            {
                string lonLat = placemark.ChildNodes[1].InnerText;
                var splitLonLat = lonLat.Split(',');
                string latLon = splitLonLat[1] + ", " + splitLonLat[0];
                coordinates.Add(latLon);
            }
            return coordinates;
        }
    }
}
