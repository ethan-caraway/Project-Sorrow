namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class contains functions for creating encounters.
	/// </summary>
	public static class EncounterHelper
	{
		#region Encounter Data Constants

		private const int BOOK_SALE_ID = 1;
		private const int PEER_PRESSURE_ID = 2;
		private const int THE_MAGICIAN_ID = 3;
		private const int LOST_AND_FOUND_ID = 4;
		private const int SELF_PROMOTION_ID = 5;
		private const int JUICY_GIVEAWAY_ID = 6;
		private const int SOCIAL_CONUNDRUM_ID = 7;
		private const int INVESTMENT_STRATEGIES_ID = 8;
		private const int RETURNING_THE_FAVOR_ID = 9;
		private const int SHOTS_ID = 10;
		private const int NUMBER_ONE_BESTSELLER_ID = 11;

		#endregion // Encounter Data Constants

		#region Public Functions

		/// <summary>
		/// Gets the instance of an encounter for a given ID.
		/// </summary>
		/// <param name="id"> The ID of the encounter. </param>
		/// <returns> The instance of the encounter. </returns>
		public static Encounter GetEncounter ( int id )
		{
			// Check ID
			switch ( id )
			{
				case BOOK_SALE_ID:
					return new BookSale ( );

				case PEER_PRESSURE_ID:
					return new PeerPressure ( );

				case THE_MAGICIAN_ID:
					return new TheMagician ( );

				case LOST_AND_FOUND_ID:
					return new LostAndFound ( );

				case SELF_PROMOTION_ID:
					return new SelfPromotion ( );

				case JUICY_GIVEAWAY_ID:
					return new JuicyGiveaway ( );

				case SOCIAL_CONUNDRUM_ID:
					return new SocialConundrum ( );

				case INVESTMENT_STRATEGIES_ID:
					return new InvestmentStrategies ( );

				case RETURNING_THE_FAVOR_ID:
					return new ReturningTheFavor ( );

				case SHOTS_ID:
					return new Shots ( );

				case NUMBER_ONE_BESTSELLER_ID:
					return new NumberOneBestseller ( );
			}

			// Return that no encounter was found
			return null;
		}

		#endregion // Public Functions
	}
}