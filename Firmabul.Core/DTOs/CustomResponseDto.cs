using System.Text.Json.Serialization;

namespace Firmabul.Core.DTOs;

public class CustomResponseDto<T>
{
    public T Data { get; set; }
    
    [JsonIgnore]
    public int StatusCode { get; set; }
    
    public List<String> Errors { get; set; }

    public static CustomResponseDto<T> Success(int statusCode, T data)
    {
        return new CustomResponseDto<T>
        {
            StatusCode = statusCode, Data = data
        };
    }
    
    public static CustomResponseDto<T> Success(int statusCode)
    {
        return new CustomResponseDto<T>
        {
            StatusCode = statusCode
        };
    }
    
    public static CustomResponseDto<T> Fail(int statusCode, List<String> errors)
    {
        return new CustomResponseDto<T>
        {
            StatusCode = statusCode, Errors = errors
        };
    }
    
    public static CustomResponseDto<T> Fail(int statusCode, String error)
    {
        return new CustomResponseDto<T>
        {
            StatusCode = statusCode, Errors = new List<string>{ error }
        };
    }
}