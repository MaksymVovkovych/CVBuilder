using System;
using CVBuilder.Application.Holidays.Responses;
using MediatR;

namespace CVBuilder.Application.Holidays.Commands;

public class UpdateHolidayCommand: IRequest<HolidayResult>
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}