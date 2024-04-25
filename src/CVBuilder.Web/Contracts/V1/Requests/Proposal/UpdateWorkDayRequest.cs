using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Responses;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class UpdateWorkDayRequest
{
    public UpdateWorkDay WorkDay { get; set; }
}

