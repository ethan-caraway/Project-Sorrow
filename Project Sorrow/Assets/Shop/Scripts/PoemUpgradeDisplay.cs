using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the display of a poem upgrade.
	/// </summary>
	public class PoemUpgradeDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Button upgradeButton;

		[SerializeField]
		private TMP_Text upgradeTitleText;

		[SerializeField]
		private TMP_Text upgradeDescriptionText;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text authorText;

		[SerializeField]
		private TMP_Text levelText;

		[SerializeField]
		private GameObject [ ] ratingImages;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Sets the upgrade to be acquired for a poem to be displayed.
		/// </summary>
		/// <param name="model"> The data for the poem. </param>
		/// <param name="canUpgrade"> Whether or not the poem can be upgraded. </param>
		public void SetUpgrade ( Poems.PoemModel model, bool canUpgrade )
		{
			// Check if the poem can be upgraded
			if ( !canUpgrade )
			{
				// Display unavailable upgrade
				upgradeTitleText.text = "<color=red>Draft Full</color>";
				upgradeDescriptionText.text = "This poem cannot be added to your draft each round as your draft is already full";
			}
			// Check level
			else if ( model.Level == 0 )
			{
				// Display upgrade
				upgradeTitleText.text = "Lv. I Upgrade";
				upgradeDescriptionText.text = "Add this poem to your draft each round";
			}
			else
			{
				// Display upgrade
				upgradeTitleText.text = $"Lv. {Utils.ToRomanNumeral ( model.Level + 1 )} Upgrade";
				upgradeDescriptionText.text = $"Characters earn <color=#A1740E>+{Poems.PoemHelper.GetBaseSnaps ( model.Level + 1 ) - 1}</color> snaps for this poem";
			}

			// Set button
			upgradeButton.interactable = canUpgrade;

			// Display poem
			SetPoem ( model );
		}

		/// <summary>
		/// Sets the upgrade owned for a poem to be displayed.
		/// </summary>
		/// <param name="model"> The data for the poem. </param>
		public void SetUpgrade ( Poems.PoemModel model )
		{
			// Display upgrade
			upgradeTitleText.text = $"Lv. {Utils.ToRomanNumeral ( model.Level )} Upgrade";
			upgradeDescriptionText.text = $"This poem is added to your draft each round\nCharacters earn <color=#A1740E>{Poems.PoemHelper.GetBaseSnaps ( model.Level )}</color> snap{(model.Level == 1 ? "" : "s" )} as a base";

			// Display poem
			SetPoem ( model );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the information for the poem.
		/// </summary>
		/// <param name="model"> The data for the poem. </param>
		private void SetPoem ( Poems.PoemModel model )
		{
			// Get poem
			Poems.PoemScriptableObject poem = Poems.PoemUtility.GetPoem ( model.ID );

			// Display poem title
			titleText.text = poem.Title;

			// Display author
			authorText.text = $"By {poem.Author}";

			// Display level
			levelText.gameObject.SetActive ( model.Level > 0 );
			if ( model.Level > 0 )
			{
				levelText.text = $"Lv. {Utils.ToRomanNumeral ( model.Level )}";
			}

			// Display rating
			for ( int i = 0; i < ratingImages.Length; i++ )
			{
				ratingImages [ i ].SetActive ( i < poem.Rating );
			}
		}

		#endregion // Private Functions
	}
}