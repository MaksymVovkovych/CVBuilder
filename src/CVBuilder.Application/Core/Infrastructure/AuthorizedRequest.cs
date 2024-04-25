using System.Collections.Generic;
using MediatR;

namespace CVBuilder.Application.Core.Infrastructure;

public abstract class AuthorizedRequest<T> : IRequest<T>
{
    public int UserId { get; set; }
    public IEnumerable<string> UserRoles { get; set; }
}
