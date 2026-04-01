using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the poet selection panel.
	/// </summary>
	public class PoetSelect : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private PoetPortrait [ ] portraits;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private Image perkImage;

		[SerializeField]
		private TMP_Text perkTitleText;

		[SerializeField]
		private TMP_Text perkDescriptionText;

		[SerializeField]
		private GameObject unlockContainer;

		[SerializeField]
		private TMP_Text unlockText;

		#endregion // UI Elements

		#region Poet Data

		private int poetIndex;

		#endregion // Poet Data

		#region Public Properties

		/// <summary>
		/// The data of the selected poet.
		/// </summary>
		public Poets.PoetScriptableObject SelectedPoet
		{
			get
			{
				return portraits [ poetIndex ].Poet;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the poet selection panel.
		/// </summary>
		public void Init ( )
		{
			// Setup poet portraits
			for ( int i = 0; i < portraits.Length; i++ )
			{
				portraits [ i ].SetPoet ( i + 1 );
			}

			// Select first poet by default
			SelectPoet ( 0 );
		}

		/// <summary>
		/// Selects a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet. </param>
		public void SelectPoet ( int index )
		{
			// Store poet
			poetIndex = index;

			// Update portraits
			for ( int i = 0; i < portraits.Length; i++ )
			{
				portraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected poet
			DisplayPoet ( index );
		}

		/// <summary>
		/// Previews a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet. </param>
		public void PreviewPoet ( int index )
		{
			// Check if selected
			if ( index != poetIndex )
			{
				// Highlight portrait
				portraits [ index ].ToggleHighlight ( true );

				// Display previewed poet
				DisplayPoet ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != poetIndex )
			{
				// Unhighlight portrait
				portraits [ index ].ToggleHighlight ( false );

				// Display selected poet
				DisplayPoet ( poetIndex );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the data for a given poet.
		/// </summary>
		/// <param name="index"> The index of the poet. </param>
		private void DisplayPoet ( int index )
		{
			// Check if unlocked
			if ( portraits [ index ].Poet.IsUnlocked )
			{
				// Display poet info
				infoContainer.SetActive ( true );
				unlockContainer.SetActive ( false );

				// Display poet title
				titleText.text = portraits [ index ].Poet.Title;

				// Diplay poet description
				descriptionText.text = portraits [ index ].Poet.Description;

				// Display perk info
				perkImage.sprite = portraits [ index ].Poet.Perk.Icon;
				perkTitleText.text = portraits [ index ].Poet.Perk.Title;
				perkDescriptionText.text = portraits [ index ].Poet.Perk.Description;
			}
			else
			{
				// Display unlock criteria
				infoContainer.SetActive ( false );
				unlockContainer.SetActive ( true );
				unlockText.text = portraits [ index ].Poet.UnlockCriteria;
			}
		}

		#endregion // Private Functions
	}
}