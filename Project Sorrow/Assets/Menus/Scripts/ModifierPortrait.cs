using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a modifier in the modifier collection select menu.
	/// </summary>
	public class ModifierPortrait : MonoBehaviour
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

		private Enums.WordModifierType modifier;
		private bool isCurrentlySelected;

		#endregion // Modifier Data

		#region Public Properties

		/// <summary>
		/// The modifier being portrayed.
		/// </summary>
		public Enums.WordModifierType Modifier
		{
			get
			{
				return modifier;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the modifier.
		/// </summary>
		/// <param name="wordModifier"> The modifier to display. </param>
		public void SetModifier ( Enums.WordModifierType wordModifier )
		{
			// Get modifier data
			modifier = wordModifier;
			Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( modifier.ToID ( ) );

			// Check if modifier is available
			if ( tag != null )
			{
				// Display upgrade
				portraitContainer.SetActive ( true );
				highlightImage.color = unselectedColor;

				// Check if discovered
				if ( Progression.ProgressManager.Progress.GetModifierStats ( (int)modifier ).IsDiscovered )
				{
					// Display modifier name
					titleText.text = tag.Title.Substring ( "Word Modifier:  ".Length );
				}
				else
				{
					// Display question marks
					titleText.text = "???";
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

			// Get hover color
			Color color = unselectedColor;
			if ( isSelected )
			{
				color = Progression.ProgressManager.Progress.GetModifierStats ( (int)modifier ).IsDiscovered ? Utils.GetModifierColor ( modifier ) : selectedColor;
			}

			// Set selected color
			highlightImage.color = color;
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
				// Get hover color
				Color hoverColor = unselectedColor;
				if ( isHovered )
				{
					hoverColor = Progression.ProgressManager.Progress.GetModifierStats ( (int)modifier ).IsDiscovered ? Utils.GetModifierColor ( modifier ) : selectedColor;
				}

				// Set highlight color
				highlightImage.color = hoverColor;
			}
		}

		#endregion // Public Functions
	}
}