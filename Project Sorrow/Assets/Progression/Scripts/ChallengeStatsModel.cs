namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for challenge progression for the player.
	/// </summary>
	[System.Serializable]
	public class ChallengeStatsModel
	{
		#region Challenge Data

		/// <summary>
		/// The total number of successful performances of poems by women.
		/// </summary>
		public int SuccessfulPerformancesByWomen = 0;

		/// <summary>
		/// The most amount of confidence a player has started a performance with.
		/// </summary>
		public int MostStartConfidence = 0;

		/// <summary>
		/// The most amount of confidence remaining in a performance.
		/// </summary>
		public int MostConfidenceRemaining = 0;

		/// <summary>
		/// The most amount of snaps earned from confidence remaining at the end of a performance.
		/// </summary>
		public int MostSnapsFromConfidenceRemaining = 0;

		/// <summary>
		/// The most amount of interest earn in a performance.
		/// </summary>
		public int MostInterest = 0;

		/// <summary>
		/// The most amount of money the player has had at once.
		/// </summary>
		public int MostMoney = 0;

		/// <summary>
		/// The most amount of debt the player has had at once.
		/// </summary>
		public int MostDebt = 0;

		/// <summary>
		/// The most amount of time in seconds a player has started a performance with.
		/// </summary>
		public float MostStartTime = 0f;

		/// <summary>
		/// The least amount of items owned in the final performance to win a run.
		/// </summary>
		public int LeastItemsWin = 100;

		/// <summary>
		/// The most amount of lines completed in a row without flubbing.
		/// </summary>
		public int MostLinesWithoutFlubbing = 0;

		#endregion // Challenge Data
	}
}