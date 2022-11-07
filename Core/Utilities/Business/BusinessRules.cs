﻿using Core.Utilities.ResponseTypes;

namespace Core.Utilities.Business;

public static class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (var logic in logics)
        {
            if (!logic.IsSuccess)
            {
                return logic;
            }
        }

        return new SuccessResult();
    }
}