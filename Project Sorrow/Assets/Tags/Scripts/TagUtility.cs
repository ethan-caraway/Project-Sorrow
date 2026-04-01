using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Tags
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for tags.
	/// </summary>
	public static class TagUtility
	{
		#region Tag Data Constants

		private const string TAG_LABEL = "Tag";

		#endregion // Tag Data Constants

		#region Tag Data

		private static AsyncOperationHandle<IList<TagScriptableObject>> handler;

		private static readonly Dictionary<string, TagScriptableObject> tags = new Dictionary<string, TagScriptableObject> ( );
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
		/// Asynchrously loads the tag scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			tags.Clear ( );
			isLoaded = false;

			// Load tags from addressables
			handler = Addressables.LoadAssetsAsync<TagScriptableObject> ( TAG_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store tags
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store tag
					TagScriptableObject tag = handler.Result [ i ];
					tags.Add ( tag.ID, tag );
				}
			}

			// Return whether or not the consumables were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a tag of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the tag. </param>
		/// <returns> The tag scriptable object. </returns>
		public static TagScriptableObject GetTag ( string id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( tags.ContainsKey ( id ) )
				{
					// Return tag
					return tags [ id ];
				}
			}

			// Return that no tag data was found
			return null;
		}

		#endregion // Public Functions
	}
}