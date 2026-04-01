using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for poems.
	/// </summary>
	public static class PoemUtility
	{
		#region Poem Data Constants

		private const string POEM_LABEL = "Poem";

		#endregion // Poem Data Constants

		#region Poem Data

		private static AsyncOperationHandle<IList<PoemScriptableObject>> handler;

		private static readonly Dictionary<int, PoemScriptableObject> poems = new Dictionary<int, PoemScriptableObject> ( );
		private static readonly Dictionary<int, List<PoemScriptableObject>> poemsByRating = new Dictionary<int, List<PoemScriptableObject>> ( );
		private static bool isLoaded = false;

		#endregion // Poem Data

		#region Public Properties

		/// <summary>
		/// Whether or not the poem addressables have been loaded.
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
		/// Asynchrously loads the poem scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			poems.Clear ( );
			poemsByRating.Clear ( );
			isLoaded = false;

			// Load poems from addressables
			handler = Addressables.LoadAssetsAsync<PoemScriptableObject> ( POEM_LABEL, null );
			await handler.Task;
			
			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store poems
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store poem
					PoemScriptableObject poem = handler.Result [ i ];
					poems.Add ( poem.ID, poem );
					Debug.Log ( $"Poem: {poem.Title}, Stanzas: {poem.Stanzas.Length}, Lines: {poem.LineCount}, Count: {poem.CharacterCount}, Rating: {poem.Rating}" );

					// Check if rating has been registered
					if ( !poemsByRating.ContainsKey ( poem.Rating ) )
					{
						poemsByRating.Add ( poem.Rating, new List<PoemScriptableObject> ( ) );

					}

					// Store poem by rating
					poemsByRating [ poem.Rating ].Add ( poem );
				}
			}

			// Return whether or not the poems were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a poem of a given ID.
		/// </summary>
		/// <returns> The poem scriptable object. </returns>
		public static PoemScriptableObject GetPoem ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( poems.ContainsKey ( id ) )
				{
					// Return a poem
					return poems [ id ];
				}
			}

			// Return that no poem data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random poem of the given rating.
		/// </summary>
		/// <param name="rating"> The rating of the poem to return. </param>
		/// <returns> The poem scriptable object. </returns>
		public static PoemScriptableObject GetPoemByRating ( int rating )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid rating
				if ( poemsByRating.ContainsKey ( rating ) )
				{
					// Return a random poem
					return poemsByRating [ rating ] [ Random.Range ( 0, poemsByRating [ rating ].Count ) ];
				}
			}

			// Return that no poem data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random poem of the given rating excluding a list of given poem IDs.
		/// </summary>
		/// <param name="rating"> The rating of the poem to return. </param>
		/// <param name="excludes"> The poem IDs that should be excluded. </param>
		/// <returns> The poem scriptable object. </returns>
		public static PoemScriptableObject GetPoemByRating ( int rating, int [ ] excludes )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid rating
				if ( poemsByRating.ContainsKey ( rating ) )
				{
					// Get poems of the rarity
					List<PoemScriptableObject> poemList = new List<PoemScriptableObject> ( poemsByRating [ rating ] );

					// Remove any excluded IDs
					for ( int i = 0; i < excludes.Length; i++ )
					{
						// Check for valid ID
						if ( excludes [ i ] > 0 )
						{
							poemList.RemoveAll ( x => x.ID == excludes [ i ] );
						}
					}

					// Return a random poem
					return poemList [ Random.Range ( 0, poemList.Count ) ];
				}
			}

			// Return that no poem data was found
			return null;
		}

		#endregion // Public Functions
	}
}