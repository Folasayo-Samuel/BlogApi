using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;

namespace BlogApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogItemsController : ControllerBase
	{
		private readonly BlogContext _context;

		public BlogItemsController(BlogContext context)
		{
			_context = context;
		}

		// GET: api/BlogItems
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogItemDTO>>> GetBlogItem()
		{
		//   if (_context.BlogItem == null)
		//   {
		// 	  return NotFound();
		//   }
			return await _context.BlogItem.Select(x => ItemToDTO(x)).ToListAsync();
		}

		// GET: api/BlogItems/5
		[HttpGet("{id}")]
		public async Task<ActionResult<BlogItemDTO>> GetBlogItem(long id)
		{
		//   if (_context.BlogItem == null)
		//   {
		// 	  return NotFound();
		//   }
		// 	var blogItem = await _context.BlogItem.FindAsync(id);

		// 	if (blogItem == null)
		// 	{
		// 		return NotFound();
		// 	}
		var blogItem = await _context.BlogItem.FindAsync(id);
		
		if(blogItem == null)
		{
			return NotFound();
		}

			return ItemToDTO(blogItem);
		}

		// PUT: api/BlogItems/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBlogItem(long id, BlogItemDTO blogItemDTO)
		{
			if (id != blogItemDTO.Id)
			{
				return BadRequest();
			}
 
 			var blogItem = await _context.BlogItem.FindAsync(id);
			if(blogItem == null)
			{
				return NotFound();
			}

			blogItem.Name = blogItemDTO.Name;
			blogItem.IsComplete = blogItemDTO.IsComplete;
			
			// _context.Entry(blogItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) when(!BlogItemExists(id))
			{
								 	return NotFound();

				// if (!BlogItemExists(id))
				// {
				// 	return NotFound();
				// }
				// else
				// {
				// 	throw;
				// }
			}

			return NoContent();
		}

		// POST: api/BlogItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<BlogItemDTO>> PostBlogItem(BlogItemDTO blogItemDTO)
		{
			var blogItem = new BlogItem
			{
				IsComplete = blogItemDTO.IsComplete,
				Name = blogItemDTO.Name
			};
			
			_context.BlogItem.Add(blogItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				nameof(GetBlogItem), 
				new { id = blogItem.Id }, 
				ItemToDTO(blogItem));

		// 	if (_context.BlogItem == null)
		//   {
		// 	  return Problem("Entity set 'BlogContext.BlogItem'  is null.");
		//   }
		// 	_context.BlogItem.Add(blogItem);
		// 	await _context.SaveChangesAsync();

			// return CreatedAtAction("GetBlogItem", new { id = blogItem.Id }, blogItem);
						// return CreatedAtAction(nameof(GetBlogItem), new { id = blogItem.Id }, blogItem);

		}

		// DELETE: api/BlogItems/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBlogItem(long id)
		{
			var blogItem = await _context.BlogItem.FindAsync(id);
			
			if(blogItem == null)
			{
				return NotFound();
			}

			_context.BlogItem.Remove(blogItem);
			await _context.SaveChangesAsync();
			
			return NoContent();
			
			// if (_context.BlogItem == null)
			// {
			// 	return NotFound();
			// }
			// var blogItem = await _context.BlogItem.FindAsync(id);
			// if (blogItem == null)
			// {
			// 	return NotFound();
			// }

			// _context.BlogItem.Remove(blogItem);
			// await _context.SaveChangesAsync();

			// return NoContent();
		}

		private bool BlogItemExists(long id)
		{
			return _context.BlogItem.Any(e => e.Id == id);
			// return (_context.BlogItem?.Any(e => e.Id == id)).GetValueOrDefault();
		}

		private static BlogItemDTO ItemToDTO(BlogItem blogItem) => 
		new BlogItemDTO
		{
			Id = blogItem.Id,
			Name = blogItem.Name,
			IsComplete = blogItem.IsComplete
		};
	}
}
