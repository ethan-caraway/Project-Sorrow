namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Red Flag item.
	/// </summary>
	public class RedFlag : Item
	{
		#region Class Constructors

		public RedFlag ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = 200 * model.ArroganceRemaining
			};
		}

		#endregion // Item Override Functions
	}
}