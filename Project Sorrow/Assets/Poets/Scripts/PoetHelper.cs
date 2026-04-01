namespace FlightPaper.ProjectSorrow.Poets
{
	/// <summary>
	/// This class contains functions for accessing poets.
	/// </summary>
	public static class PoetHelper
	{
		#region Poet Data Constants

		private const int PROFESSOR_ID = 1;
		private const int LIT_BRO_ID = 2;
		private const int NOVELIST_ID = 3;
		private const int FEMINIST_ID = 4;
		private const int NEPO_BABY_ID = 5;
		private const int POET_LAUREATE_ID = 6;
		private const int BEATNIK_ID = 7;
		private const int IVY_LEAGUE_STUDENT_ID = 8;
		private const int TWINS_ID = 9;
		private const int BIKER_ID = 10;
		private const int MFA_STUDENT_ID = 11;
		private const int PERFORMANCE_ARTIST_ID = 12;

		#endregion // Poet Data Constants

		#region Public Functions
		
		/// <summary>
		/// Gets whether or not a given poet is unlocked to the player.
		/// </summary>
		/// <param name="id"> The ID of the poet. </param>
		/// <returns> Whether or not the poet is unlocked. </returns>
		public static bool IsUnlocked ( int id )
		{
			// Check if all poets are unlocked
			if ( Progression.ProgressManager.Progress.IsAllUnlocked )
			{
				return true;
			}

			// Check ID
			switch ( id )
			{
				case PROFESSOR_ID:
					return IsProfessorUnlocked ( );

				case LIT_BRO_ID:
					return IsLitBroUnlocked ( );

				case NOVELIST_ID:
					return IsNovelistUnlocked ( );

				case FEMINIST_ID:
					return IsFeministUnlocked ( );

				case NEPO_BABY_ID:
					return IsNepoBabyUnlocked ( );

				case POET_LAUREATE_ID:
					return IsPoetLaureateUnlocked ( );

				case BEATNIK_ID:
					return IsBeatnikUnlocked ( );

				case IVY_LEAGUE_STUDENT_ID:
					return IsIvyLeagueStudentUnlocked ( );

				case TWINS_ID:
					return IsTwinsUnlocked ( );

				case BIKER_ID:
					return IsBikerUnlocked ( );

				case MFA_STUDENT_ID:
					return IsMFAStudentUnlocked ( );

				case PERFORMANCE_ARTIST_ID:
					return IsPerformanceArtistUnlocked ( );
			}

			// Return that the poet is not unlocked by default
			return false;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not the Professor poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsProfessorUnlocked ( )
		{
			// Unlock by default
			return true;
		}

		/// <summary>
		/// Gets whether or not the Lit Bro poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsLitBroUnlocked ( )
		{
			// Check for a record of at least 8 confidence remaining
			return Progression.ProgressManager.Progress.ChallengeStats.MostConfidenceRemaining >= 8;
		}

		/// <summary>
		/// Gets whether or not the Novelist poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsNovelistUnlocked ( )
		{
			// Check for a record of a least 6 minutes start time
			return Progression.ProgressManager.Progress.ChallengeStats.MostStartTime >= 6f * 60f;
		}

		/// <summary>
		/// Gets whether or not the Feminist poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsFeministUnlocked ( )
		{
			// Check for 50 successful performances of poems by women
			return Progression.ProgressManager.Progress.ChallengeStats.SuccessfulPerformancesByWomen >= 50;
		}

		/// <summary>
		/// Gets whether or not the Nepo Baby poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsNepoBabyUnlocked ( )
		{
			// Check for a record of owning at least $100
			return Progression.ProgressManager.Progress.ChallengeStats.MostMoney >= 100;
		}

		/// <summary>
		/// Gets whether or not the Poet Laureate poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsPoetLaureateUnlocked ( )
		{
			// Check for a record of earning at least $10 in interest
			return Progression.ProgressManager.Progress.ChallengeStats.MostInterest >= 10;
		}

		/// <summary>
		/// Gets whether or not the Beatnik poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsBeatnikUnlocked ( )
		{
			// Check less than 6 items owned for a win
			return Progression.ProgressManager.Progress.ChallengeStats.LeastItemsWin < 6;
		}

		/// <summary>
		/// Gets whether or not the Ivy League Student poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsIvyLeagueStudentUnlocked ( )
		{
			// Check for win on Prestige III difficulty
			return Progression.ProgressManager.Progress.HighestDifficultyWin >= 3;
		}

		/// <summary>
		/// Gets whether or not the Twins poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsTwinsUnlocked ( )
		{
			// Check for win on Prestige IV difficulty
			return Progression.ProgressManager.Progress.HighestDifficultyWin >= 4;
		}

		/// <summary>
		/// Gets whether or not the Biker poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsBikerUnlocked ( )
		{
			// Check for 300 or more snaps from confidence remaining
			return Progression.ProgressManager.Progress.ChallengeStats.MostSnapsFromConfidenceRemaining >= 300;
		}

		/// <summary>
		/// Gets whether or not the MFA Student poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsMFAStudentUnlocked ( )
		{
			// Check for highest round
			for ( int i = 0; i < Progression.ProgressManager.Progress.PoetStats.Length; i++ )
			{
				// Check for at least 2 successful rounds
				if ( Progression.ProgressManager.Progress.PoetStats [ i ].HighestRound > 2 )
				{
					// Return that the poet is unlocked
					return true;
				}
			}

			// Return that the poet is not unlocked
			return false;
		}

		/// <summary>
		/// Gets whether or not the Performance Artist poet is unlocked.
		/// </summary>
		/// <returns> Whether or not the poet is unlocked. </returns>
		private static bool IsPerformanceArtistUnlocked ( )
		{
			// Check for 300 or more snaps from confidence remaining
			return Progression.ProgressManager.Progress.ChallengeStats.MostLinesWithoutFlubbing >= 30;
		}

		#endregion // Private Functions
	}
}