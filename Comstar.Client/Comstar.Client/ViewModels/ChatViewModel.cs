using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Comstar.Client.Helpers;
using Comstar.Client.Models;
using Comstar.Client.Views;

using Xamarin.Forms;

namespace Comstar.Client.ViewModels
{
	public class ChatViewModel : BaseViewModel
	{
		public ObservableRangeCollection<ChatRecord> ChatRecords { get; set; }
		public Command LoadChatRecordsCommand { get; set; }

		public ChatViewModel()
		{
			Title = "Browse";
			ChatRecords = new ObservableRangeCollection<ChatRecord>();
			LoadChatRecordsCommand = new Command(async () => await ExecuteLoadChatRecordsCommand());
		}

		async Task ExecuteLoadChatRecordsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				ChatRecords.Clear();
				var chatRecords = await DataStore.GetChatRecordsAsync(true);
				ChatRecords.ReplaceRange(chatRecords);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}