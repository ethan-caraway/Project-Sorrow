using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the items tab in the collections menu.
	/// </summary>
	public class ItemsCollection : MonoBehaviour
	{
		#region Item Data Constants

		private const int TOTAL_PAGES = 6;

		#endregion // Item Data Constants

		#region UI Elements

		[SerializeField]
		private ItemPortrait [ ] itemPortraits;

		[SerializeField]
		private TMP_Text pageText;

		[SerializeField]
		private Button prevButton;

		[SerializeField]
		private Button nextButton;

		[SerializeField]
		private HUD.ItemDisplay itemDisplay;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text ownStatText;

		[SerializeField]
		private TMP_Text winStatText;

		[SerializeField]
		private TMP_Text difficultyStatText;

		[SerializeField]
		private GameObject undiscoveredContainer;

		#endregion // UI Elements

		#region Item Data

		[SerializeField]
		private Color undiscoveredColor;

		private int portraitIndex;
		private int pageIndex;

		#endregion // Item Data

		#region Public Functions

		/// <summary>
		/// Initializes the item collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display the first page of items
			DisplayPage ( 0 );
			DisplayItem ( 0 );
		}

		/// <summary>
		/// Selects a given item.
		/// </summary>
		/// <param name="index"> The index of the item portrait. </param>
		public void SelectItem ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < itemPortraits.Length; i++ )
			{
				itemPortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected item
			//DisplayItem ( index );
		}

		/// <summary>
		/// Previews a given item.
		/// </summary>
		/// <param name="index"> The index of the item portrait. </param>
		public void PreviewItem ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				itemPortraits [ index ].ToggleHover ( true );

				// Display previewed item
				DisplayItem ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given item.
		/// </summary>
		/// <param name="index"> The index of the item portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				itemPortraits [ index ].ToggleHover ( false );

				// Display selected item
				DisplayItem ( portraitIndex );
			}
		}

		/// <summary>
		/// Display the previous page of items.
		/// </summary>
		public void PrevPage ( )
		{
			// Load page
			DisplayPage ( pageIndex - 1 );
		}

		/// <summary>
		/// Display the next page of items.
		/// </summary>
		public void NextPage ( )
		{
			// Load page
			DisplayPage ( pageIndex + 1 );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the items for a given page.
		/// </summary>
		/// <param name="index"> The index of the page. </param>
		private void DisplayPage ( int index )
		{
			// Store page
			pageIndex = index;

			// Display each item
			for ( int i = 0; i < itemPortraits.Length; i++ )
			{
				// Display item
				itemPortraits [ i ].SetItem ( i + 1 + ( index * itemPortraits.Length ) );
			}

			// Display page
			pageText.text = $"{index + 1}/{TOTAL_PAGES}";
			prevButton.interactable = index > 0;
			nextButton.interactable = index + 1 < TOTAL_PAGES;

			// Select first item on the page
			SelectItem ( 0 );
		}

		/// <summary>
		/// Displays the data for given item.
		/// </summary>
		/// <param name="index"> The index of the item portrait. </param>
		private void DisplayItem ( int index )
		{
			// Get item
			Items.ItemScriptableObject item = itemPortraits [ index ].Item;

			// Get stats
			Progression.ItemStatsModel stats = Progression.ProgressManager.Progress.GetItemStats ( item.ID );

			// Display item
			itemDisplay.gameObject.SetActive ( true );
			itemDisplay.SetItem ( item, string.Empty, stats.IsDiscovered );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display item info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = item.Title;

				// Check for variable description
				if ( item.IsVariableDescription )
				{
					descriptionText.text = Items.ItemHelper.GetItem ( item.ID, string.Empty ).GetWouldBeVariableDescription ( item.Description );
				}
				else
				{
					descriptionText.text = item.Description;
				}

				// Display stats
				ownStatText.text = stats.Owns.ToString ( );
				winStatText.text = stats.Wins.ToString ( );
				difficultyStatText.text = stats.HighestDifficultyWin > 0 ? $"Prestige {Utils.ToRomanNumeral ( stats.HighestDifficultyWin )}" : "N/A";
			}
			else
			{
				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		#endregion // Private Functions
	}
}