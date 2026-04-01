namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Running Shoes item.
	/// </summary>
	public class RunningShoes : Item
	{
		#region Class Constructors

		public RunningShoes ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Apply applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = (int)(model.TimeRemaining / 10 ) * 5
			};
		}

		#endregion // Item Override Functions
	}
}