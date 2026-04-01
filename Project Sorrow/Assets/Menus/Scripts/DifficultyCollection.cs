using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the difficulty tab in the collections menu.
	/// </summary>
	public class DifficultiesCollection : MonoBehaviour
	{
		#region Difficulty Data Constants

		private const int TOTAL_PAGES = 2;

		#endregion // Difficulty Data Constants

		#region UI Elements

		[SerializeField]
		private DifficultyPortrait [ ] difficultyPortraits;

		[SerializeField]
		private TMP_Text pageText;

		[SerializeField]
		private Button prevButton;

		[SerializeField]
		private Button nextButton;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text [ ] penaltyTexts;

		[SerializeField]
		private TMP_Text runStatText;

		[SerializeField]
		private TMP_Text winStatText;

		[SerializeField]
		private TMP_Text scoreStatText;

		[SerializeField]
		private GameObject lockedContainer;

		[SerializeField]
		private TMP_Text lockedTitleText;

		[SerializeField]
		private TMP_Text lockedCriteriaText;

		#endregion // UI Elements

		#region Difficulty Data

		[SerializeField]
		private Difficulty.DifficultyScriptableObject [ ] difficulties;

		private int portraitIndex;
		private int pageIndex;

		#endregion // Difficulty Data

		#region Public Functions

		/// <summary>
		/// Initializes the difficulty collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display the first page of difficulties
			DisplayPage ( 0 );
		}

		/// <summary>
		/// Selects a given difficulty.
		/// </summary>
		/// <param name="index"> The index of the difficulty portrait. </param>
		public void SelectDifficulty ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < difficultyPortraits.Length; i++ )
			{
				difficultyPortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected difficulty
			DisplayDifficulty ( index );
		}

		/// <summary>
		/// Previews a given difficulty.
		/// </summary>
		/// <param name="index"> The index of the difficulty portrait. </param>
		public void PreviewDifficulty ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				difficultyPortraits [ index ].ToggleHover ( true );

				// Display previewed difficulty
				DisplayDifficulty ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given difficulty.
		/// </summary>
		/// <param name="index"> The index of the difficulty portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				difficultyPortraits [ index ].ToggleHover ( false );

				// Display selected difficulty
				DisplayDifficulty ( portraitIndex );
			}
		}

		/// <summary>
		/// Display the previous page of difficulties.
		/// </summary>
		public void PrevPage ( )
		{
			// Load page
			DisplayPage ( pageIndex - 1 );
		}

		/// <summary>
		/// Display the next page of difficulties.
		/// </summary>
		public void NextPage ( )
		{
			// Load page
			DisplayPage ( pageIndex + 1 );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the difficulties for a given page.
		/// </summary>
		/// <param name="index"> The index of the page. </param>
		private void DisplayPage ( int index )
		{
			// Store page
			pageIndex = index;

			// Display each difficulty
			for ( int i = 0; i < difficultyPortraits.Length; i++ )
			{
				// Display difficulty
				difficultyPortraits [ i ].SetDifficulty ( difficulties [ i + ( index * difficultyPortraits.Length ) ] );
			}

			// Display page
			pageText.text = $"{index + 1}/{TOTAL_PAGES}";
			prevButton.interactable = index > 0;
			nextButton.interactable = index + 1 < TOTAL_PAGES;

			// Select first difficulty on the page
			SelectDifficulty ( 0 );
		}

		/// <summary>
		/// Displays the data for given difficulty.
		/// </summary>
		/// <param name="index"> The index of the difficulty portrait. </param>
		private void DisplayDifficulty ( int index )
		{
			// Get difficulty
			Difficulty.DifficultyScriptableObject difficulty = difficultyPortraits [ index ].Difficulty;

			// Get stats
			Progression.DifficultyStatsModel stats = Progression.ProgressManager.Progress.GetDifficultyStats ( difficulty.ID );

			// Check if unlocked
			if ( Difficulty.DifficultyHelper.IsUnlocked ( difficulty.ID ) )
			{
				// Display difficulty info
				infoContainer.SetActive ( true );
				lockedContainer.SetActive ( false );
				titleText.text = difficulty.Title;
				descriptionText.text = difficulty.Description;

				// Display penalties
				for ( int i = 0; i < penaltyTexts.Length; i++ )
				{
					// Check for penalty
					if ( i < difficulty.Penalties.Length )
					{
						penaltyTexts [ i ].gameObject.SetActive ( true );
						penaltyTexts [ i ].text = $"\u2022<indent=1em>{difficulty.Penalties [ i ]}";
					}
					else
					{
						penaltyTexts [ i ].gameObject.SetActive ( false );
					}
				}

				// Display stats
				runStatText.text = stats.Runs.ToString ( );
				winStatText.text = stats.Wins.ToString ( );
				scoreStatText.text = stats.HighScore.ToString ( );
			}
			else
			{
				// Display discovery info
				infoContainer.SetActive ( false );
				lockedContainer.SetActive ( true );
				lockedTitleText.text = difficulty.Title;
				lockedCriteriaText.text = difficulty.UnlockCriteria;
			}
		}

		#endregion // Private Functions
	}
}