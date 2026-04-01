using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a poet in the character select menu.
	/// </summary>
	public class PoetPortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private Image highlightImage;

		[SerializeField]
		private Image portraitImage;

		#endregion // UI Element

		#region Poet Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 highlightColor;

		private Poets.PoetScriptableObject poet;

		#endregion // Poet Data

		#region Public Properties

		/// <summary>
		/// The data of the poet being portrayed.
		/// </summary>
		public Poets.PoetScriptableObject Poet
		{
			get
			{
				return poet;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the poet.
		/// </summary>
		/// <param name="id"> The ID of the poet. </param>
		public void SetPoet ( int id )
		{
			// Get poet data
			poet = Poets.PoetUtility.GetPoet ( id );

			// Hide highlight
			highlightImage.gameObject.SetActive ( false );

			// Check if poet is available
			if ( poet != null )
			{
				// Display poet
				portraitContainer.SetActive ( true );
				portraitImage.sprite = poet.Icon;

				// Check if unlocked
				if ( poet.IsUnlocked )
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
			// Set selected color
			highlightImage.color = selectedColor;

			// Enable/disable highlight
			highlightImage.gameObject.SetActive ( isSelected );
		}

		/// <summary>
		/// Toggles whether or not the highlight is visible.
		/// </summary>
		/// <param name="isHighlighted"> Whether or not this portrait is highlighted. </param>
		public void ToggleHighlight ( bool isHighlighted )
		{
			// Set highlight color
			highlightImage.color = highlightColor;

			// Enable/disable highlight
			highlightImage.gameObject.SetActive ( isHighlighted );
		}

		#endregion // Public Functions
	}
}