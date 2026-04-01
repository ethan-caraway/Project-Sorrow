namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Amp item.
	/// </summary>
	public class Amp : Item
	{
		#region Class Constructors

		public Amp ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private bool isPerfect = true;

		#endregion // Item Data

		#region Item Override Functions

		public override bool OnFlub ( )
		{
			// Mark flub
			isPerfect = false;

			// Return that the item was triggered
			return true;
		}

		public override bool IsFlubEffectPositive ( )
		{
			// Return that the effect is negative
			return false;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get snaps from line
			int snaps = 0;
			if ( isPerfect )
			{
				snaps = total;
			}

			// Reset line tracking
			isPerfect = true;

			// Return additional snaps earned
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Snaps = snaps
			};
		}

		#endregion // Item Override Functions
	}
}