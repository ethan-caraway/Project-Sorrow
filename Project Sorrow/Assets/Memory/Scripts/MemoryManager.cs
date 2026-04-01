using System.IO;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Memory
{
	/// <summary>
	/// This class controls saving and loading player data.
	/// </summary>
	public static class MemoryManager
	{
		#region Save Data Constants

		private const string SAVE_FILE = "SaveData";

		#endregion // Save Data Constants

		#region Save Data

		private static SaveModel saveData;

		#endregion // Save Data

		#region Public Properties

		/// <summary>
		/// Whether or not there is previous run data in the save data.
		/// </summary>
		public static bool HasPreviousRun
		{
			get
			{
				return saveData.Run != null && !string.IsNullOrEmpty ( saveData.Run.Checkpoint );
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Loads the save data from memory.
		/// </summary>
		public static void Load ( )
		{
			// Get the file path
			string path = GetSaveFilePath ( );

			// Check for existing save data
			if ( File.Exists ( path ) )
			{
				// Load json string from file
				string json = File.ReadAllText ( path );

				// Store loaded data
				saveData = JsonUtility.FromJson<SaveModel> ( json );
			}
			else
			{
				// Create new save data
				saveData = new SaveModel ( );
			}

			// Set data from save
			FromSaveData ( );
		}

		/// <summary>
		/// Saves the save data to memory.
		/// </summary>
		public static void Save ( )
		{
			// Convert data to save
			ToSaveData ( );

			// Get the file path
			string path = GetSaveFilePath ( );

			// Convert data to json
			string json = JsonUtility.ToJson ( saveData );

			// Save to file
			File.WriteAllText ( path, json );
		}

		/// <summary>
		/// Deletes the save file from memory, resetting all progress.
		/// </summary>
		public static void Delete ( )
		{
			// Get the file path
			string path = GetSaveFilePath ( );

			// Check for file
			if ( File.Exists ( path ) )
			{
				// Delete file
				File.Delete ( path );
			}

			// Reset save data
			saveData = new SaveModel ( );
		}

		/// <summary>
		/// Gets the data for the previous run.
		/// </summary>
		/// <returns> The data for the previous run. </returns>
		public static RunModel GetPreviousRun ( )
		{
			return saveData.Run;
		}

		/// <summary>
		/// Gets the ID of the difficulty for the previous run.
		/// </summary>
		/// <returns> The ID of the difficulty. </returns>
		public static int GetPreviousDifficulty ( )
		{
			return saveData.Difficulty;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Gets the file path for the save data.
		/// </summary>
		/// <returns> The save data file path. </returns>
		private static string GetSaveFilePath ( )
		{
			return $"{Application.persistentDataPath}/{SAVE_FILE}.json";
		}

		/// <summary>
		/// Sets the runtime data from the save data.
		/// </summary>
		private static void FromSaveData ( )
		{
			// Set the player progression
			Progression.ProgressManager.Progress = saveData.Progress;
		}

		/// <summary>
		/// Sets the save data from the runtime data.
		/// </summary>
		private static void ToSaveData ( )
		{
			// Set the run data
			saveData.Run = GameManager.Run;

			// Set difficulty data
			saveData.Difficulty = GameManager.Difficulty != null ? GameManager.Difficulty.ID : 0;

			// Set the player progression
			saveData.Progress = Progression.ProgressManager.Progress;
		}

		#endregion // Private Functions
	}
}