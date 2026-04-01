namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class stores the data for an owned consumable.
	/// </summary>
	[System.Serializable]
	public class ConsumableModel
	{
		#region Consumable Data Constants

		/// <summary>
		/// The ID used for no consumable.
		/// </summary>
		public const int NO_CONSUMABLE_ID = 0;

		#endregion // Consumable Data Constants

		#region Consumable Data

		/// <summary>
		/// The ID of the consumable.
		/// </summary>
		public int ID;

		/// <summary>
		/// The current number of instances of this consumable owned.
		/// </summary>
		public int Count;

		/// <summary>
		/// The data for the consumable.
		/// </summary>
		[System.NonSerialized]
		public Consumable Consumable;

		#endregion // Consumable Data

		#region Public Functions

		/// <summary>
		/// Checks whether or not this model contains valid consumable data.
		/// </summary>
		/// <returns> Whether or not the consumable is valid. </returns>
		public bool IsValid ( )
		{
			// Validate data
			return ID != NO_CONSUMABLE_ID && Consumable != null;
		}

		#endregion // Public Functions
	}
}