using System;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace CVBuilder.Application.Holidays.Responses;

public class HolidayResult
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}