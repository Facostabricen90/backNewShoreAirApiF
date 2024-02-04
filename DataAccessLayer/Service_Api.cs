using BusinessLayer.Models;
using BusinessLayer.Models.GateWays;
using DataAccessLayer.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;

namespace DataAccessLayer
{
    public class Service_Api : IFlights
    {
        private static string? _baseUrl;
        static HttpClient client = new HttpClient();

        public Service_Api()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<Flight>> GetFlights(int id)
        {
            List<Flight> listFlight = new List<Flight>();


            //Make an asynchronous HTTP GET request to the URL
            var responseApi = await client.GetAsync("api/flights/" + id);

            //Check if the request was successful. If the response status code indicates success
            if (responseApi.IsSuccessStatusCode)
            {
                //Reads the content of the response as a JSON string. This assumes that the response contains data in JSON format
                var json_response_FlightModel = await responseApi.Content.ReadAsStringAsync();
                //Deserialize JSON string into a list of Success Response Dto objects using Newtonsoft.Json
                var result_response = JsonConvert.DeserializeObject<List<SuccessResponseDto>>(json_response_FlightModel);
                foreach (var (valueData, transport) in from valueData in result_response
                                                       let transport = new Transport()
                                                       select (valueData, transport))
                {
                    transport.FlightCarrier = valueData.flightCarrier;
                    transport.FlightNumber = valueData.flightNumber;
                    Flight flight = new Flight(transport, valueData.departureStation, valueData.arrivalStation, valueData.price);
                    listFlight.Add(flight);
                }
            }

            return listFlight;
        }
    }
}
