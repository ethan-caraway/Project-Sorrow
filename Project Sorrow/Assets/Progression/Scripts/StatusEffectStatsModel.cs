namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a status effect usage for the player.
	/// </summary>
	[System.Serializable]
	public class StatusEffectStatsModel
	{
		#region Status Effect Data

		/// <summary>
		/// The ID of the status effect.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this status effect has been applied by the player.
		/// </summary>
		public int Applies = 0;

		/// <summary>
		/// The highest number of stacks of this status effect owned at once by the player.
		/// </summary>
		public int Stacks = 0;

		/// <summary>
		/// The unique stat for tracking the triggered effects of this status effect by the player.
		/// </summary>
		public int Triggered = 0;

		#endregion // Modifier Data

		#region Public Properties

		/// <summary>
		/// Whether or not this status effect has been discovered by the player.
		/// </summary>
		public bool IsDiscovered
		{
			get
			{
				return Applies > 0;
			}
		}

		#endregion // Public Properties
	}
}