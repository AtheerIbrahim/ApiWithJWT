using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWithJWT.Dtos;
using ApiWithJWT.Dtos.Carts;
using ApiWithJWT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithJWT.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
	private readonly ICartService _cartService;

	public CartController(ICartService cartService)
	{
		_cartService = cartService;
	}

	[HttpGet]
	public async Task<IActionResult> GetCartInfo()
	{
		try
		{
			var carts = await _cartService.GetAllCartService();
			return Ok(carts);
		}
		catch (Exception e)
		{
			return StatusCode(500, new FailureResponse { Message = e.Message });
		}
	}

	[HttpGet("{cartId}")]
	public async Task<CartDto?> GetCartById(Guid cartId)
	{
		var cart = await _cartService.GetCartInfoById(cartId);
		if (cart == null)
			return null;

		return cart;
	}

	[HttpDelete("{cartId}")]
	public async Task<IActionResult> DeleteACart(Guid cartId)
	{
		try
		{
			await _cartService.DeleteCartInfo(cartId);
			return Ok($"cart With Id:{cartId} has been Deleted successfully");
		}
		catch (Exception e)
		{
			return StatusCode(500, new FailureResponse { Message = e.Message });
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateCart(CreateCartDto cartDto)
	{
		var newCart = await _cartService.CreateCart(cartDto);
		return Ok(newCart);
	}

	[HttpPut("{cartId}")]
	public async Task<IActionResult> NewCartInfo(Guid cartId)
	{
		try
		{
			await _cartService.NewCartInfos(cartId);
			return Ok($"cart With Id:{cartId} has been Added successfully");
		}
		catch (Exception e)
		{
			return StatusCode(500, new FailureResponse { Message = e.Message });
		}
	}


}
