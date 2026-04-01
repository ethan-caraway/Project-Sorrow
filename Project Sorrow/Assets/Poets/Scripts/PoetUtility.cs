using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Poets
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for poets.
	/// </summary>
	public static class PoetUtility
	{
		#region Poet Data Constants

		private const string POET_LABEL = "Poet";

		#endregion // Poet Data Constants

		#region Poet Data

		private static AsyncOperationHandle<IList<PoetScriptableObject>> handler;

		private static readonly Dictionary<int, PoetScriptableObject> poets = new Dictionary<int, PoetScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Poet Data

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
		/// Asynchrously loads the poet scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			poets.Clear ( );
			isLoaded = false;

			// Load consumables from addressables
			handler = Addressables.LoadAssetsAsync<PoetScriptableObject> ( POET_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store poets
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store poet
					PoetScriptableObject poet = handler.Result [ i ];
					poets.Add ( poet.ID, poet );
				}
			}

			// Return whether or not the consumables were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a poet of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the poet. </param>
		/// <returns> The poet scriptable object. </returns>
		public static PoetScriptableObject GetPoet ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( poets.ContainsKey ( id ) )
				{
					// Return poet
					return poets [ id ];
				}
			}

			// Return that no poet data was found
			return null;
		}

		#endregion // Public Functions
	}
}