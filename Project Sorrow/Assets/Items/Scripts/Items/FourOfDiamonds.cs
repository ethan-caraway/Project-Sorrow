namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the 4 of Diamonds item.
	/// </summary>
	public class FourOfDiamonds : Item
	{
		#region Class Constructors

		public FourOfDiamonds ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ).ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "4" );
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for word exactly 4 characters long
			if ( length == 4 )
			{
				// Increment scale value
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 4 );

				// Trigger item
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = "Upgrade"
					}
				};
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 4 );
		}

		#endregion // Item Override Functions
	}
}