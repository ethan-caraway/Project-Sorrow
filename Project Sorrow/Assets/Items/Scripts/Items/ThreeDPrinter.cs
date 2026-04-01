using System.Collections.Generic;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the 3D Printer item.
	/// </summary>
	public class ThreeDPrinter : Item
	{
		#region Class Constructors

		public ThreeDPrinter ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Check for available slots
			if ( GameManager.Run.CanAddItem ( ) )
			{
				// Store items to exclude
				List<int> excludeIDs = new List<int> ( );

				// Check if duplicate items can appear
				bool canDuplicate = false;
				for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( GameManager.Run.IsValidUpgrade ( i ) )
					{
						// Trigger upgrade
						if ( GameManager.Run.UpgradeData [ i ].Upgrade.OnDuplicateItems ( ) )
						{
							// Store that duplicates can appear
							canDuplicate = true;
							break;
						}
					}
				}

				// Check if excludes need to be added
				if ( !canDuplicate )
				{
					// Exclude owned items
					for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( i ) )
						{
							excludeIDs.Add ( GameManager.Run.ItemData [ i ].ID );
						}
					}
				}
				else
				{
					// Prevent this item from creating a duplicate of itself
					excludeIDs.Add ( ID );
				}

				// Generate Common item
				ItemScriptableObject item = ItemUtility.GetItemByRarity ( Enums.Rarity.COMMON, excludeIDs.ToArray ( ) );

				// Store new item
				ItemModel itemData = GameManager.Run.AddItem ( item.ID );

				// Trigger item being added
				if ( itemData != null && itemData.IsValid ( ) )
				{
					itemData.Item.OnAdd ( null );
				}
			}
		}

		#endregion // Item Override Functions
	}
}