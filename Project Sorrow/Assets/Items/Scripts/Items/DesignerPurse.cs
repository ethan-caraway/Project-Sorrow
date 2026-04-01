namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Designer Purse item.
	/// </summary>
	public class DesignerPurse : Item
	{
		#region Class Constructors

		public DesignerPurse ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check for money
			if ( GameManager.Run.Money > 0 )
			{
				// Apply applause
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = GetApplauseFromMoney ( GameManager.Run.Money )
				};
			}

			// Return that no applause was earned
			return base.OnApplause ( model, total );
		}

		public override int GetIntScaleValue ( int current )
		{
			// Get applause for money
			return GetApplauseFromMoney ( GameManager.Run.Money );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for money
			return GetApplauseFromMoney ( GameManager.Run.Money );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of applause earned from an amount of money.
		/// </summary>
		/// <param name="money"> The amount of money the player has. </param>
		/// <returns> The amount of applause earned. </returns>
		private int GetApplauseFromMoney ( int money )
		{
			return money * 20;
		}

		#endregion // Private Functions
	}
}