using System.Collections.Generic;
using Newtonsoft.Json;

public class PokemonInformationResponse{
    public IEnumerable<PokemonNameResponse> Names { get; set;}
    public PokemonUrlResponse Habitat { get; set; }
    [JsonProperty("is_legendary")]
    public bool IsLegendary { get; set; }
    [JsonProperty("flavor_text_entries")]
    public IEnumerable<FlavorTextEntryResponse> FlavorTextEntries { get; set; }
}