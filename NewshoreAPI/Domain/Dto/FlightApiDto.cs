using Newtonsoft.Json;

namespace Domain.Dto
{
    public class FlightApiDto
    {
        [JsonProperty("departureStation")]
        public string DepartureStation { get; set; }

        [JsonProperty("arrivalStation")]
        public string ArrivalStation { get; set; }

        [JsonProperty("flightCarrier")]
        public string FlightCarrier { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
