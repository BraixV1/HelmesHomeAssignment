using System.Net;

namespace App.DTO.v1_0;

public class RestAPIErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}