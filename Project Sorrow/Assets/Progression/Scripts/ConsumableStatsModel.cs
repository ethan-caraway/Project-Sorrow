namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the stats for a consumable usage for the player.
	/// </summary>
	[System.Serializable]
	public class ConsumableStatsModel
	{
		#region Consumable Data

		/// <summary>
		/// The ID of the consumable.
		/// </summary>
		public int ID = 0;

		/// <summary>
		/// The total number of times this consumable has been owned by the player.
		/// </summary>
		public int Owns = 0;

		/// <summary>
		/// The total number of times this consumable has been consumed by the player.
		/// </summary>
		public int Consumed = 0;

		#endregion // Consumable Data

		#region Public Properties

		/// <summary>
		/// Whether or not this consumable has been discovered by the player.
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