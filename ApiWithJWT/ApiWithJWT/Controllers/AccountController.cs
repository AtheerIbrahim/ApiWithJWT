﻿
using ApiWithJWT.Dtos;
using ApiWithJWT.Services.Interfaces;
using ApiWithJWT.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithJWT.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
	[HttpPost(nameof(Authenticate))]
	public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
		   => await accountService.Authenticate(request);


	[HttpPost(nameof(RegisterCustomer))]
	public async Task<BaseResult> RegisterCustomer(RegistrationRequest request)
		   => await accountService.RegisterCustomer(request);

	[Authorize(Roles ="Admin")]
	[HttpGet(nameof(TestJwt))]
	public IActionResult TestJwt()
	{
		return Ok("Hello");
	}

}
