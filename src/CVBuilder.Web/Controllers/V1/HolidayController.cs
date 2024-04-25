using System.Threading.Tasks;
using CVBuilder.Application.Holidays.Commands;
using CVBuilder.Application.Holidays.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Holiday;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class HolidayController : BaseAuthApiController
{
    /// <summary>
    /// Create Holiday
    /// </summary>
    [HttpPost(ApiRoutes.Holiday.CreateHoliday)]
    public async Task<IActionResult> CreateHoliday(CreateHolidayRequest request)
    {
        var command = Mapper.Map<CreateHolidayCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update Holiday
    /// </summary>
    [HttpPut(ApiRoutes.Holiday.UpdateHoliday)]
    public async Task<IActionResult> UpdateHoliday(UpdateHolidayRequest request)
    {
        var command = Mapper.Map<UpdateHolidayCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete holiday
    /// </summary>
    [HttpDelete(ApiRoutes.Holiday.DeleteHoliday)]
    public async Task<IActionResult> DeleteHoliday([FromRoute] int id)
    {
        var command = new DeleteHolidayCommand { Id = id };
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get all holidays
    /// </summary>
    [HttpGet(ApiRoutes.Holiday.GetAllHoliday)]
    public async Task<IActionResult> GetAllHolidays()
    {
        var command = new GetAllHolidaysQuery();
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}