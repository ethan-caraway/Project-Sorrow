namespace FlightPaper.ProjectSorrow.StatusEffects
{
	/// <summary>
	/// This class stores the data for a status effect.
	/// </summary>
	[System.Serializable]
	public class StatusEffectModel
	{
		#region Status Effect Data

		/// <summary>
		/// The type of the status effect.
		/// </summary>
		public Enums.StatusEffectType Type;

		/// <summary>
		/// The number of stacks of this status effect.
		/// </summary>
		public int Count;

		#endregion // Status Effect Data

		#region Public Functions

		/// <summary>
		/// Checks whether or not this model contains valid status effect data.
		/// </summary>
		/// <returns> Whether or not the status effect is valid. </returns>
		public bool IsValid ( )
		{
			// Validate data
			return Type != Enums.StatusEffectType.NONE && Count > 0;
		}

		#endregion // Public Functions
	}
}