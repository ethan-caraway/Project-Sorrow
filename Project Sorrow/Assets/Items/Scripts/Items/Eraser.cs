namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Eraser item.
	/// </summary>
	public class Eraser : Item
	{
		#region Class Constructors

		public Eraser ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID ).ToString ( "0.#" ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "1" );
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for modified word
			if ( modifier != Enums.WordModifierType.NONE )
			{
				// Increment scale
				GameManager.Run.AddItemFloatScaleValue ( ID, InstanceID, 0.1f );

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

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Clear modifiers from poem
			GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].Modifiers = null;
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Get scale value
			float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Return applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = (int)( total * ( scale - 1f ) )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 1f );
		}

		#endregion // Item Override Functions
	}
}