using System.Threading.Tasks;
using System.Collections.Generic;

public interface ITranslationService{
    Task<IEnumerable<Pokemon>> TranslateAllPokemon(IEnumerable<Pokemon> pokemons);
}