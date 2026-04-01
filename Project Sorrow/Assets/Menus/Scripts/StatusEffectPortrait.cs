using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a status effect in the status effect collection select menu.
	/// </summary>
	public class StatusEffectPortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private Image highlightImage;

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private TMP_Text titleText;

		#endregion // UI Element

		#region Status Effect Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 unselectedColor;

		private Enums.StatusEffectType statusEffect;
		private bool isCurrentlySelected;

		#endregion // Status Effect Data

		#region Public Properties

		/// <summary>
		/// The status effect being portrayed.
		/// </summary>
		public Enums.StatusEffectType StatusEffect
		{
			get
			{
				return statusEffect;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the status effect.
		/// </summary>
		/// <param name="type"> The status effect to display. </param>
		public void SetStatusEffect ( Enums.StatusEffectType type )
		{
			// Get status effect data
			statusEffect = type;
			StatusEffects.StatusEffectScriptableObject data = StatusEffects.StatusEffectUtility.GetStatusEffect ( type );
			Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( data.Tag );

			// Check if status effect is available
			if ( tag != null )
			{
				// Display status effect
				portraitContainer.SetActive ( true );
				highlightImage.color = unselectedColor;

				// Check if discovered
				if ( Progression.ProgressManager.Progress.GetStatusEffectStats ( (int)statusEffect ).IsDiscovered )
				{
					// Display icon
					iconImage.gameObject.SetActive ( true );
					iconImage.sprite = data.Icon;

					// Display display name
					titleText.text = tag.Title.Substring ( "Word Modifier:  ".Length );
				}
				else
				{
					// Hide icon
					iconImage.gameObject.SetActive ( false );

					// Display question marks
					titleText.text = "<color=#828282>???</color>";
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
				color = Progression.ProgressManager.Progress.GetStatusEffectStats ( (int)statusEffect ).IsDiscovered ? Utils.GetStatusEffectColor ( statusEffect ) : selectedColor;
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
					hoverColor = Progression.ProgressManager.Progress.GetStatusEffectStats ( (int)statusEffect ).IsDiscovered ? Utils.GetStatusEffectColor ( statusEffect ) : selectedColor;
				}

				// Set highlight color
				highlightImage.color = hoverColor;
			}
		}

		#endregion // Public Functions
	}
}