using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Holiday;

public class CreateHolidayRequest
{
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}