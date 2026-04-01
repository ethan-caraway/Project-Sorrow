namespace FlightPaper.ProjectSorrow.Difficulty
{
	/// <summary>
	/// This class contains functions for accessing difficulties.
	/// </summary>
	public static class DifficultyHelper
	{
		#region Public Functions

		/// <summary>
		/// Gets whether or not a given difficulty is unlocked by the player.
		/// </summary>
		/// <param name="id"> The ID of the difficulty. </param>
		/// <returns> Whether or not the difficulty is unlocked. </returns>
		public static bool IsUnlocked ( int id )
		{
			// Check if all difficulties are unlocked
			if ( Progression.ProgressManager.Progress.IsAllUnlocked )
			{
				return true;
			}

			// Get highest win
			int highestWin = 0;
			for ( int i = 0; i < Progression.ProgressManager.Progress.DifficultyStats.Length; i++ )
			{
				// Check for at least one win
				if ( Progression.ProgressManager.Progress.DifficultyStats [ i ].Wins > 0 )
				{
					highestWin++;
				}
				else
				{
					break;
				}
			}

			// Return whether or not the difficulty is unlocked
			return id <= highestWin + 1;
		}

		#endregion // Public Functions
	}
}