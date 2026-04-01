using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of an item in the collections item select menu.
	/// </summary>
	public class ItemPortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private HUD.ItemDisplay itemDisplay;

		[SerializeField]
		private Image borderImage;

		[SerializeField]
		private RawImage rarityImage;

		#endregion // UI Element

		#region Item Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 undiscoveredColor;

		private Items.ItemScriptableObject item;
		private bool isCurrentlySelected;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// The data of the item being portrayed.
		/// </summary>
		public Items.ItemScriptableObject Item
		{
			get
			{
				return item;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the item.
		/// </summary>
		/// <param name="id"> The ID of the item to display. </param>
		public void SetItem ( int id )
		{
			// Get item data
			item = Items.ItemUtility.GetItem ( id );

			// Check if item is available
			if ( item != null )
			{
				// Display item
				portraitContainer.SetActive ( true );
				itemDisplay.SetItem ( item, string.Empty, Progression.ProgressManager.Progress.GetItemStats ( item.ID ).IsDiscovered );
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
			// Check for item
			if ( item != null )
			{
				// Store selection
				isCurrentlySelected = isSelected;

				// Get unselected color
				Color unselectedColor = Progression.ProgressManager.Progress.GetItemStats ( item.ID ).IsDiscovered ? Utils.GetRarityColor ( item.Rarity ) : undiscoveredColor;

				// Set selected color
				borderImage.color = isSelected ? selectedColor : unselectedColor;
			}
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
				// Get whether or not the item is discovered
				bool isDiscovered = Progression.ProgressManager.Progress.GetItemStats ( item.ID ).IsDiscovered;

				// Get highlighted color
				Color highlightedColor = isDiscovered ? Utils.GetRarityAltColor ( item.Rarity ) : selectedColor;

				// Get unhighlighted color
				Color unhighlightedColor = isDiscovered ? Utils.GetRarityColor ( item.Rarity ) : undiscoveredColor;

				// Set highlight color
				borderImage.color = isHovered ? highlightedColor : unhighlightedColor;
				rarityImage.color = isHovered && isDiscovered ? highlightedColor : rarityImage.color;
			}
		}

		#endregion // Public Functions
	}
}