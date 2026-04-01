using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the poets tab in the collections menu.
	/// </summary>
	public class PoetsCollection : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private PoetPortrait [ ] poetPortraits;

		[SerializeField]
		private Image poetImage;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private Image perkImage;

		[SerializeField]
		private TMP_Text perkTitleText;

		[SerializeField]
		private TMP_Text perkDescriptionText;

		[SerializeField]
		private Button [ ] tabButtons;

		[SerializeField]
		private GameObject [ ] tabContainers;

		[SerializeField]
		private TMP_Text runStatText;

		[SerializeField]
		private TMP_Text winStatText;

		[SerializeField]
		private TMP_Text difficultyStatText;

		[SerializeField]
		private TMP_Text performanceStatText;

		[SerializeField]
		private TMP_Text roundStatText;

		[SerializeField]
		private TMP_Text scoreStatText;

		[SerializeField]
		private TMP_Text bioText;

		[SerializeField]
		private GameObject lockedContainer;

		[SerializeField]
		private TMP_Text unlockCriteriaText;

		#endregion // UI Elements

		#region Poet Data

		private int portraitIndex;

		#endregion // Poet Data

		#region Public Functions

		/// <summary>
		/// Initializes the poet collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display each poet
			for ( int i = 0; i < poetPortraits.Length; i++ )
			{
				// Display poet
				poetPortraits [ i ].SetPoet ( i + 1 );
			}

			// Select first poet
			SelectPoet ( 0 );
		}

		/// <summary>
		/// Selects a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet portrait. </param>
		public void SelectPoet ( int index )
		{
			// Store poet
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < poetPortraits.Length; i++ )
			{
				poetPortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected poet
			DisplayPoet ( index );
		}

		/// <summary>
		/// Previews a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet portrait. </param>
		public void PreviewPoet ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				poetPortraits [ index ].ToggleHighlight ( true );

				// Display previewed poet
				DisplayPoet ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				poetPortraits [ index ].ToggleHighlight ( false );

				// Display selected poet
				DisplayPoet ( portraitIndex );
			}
		}

		/// <summary>
		/// Displays the selected info tab for the poet.
		/// </summary>
		/// <param name="index"> The index of the tab. </param>
		public void OpenTab ( int index )
		{
			// Display the tab
			for ( int i = 0; i < tabContainers.Length; i++ )
			{
				tabButtons [ i ].interactable = i != index;
				tabContainers [ i ].SetActive ( i == index );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the data for a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet portrait. </param>
		private void DisplayPoet ( int index )
		{
			// Get judge
			Poets.PoetScriptableObject poet = poetPortraits [ index ].Poet;

			// Display poet
			poetImage.sprite = poet.Icon;

			// Get stats
			Progression.PoetStatsModel stats = Progression.ProgressManager.Progress.GetPoetStats ( poet.ID );

			// Check if discovered
			if ( poet.IsUnlocked )
			{
				// Display poet
				poetImage.color = Color.white;

				// Display poet info
				infoContainer.SetActive ( true );
				lockedContainer.SetActive ( false );
				titleText.text = poet.Title;
				bioText.text = poet.Description;

				// Dispay perk info
				perkImage.sprite = poet.Perk.Icon;
				perkTitleText.text = poet.Perk.Title;
				perkDescriptionText.text = poet.Perk.Description;

				// Display stats
				runStatText.text = stats.Runs.ToString ( );
				winStatText.text = stats.Wins.ToString ( );
				difficultyStatText.text = stats.HighestDifficultyWin > 0 ? $"Prestige {Utils.ToRomanNumeral ( stats.HighestDifficultyWin )}" : "N/A";
				performanceStatText.text = stats.HighestPerformance.ToString ( );
				roundStatText.text = stats.HighestRound.ToString ( );
				scoreStatText.text = stats.HighScore.ToString ( );

				// Open stats tab by default
				OpenTab ( 0 );
			}
			else
			{
				// Display silhouette
				poetImage.color = Color.black;

				// Display discovery info
				infoContainer.SetActive ( false );
				lockedContainer.SetActive ( true );

				// Display unlock criteria
				unlockCriteriaText.text = poet.UnlockCriteria;
			}
		}

		#endregion // Private Functions
	}
}