namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Gift Card item.
	/// </summary>
	public class GiftCard : Item
	{
		#region Class Constructors

		public GiftCard ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int START_SNAPS = 50;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ).ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", START_SNAPS.ToString ( ) );
		}

		public override ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			// Get snaps
			int snaps = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Trigger item
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = Enums.SplashColorType.SNAPS_GOLD,
					SplashText = $"+{snaps}"
				},
				Snaps = snaps
			};
		}

		public override bool OnPurchase ( int money, int price )
		{
			// Get current scale value
			int scale = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Check for initial purchase
			if ( scale > START_SNAPS )
			{
				// Return that the item was not triggered
				return base.OnPurchase ( money, price );
			}

			// Check for remaining scale value
			if ( scale > 0 )
			{
				// Reduce by price
				scale -= price;

				// Check for underflow
				if ( scale < 0 )
				{
					scale = 0;
				}

				// Update scale value
				GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, scale );

				// Return that the item was triggered
				return true;
			}

			// Return that the item was not triggered
			return base.OnPurchase ( money, price );
		}

		public override bool IsPurchaseEffectPositive ( )
		{
			// Show negative effect
			return false;
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, START_SNAPS + model.Prices.GetItemPrice ( Enums.Rarity.COMMON ) );
		}

		#endregion // Item Override Functions
	}
}