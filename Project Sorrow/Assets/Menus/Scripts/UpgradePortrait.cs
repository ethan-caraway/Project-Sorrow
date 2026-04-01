using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of an upgrade in the upgrade collection select menu.
	/// </summary>
	public class UpgradePortrait : MonoBehaviour
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

		private Upgrades.UpgradeScriptableObject upgrade;
		private bool isCurrentlySelected;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// The data of the upgrade being portrayed.
		/// </summary>
		public Upgrades.UpgradeScriptableObject Upgrade
		{
			get
			{
				return upgrade;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Initializes the display of the upgrade.
		/// </summary>
		/// <param name="id"> The ID of the upgrade to display. </param>
		public void SetUpgrade ( int id )
		{
			// Get judge data
			upgrade = Upgrades.UpgradeUtility.GetUpgrade ( id );

			// Check if upgrade is available
			if ( upgrade != null )
			{
				// Display upgrade
				portraitContainer.SetActive ( true );
				portraitImage.sprite = upgrade.Icon;
				highlightImage.color = unselectedColor;

				// Check if discovered
				if ( Progression.ProgressManager.Progress.GetUpgradeStats ( upgrade.ID ).IsDiscovered )
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