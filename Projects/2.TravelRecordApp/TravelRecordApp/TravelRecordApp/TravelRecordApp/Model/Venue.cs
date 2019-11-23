using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    public class LabeledLatLng
    {
        public string label { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public IList<LabeledLatLng> labeledLatLngs { get; set; }
        public int distance { get; set; }
        public string postalCode { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string crossStreet { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public bool primary { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }
        public string referralId { get; set; }
        public bool hasPerk { get; set; }

        public static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();

            var url = GenerateUrl(latitude, longitude);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var deserializedJsonResponse = JsonConvert.DeserializeObject<FoursquareDto>(json);
                venues = deserializedJsonResponse.response.venues.ToList();
            }

            return venues;
        }

        private static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID,
                Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
    }

    public class FoursquareDto
    {
        public Response response { get; set; }
    }
}