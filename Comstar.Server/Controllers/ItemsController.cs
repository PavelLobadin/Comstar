using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Comstar.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comstar.Server.Controllers
{
	[Route("api/[controller]")]
	public class ItemsController : Controller
	{
		private static List<Item> _items = new List<Item>
		{
			new Item { Id = Guid.NewGuid().ToString(), Text = "Тестовая заявка", Description="Описание тестовой заявки" }
		};

		// GET api/values
		[HttpGet]
		public IEnumerable<Item> Get()
		{
			return _items;
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public Item Get(string id)
		{
			return _items.FirstOrDefault(x => x.Id == id);
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]Item value)
		{
			_items.Add(value);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(string id, [FromBody]Item value)
		{
			var idx = _items.FindIndex(x => x.Id == id);
			if (idx < 0)
			{
				throw new HttpRequestException();
			}

			_items[idx] = value;
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(string id)
		{
			_items.RemoveAll(x => x.Id == id);
		}
	}
}
