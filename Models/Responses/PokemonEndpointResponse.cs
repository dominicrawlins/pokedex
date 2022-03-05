using System.Collections.Generic;
using Newtonsoft.Json;

public class PokemonEndpointResponse{
    public IEnumerable<PokemonUrlResponse> Results {get; set;}

    public PokemonEndpointResponse(){
    }
}