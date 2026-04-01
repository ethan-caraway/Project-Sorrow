namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Dictionary item.
	/// </summary>
	public class Dictionary : Item
	{
		#region Class Constructors

		public Dictionary ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for word longer than 5 letters
			if ( length > 5 )
			{
				// Return snaps earned for a word longer than 5 letters
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Snaps = 30
				};
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		#endregion // Item Override Functions
	}
}