using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a judge in the collections judge select menu.
	/// </summary>
	public class JudgePortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private Image highlightImage;

		[SerializeField]
		private Image portraitImage;

		#endregion // UI Element

		#region Judge Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 unselectedColor;

		private Judges.JudgeScriptableObject judge;
		private bool isCurrentlySelected;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// The data of the judge being portrayed.
		/// </summary>
		public Judges.JudgeScriptableObject Judge
		{
			get
			{
				return judge;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the judge.
		/// </summary>
		/// <param name="id"> The ID of the judge to display. </param>
		public void SetJudge ( int id )
		{
			// Get judge data
			judge = Judges.JudgeUtility.GetJudge ( id );

			// Check if judge is available
			if ( judge != null )
			{
				// Display judge
				portraitContainer.SetActive ( true );
				portraitImage.sprite = judge.Icon;
				highlightImage.color = unselectedColor;

				// Check if discovered
				if ( Progression.ProgressManager.Progress.GetJudgeStats ( judge.ID ).IsDiscovered )
				{
					// Display icon
					portraitImage.color = Color.white;
				}
				else
				{
					// Display silhouette
					portraitImage.color = Color.black;
				}
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