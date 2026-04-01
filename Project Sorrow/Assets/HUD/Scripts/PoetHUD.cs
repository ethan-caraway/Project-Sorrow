using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the poet display in the HUD.
	/// </summary>
	public class PoetHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private Image perkImage;

		[SerializeField]
		private TMP_Text perkTitleText;

		[SerializeField]
		private TMP_Text perkDescriptionText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Displays the data for the current poet.
		/// </summary>
		/// <param name="poet"> The data for the poet. </param>
		public void SetPoet ( Poets.PoetScriptableObject poet )
		{
			// Set the poet title
			titleText.text = poet.Title;

			// Set perk icon
			perkImage.sprite = poet.Perk.Icon;

			// Set perk title
			perkTitleText.text = poet.Perk.Title;

			// Set perk description
			perkDescriptionText.text = poet.Perk.Description;
		}

		#endregion // Public Functions
	}
}