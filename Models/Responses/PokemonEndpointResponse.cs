using System.Collections.Generic;

public class PokemonEndpointResponse{
    public IEnumerable<PokemonUrlResponse> results {get; set;}

    public PokemonEndpointResponse(){
    }
}