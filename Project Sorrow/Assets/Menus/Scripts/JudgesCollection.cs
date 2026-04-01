using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the judges tab in the collections menu.
	/// </summary>
	public class JudgesCollection : MonoBehaviour
	{
		#region Judges Data Constants

		private const int TOTAL_PAGES = 2;

		#endregion // Judges Data Constants

		#region UI Elements

		[SerializeField]
		private JudgePortrait [ ] judgePortraits;

		[SerializeField]
		private TMP_Text pageText;

		[SerializeField]
		private Button prevButton;

		[SerializeField]
		private Button nextButton;

		[SerializeField]
		private Image judgeImage;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text performanceStatText;

		[SerializeField]
		private TMP_Text successStatText;

		[SerializeField]
		private TMP_Text scoreStatText;

		[SerializeField]
		private GameObject undiscoveredContainer;

		#endregion // UI Elements

		#region Judge Data

		private int portraitIndex;
		private int pageIndex;

		#endregion // Judge Data

		#region Public Functions

		/// <summary>
		/// Initializes the judge collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display the first page of judge
			DisplayPage ( 0 );
		}

		/// <summary>
		/// Selects a given judge.
		/// </summary>
		/// <param name="index"> The index of the judge portrait. </param>
		public void SelectJudge ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < judgePortraits.Length; i++ )
			{
				judgePortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected item
			DisplayJudge ( index );
		}

		/// <summary>
		/// Previews a given judge.
		/// </summary>
		/// <param name="index"> The index of the judge portrait. </param>
		public void PreviewJudge ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				judgePortraits [ index ].ToggleHover ( true );

				// Display previewed judge
				DisplayJudge ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given judge.
		/// </summary>
		/// <param name="index"> The index of the judge portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				judgePortraits [ index ].ToggleHover ( false );

				// Display selected judge
				DisplayJudge ( portraitIndex );
			}
		}

		/// <summary>
		/// Display the previous page of judges.
		/// </summary>
		public void PrevPage ( )
		{
			// Load page
			DisplayPage ( pageIndex - 1 );
		}

		/// <summary>
		/// Display the next page of judges.
		/// </summary>
		public void NextPage ( )
		{
			// Load page
			DisplayPage ( pageIndex + 1 );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the judges for a given page.
		/// </summary>
		/// <param name="index"> The index of the page. </param>
		private void DisplayPage ( int index )
		{
			// Store page
			pageIndex = index;

			// Display each judge
			for ( int i = 0; i < judgePortraits.Length; i++ )
			{
				// Display judge
				judgePortraits [ i ].SetJudge ( i + 1 + ( index * judgePortraits.Length ) );
			}

			// Display page
			pageText.text = $"{index + 1}/{TOTAL_PAGES}";
			prevButton.interactable = index > 0;
			nextButton.interactable = index + 1 < TOTAL_PAGES;

			// Select first judge on the page
			SelectJudge ( 0 );
		}

		/// <summary>
		/// Displays the data for given judge.
		/// </summary>
		/// <param name="index"> The index of the judge portrait. </param>
		private void DisplayJudge ( int index )
		{
			// Get judge
			Judges.JudgeScriptableObject judge = judgePortraits [ index ].Judge;

			// Display judge
			judgeImage.sprite = judge.Icon;

			// Get stats
			Progression.JudgeStatsModel stats = Progression.ProgressManager.Progress.GetJudgeStats ( judge.ID );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display judge
				judgeImage.color = Color.white;

				// Display judge info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = judge.Title;
				descriptionText.text = judge.Description;

				// Display stats
				performanceStatText.text = stats.Performances.ToString ( );
				successStatText.text = stats.Successes.ToString ( );
				scoreStatText.text = stats.HighScore.ToString ( );
			}
			else
			{
				// Display silhouette
				judgeImage.color = Color.black;

				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		#endregion // Private Functions
	}
}