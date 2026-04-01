namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Pocket Watch item.
	/// </summary>
	public class PocketWatch : Item
	{
		#region Class Constructors

		public PocketWatch ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnMaxConfidence ( int current )
		{
			return -2;
		}

		public override float OnTimeAllowance ( float current )
		{
			return 90f;
		}

		#endregion // Item Override Functions
	}
}