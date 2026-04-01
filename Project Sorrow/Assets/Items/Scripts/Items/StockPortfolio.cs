namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Stock Portfolio item.
	/// </summary>
	public class StockPortfolio : Item
	{
		#region Class Constructors

		public StockPortfolio ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Increase interest cap
			model.InterestCap += 5;
		}

		public override bool OnInterest ( int interest )
		{
			// Check if interest is above the default cap
			return interest > Performance.PerformanceModel.DEFAULT_INTEREST_CAP;
		}

		#endregion // Item Override Functions
	}
}