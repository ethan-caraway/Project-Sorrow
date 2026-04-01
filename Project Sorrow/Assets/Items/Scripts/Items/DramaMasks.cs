namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Drama Masks item.
	/// </summary>
	public class DramaMasks : Item
	{
		#region Class Constructors

		public DramaMasks ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors


		#region Item Override Functions

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Track total stacks of status effects
			int count = 0;
			for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( GameManager.Run.IsValidStatusEffect ( i ) )
				{
					// Increment count
					count += GameManager.Run.StatusEffectData [ i ].Count;
				}
			}

			// Check for status effects
			if ( count > 0 )
			{
				// Apply Greedy
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Snaps = 75 * count
				};
			}

			// Return that the item was not triggered
			return base.OnLineComplete ( total );
		}

		#endregion // Item Override Functions
	}
}