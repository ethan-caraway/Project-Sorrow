namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Loyalty Card item.
	/// </summary>
	public class LoyaltyCard : Item
	{
		#region Class Constructors

		public LoyaltyCard ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int COUNT_TOTAL = 10;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			return COUNT_TOTAL;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get count
			int count = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Decrement count
			count--;
			
			// Check for trigger
			if ( count == 0 )
			{
				// Reset count
				GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );

				// Return x6 snaps
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = "<b>x6</b>"
					},
					Snaps = total * 5
				};
			}
			else
			{
				// Store decremented count
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );

				// Highlight the count
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SERIOUS_GREY,
						SplashText = count.ToString ( )
					}
				};
			}
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );
		}

		#endregion // Item Override Functions
	}
}