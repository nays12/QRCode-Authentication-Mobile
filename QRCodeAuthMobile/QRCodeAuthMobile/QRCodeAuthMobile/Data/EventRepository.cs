/*
 * Purpose: 
 * This is a Data Repository Class that is responsible for handling all the database operations invloving the
 * Events Table in the local SQLite database in the User's mobile device
 * 
 * Contributors:
 * Naomi Wiggins 
 * 
 */

using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Data
{
	public class EventRepository
	{
		protected static SQLiteAsyncConnection db;
		public static string StatusMessage { get; set; }

		public EventRepository(string dbPath)
		{
			db = new SQLiteAsyncConnection(dbPath);
		}

		public static async Task InitializeTableAsync()
		{
			try
			{
				await db.CreateTableAsync<Event>();
				StatusMessage = string.Format("Success! Created a table for Events.");
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not create the Events table. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task AddEventAsync(Event ev)
		{
			try
			{
				await db.InsertAsync(ev);
				StatusMessage = string.Format("Success! Added event {0}.", ev.Name);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to add event {0}. Error: {1}", ev.Name, ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task<List<Event>> GetAllEventsAsync()
		{
			List<Event> events = new List<Event>();
			try
			{
				events = await db.Table<Event>().ToListAsync();
				StatusMessage = string.Format("Success! Retrieved all events.");
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return events;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get event list. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}		
		}


	}
}
