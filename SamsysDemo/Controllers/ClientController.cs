using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamsysDemo.BLL.Services;
using SamsysDemo.Infrastructure.Helpers;
using SamsysDemo.Infrastructure.Models.Client;

namespace SamsysDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet("{id}")]
        public async Task<MessagingHelper<ClientDTO>> Get(long id)
        {
            return await _clientService.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<MessagingHelper> Update(int id, UpdateClientDTO clientToUpdateDTO)
        {
            return await _clientService.Update(id, clientToUpdateDTO);
        }

        [HttpPost("{id}/[action]")]
        public async Task<MessagingHelper> Enable(long id)
        {
            return await _clientService.EnableClient(id);
        }

        [HttpPost("{id}/[action]")]
        public async Task<MessagingHelper> Disable(long id)
        {
            return await _clientService.DisableClient(id);
        }
    }
}
