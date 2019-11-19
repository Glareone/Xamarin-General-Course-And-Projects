using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();

            var url = GenerateURL(latitude, longitude);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var deserializedJsonResponse = JsonConvert.DeserializeObject<FoursquareDto>(json);
                venues = deserializedJsonResponse.response.venues.ToList();
            }

            return venues;
        }

        private static string GenerateURL(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID,
                Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}