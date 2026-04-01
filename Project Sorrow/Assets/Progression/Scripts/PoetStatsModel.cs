namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a poet for the player.
	/// </summary>
	[System.Serializable]
	public class PoetStatsModel
	{
		#region Poet Data

		/// <summary>
		/// The ID of the poet.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of runs performed with this poet by the player.
		/// </summary>
		public int Runs = 0;

		/// <summary>
		/// The total number of runs won with this poet by the player.
		/// </summary>
		public int Wins = 0;

		/// <summary>
		/// The highest difficulty the player has won a run with this poet.
		/// </summary>
		public int HighestDifficultyWin = 0;

		/// <summary>
		/// The highest performance reached in a run with this poet by the player.
		/// </summary>
		public int HighestPerformance = 0;

		/// <summary>
		/// The highest round reached in a run with this poet by the player.
		/// </summary>
		public int HighestRound = 0;

		/// <summary>
		/// The highest score the player has earned during a performance with this poet.
		/// </summary>
		public int HighScore = 0;

		#endregion // Poet Data
	}
}