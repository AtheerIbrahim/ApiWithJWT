using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApiWithJWT.Data;
using ApiWithJWT.Dtos;
using ApiWithJWT.Dtos.Carts;
using ApiWithJWT.Models;
using ApiWithJWT.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiWithJWT.Services;

public class CartService : ICartService
{
	private readonly ApplicationDbContext _dbContext;

	public CartService(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	//Get All
	public async Task<List<CartDto>> GetAllCartService()
	{
		return await _dbContext
			.Carts.Select(c => new CartDto
			{
				CartId = c.CartId,
				Quantity = c.Quantity,
				CustomerId = c.CustomerId,
			})
			.ToListAsync();
	}

	//Get by Id
	public async Task<CartDto> GetCartInfoById(Guid cartId)
	{
		try
		{
			var cart = await _dbContext
				.Carts.Where(c => c.CartId == cartId)
				.Select(c => new CartDto
				{
					CartId = c.CartId,
					CustomerId = c.CustomerId,
					Quantity = c.Quantity,
				})
				.FirstOrDefaultAsync();
			if (cart != null)
			{
				return cart;
			}
			else
			{
				return null;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{e.Message}");
			return null;
		}
	}

	//Delete
	public async Task<bool> DeleteCartInfo(Guid cartId) //good
	{
		bool status;
		try
		{
			var cart = await _dbContext.Carts.FindAsync(cartId);

			if (cart != null)
			{
				_dbContext.Carts.Remove(cart);
				await _dbContext.SaveChangesAsync(); // Save changes asynchronously
			}
			status = true;
		}
		catch (Exception)
		{
			status = false;
		}
		return status;
	}


	public async Task<CartDto> CreateCart(CreateCartDto createCartDto)
	{
		var cart = new Cart
		{
			CartId = Guid.NewGuid(),
			Quantity = createCartDto.Quantity,
			CustomerId = createCartDto.CustomerId,
		};
		await _dbContext.Carts.AddAsync(cart);

		await _dbContext.SaveChangesAsync();


		return new CartDto
		{
			CartId = cart.CartId,
			Quantity = createCartDto.Quantity,
			CustomerId = createCartDto.CustomerId,
		};
	}

	//Post-Create
	public async Task<bool> NewCartInfos(Guid cartId)
	{
		bool status;
		try
		{
			var cart = await _dbContext.Carts.FindAsync(cartId);

			if (cart != null)
			{
				_dbContext.Carts.AddAsync(cart);
				await _dbContext.SaveChangesAsync(); // Save changes asynchronously
			}
			status = true;
		}
		catch (Exception)
		{
			status = false;
		}
		return status;
	}

	//update
	public async Task<bool> UpdateCartService(Guid cartId, UpdateCartDto updateCart)
	{
		bool status;
		try
		{
			var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
			if (cart != null)
			{
				cart.Quantity = updateCart.Quantity;
				await _dbContext.SaveChangesAsync();
			}
			status = true;
		}
		catch (Exception)
		{
			status = false;
		}
		return status;
	}
}
