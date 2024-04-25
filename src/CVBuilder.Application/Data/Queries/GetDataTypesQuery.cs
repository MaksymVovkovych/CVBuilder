using System;
using System.Collections.Generic;
using CVBuilder.Application.Data.Responses;
using MediatR;

namespace CVBuilder.Application.Data.Queries;

public class GetDataTypesQuery : IRequest<IEnumerable<DataTypeResult>>
{
    public Type EnumType { get; set; }
}