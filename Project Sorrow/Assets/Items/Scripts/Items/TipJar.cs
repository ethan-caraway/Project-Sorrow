namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Tip Jar item.
	/// </summary>
	public class TipJar : Item
	{
		#region Class Constructors

		public TipJar ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private int modifiedWords = 0;

		#endregion // Item Data

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Reset count
			modifiedWords = 0;
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for modified word
			if ( modifier != Enums.WordModifierType.NONE )
			{
				// Increment count
				modifiedWords++;
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increase commision
			stats.Commission += modifiedWords;
		}

		public override bool OnCommission ( int commission )
		{
			// Return that the item triggered when at least on modified word
			return modifiedWords > 0;
		}

		#endregion // Item Override Functions
	}
}