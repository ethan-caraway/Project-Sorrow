namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Piggy Bank item.
	/// </summary>
	public class PiggyBank : Item
	{
		#region Class Constructors

		public PiggyBank ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increase commission
			stats.Commission += 3;
		}

		public override bool OnCommission ( int commission )
		{
			// Trigger item
			return true;
		}

		#endregion // Item Override Functions
	}
}