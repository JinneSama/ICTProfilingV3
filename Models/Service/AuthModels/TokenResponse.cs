namespace Models.Service.AuthModels
{
    public class TokenResponse
    {
        [Newtonsoft.Json.JsonProperty("token")]
        public string Token { get; set; }
    }
}
