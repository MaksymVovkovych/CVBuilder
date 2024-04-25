using System;
using CVBuilder.Application.Holidays.Responses;
using MediatR;

namespace CVBuilder.Application.Holidays.Commands;

public class CreateHolidayCommand: IRequest<HolidayResult>
{
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}