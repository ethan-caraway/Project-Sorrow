using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Luxury Watch item.
	/// </summary>
	public class LuxuryWatch : Item
	{
		#region Class Constructors

		public LuxuryWatch ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", Utils.FormatTime ( GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID ) ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", Utils.FormatTime ( 3f ) );
		}

		public override float OnTimeAllowance ( float current )
		{
			return GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );
		}

		public override void OnCompletePerformance ( PerformanceModel model, SummaryStatsModel stats )
		{
			// Add time remaining
			GameManager.Run.AddItemFloatScaleValue ( ID, InstanceID, 3f * model.ConfidenceRemaining );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 3f );
		}

		#endregion // Item Override Functions
	}
}