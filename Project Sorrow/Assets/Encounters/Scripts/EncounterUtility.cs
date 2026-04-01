using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for encounters.
	/// </summary>
	public static class EncounterUtility
	{
		#region Encounter Data Constants

		private const string ENCOUNTER_LABEL = "Encounter";

		#endregion // Encounter Data Constants

		#region Encounter Data

		private static AsyncOperationHandle<IList<EncounterScriptableObject>> handler;

		private static readonly Dictionary<int, EncounterScriptableObject> encounters = new Dictionary<int, EncounterScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Encounter Data

		#region Public Properties

		/// <summary>
		/// Whether or not the encounter addressables have been loaded.
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
		/// Asynchrously loads the encounter scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			encounters.Clear ( );
			isLoaded = false;

			// Load encounters from addressables
			handler = Addressables.LoadAssetsAsync<EncounterScriptableObject> ( ENCOUNTER_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store encounters
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store encounter
					EncounterScriptableObject encounter = handler.Result [ i ];
					encounters.Add ( encounter.ID, encounter );
				}
			}

			// Return whether or not the encounters were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for an encounter of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the encounter. </param>
		/// <returns> The encounter scriptable object. </returns>
		public static EncounterScriptableObject GetEncounter ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( encounters.ContainsKey ( id ) )
				{
					// Return encounter
					return encounters [ id ];
				}
			}

			// Return that no encounter data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random encounter of the given round and rarity excluding a list of given encounter IDs.
		/// </summary>
		/// <param name="round"> The current round of the performance. </param>
		/// <param name="excludes"> The encounter IDs that should be excluded. </param>
		/// <returns> The encounter scriptable object. </returns>
		public static EncounterScriptableObject GetEncounterByRound ( int round, int [ ] excludes )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Get encounters
				List<EncounterScriptableObject> encounterList = encounters.Values.ToList ( );

				// Remove any encounters that do not meet the minimum round requirement
				encounterList.RemoveAll ( x => x.MinRound > round );

				// Remove any excluded IDs
				if ( excludes != null )
				{
					for ( int i = 0; i < excludes.Length; i++ )
					{
						// Check for valid ID
						if ( excludes [ i ] > 0 )
						{
							encounterList.RemoveAll ( x => x.ID == excludes [ i ] );
						}
					}
				}

				// Check for remaining encounters
				if ( encounterList.Count > 0 )
				{
					// Return a random encounter
					return encounterList [ Random.Range ( 0, encounterList.Count ) ];
				}
			}

			// Return that no encounter data was found
			return null;
		}

		#endregion // Public Functions
	}
}