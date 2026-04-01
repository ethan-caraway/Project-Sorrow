namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Disguise item.
	/// </summary>
	public class Disguise : Item
	{
		#region Class Constructors

		public Disguise ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnMaxConfidence ( int current )
		{
			// Decrease confidence
			return -1;
		}

		public override int OnMaxArrogance ( int current )
		{
			// Increase arrogance
			return 3;
		}

		#endregion // Item Override Functions
	}
}