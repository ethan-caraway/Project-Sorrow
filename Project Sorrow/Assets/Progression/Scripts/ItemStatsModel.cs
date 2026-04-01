namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for an item usage for the player.
	/// </summary>
	[System.Serializable]
	public class ItemStatsModel
	{
		#region Item Data

		/// <summary>
		/// The ID of the item.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this item has been owned by the player.
		/// </summary>
		public int Owns = 0;

		/// <summary>
		/// The total number of runs won owning this item for the final judge.
		/// </summary>
		public int Wins = 0;

		/// <summary>
		/// The highest difficulty the player has won a run owning this item for the final judge.
		/// </summary>
		public int HighestDifficultyWin = 0;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// Whether or not this item has been discovered by the player.
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