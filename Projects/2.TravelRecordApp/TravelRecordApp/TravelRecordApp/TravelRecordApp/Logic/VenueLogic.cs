using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TravelRecordApp.Model;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();

            var url = Venue.GenerateURL(latitude, longitude);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
            }

            return venues;
        }
    }
}
