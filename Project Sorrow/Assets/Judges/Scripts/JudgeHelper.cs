using System.Collections.Generic;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Judges
{
	/// <summary>
	/// This class contains functions for judge challenges.
	/// </summary>
	public static class JudgeHelper
	{
		#region Gym Coach Functions

		private const int GYM_COACH_ID = 1;

		/// <summary>
		/// Gets the ID of the Gym Coach judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetGymCoachId ( )
		{
			// Return ID
			return GYM_COACH_ID;
		}

		/// <summary>
		/// Gets the reduced time allowance for the Gym Coach judge.
		/// </summary>
		/// <param name="time"> The current time allowance for the performance. </param>
		/// <returns> The new time allowance. </returns>
		public static float GetGymCoachTime ( float time )
		{
			// Return the reduced time
			return time * 0.5f;
		}

		#endregion // Gym Coach Functions

		#region Public Speaker Functions

		private const int PUBLIC_SPEAKER_ID = 2;

		/// <summary>
		/// Gets the ID of the Public Speaker judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetPublicSpeakerId ( )
		{
			// Return ID
			return PUBLIC_SPEAKER_ID;
		}

		/// <summary>
		/// Gets the increased flub penalty for the Public Speaker judge.
		/// </summary>
		/// <param name="penalty"> The current flub penalty for the performance. </param>
		/// <returns> The new flub penalty. </returns>
		public static int GetPublicSpeakerFlubPenalty ( int penalty )
		{
			// Return the increased penalty
			return penalty - 50;
		}

		#endregion // Speechwriter Functions

		#region Parents Functions

		private const int PARENTS_ID = 3;

		/// <summary>
		/// Gets the ID of the Parents judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetParentsId ( )
		{
			// Return ID
			return PARENTS_ID;
		}

		/// <summary>
		/// Gets the reduced max confidence for the Parents judge.
		/// </summary>
		/// <param name="confidence"> The current max confidence for the performance. </param>
		/// <returns> The new max confidence. </returns>
		public static int GetParentsConfidence ( int confidence )
		{
			// Return the reduced confidence
			return confidence / 2;
		}

		#endregion // Parents Functions

		#region Literary Critic Functions

		private const int LITERARY_CRITIC_ID = 4;

		/// <summary>
		/// Gets the ID of the Literary Critic judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetLiteraryCriticId ( )
		{
			// Return ID
			return LITERARY_CRITIC_ID;
		}

		/// <summary>
		/// Gets the increased snaps goal for the Literary Critic judge.
		/// </summary>
		/// <param name="round"> The data for the round difficulty. </param>
		/// <returns> The new snaps goal. </returns>
		public static int GetLiteraryCriticSnapsGoal ( Difficulty.RoundDifficultyModel round )
		{
			// Return the increased snaps goal
			return round.StartingSnaps + ( round.SnapsIncrement * 11 );
		}

		#endregion // Literary Critic Functions

		#region The Minimalist Functions

		private const int MINIMALIST_ID = 5;

		/// <summary>
		/// Gets the ID of the Minimalist judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetMinimalistId ( )
		{
			// Return ID
			return MINIMALIST_ID;
		}

		/// <summary>
		/// Gets the poem to perform for the Minimalist judge.
		/// </summary>
		/// <returns> The ID of the poem. </returns>
		public static int GetMinimalistPoem ( )
		{
			// Return the ID of a 0 star poem
			return Poems.PoemUtility.GetPoemByRating ( 0 ).ID;
		}

		#endregion // The Minimalist Functions

		#region Business Man Functions

		private const int BUSINESS_MAN_ID = 6;

		/// <summary>
		/// Gets the ID of the Business Man judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetBusinessManId ( )
		{
			// Return ID
			return BUSINESS_MAN_ID;
		}

		/// <summary>
		/// Gets the monetary flub penalty for the Business Man judge.
		/// </summary>
		/// <returns> The new flub penalty. </returns>
		public static int GetBusinessManFlubPenalty ( )
		{
			// Return the increased penalty
			return -3;
		}

		#endregion // Business Man Functions

		#region Pretentious Snob Functions

		private const int PRETENTIOUS_SNOB_ID = 7;

		/// <summary>
		/// Gets the ID of the Pretentious Snob judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetPretentiousSnobId ( )
		{
			// Return ID
			return PRETENTIOUS_SNOB_ID;
		}

		/// <summary>
		/// Gets whether or not a penalty should be applied to a word for its length.
		/// </summary>
		/// <param name="length"> The length of the word. </param>
		/// <returns> Whether or not to apply a penalty. </returns>
		public static bool IsPretentiousSnobPenalty ( int length )
		{
			return length < 5;
		}

		/// <summary>
		/// Gets the snaps penalty for the Pretentious Snob judge.
		/// </summary>
		/// <param name="snaps"> The amount of snaps earned for the word. </param>
		/// <returns> The new flub penalty. </returns>
		public static int GetPretentiousSnobPenalty ( int snaps )
		{
			// Check for snaps earned
			if ( snaps < 1 )
			{
				// Return no penalty
				return 0;
			}

			// Return the increased penalty
			return ( snaps / 2 ) * -1;
		}

		#endregion // Pretentious Snob Functions

		#region Bestselling Author Functions

		private const int BESTSELLING_AUTHOR_ID = 8;

		/// <summary>
		/// Gets the ID of the Bestselling Author judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetBestsellingAuthorId ( )
		{
			// Return ID
			return BESTSELLING_AUTHOR_ID;
		}

		/// <summary>
		/// Gets whether or not a penalty should be applied to a word for its length.
		/// </summary>
		/// <param name="length"> The length of the word. </param>
		/// <returns> Whether or not to apply a penalty. </returns>
		public static bool IsBestsellingAuthorPenalty ( int length )
		{
			return length > 5;
		}

		/// <summary>
		/// Gets the snaps penalty for the Bestselling Author judge.
		/// </summary>
		/// <param name="snaps"> The amount of snaps earned for the word. </param>
		/// <returns> The new flub penalty. </returns>
		public static int GetBestsellingAuthorPenalty ( int snaps )
		{
			// Check for snaps earned
			if ( snaps < 1 )
			{
				// Return no penalty
				return 0;
			}

			// Return the increased penalty
			return snaps * -1;
		}

		#endregion // Bestselling Author Functions

		#region Censor Functions

		private const int CENSOR_ID = 9;

		/// <summary>
		/// Gets the ID of the Censor judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetCensorId ( )
		{
			// Return ID
			return CENSOR_ID;
		}

		/// <summary>
		/// Gets the amount of word modifiers for a performance for the Censor judge.
		/// </summary>
		/// <returns> The type for the word modifier. </returns>
		public static Enums.WordModifierType [ ] GetCensorWordModifier ( )
		{
			// Return one Redacted word per line
			return new Enums.WordModifierType [ ]
			{
				Enums.WordModifierType.REDACTED
			};
		}

		#endregion // Censor Functions

		#region The Imagist Functions

		private const int IMAGIST_ID = 10;

		/// <summary>
		/// Gets the ID of The Imagist judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetImagistId ( )
		{
			// Return ID
			return IMAGIST_ID;
		}

		/// <summary>
		/// Gets the amount of word modifiers for a performance for The Imagist judge.
		/// </summary>
		/// <returns> The type for the word modifiers. </returns>
		public static Enums.WordModifierType [ ] GetImagistWordModifier ( )
		{
			// Return two Highlighted word per line
			return new Enums.WordModifierType [ ]
			{
				Enums.WordModifierType.HIGHLIGHT,
				Enums.WordModifierType.HIGHLIGHT
			};
		}

		#endregion // The Imagist Functions

		#region Librarian Functions

		private const int LIBRARIAN_ID = 11;

		/// <summary>
		/// Gets the ID of the Librarian judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetLibrarianId ( )
		{
			// Return ID
			return LIBRARIAN_ID;
		}

		/// <summary>
		/// Gets the amount of applause removed for a performance for the Librarian judge.
		/// </summary>
		/// <param name="total"> The total amount of applause earned for the performance. </param>
		/// <returns> The applause removed for the performance. </returns>
		public static Performance.ApplauseModel GetLibrarianApplause ( int total )
		{
			// Return the applause penalty
			return new Performance.ApplauseModel
			{
				Applause = total / -2
			};
		}

		#endregion // Librarian Functions

		#region The Futurist Functions

		private const int FUTURIST_ID = 12;

		/// <summary>
		/// Gets the ID of The Futurist judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetFuturistId ( )
		{
			// Return ID
			return FUTURIST_ID;
		}

		/// <summary>
		/// Gets the amount of confidence allowed for a performance for The Futurist judge.
		/// </summary>
		/// <returns> The amount of confidence for the performance. </returns>
		public static int GetFuturistConfidence ( )
		{
			// Return the reduced confidence
			return 1;
		}

		/// <summary>
		/// Get the amount of time in seconds penalized for a flub during a performance for the The Futurist judge.
		/// </summary>
		/// <returns> The amount of time in seconds penalized. </returns>
		public static float GetFuturistFlubPenalty ( )
		{
			return 20f;
		}

		#endregion // The Futurist Functions

		#region The Afro-Surrealist Functions

		private const int AFRO_SURREALIST_ID = 13;

		/// <summary>
		/// Gets the ID of The Afro-Surrealist judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetAfroSurrealistId ( )
		{
			// Return ID
			return AFRO_SURREALIST_ID;
		}

		/// <summary>
		/// Gets the amount of time in seconds allowed for a performance for The Afro-Surrealist judge.
		/// </summary>
		/// <returns> The amount of time in seconds allowed for the performance. </returns>
		public static float GetAfroSurrealistTimeAllowance ( )
		{
			// Return the reduced time allowance
			return 5f;
		}

		/// <summary>
		/// Get the amount of time in seconds earned per character during a performance for the The Afro-Surrealist judge.
		/// </summary>
		/// <returns> The amount of time in seconds earned. </returns>
		public static float GetAfroSurrealistTimeEarned ( )
		{
			return 0.2f;
		}

		#endregion // The Afro-Surrealist Functions

		#region Historian Functions

		private const int HISTORIAN_ID = 14;

		/// <summary>
		/// Gets the ID of the Historian judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetHistorianId ( )
		{
			// Return ID
			return HISTORIAN_ID;
		}

		/// <summary>
		/// Gets the list of word modifiers by the line of the poem for the Historian judge.
		/// </summary>
		/// <returns> The list of word modifiers by the line. </returns>
		public static Dictionary<Poems.LineKey, List<Poems.WordModel>> GetHistorianModifiersByLine ( )
		{
			// Return no modifiers
			return null;
		}

		#endregion // Historian Functions

		#region Editor Functions

		private const int EDITOR_ID = 15;

		/// <summary>
		/// Gets the ID of the Editor judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetEditorId ( )
		{
			// Return ID
			return EDITOR_ID;
		}

		/// <summary>
		/// Gets the total snaps earned for a character for the Editor judge.
		/// </summary>
		/// <param name="total"> The total amount of snaps earned for the character. </param>
		/// <returns> The total amount of snaps earned. </returns>
		public static int GetEditorTotalSnaps ( int total )
		{
			// Return total snaps
			return total > 4 ? 4 : total;
		}

		#endregion // Editor Functions

		#region Health Inspector Functions

		private const int HEALTH_INSPECTOR_ID = 16;

		/// <summary>
		/// Gets the ID of the Health Inspector judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetHealthInspectorId ( )
		{
			// Return ID
			return HEALTH_INSPECTOR_ID;
		}

		#endregion // Health Inspector Functions

		#region Clown Functions

		private const int CLOWN_ID = 17;

		/// <summary>
		/// Gets the ID of the Clown judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetClownId ( )
		{
			// Return ID
			return CLOWN_ID;
		}

		/// <summary>
		/// Sets whether or not items are enabled for the Clown judge.
		/// </summary>
		/// <param name="isEnabled"> Whether or not the items are enabled. </param>
		public static void SetClownItemsEnabled ( bool isEnabled )
		{
			// Get item count
			int itemTotal = GameManager.Run.ItemCount;

			// Check for items
			if ( itemTotal > 0 )
			{
				// Get random item
				int index = Random.Range ( 0, itemTotal );
				
				// Set enabled/disabled for items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) )
					{
						// Set enabled/disabled
						GameManager.Run.ItemData [ i ].Item.IsEnabled = isEnabled || i != index;
					}
				}
			}
		}

		#endregion // Clown Functions

		#region Thespian Functions

		private const int THESPIAN_ID = 18;

		/// <summary>
		/// Gets the ID of the Thespian judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetThespianId ( )
		{
			// Return ID
			return THESPIAN_ID;
		}

		/// <summary>
		/// Gets the amount of additional snaps earned from lines or stanzas for the Thespian judge.
		/// </summary>
		/// <returns> The amount of additional snaps earned from lines or stanzas. </returns>
		public static int GetThespianLineSnaps ( )
		{
			// Return no additional snaps
			return 0;
		}

		#endregion // Thespian Functions

		#region Hermit Functions

		private const int HERMIT_ID = 19;

		/// <summary>
		/// Gets the ID of the Hermit judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetHermitId ( )
		{
			// Return ID
			return HERMIT_ID;
		}

		/// <summary>
		/// Gets the reduced reputation for the Hermit judge.
		/// </summary>
		/// <param name="reputation"> The current reputation for the performance. </param>
		/// <returns> The new reputation. </returns>
		public static int GetHermitReputation ( int reputation )
		{
			// Return the reduced reputation
			return reputation * 0;
		}

		#endregion // Hermit Functions

		#region Therapist Functions

		private const int THERAPIST_ID = 20;

		/// <summary>
		/// Gets the ID of the Therapist judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetTherapistId ( )
		{
			// Return ID
			return THERAPIST_ID;
		}

		/// <summary>
		/// Sets the status effects for a performance for the Therapist judge.
		/// </summary>
		public static void SetTherapistStatusEffects ( )
		{
			// Clear status effects
			GameManager.Run.StatusEffectData = new StatusEffects.StatusEffectModel [ RunModel.MAX_STATUS_EFFECTS ];
		}

		#endregion // Therapist Functions

		#region Celebrity Functions

		private const int CELEBRITY_ID = 21;

		/// <summary>
		/// Gets the ID of the Celebrity judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetCelebrityId ( )
		{
			// Return ID
			return CELEBRITY_ID;
		}

		/// <summary>
		/// Sets the status effects for a performance for the Celebrity judge.
		/// </summary>
		public static void SetCelebrityStatusEffects ( )
		{
			// Set status effects
			GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.ANXIOUS, 5 );
		}

		/// <summary>
		/// Gets the current number of words performed during a performance for the Celebrity judge.
		/// </summary>
		/// <returns> The data for the status effect to apply. </returns>
		public static StatusEffects.StatusEffectModel GetCelebrityWordCount ( int count )
		{
			// Check for every 10th word
			if ( count % 10 == 0 )
			{
				// Apply status effect
				return new StatusEffects.StatusEffectModel
				{
					Type = Enums.StatusEffectType.ANXIOUS,
					Count = 1
				};
			}

			// Return that no status effect was applied
			return null;
		}

		#endregion // Celebrity Functions

		#region Bartender Functions

		private const int BARTENDER_ID = 22;

		/// <summary>
		/// Gets the ID of the Bartender judge.
		/// </summary>
		/// <returns> The ID of the judge. </returns>
		public static int GetBartenderId ( )
		{
			// Return ID
			return BARTENDER_ID;
		}

		/// <summary>
		/// Sets the status effects for a performance for the Bartender judge.
		/// </summary>
		public static StatusEffects.StatusEffectModel GetBartenderStatusEffects ( )
		{
			// Set status effects
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.IMPAIRED,
				Count = 3
			};
		}

		#endregion // Bartender Functions
	}
}