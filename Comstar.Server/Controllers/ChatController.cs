using System;
using System.Collections.Generic;
using Comstar.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comstar.Server.Controllers
{
	[Route("api/[controller]")]
	public class ChatController : Controller
	{
		private static List<ChatRecord> _chatRecords = new List<ChatRecord>
		{
			new ChatRecord
			{
				Id = Guid.NewGuid().ToString(),
				Author = "Вася",
				CreatedAt = DateTime.Now.AddMinutes(-10),
				Message = "Привет, всем!"
			},
			new ChatRecord
			{
				Id = Guid.NewGuid().ToString(),
				Author = "Петя",
				CreatedAt = DateTime.Now.AddMinutes(-9),
				Message = "Привет, Вася!"
			}
		};

		[HttpGet]
		public IEnumerable<ChatRecord> Get()
		{
			return _chatRecords;
		}

		[HttpPost]
		public void Post([FromBody]ChatRecord value)
		{
			_chatRecords.Add(value);
		}
	}
}