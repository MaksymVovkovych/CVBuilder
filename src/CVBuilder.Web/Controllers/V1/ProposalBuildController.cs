using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.ProposalBuild;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class ProposalBuildController : BaseAuthApiController
{
    /// <summary>
    /// Create a new ProposalBuild
    /// </summary>
    [HttpPost(ApiRoutes.ProposalBuild.CreateProposalBuild)]
    public async Task<ActionResult<ProposalBuildResult>> CreateProposalBuild(CreateProposalBuildRequest request)
    {
        var command = Mapper.Map<CreateProposalBuildCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing ProposalBuild
    /// </summary>
    [HttpPut(ApiRoutes.ProposalBuild.UpdateProposalBuild)]
    public async Task<ActionResult<ProposalBuildResult>> UpdateProposalBuild(UpdateProposalBuildRequest request)
    {
        var command = Mapper.Map<UpdateProposalBuildCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get list of ProposalBuild
    /// </summary>
    [HttpGet(ApiRoutes.ProposalBuild.GetAllProposalBuilds)]
    public async Task<ActionResult<List<ProposalBuildResult>>> GetAllProposalBuilds()
    {
        var command = new GetAllProposalBuildsQuery();
        var result = await Mediator.Send(command);
        return result;
    }
}