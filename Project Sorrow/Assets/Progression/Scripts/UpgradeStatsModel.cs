namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for an upgrade usage for the player.
	/// </summary>
	[System.Serializable]
	public class UpgradeStatsModel
	{
		#region Upgrade Data

		/// <summary>
		/// The ID of the upgrade.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this upgrade has been owned by the player.
		/// </summary>
		public int Owns = 0;

		/// <summary>
		/// The total number of runs won owning this upgrade for the final judge.
		/// </summary>
		public int Wins = 0;

		/// <summary>
		/// The highest difficulty the player has won a run owning this upgrade for the final judge.
		/// </summary>
		public int HighestDifficultyWin = 0;

		#endregion // Upgrade Data

		#region Public Properties

		/// <summary>
		/// Whether or not this upgrade has been discovered by the player.
		/// </summary>
		public bool IsDiscovered
		{
			get
			{
				return Owns > 0;
			}
		}

		#endregion // Public Properties
	}
}