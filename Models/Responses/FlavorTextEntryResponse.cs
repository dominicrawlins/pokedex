using Newtonsoft.Json;

public class FlavorTextEntryResponse{
    [JsonProperty("flavor_text")]
    public string FlavorText { get; set; }
    public PokemonUrlResponse Language { get; set; }
}