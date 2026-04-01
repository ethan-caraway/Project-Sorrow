using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.ProjectSorrow.Judges
{
	/// <summary>
	/// This static class controls loading and storing the scriptable objects for judges.
	/// </summary>
	public static class JudgeUtility
	{
		#region Judge Data Constants

		private const string JUDGE_LABEL = "Judge";

		#endregion // Judge Data Constants

		#region Judge Data

		private static AsyncOperationHandle<IList<JudgeScriptableObject>> handler;

		private static readonly Dictionary<int, JudgeScriptableObject> judges = new Dictionary<int, JudgeScriptableObject> ( );
		private static bool isLoaded = false;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// Whether or not the judge addressables have been loaded.
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
		/// Asynchrously loads the judge scriptable objects from addressables.
		/// </summary>
		/// <returns> Whether or not the addressables were succesfully loaded. </returns>
		public static async Task<bool> Load ( )
		{
			// Clear previous data
			judges.Clear ( );
			isLoaded = false;

			// Load judges from addressables
			handler = Addressables.LoadAssetsAsync<JudgeScriptableObject> ( JUDGE_LABEL, null );
			await handler.Task;

			// Check for successful load
			if ( handler.Status == AsyncOperationStatus.Succeeded )
			{
				// Mark successful load
				isLoaded = true;

				// Store judges
				for ( int i = 0; i < handler.Result.Count; i++ )
				{
					// Store judge
					JudgeScriptableObject judge = handler.Result [ i ];
					judges.Add ( judge.ID, judge );
				}
			}

			// Return whether or not the judges were successfully loaded
			return isLoaded;
		}

		/// <summary>
		/// Gets the data for a judge of a given ID.
		/// </summary>
		/// <param name="id"> The ID of the judge. </param>
		/// <returns> The judge scriptable object. </returns>
		public static JudgeScriptableObject GetJudge ( int id )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Check for valid id
				if ( judges.ContainsKey ( id ) )
				{
					// Return judge
					return judges [ id ];
				}
			}

			// Return that no judge data was found
			return null;
		}

		/// <summary>
		/// Gets the data for a random judge of the given round excluding a list of given judge IDs.
		/// </summary>
		/// <param name="round"> The current round of the performance. </param>
		/// <param name="excludes"> The judge IDs that should be excluded. </param>
		/// <returns> The judge scriptable object. </returns>
		public static JudgeScriptableObject GetJudgeByRound ( int round, int [ ] excludes )
		{
			// Check if data is loaded
			if ( isLoaded )
			{
				// Get judges
				List<JudgeScriptableObject> judgeList = judges.Values.ToList ( );

				// Remove any judges that do not meet the minimum round requirement
				judgeList.RemoveAll ( x => x.MinRound > round );

				// Remove any excluded IDs
				if ( excludes != null )
				{
					for ( int i = 0; i < excludes.Length; i++ )
					{
						// Check for valid ID
						if ( excludes [ i ] > 0 )
						{
							judgeList.RemoveAll ( x => x.ID == excludes [ i ] );
						}
					}
				}

				// Check for remaining judges
				if ( judgeList.Count > 0 )
				{
					// Return a random judge
					return judgeList [ Random.Range ( 0, judgeList.Count ) ];
				}
			}

			// Return that no judge data was found
			return null;
		}

		#endregion // Public Functions
	}
}