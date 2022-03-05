using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class YodaTranslationService{
    private readonly HttpClient httpClient;
    private static Dictionary<string, string> cachedTranslations;

    public YodaTranslationService(){
        httpClient = new HttpClient();
        cachedTranslations = new Dictionary<string, string>();
    }

    public async Task<IEnumerable<Pokemon>> TranslateAllPokemon(IEnumerable<Pokemon> pokemons){
        var translationsNeeded = pokemons.Select(p => p.Description);
        var successful = await UpdateCache(translationsNeeded);
        
        return successful ? UpdatePokemonDescriptions(pokemons.ToList()) : pokemons;
    }

    private async Task<bool> UpdateCache(IEnumerable<string> translationsNeeded){
        (var success, var translations) = await GetTranslations(translationsNeeded);
        if(!success){
            return false;;
        }

        for(int i = 0; i < translations.Count; i++){
            var originalText = translationsNeeded.ElementAt(i);
            var translation = translations[i];

            if(cachedTranslations.ContainsKey(originalText)){
                cachedTranslations[originalText] = translation.ToString();
                continue;
            }

            cachedTranslations.Add(originalText, (string)translation);
        }

        return true;
    }

    private async Task<(bool, JArray)> GetTranslations(IEnumerable<string> translationsNeeded){
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Post;
        requestMessage.RequestUri = new System.Uri("https://api.funtranslations.com/translate/yoda");
        requestMessage.Content = JsonContent.Create(new { Text = translationsNeeded });

        var response = await httpClient.SendAsync( requestMessage);

        if(response.StatusCode != System.Net.HttpStatusCode.OK){
            return (false, null);
        }


        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

        return (true, jsonResponse.contents.translated);
    }

    private List<Pokemon> UpdatePokemonDescriptions(List<Pokemon> pokemons){
        foreach(var pokemon in pokemons){
            pokemon.Description = cachedTranslations[pokemon.Description];
        }

        return pokemons;
    }
}