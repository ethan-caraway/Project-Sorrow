namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Sunglasses item.
	/// </summary>
	public class Sunglasses : Item
	{
		#region Class Constructors

		public Sunglasses ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnMaxConfidence ( int current )
		{
			// Add to max confidence
			return 2;
		}

		#endregion // Item Override Functions
	}
}