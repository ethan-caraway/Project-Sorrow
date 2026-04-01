namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a judge for the player.
	/// </summary>
	[System.Serializable]
	public class JudgeStatsModel
	{
		#region Judge Data

		/// <summary>
		/// The ID of the judge.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this judge has been performed against by the player.
		/// </summary>
		public int Performances = 0;

		/// <summary>
		/// The total number of times this judge has been successfully performed against by the player.
		/// </summary>
		public int Successes = 0;

		/// <summary>
		/// The highest score the player has earned during a performance against this judge.
		/// </summary>
		public int HighScore = 0;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// Whether or not this judge has been discovered by the player.
		/// </summary>
		public bool IsDiscovered
		{
			get
			{
				return Performances > 0;
			}
		}

		#endregion // Public Properties
	}
}