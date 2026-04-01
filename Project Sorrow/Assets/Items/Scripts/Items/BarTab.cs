namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Bar Tab item.
	/// </summary>
	public class BarTab : Item
	{
		#region Class Constructors

		public BarTab ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check for debt
			if ( GameManager.Run.Debt < 0 )
			{
				// Apply applause
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = GetApplauseFromDebt ( GameManager.Run.Debt )
				};
			}

			// Return that the bonus was not applied
			return base.OnApplause ( model, total );
		}

		public override int GetIntScaleValue ( int current )
		{
			// Get applause for dept
			return GetApplauseFromDebt ( GameManager.Run.Debt );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for debt
			return GetApplauseFromDebt ( GameManager.Run.Debt );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of applause earned from an amount of debt.
		/// </summary>
		/// <param name="debt"> The amount of debt the player has. </param>
		/// <returns> The amount of applause earned. </returns>
		private int GetApplauseFromDebt ( int debt )
		{
			return ( debt * -1 ) * 20;
		}

		#endregion // Private Functions
	}
}