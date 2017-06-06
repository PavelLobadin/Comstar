namespace Comstar.Client.Models
{
	public class Item : BaseDataObject
	{
		string _text = string.Empty;
		public string Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		string _description = string.Empty;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private string _status = string.Empty;

		public string Status
		{
			get => _status;
			set => SetProperty(ref _status, value);
		}
	}
}
