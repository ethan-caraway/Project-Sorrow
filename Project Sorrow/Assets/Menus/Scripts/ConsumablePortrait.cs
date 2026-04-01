using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a consumable in the consumable collection select menu.
	/// </summary>
	public class ConsumablePortrait : MonoBehaviour
	{
		#region UI Element

		[SerializeField]
		private GameObject portraitContainer;

		[SerializeField]
		private HUD.ConsumableDisplay consumableDisplay;

		[SerializeField]
		private Image borderImage;

		[SerializeField]
		private RawImage rarityImage;

		#endregion // UI Element

		#region Consumable Data

		[SerializeField]
		private Color32 selectedColor;

		[SerializeField]
		private Color32 undiscoveredColor;

		private Consumables.ConsumableScriptableObject consumable;
		private bool isCurrentlySelected;

		#endregion // Consumable Data

		#region Public Properties

		/// <summary>
		/// The data of the consumable being portrayed.
		/// </summary>
		public Consumables.ConsumableScriptableObject Consumable
		{
			get
			{
				return consumable;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the consumable.
		/// </summary>
		/// <param name="id"> The ID of the consumable to display. </param>
		public void SetConsumable ( int id )
		{
			// Get consumable data
			consumable = Consumables.ConsumableUtility.GetConsumable ( id );

			// Check if consumable is available
			if ( consumable != null )
			{
				// Display consumable
				portraitContainer.SetActive ( true );
				consumableDisplay.SetConsumable ( consumable, 1, Progression.ProgressManager.Progress.GetConsumableStats ( consumable.ID ).IsDiscovered );
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
			// Check for consumable
			if ( consumable != null )
			{
				// Store selection
				isCurrentlySelected = isSelected;

				// Get unselected color
				Color unselectedColor = Progression.ProgressManager.Progress.GetConsumableStats ( consumable.ID ).IsDiscovered ? Utils.GetRarityColor ( consumable.Rarity ) : undiscoveredColor;

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
				bool isDiscovered = Progression.ProgressManager.Progress.GetConsumableStats ( consumable.ID ).IsDiscovered;

				// Get unhighlighted color
				Color highlightedColor = isDiscovered ? Utils.GetRarityAltColor ( consumable.Rarity ) : selectedColor;

				// Get unhighlighted color
				Color unhighlightedColor = isDiscovered ? Utils.GetRarityColor ( consumable.Rarity ) : undiscoveredColor;

				// Set highlight color
				borderImage.color = isHovered ? highlightedColor : unhighlightedColor;
				rarityImage.color = isHovered && isDiscovered ? highlightedColor : rarityImage.color;
			}
		}

		#endregion // Public Functions
	}
}