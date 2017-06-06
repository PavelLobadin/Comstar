using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Comstar.Client.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Comstar.Client.Services
{
	public class RestDataStore : IDataStore<Item>
	{
		bool isInitialized;
		List<Item> items;

		public async Task<bool> AddItemAsync(Item item)
		{
			await InitializeAsync();

			items.Add(item);
			var client = CreateHttpClient();
			await client.PostAsync("api/items", new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));
			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Item item)
		{
			await InitializeAsync();

			items.Add(item);
			var client = CreateHttpClient();
			await client.PutAsync($"api/items/{item.Id}", new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));
			return await Task.FromResult(true);

		}

		public async Task<bool> DeleteItemAsync(Item item)
		{
			await InitializeAsync();

			var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);

			return await Task.FromResult(true);
		}

		public async Task<Item> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
		{
			return await ApiAsync<IEnumerable<Item>>("api/items");
		}

		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}

		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			items = new List<Item>();
			var _items = new List<Item>
			{
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Buy some cat food", Description="The cats are hungry"},
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Learn F#", Description="Seems like a functional idea"},
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Learn to play guitar", Description="Noted"},
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Buy some new candles", Description="Pine and cranberry for that winter feel"},
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Complete holiday shopping", Description="Keep it a secret!"},
				new Item { Id = Guid.NewGuid().ToString(), Text = "1Finish a todo list", Description="Done"},
			};

			foreach (Item item in _items)
			{
				items.Add(item);
			}

			isInitialized = true;
		}

		private static async Task<T> ApiAsync<T>(string requestUri)
		{
			var client = CreateHttpClient();
			var response = await client.GetAsync(requestUri);
			if (response.IsSuccessStatusCode)
			{
				var stream = await response.Content.ReadAsStreamAsync();
				return JsonSerializer.Create().Deserialize<T>(new JsonTextReader(new StreamReader(stream)));
			}

			return default(T);
		}

		private static HttpClient CreateHttpClient()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("http://10.0.2.2:51629");
			client.MaxResponseContentBufferSize = 256000;
			return client;
		}
	}
}