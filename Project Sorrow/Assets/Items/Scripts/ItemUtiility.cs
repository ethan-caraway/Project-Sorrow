using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for items.
	/// </summary>
	public static class ItemUtility
	{
		#region Item Data Constants

		private const string ITEM_LABEL = "Item";

		#endregion // Item Data Constants

		#region Item Data

		private static AsyncOperationHandle<IList<ItemScriptableObject>> handler;

		private static readonly Dictionary<int, ItemScriptableObject> items = new Dictionary<int, ItemScriptableObject> ( );
		private static readonly Dictionary<Enums.Rarity, List<ItemScriptableObject>> itemsByRarity = new Dictionary<Enums.Rarity, List<ItemScriptableObject>> ( );
		private static bool isLoaded = false;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// Whether or not the item addressables have been loaded.
		/// </summary>
		public static bool IsLoaded
		{
			get
			{
				return isLoaded;
			}
		}

		/// <summary>
		/// The percentage of completion for loading the addressables.
		/// </summary>
		public static float PercentageComplete
		{
			get
			{
				return handler.IsValid ( ) ? handler.PercentComplete : ( isLoaded ? 1f : 0f );
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Asynchrously loads the item scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			items.Clear ( );
			itemsByRarity.Clear ( );
			isLoaded = false;

			// Load items from addressables
			handler = Addressables.LoadAssetsAsync<ItemScriptableObject> ( ITEM_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store items
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store item
					ItemScriptableObject item = handler.Result [ i ];
					items.Add ( item.ID, item );
					
					// Check if rarity has been registered
					if ( !itemsByRarity.ContainsKey ( item.Rarity ) )
					{
						itemsByRarity.Add ( item.Rarity, new List<ItemScriptableObject> ( ) );
					}

					// Store item by rarity
					itemsByRarity [ item.Rarity ].Add ( item );
				}
			}

			// Return whether or not the items were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for an item of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The item scriptable object. </returns>
		public static ItemScriptableObject GetItem ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( items.ContainsKey ( id ) )
				{
					// Return item
					return items [ id ];
				}
			}

			// Return that no item data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random item of the given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item to return. </param>
		/// <returns> The item scriptable object. </returns>
		public static ItemScriptableObject GetItemByRarity ( Enums.Rarity rarity )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid rarity
				if ( itemsByRarity.ContainsKey ( rarity ) )
				{
					// Return a random item
					return itemsByRarity [ rarity ] [ Random.Range ( 0, itemsByRarity [ rarity ].Count ) ];
				}
			}

			// Return that no item data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random item of the given rarity excluding a list of given item IDs.
		/// </summary>
		/// <param name="rarity"> The rarity of the item to return. </param>
		/// <param name="excludes"> The item IDs that should be excluded. </param>
		/// <returns> The item scriptable object. </returns>
		public static ItemScriptableObject GetItemByRarity ( Enums.Rarity rarity, int [ ] excludes )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid rarity
				if ( itemsByRarity.ContainsKey ( rarity ) )
				{
					// Get items of the rarity
					List<ItemScriptableObject> itemList = new List<ItemScriptableObject> ( itemsByRarity [ rarity ] );

					// Remove any excluded IDs
					for ( int i = 0; i < excludes.Length; i++ )
					{
						// Check for valid ID
						if ( excludes [ i ] > 0 )
						{
							itemList.RemoveAll ( x => x.ID == excludes [ i ] );
						}
					}

					// Check for remaining items
					if ( itemList.Count > 0 )
					{
						// Return a random item
						return itemList [ Random.Range ( 0, itemList.Count ) ];
					}
				}
			}

			// Return that no item data was found
			return null;
		}

		#endregion // Public Functions
	}
}