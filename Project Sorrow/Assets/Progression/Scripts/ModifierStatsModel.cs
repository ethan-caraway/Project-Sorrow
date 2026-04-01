namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a modifier usage for the player.
	/// </summary>
	[System.Serializable]
	public class ModifierStatsModel
	{
		#region Modifier Data

		/// <summary>
		/// The ID of the modifier.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this modifier has been applied by the player.
		/// </summary>
		public int Applies = 0;

		/// <summary>
		/// The total number of times this modifier has been performed by the player.
		/// </summary>
		public int Performed = 0;

		/// <summary>
		/// The unique stat for tracking the triggered effects of this modifier by the player.
		/// </summary>
		public int Triggered = 0;

		#endregion // Modifier Data

		#region Public Properties

		/// <summary>
		/// Whether or not this modifier has been discovered by the player.
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