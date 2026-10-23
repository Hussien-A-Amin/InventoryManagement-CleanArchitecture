using EntityFramework.Exceptions.Common;
using InventoryManagement.Application.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Api.ResponseHandlers;

public class Result
{
    [JsonPropertyOrder(1)]
    public bool IsSuccess { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(4)]
    public List<Error> Errors { get; set; } = default!;


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(3)]
    public string Message { get; set; }

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;

    }







    public static Result Success() => new(true);
    public static Result Failure() => new(false);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true);



    //public static Result<TValue> Failure<TValue>(Error error) => new(default, false);

    //internal static Result<T> Success<T>(Task<List<VotePerDayResponse>> votePerDay)
    //{
    //    throw new NotImplementedException();
    //}
}


public class Result<TValue>(TValue? value, bool isSuccess) : Result(isSuccess)
{
    private readonly TValue? _value = value;

    [JsonPropertyOrder(2)]
    public TValue? Data { get => value; }
}





