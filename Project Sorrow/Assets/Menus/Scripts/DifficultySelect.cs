using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the difficulty selection panel.
	/// </summary>
	public class DifficultySelect : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Button decreaseButton;

		[SerializeField]
		private Button increaseButton;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private Image [ ] markers;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text [ ] penaltyTexts;

		[SerializeField]
		private GameObject unlockContainer;

		[SerializeField]
		private TMP_Text unlockText;

		#endregion // UI Elements

		#region Difficulty Data

		[SerializeField]
		private Difficulty.DifficultyScriptableObject [ ] difficulties;

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 unselectedColor;

		private int difficultyIndex;

		#endregion // Difficulty Data

		#region Public Properties

		/// <summary>
		/// The data for the selected difficulty.
		/// </summary>
		public Difficulty.DifficultyScriptableObject SelectedDifficulty
		{
			get
			{
				return difficulties [ difficultyIndex ];
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the difficulty panel.
		/// </summary>
		public void Init ( )
		{
			// Set default difficulty
			difficultyIndex = 0;

			// Display selected difficulty
			DisplayDifficulty ( difficultyIndex );
		}

		/// <summary>
		/// Decreases the selected difficulty
		/// </summary>
		public void DecreaseDifficulty ( )
		{
			// Decrease difficulty
			difficultyIndex--;

			// Display selected difficulty
			DisplayDifficulty ( difficultyIndex );
		}

		/// <summary>
		/// Increases the selected difficulty
		/// </summary>
		public void IncreaseDifficulty ( )
		{
			// Increase difficulty
			difficultyIndex++;

			// Display selected difficulty
			DisplayDifficulty ( difficultyIndex );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the information for the selected difficulty.
		/// </summary>
		/// <param name="index"> The index of the selected difficulty. </param>
		private void DisplayDifficulty ( int index )
		{
			// Enable/disable the decrease button
			decreaseButton.interactable = index > 0;

			// Enable/disable the increase button
			increaseButton.interactable = index < difficulties.Length - 1;

			// Display difficulty title
			titleText.text = difficulties [ index ].Title;

			// Display markers
			for ( int i = 0; i < markers.Length; i++ )
			{
				markers [ i ].color = i <= index ? selectedColor : unselectedColor;
			}

			// Check for unlock
			if ( Progression.ProgressManager.Progress.IsAllUnlocked || difficulties [ index ].ID <= Progression.ProgressManager.Progress.HighestDifficultyWin + 1 )
			{
				// Display difficulty info
				infoContainer.SetActive ( true );
				unlockContainer.SetActive ( false );

				// Display difficulty description
				descriptionText.text = difficulties [ index ].Description;

				// Display penalties
				for ( int i = 0; i < penaltyTexts.Length; i++ )
				{
					// Check for penalty
					if ( i < difficulties [ index ].Penalties.Length )
					{
						// Display penalty
						penaltyTexts [ i ].gameObject.SetActive ( true );
						penaltyTexts [ i ].text = $"\u2022<indent=1em>{difficulties [ index ].Penalties [ i ]}";
					}
					else
					{
						// Hide penalty
						penaltyTexts [ i ].gameObject.SetActive ( false );
					}
				}
			}
			else
			{
				// Display difficulty unlock info
				infoContainer.SetActive ( false );
				unlockContainer.SetActive ( true );

				// Display unlock criteria
				unlockText.text = difficulties [ index ].UnlockCriteria;
			}
			
		}

		#endregion // Private Functions
	}
}