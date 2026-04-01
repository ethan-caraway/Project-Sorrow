namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Lunchbox item.
	/// </summary>
	public class Lunchbox : Item
	{
		#region Class Constructors

		public Lunchbox ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for consumables owned
			return GetApplausePerConsumables ( );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Return applause earned
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GetApplausePerConsumables ( )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerConsumables ( ) );
		}

		public override void OnAddAnyConsumable ( int count )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerConsumables ( ) );
		}

		public override void OnRemoveAnyConsumable ( )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerConsumables ( ) );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of applause earned from the amount of consumables owned.
		/// </summary>
		/// <returns> The amount of applause earned. </returns>
		private int GetApplausePerConsumables ( )
		{
			return GameManager.Run.ConsumableCount * 100;
		}

		#endregion // Private Functions
	}
}