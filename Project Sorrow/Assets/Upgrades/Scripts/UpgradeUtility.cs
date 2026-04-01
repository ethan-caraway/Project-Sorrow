using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for upgrades.
	/// </summary>
	public static class UpgradeUtility
	{
		#region Upgrade Data Constants

		private const string UPGRADE_LABEL = "Upgrade";

		#endregion // Upgrade Data Constants

		#region Upgrade Data

		private static AsyncOperationHandle<IList<UpgradeScriptableObject>> handler;

		private static readonly Dictionary<int, UpgradeScriptableObject> upgrades = new Dictionary<int, UpgradeScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Upgrade Data

		#region Public Properties

		/// <summary>
		/// Whether or not the upgrade addressables have been loaded.
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
		/// Asynchrously loads the upgrade scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			upgrades.Clear ( );
			isLoaded = false;

			// Load upgrades from addressables
			handler = Addressables.LoadAssetsAsync<UpgradeScriptableObject> ( UPGRADE_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store upgrades
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store upgrade
					UpgradeScriptableObject upgrade = handler.Result [ i ];
					upgrades.Add ( upgrade.ID, upgrade );
				}
			}

			// Return whether or not the upgrades were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for an upgrade of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		/// <returns> The upgrade scriptable object. </returns>
		public static UpgradeScriptableObject GetUpgrade ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( upgrades.ContainsKey ( id ) )
				{
					// Return upgrade
					return upgrades [ id ];
				}
			}

			// Return that no upgrade data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random upgrade excluding a list of given upgrade IDs.
		/// </summary>
		/// <param name="excludes"> The upgrade IDs that should be excluded. </param>
		/// <returns> The upgrade scriptable object. </returns>
		public static UpgradeScriptableObject GetRandomUpgrade ( int [ ] excludes )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Get upgrade
				List<UpgradeScriptableObject> upgradeList = upgrades.Values.ToList ( );

				// Remove any excluded IDs
				if ( excludes != null )
				{
					for ( int i = 0; i < excludes.Length; i++ )
					{
						// Check for valid ID
						if ( excludes [ i ] > 0 )
						{
							upgradeList.RemoveAll ( x => x.ID == excludes [ i ] );
						}
					}
				}

				// Check for remaining upgrades
				if ( upgradeList.Count > 0 )
				{
					// Return a random upgrade
					return upgradeList [ Random.Range ( 0, upgradeList.Count ) ];
				}
			}

			// Return that no upgrade data was found
			return null;
		}

		#endregion // Public Functions
	}
}