using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile
{
	public class EventRepository
	{
		SQLiteAsyncConnection dbconn;
		public string StatusMessage { get; set; }

		public EventRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<Event>();
		}

		public async Task AddEventAsync(Event ev)
		{
			int result = 0;
			try
			{
				result = await dbconn.InsertAsync(new Event
				{
					owner = ev.owner,
					name = ev.name,
					type = ev.type,
					details = ev.details,
					start_time = ev.start_time,
					end_time = ev.end_time,
					credentials_captured = ev.credentials_captured
				});

				StatusMessage = string.Format("Success! Added event {0}. You now have {1} past events.", ev.name, result);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to add event {0}. Error: {1}", ev.name, ex.Message);
			}
		}

		public async Task<List<Event>> GetAllCredentialsAsync()
		{
			try
			{
				return await dbconn.Table<Event>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get event list. {0}", ex.Message);
			}

			return new List<Event>();
		}
	}
}
