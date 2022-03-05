using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Linq;
using System;

public class PokeApiService{
    private readonly HttpClient httpClient;
    public PokeApiService(){
        httpClient = new HttpClient();
    }

    public async Task<PokemonEndpointResponse> GetAllPokemon(){
        var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon-species");
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<PokemonEndpointResponse>(responseContent);
    }
}