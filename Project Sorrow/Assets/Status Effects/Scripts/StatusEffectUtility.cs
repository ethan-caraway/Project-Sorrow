using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.StatusEffects
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for status effects.
	/// </summary>
	public static class StatusEffectUtility
	{
		#region Status Effect Data Constants

		private const string STATUS_EFFECT_LABEL = "Status Effect";

		#endregion // Status Effect Data Constants

		#region Status Effect Data

		private static AsyncOperationHandle<IList<StatusEffectScriptableObject>> handler;

		private static readonly Dictionary<Enums.StatusEffectType, StatusEffectScriptableObject> statusEffects = new Dictionary<Enums.StatusEffectType, StatusEffectScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Status Effect Data

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
		/// Asynchrously loads the status effect scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			statusEffects.Clear ( );
			isLoaded = false;

			// Load status effects from addressables
			handler = Addressables.LoadAssetsAsync<StatusEffectScriptableObject> ( STATUS_EFFECT_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store status effects
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store status effects
					StatusEffectScriptableObject statusEffect = handler.Result [ i ];
					statusEffects.Add ( statusEffect.Type, statusEffect );
				}
			}

			// Return whether or not the status effects were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a status effect of a given type.
		/// </summary>
		/// <param name="type"> The type of the status effect. </param>
		/// <returns> The status effect scriptable object. </returns>
		public static StatusEffectScriptableObject GetStatusEffect ( Enums.StatusEffectType type )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( statusEffects.ContainsKey ( type ) )
				{
					// Return status effect
					return statusEffects [ type ];
				}
			}

			// Return that no status effect data was found
			return null;
		}

		#endregion // Public Functions
	}
}