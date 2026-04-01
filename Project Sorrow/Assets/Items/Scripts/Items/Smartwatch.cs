namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Smartwatch item.
	/// </summary>
	public class Smartwatch : Item
	{
		#region Class Constructors

		public Smartwatch ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override float OnTimeAllowance ( float current )
		{
			return 45f;
		}

		#endregion // Item Override Functions
	}
}