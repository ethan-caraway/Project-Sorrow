namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Tea Kettle item.
	/// </summary>
	public class TeaKettle : Item
	{
		#region Class Constructors

		public TeaKettle ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "3" );
		}

		public override ItemTriggerModel OnCharacterComplete ( Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for Italics
			if ( modifier == Enums.WordModifierType.ITALICS )
			{
				// Increment scale value
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 3 );

				// Highlight item
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
			return base.OnCharacterComplete ( modifier, model );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Return applause
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
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 3 );
		}

		#endregion // Item Override Functions
	}
}