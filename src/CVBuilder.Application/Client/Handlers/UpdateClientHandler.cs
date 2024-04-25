using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Client.Handlers;
using Models.Entities;
internal class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User, int> _userRepository;

    public UpdateClientHandler(
        IMapper mapper,
        IRepository<User, int> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ClientResponse> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _userRepository.GetByIdAsync(command.Id);

        if (client == null) throw new NotFoundException("No client in db");

        client.IdentityUser.FirstName = command.FirstName;
        client.IdentityUser.LastName = command.LastName;
        client.IdentityUser.PhoneNumber = command.PhoneNumber;
        client.Site = command.Site;
        client.Contacts = command.Contacts;
        client.CompanyName = command.CompanyName;
        client.UpdatedAt = DateTime.Now;

        client = await _userRepository.UpdateAsync(client);

        return _mapper.Map<ClientResponse>(client);
    }
}