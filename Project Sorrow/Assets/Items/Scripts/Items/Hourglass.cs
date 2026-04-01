using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Hourglass item.
	/// </summary>
	public class Houglass : Item
	{
		#region Class Constructors

		public Houglass ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override float OnTimeAllowance ( float current )
		{
			return GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );
		}

		public override void OnCompletePerformance ( PerformanceModel model, SummaryStatsModel stats )
		{
			// Add time remaining
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, model.TimeRemaining );
		}

		#endregion // Item Override Functions
	}
}