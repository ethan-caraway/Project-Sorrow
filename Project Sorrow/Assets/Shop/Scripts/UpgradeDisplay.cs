using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the display of an upgrade.
	/// </summary>
	public class UpgradeDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Display the information for an upgrade.
		/// </summary>
		/// <param name="upgrade"> The data for the upgrade. </param>
		public void SetUpgrade ( Upgrades.UpgradeScriptableObject upgrade )
		{
			// Display icon
			iconImage.sprite = upgrade.Icon;

			// Display title
			titleText.text = upgrade.Title;

			// Display description
			descriptionText.text = upgrade.Description;
		}

		#endregion // Public Functions
	}
}