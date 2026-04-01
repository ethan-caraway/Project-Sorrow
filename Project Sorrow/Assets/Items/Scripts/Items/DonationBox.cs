namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Donation Box item.
	/// </summary>
	public class DonationBox : Item
	{
		#region Class Constructors

		public DonationBox ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Check for interest
			if ( stats.Interest == 0 )
			{
				// Double commission
				stats.Commission = stats.Commission * 2;
			}
		}

		public override bool OnCommission ( int commission )
		{
			return commission > Performance.PerformanceModel.DEFAULT_COMMISSION;
		}

		#endregion // Item Override Functions
	}
}