using System.Collections.Generic;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Free Items upgrade.
	/// </summary>
	public class FreeItems : Upgrade
	{
		#region Class Constructors

		public FreeItems ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnAdd ( )
		{
			// Get item stats
			int itemCount = GameManager.Run.ItemCount;
			int maxItems = GameManager.Run.MaxItems;

			// Get excluded item IDs
			List<int> excludeIDs = new List<int> ( );
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.ItemData [ i ] != null )
				{
					excludeIDs.Add ( GameManager.Run.ItemData [ i ].ID );
				}
			}

			// Generate items if there is room
			for ( int i = 0; i < 2 && itemCount < maxItems; i++ )
			{
				// Generate item
				Items.ItemScriptableObject item = GameManager.Run.RarityData.GenerateItem ( excludeIDs.ToArray ( ) );
				if ( item != null )
				{
					// Add item
					Items.ItemModel itemData = GameManager.Run.AddItem ( item.ID );
					excludeIDs.Add ( item.ID );
					itemCount++;

					// Trigger item
					if ( itemData != null && itemData.IsValid ( ) )
					{
						itemData.Item.OnAdd ( null );
					}
				}
			}

			return base.OnAdd ( );
		}

		#endregion // Upgrade Override Functions
	}
}