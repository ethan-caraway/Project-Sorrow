namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a difficulty for the player.
	/// </summary>
	[System.Serializable]
	public class DifficultyStatsModel
	{
		#region Difficulty Data

		/// <summary>
		/// The ID of the difficulty.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of runs that have been attempted on this difficulty by the player.
		/// </summary>
		public int Runs = 0;

		/// <summary>
		/// The total number of runs won on this difficulty by the player.
		/// </summary>
		public int Wins = 0;

		/// <summary>
		/// The highest score the player has earned during a performance on this difficulty.
		/// </summary>
		public int HighScore = 0;

		#endregion // Difficulty Data
	}
}