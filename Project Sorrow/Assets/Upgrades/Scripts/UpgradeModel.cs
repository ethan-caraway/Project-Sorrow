namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class stores the data for an owned upgrade.
	/// </summary>
	[System.Serializable]
	public class UpgradeModel
	{
		#region Upgrade Data Constants

		/// <summary>
		/// The ID used for no upgrade.
		/// </summary>
		public const int NO_UPGRADE_ID = 0;

		#endregion // Upgrade Data Constants

		#region Upgrade Data

		/// <summary>
		/// The ID of the upgrade.
		/// </summary>
		public int ID;

		/// <summary>
		/// The data for the upgrade.
		/// </summary>
		[System.NonSerialized]
		public Upgrade Upgrade;

		#endregion // Upgrade Data

		#region Public Functions

		/// <summary>
		/// Checks whether or not this model contains valid upgrade data.
		/// </summary>
		/// <returns> Whether or not the upgrade is valid. </returns>
		public bool IsValid ( )
		{
			// Validate data
			return ID != NO_UPGRADE_ID && Upgrade != null;
		}

		#endregion // Public Functions
	}
}