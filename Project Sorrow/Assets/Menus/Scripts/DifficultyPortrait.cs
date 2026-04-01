using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a difficulty in the difficulty collection select menu.
	/// </summary>
	public class DifficultyPortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private Image highlightImage;

		[SerializeField]
		private TMP_Text titleText;

		#endregion // UI Element

		#region Modifier Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 unselectedColor;

		private Difficulty.DifficultyScriptableObject difficulty;
		private bool isCurrentlySelected;

		#endregion // Modifier Data

		#region Public Properties

		/// <summary>
		/// The difficulty being portrayed.
		/// </summary>
		public Difficulty.DifficultyScriptableObject Difficulty
		{
			get
			{
				return difficulty;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the difficulty.
		/// </summary>
		/// <param name="difficultyData"> The data for the difficulty to display. </param>
		public void SetDifficulty ( Difficulty.DifficultyScriptableObject difficultyData )
		{
			// Get modifier data
			difficulty = difficultyData;

			// Check if difficulty is available
			if ( difficulty != null )
			{
				// Display upgrade
				portraitContainer.SetActive ( true );
				highlightImage.color = unselectedColor;
				titleText.text = difficulty.Title;
			}
			else
			{
				// Hide portrait
				portraitContainer.SetActive ( false );
			}
		}

		/// <summary>
		/// Toggles whether or not this portrait is selected.
		/// </summary>
		/// <param name="isSelected"> Whether or not this portrait is selected. </param>
		public void ToggleSelect ( bool isSelected )
		{
			// Store selection
			isCurrentlySelected = isSelected;

			// Set selected color
			highlightImage.color = isSelected ? selectedColor : unselectedColor;
		}

		/// <summary>
		/// Toggles whether or not this portrait is hovered over.
		/// </summary>
		/// <param name="isHovered"> Whether or not this portrait is hovered over. </param>
		public void ToggleHover ( bool isHovered )
		{
			// Check if selected
			if ( !isCurrentlySelected )
			{
				// Set highlight color
				highlightImage.color = isHovered ? selectedColor : unselectedColor;
			}
		}

		#endregion // Public Functions
	}
}