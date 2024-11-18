using EzzePay.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EzzePay.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("CashIn")]
    public async Task<IActionResult> CashIn()
    {
        try
        {
            var data = await _authService.GetAccessTokenAsync();
            return Ok(new { message = "CashIn success", response = data });
        }
        catch (Exception)
        {
            return StatusCode(500, new { statusCode = 500, message = "Erro interno" });
        }
    }

    [HttpGet("CashOut")]
    public async Task<IActionResult> CashOut()
    {
        try
        {
            var data = await _authService.GetAccessTokenAsync();
            return Ok(new { message = "CashIn success", response = data });
        }
        catch (Exception)
        {
            return StatusCode(500, new { statusCode = 500, message = "Erro interno" });
        }
    }
}