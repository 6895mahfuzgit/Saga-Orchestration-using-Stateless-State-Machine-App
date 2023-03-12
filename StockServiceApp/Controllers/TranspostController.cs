using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportServiceApp.Core.Models;
using TransportServiceApp.Core.Repositories;

namespace TransportServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranspostController : ControllerBase
    {

        private readonly ITranspostRepository _transpostRepository;

        public TranspostController(ITranspostRepository transpostRepository)
        {
            _transpostRepository = transpostRepository;
        }

        [HttpPost("add")]
        public async Task<Transpost> Add(Transpost transpost)
        {
            return await _transpostRepository.Add(transpost);
        }


        [HttpPost("remove")]
        public async Task<Transpost> Remove(Transpost transpost)
        {
            return await _transpostRepository.Remove(transpost);
        }

    }
}
