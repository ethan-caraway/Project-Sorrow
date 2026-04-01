namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Cigarettes item.
	/// </summary>
	public class Cigarettes : Item
	{
		#region Class Constructors

		public Cigarettes ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "5" );
		}

		public override int OnMaxConfidence ( int current )
		{
			// Add current scale value
			return GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );
		}

		public override void OnInitShop ( Shop.ShopModel model )
		{
			// Check for scale value
			if ( GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ) > 0 )
			{
				// Decrement scale value
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );
			}
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 5 );
		}

		#endregion // Item Override Functions
	}
}