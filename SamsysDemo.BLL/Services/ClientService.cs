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
                    PhoneNumber = client.PhoneNumber,
                    Birthday = client.Birthday ,
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

                client.Update(clientToUpdate.Name, clientToUpdate.PhoneNumber, clientToUpdate.Birthday);
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

        public async Task<MessagingHelper<ListClientPagedDTO>> GetClientsByPage(int pageNumber, int pageSize, string searchTerm)
        {
            MessagingHelper<ListClientPagedDTO> response = new();
            try
            {
                var (clients, totalCount) = await _unitOfWork.ClientRepository.GetAllByPage(pageNumber,pageSize, searchTerm);
                
                    if (clients == null || clients.Count == 0)
                {
                    response.SetMessage("Não existem clientes.");
                    response.Success = false;
                    return response;
                }

                IList<ClientDTO> clientDTOs = new List<ClientDTO>();
                foreach (var client in clients)
                {
                    var clientDTO = new ClientDTO
                    {
                        
                        Id = client.Id,
                        Name = client.Name,
                        IsActive=client.IsActive,
                        PhoneNumber=client.PhoneNumber,
                        Birthday=client.Birthday,
                        
                    };
                    clientDTOs.Add(clientDTO);
                }

                ListClientPagedDTO listClientsDTO = new ListClientPagedDTO
                {
                    clients = clientDTOs,
                    totalPages = totalCount
                };
                    
                    

                response.Obj = listClientsDTO;
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

        public async Task<MessagingHelper> CreateClient(NewClientDTO newClientDTO)
        {
            MessagingHelper<Client> response = new();
            try
            {
                Client client = new()
                {
                    IsActive = true,
                    PhoneNumber = newClientDTO.PhoneNumber,
                    IsRemoved = false,
                    Name = newClientDTO.Name,
                    Birthday = newClientDTO.Birthday
                };

                await _unitOfWork.ClientRepository.Insert(client);
                await _unitOfWork.SaveAsync();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.SetMessage($"Ocorreu um erro inesperado ao criar o cliente. Tente novamente.");
                return response;
            }

        }
    }
}
