namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Antique item.
	/// </summary>
	public class Antique : Item
	{
		#region Class Constructors

		public Antique ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			// Get applause for uncommon items owned
			return GetApplausePerUncommonItem ( );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Return applause earned
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GetApplausePerUncommonItem ( )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerUncommonItem ( ) );
		}

		public override void OnAddAnyItem ( )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerUncommonItem ( ) );
		}

		public override void OnRemoveAnyItem ( )
		{
			// Set applause
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, GetApplausePerUncommonItem ( ) );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets the amount of applause earned from the amount of Uncommon items owned.
		/// </summary>
		/// <returns> The amount of applause earned. </returns>
		private int GetApplausePerUncommonItem ( )
		{
			// Track total uncommon item
			int total = 0;
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Get item
					ItemScriptableObject item = ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID );

					// Check if uncommon
					if ( item.Rarity == Enums.Rarity.UNCOMMON )
					{
						total++;
					}
				}
			}

			// Return total applause earned from this item
			return total * 75;
		}

		#endregion // Private Functions
	}
}