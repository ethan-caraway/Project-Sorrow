namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Toolbox item.
	/// </summary>
	public class Toolbox : Item
	{
		#region Class Constructors

		public Toolbox ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override float GetWouldBeFloatScaleValue ( )
		{
			return GetMultiplier ( );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Get applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = (int)( total * ( GetMultiplier ( ) - 1f ) )
			};
		}

		public override void OnAddAnyItem ( )
		{
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, GetMultiplier ( ) );
		}

		public override void OnRemoveAnyItem ( )
		{
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, GetMultiplier ( ) );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Calculates the multiplier based on the number of utility items owned.
		/// </summary>
		/// <returns> The multiplier for the item. </returns>
		private float GetMultiplier ( )
		{
			// Store multiplier
			float multiplier = 1f;

			// Add to multiplier per utility item owned
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Get item
					ItemScriptableObject item = ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID );
					if ( item != null && item.IsUtility )
					{
						multiplier += 0.5f;
					}
				}
			}

			// Return multiplier
			return multiplier;
		}

		#endregion // Private Functions
	}
}