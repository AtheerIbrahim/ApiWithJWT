﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using ApiWithJWT.Models;
using ApiWithJWT.Settings;
using ApiWithJWT.Dtos;
using ApiWithJWT.Wrappers;
using ApiWithJWT.Services.Interfaces;

namespace ApiWithJWT.Services;

public class AccountService(
	UserManager<Account> userManager,
	IOptionsSnapshot<JWTSettings> jwtSettings,
	SignInManager<Account> signInManager) : IAccountService

{


	public async Task<BaseResult> RegisterCustomer(RegistrationRequest request)
	{
		var user = new Customer
		{
			Name = request.Name,
			Email = request.Email,
			UserName = request.UserName

		};

		var result = await userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
			return new BaseResult(result.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));


		var roleResult = await userManager.AddToRoleAsync(user, "Customer");

		if (!roleResult.Succeeded)
			return new BaseResult(roleResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));

		return new BaseResult();
	}


	public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
	{

		var managedUser = await userManager.FindByEmailAsync(request.Email!);
		if (managedUser == null)
		{
			return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.ErrorInIdentity, "Bad credentials"));
		}

		var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password!);
		if (!isPasswordValid)
		{
			return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.ErrorInIdentity, "Bad credentials"));
		}

		var user = userManager.Users.FirstOrDefault(u => u.Email == request.Email);

		if (user is null)
		{
			return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.AccessDenied, "You are not Authorized"));
		}

		var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
		var jwToken = await GenerateJwtToken(user);


		return new BaseResult<AuthenticationResponse>(
			   new AuthenticationResponse
			   {
				   UserName = user.UserName,
				   Email = user.Email,
				   Token = new JwtSecurityTokenHandler().WriteToken(jwToken),
			   });
	}


	private async Task<JwtSecurityToken> GenerateJwtToken(Account user)
	{
		await userManager.UpdateSecurityStampAsync(user);


		var signingCredentials = CreateSigningCredentials();

		var jwtSecurityToken = new JwtSecurityToken(
			issuer: jwtSettings.Value.Issuer,
			audience: jwtSettings.Value.Audience,
			claims: await GetClaimsAsync(),
			expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.DurationInMinutes),
			signingCredentials: signingCredentials);
		return jwtSecurityToken;

		async Task<IList<Claim>> GetClaimsAsync()
		{
			var result = await signInManager.ClaimsFactory.CreateAsync(user);
			return result.Claims.ToList();
		}
	}



	private SigningCredentials CreateSigningCredentials()
	{
		return new SigningCredentials(
			new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwtSettings.Value.Key)
			),
			SecurityAlgorithms.HmacSha256
		);
	}
}
