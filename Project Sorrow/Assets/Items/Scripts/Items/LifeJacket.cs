namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Life Jacket item.
	/// </summary>
	public class LifeJacket : Item
	{
		#region Class Constructors

		public LifeJacket ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Reduce flub penalty
			model.FlubPenalty = model.FlubPenalty / 2;
		}

		public override bool OnFlub ( )
		{
			return true;
		}

		#endregion // Item Override Functions
	}
}