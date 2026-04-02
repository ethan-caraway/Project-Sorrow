namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Yo-yo item.
	/// </summary>
	public class Yoyo : Item
	{
		#region Class Constructors

		public Yoyo ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int COUNT_TOTAL = 3;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Get scalers
			float current = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );
			float next = current == 1f ? 1.5f : 1f;

			// Add the current scale values to the description
			return description.Replace ( "{0}", current.ToString ( ) ).Replace ( "{1}", next.ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "1.5" ).Replace ( "{1}", "1" );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			return COUNT_TOTAL;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get scale value
			float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Get count
			int count = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Decrement count
			count--;

			// Check for trigger
			if ( count == 0 )
			{
				// Reset count
				GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );

				// Swap scale value
				GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, scale == 1f ? 1.5f : 1f );
			}
			else
			{
				// Store decremented count
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );
			}

			// Return the total multiplied by the current scale value
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = Enums.SplashColorType.SNAPS_GOLD,
					SplashText = $"<b>x{scale}</b>"
				},
				Snaps = (int)( total * ( scale - 1f ) )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 3 );
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 1.5f );
		}

		#endregion // Item Override Functions
	}
}