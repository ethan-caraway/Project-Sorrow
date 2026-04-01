namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Defibrillator item.
	/// </summary>
	public class Defibrillator : Item
	{
		#region Class Constructors

		public Defibrillator ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool OnLoseConfidence ( Performance.PerformanceModel model )
		{
			// Check for no remaining confidence
			if ( model.ConfidenceRemaining <= 0 )
			{
				// Recover confidence
				model.ConfidenceRemaining = (int)( GameManager.Run.MaxConfidence * 0.3f );

				// Lose time
				model.TimeRemaining -= 30f;

				// Return that the item was triggered
				return true;
			}

			// Return that the item was not triggered
			return base.OnLoseConfidence ( model );
		}

		#endregion // Item Override Functions
	}
}