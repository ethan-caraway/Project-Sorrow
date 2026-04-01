namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the IOU item.
	/// </summary>
	public class IOU : Item
	{
		#region Class Constructors

		public IOU ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Return snaps
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Snaps = GetSnapsFromDebt ( GameManager.Run.Debt )
			};
		}

		public override int GetIntScaleValue ( int current )
		{
			// Get snaps for dept
			return GetSnapsFromDebt ( GameManager.Run.Debt );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for debt
			return GetSnapsFromDebt ( GameManager.Run.Debt );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of snaps earned from an amount of debt.
		/// </summary>
		/// <param name="debt"> The amount of debt the player has. </param>
		/// <returns> The amount of snaps earned. </returns>
		private int GetSnapsFromDebt ( int debt )
		{
			return ( debt * -1 ) * 2;
		}

		#endregion // Private Functions
	}
}