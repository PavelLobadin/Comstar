using System;

namespace Comstar.Client.Models
{
	public class ChatRecord : BaseDataObject
	{
		private string _author;

		private string _message;

		private DateTime _timestamp;

		public string Author { get => _author; set => SetProperty(ref _author, value); }

		public string Message { get => _message; set => SetProperty(ref _message, value); }

		public DateTime Timestamp { get => _timestamp; set => SetProperty(ref _timestamp, value); }
	}
}