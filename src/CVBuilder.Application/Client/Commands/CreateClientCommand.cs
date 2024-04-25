﻿using CVBuilder.Application.Client.Responses;
using MediatR;

namespace CVBuilder.Application.Client.Commands;

public class CreateClientCommand : IRequest<ClientResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Site { get; set; }
    public string Contacts { get; set; }
    public string CompanyName { get; set; }
}