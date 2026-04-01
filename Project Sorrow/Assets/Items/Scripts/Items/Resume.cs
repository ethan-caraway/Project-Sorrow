namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Resume item.
	/// </summary>
	public class Resume : Item
	{
		#region Class Constructors

		public Resume ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnReputation ( int current )
		{
			return 5;
		}

		#endregion // Item Override Functions
	}
}