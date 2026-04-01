namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class contains functions for creating upgrades.
	/// </summary>
	public static class UpgradeHelper
	{
		#region Upgrade Data Constants

		private const int FREE_MONEY_ID = 1;
		private const int CONFIDENCE_BOOST_ID = 2;
		private const int EXTRA_TIME_ID = 3;
		private const int ITEM_EXPANSION_ID = 4;
		private const int CONSUMABLE_EXPANSION_ID = 5;
		private const int FREE_BONUS_ID = 6;
		private const int REROLL_DISCOUNT_ID = 7;
		private const int IMPROVED_REROLL_ID = 8;
		private const int COMMISSION_BOOST_ID = 9;
		private const int INTEREST_BOOST_ID = 10;
		private const int IMPROVED_TIME_ID = 11;
		private const int REPUTATION_BOOST_ID = 12;
		private const int FREE_ITEMS_ID = 13;
		private const int FREE_CONSUMABLES_ID = 14;
		private const int IMPROVED_RARITY_ID = 15;
		private const int BAR_EXPANSION_ID = 16;
		private const int IMPROVED_BAR_ID = 17;
		private const int ARROGANCE_BOOST_ID = 18;

		#endregion // Upgrade Data Constants

		#region Public Functions

		/// <summary>
		/// Gets an instance of an upgrade for a given ID.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		/// <returns> The instance of the upgrade. </returns>
		public static Upgrade GetUpgrade ( int id )
		{
			// Check ID
			switch ( id )
			{
				case FREE_MONEY_ID:
					return new FreeMoney ( id );

				case CONFIDENCE_BOOST_ID:
					return new ConfidenceBoost ( id );

				case EXTRA_TIME_ID:
					return new ExtraTime ( id );

				case ITEM_EXPANSION_ID:
					return new ItemExpansion ( id );

				case CONSUMABLE_EXPANSION_ID:
					return new ConsumableExpansion ( id );

				case FREE_BONUS_ID:
					return new FreeApplause ( id );

				case REROLL_DISCOUNT_ID:
					return new RerollDiscount ( id );

				case IMPROVED_REROLL_ID:
					return new ImprovedReroll ( id );

				case COMMISSION_BOOST_ID:
					return new CommissionBoost ( id );

				case INTEREST_BOOST_ID:
					return new InterestBoost ( id );

				case IMPROVED_TIME_ID:
					return new ImprovedTime ( id );

				case REPUTATION_BOOST_ID:
					return new ReputationBoost ( id );

				case FREE_ITEMS_ID:
					return new FreeItems ( id );

				case FREE_CONSUMABLES_ID:
					return new FreeConsumables ( id );

				case IMPROVED_RARITY_ID:
					return new ImprovedRarity ( id );

				case BAR_EXPANSION_ID:
					return new BarExpansion ( id );

				case IMPROVED_BAR_ID:
					return new ImprovedBar ( id );

				case ARROGANCE_BOOST_ID:
					return new ArroganceBoost ( id );
			}

			// Return that no upgrade was found
			return null;
		}

		#endregion // Public Functions
	}
}