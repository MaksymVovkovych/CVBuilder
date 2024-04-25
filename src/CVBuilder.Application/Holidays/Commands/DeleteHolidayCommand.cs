using CVBuilder.Application.Holidays.Responses;
using MediatR;

namespace CVBuilder.Application.Holidays.Commands;

public class DeleteHolidayCommand: IRequest<HolidayResult>
{
    public int Id { get; set; }
}