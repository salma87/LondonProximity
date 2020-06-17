using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace LondonProximity.API
{
    public class ProximityCalculator : IProximityCalculator
    {
        private readonly HttpClient client = new HttpClient();
        private string latitude = "";
        private string longitude = "";
        private double lon = 0.0;
        private double lat = 0.0;
        private bool isClose = false;
        private string apiLoc = "https://bpdts-test-app.herokuapp.com/users";
        List<LondonJobApplicant> proxList = new List<LondonJobApplicant>();


        public async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "London Proximity");

            var streamTask = client.GetStreamAsync(apiLoc);
            var repositories = await JsonSerializer.DeserializeAsync<List<User>>(await streamTask);

            foreach (var repo in repositories)
            {
                latitude = repo.Latitude.ToString();
                longitude = repo.Longitude.ToString();
                lat = Convert.ToDouble(latitude);
                lon = Convert.ToDouble(longitude);

                isClose = CalcCoords(lat, lon);

                if (isClose)
                    proxList.Add(new LondonJobApplicant { ID = repo.ID, FirstName = repo.FirstName, LastName = repo.LastName, EmailAddress = repo.EmailAddress });
            }
        }

        public bool CalcCoords(double lati, double longi)
        {
            double londonLat = 51 + (30 / 60.0) + (26 / 60.0 / 60.0);
            double londonLon = 0 - (7 / 60.0) - (39 / 60.0 / 60.0);

            GeoCoordinate sCoord = new GeoCoordinate(londonLat, londonLon);
            GeoCoordinate eCoord = new GeoCoordinate(lati, longi);

            double disInMilesLon = sCoord.GetDistanceTo(eCoord) * 0.000621371;

            if (disInMilesLon <= 50)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LondonJobApplicant>> AllApplicantsAsync()
        {
            await ProcessRepositories();
            return proxList;
        }
    }
 }
