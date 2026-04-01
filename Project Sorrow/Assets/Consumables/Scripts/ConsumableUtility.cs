using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for consumables.
	/// </summary>
	public static class ConsumableUtility
	{
		#region Consumable Data Constants

		private const string CONSUMABLE_LABEL = "Consumable";

		#endregion // Consumable Data Constants

		#region Consumable Data

		private static AsyncOperationHandle<IList<ConsumableScriptableObject>> handler;

		private static readonly Dictionary<int, ConsumableScriptableObject> consumables = new Dictionary<int, ConsumableScriptableObject> ( );
		private static readonly Dictionary<Enums.Rarity, List<ConsumableScriptableObject>> consumablesByRarity = new Dictionary<Enums.Rarity, List<ConsumableScriptableObject>> ( );
		private static readonly Dictionary<Enums.Rarity, ConsumableScriptableObject> loans = new Dictionary<Enums.Rarity, ConsumableScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Consumable Data

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
		/// Asynchrously loads the consumable scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			consumables.Clear ( );
			consumablesByRarity.Clear ( );
			loans.Clear ( );
			isLoaded = false;

			// Load consumables from addressables
			handler = Addressables.LoadAssetsAsync<ConsumableScriptableObject> ( CONSUMABLE_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store consumables
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store consumable
					ConsumableScriptableObject consumable = handler.Result [ i ];
					consumables.Add ( consumable.ID, consumable );
					
					// Check if rarity has been registered
					if ( !consumablesByRarity.ContainsKey ( consumable.Rarity ) )
					{
						consumablesByRarity.Add ( consumable.Rarity, new List<ConsumableScriptableObject> ( ) );
					}

					// Store consumable by rarity
					consumablesByRarity [ consumable.Rarity ].Add ( consumable );

					// Check for loan
					if ( consumable.Type == Enums.ConsumableType.LOAN )
					{
						loans.Add ( consumable.Rarity, consumable );
					}
				}
			}

			// Return whether or not the consumables were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a consumable of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <returns> The consumable scriptable object. </returns>
		public static ConsumableScriptableObject GetConsumable ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( consumables.ContainsKey ( id ) )
				{
					// Return consumable
					return consumables [ id ];
				}
			}

			// Return that no consumable data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random consumable of the given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the consumable to return. </param>
		/// <param name="loanChance"> The chance to generate a loan specifically. A negative value will exclude the possibility of generating a loan. </param>
		/// <returns> The consumable scriptable object. </returns>
		public static ConsumableScriptableObject GetConsumableByRarity ( Enums.Rarity rarity, float loanChance )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for loan chance
				if ( loanChance > 0f && Random.Range ( 0f, 1f ) < loanChance )
				{
					// Return loan
					return loans [ rarity ];
				}

				// Check for valid rarity
				if ( consumablesByRarity.ContainsKey ( rarity ) )
				{
					// Get consumables by rarity
					List<ConsumableScriptableObject> consumablesList = new List<ConsumableScriptableObject> ( consumablesByRarity [ rarity ] );

					// Check if loans should be excluded
					if ( loanChance < 0f )
					{
						consumablesList.RemoveAll ( x => x.Type == Enums.ConsumableType.LOAN );
					}

					// Return a random consumable
					return consumablesList [ Random.Range ( 0, consumablesByRarity [ rarity ].Count ) ];
				}
			}

			// Return that no consumable data was found
			return null;
		}

		#endregion // Public Functions
	}
}