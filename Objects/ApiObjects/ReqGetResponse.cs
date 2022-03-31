using Newtonsoft.Json;
namespace Objects
{
    public class ReqGetResponse<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? page { get; set;}
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? per_page { get; set;}
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? total { get; set;}
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? total_pages { get; set;}
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T data{get; set;}
        public Support support{get; set;}
        
    }
}
