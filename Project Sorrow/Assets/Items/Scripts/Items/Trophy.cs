namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Trophy item.
	/// </summary>
	public class Trophy : Item
	{
		#region Class Constructors

		public Trophy ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int GetIntScaleValue ( int current )
		{
			// Get applause for reputation
			return GetApplauseFromReputation ( GameManager.Run.Reputation );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for reputation
			return GetApplauseFromReputation ( GameManager.Run.Reputation );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GetApplauseFromReputation ( GameManager.Run.Reputation )
			};
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of applause earned from an amount of reputation.
		/// </summary>
		/// <param name="reputation"> The amount of reputation the player has. </param>
		/// <returns> The amount of applause earned. </returns>
		private int GetApplauseFromReputation ( int reputation )
		{
			return reputation * 15;
		}

		#endregion // Private Functions
	}
}