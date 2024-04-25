using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Holiday;

public class UpdateHolidayRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string HolidayName { get; set; }
}