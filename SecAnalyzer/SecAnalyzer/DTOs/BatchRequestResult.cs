using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class BatchRequestResult
    {
        [DeserializeAs(Name = "symbol")]
        public string Symbol { get; set; }
    }
}
