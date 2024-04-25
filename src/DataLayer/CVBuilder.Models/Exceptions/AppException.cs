using System;

namespace CVBuilder.Models.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message)
    {
    }
}