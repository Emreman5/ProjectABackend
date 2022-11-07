﻿namespace Core.Utilities.ResponseTypes;

public interface IResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}