namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Improved Rarity upgrade.
	/// </summary>
	public class ImprovedRarity : Upgrade
	{
		#region Class Constructors

		public ImprovedRarity ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override Shop.RarityModel OnRarity ( Shop.RarityModel model )
		{
			// Set increased chances
			return GetRarityChances ( );
		}

		#endregion // Upgrade Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the increased chances for item rarities with this item.
		/// </summary>
		/// <returns> The data for the item prices. </returns>
		private Shop.RarityModel GetRarityChances ( )
		{
			// Return increased chances
			return new Shop.RarityModel
			{
				CommonChance = 0.5f,
				UncommonChance = 0.25f,
				RareChance = 0.20f,
				LegendaryChance = 0.05f
			};
		}

		#endregion // Private Functions
	}
}