using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        private readonly ILogger<PokedexController> _logger;

        public PokedexController(ILogger<PokedexController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PokemonEndpointResponse> Get()
        {
            var service = new PokeApiService();

            return await service.GetAllPokemon();
        }
    }
}
