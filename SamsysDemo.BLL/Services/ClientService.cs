using Microsoft.EntityFrameworkCore;
using SamsysDemo.Infrastructure.Entities;
using SamsysDemo.Infrastructure.Helpers;
using SamsysDemo.Infrastructure.Interfaces.Repositories;
using SamsysDemo.Infrastructure.Models.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.BLL.Services
{


    public class ClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<MessagingHelper<ClientDTO>> Get(long id)
        {
            MessagingHelper<ClientDTO> response = new();
            try
            {
                Client? client = await _unitOfWork.ClientRepository.GetById(id);
                if (client is null)
                {
                    response.SetMessage($"O cliente não existe. | Id: {id}");
                    response.Success = false;
                    return response;
                }
                response.Obj = new ClientDTO
                {
                    Id = client.Id,
                    IsActive = client.IsActive,
                    ConcurrencyToken = Convert.ToBase64String(client.ConcurrencyToken),
                    Name = client.Name,
                    PhoneNumber = client.PhoneNumber
                };
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.SetMessage($"Ocorreu um erro inesperado ao obter o cliente.");
                return response;
            }
        }

        public async Task<MessagingHelper> Update(long id, UpdateClientDTO clientToUpdate)
        {
            MessagingHelper<Client> response = new();
            try
            {
                Client? client = await _unitOfWork.ClientRepository.GetById(id);
                if (client is null)
                {
                    response.SetMessage($"O cliente não existe. | Id: {id}");
                    response.Success = false;
                    return response;
                }
                client.Update(clientToUpdate.Name, clientToUpdate.PhoneNumber);
                _unitOfWork.ClientRepository.Update(client, clientToUpdate.ConcurrencyToken);
                await _unitOfWork.SaveAsync();
                response.Success = true;
                response.Obj = client;
                return response;
            }
            catch (DbUpdateConcurrencyException exce)
            {
                response.Success = false;
                response.SetMessage($"Os dados do cliente foram atualizados posteriormente por outro utilizador!.");
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.SetMessage($"Ocorreu um erro inesperado ao atualizar o cliente. Tente novamente.");
                return response;
            }
        }

        public async Task<MessagingHelper> DisableClient(long id)
        {
            MessagingHelper<Client> response = new();
            try
            {
                Client? client = await _unitOfWork.ClientRepository.GetById(id);
                if (client is null)
                {
                    response.SetMessage($"O cliente não existe. | Id: {id}");
                    response.Success = false;
                    return response;
                }
                client.SetStatus(false);
                _unitOfWork.ClientRepository.Update(client, Convert.ToBase64String(client.ConcurrencyToken));
                await _unitOfWork.SaveAsync();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.SetMessage($"Ocorreu um erro inativar o cliente.");
                return response;
            }
        }

        public async Task<MessagingHelper> EnableClient(long id)
        {
            MessagingHelper<Client> response = new();
            try
            {
                Client? client = await _unitOfWork.ClientRepository.GetById(id);
                if (client is null)
                {
                    response.SetMessage($"O cliente não existe. | Id: {id}");
                    response.Success = false;
                    return response;
                }
                client.SetStatus(true);
                _unitOfWork.ClientRepository.Update(client, Convert.ToBase64String(client.ConcurrencyToken));
                await _unitOfWork.SaveAsync();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.SetMessage($"Ocorreu um erro ativar o cliente.");
                return response;
            }
        }
    }
}
