using Newtonsoft.Json;

namespace VehicleTracking.Api.Extensions.ErrorHandling
{
    public class ErrorResponse
    {
        public string StatusCode { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
