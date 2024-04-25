using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Data.Queries;
using CVBuilder.Application.Data.Responses;
using CVBuilder.EFContext.Extensions;
using MediatR;

namespace CVBuilder.Application.Data.Handlers;

public class GetDataHandler : IRequestHandler<GetDataTypesQuery, IEnumerable<DataTypeResult>>
{
    public async Task<IEnumerable<DataTypeResult>> Handle(GetDataTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(
            from Enum item in Enum.GetValues(request.EnumType)
            select new DataTypeResult
            {
                Id = Convert.ToInt32(item),
                Name = item.ToString(),
                Description = item.ToDescription()
            });
    }
}