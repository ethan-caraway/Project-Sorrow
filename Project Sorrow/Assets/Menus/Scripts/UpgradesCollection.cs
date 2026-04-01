using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the upgrades tab in the collections menu.
	/// </summary>
	public class UpgradesCollection : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private UpgradePortrait [ ] upgradePortraits;

		[SerializeField]
		private Image upgradeImage;

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

		#region Upgrade Data

		private int portraitIndex;

		#endregion // Upgrade Data

		#region Public Functions

		/// <summary>
		/// Initializes the upgrade collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display each upgrade
			for ( int i = 0; i < upgradePortraits.Length; i++ )
			{
				// Display item
				upgradePortraits [ i ].SetUpgrade ( i + 1 );
			}

			// Display the first upgrade
			SelectUpgrade ( 0 );
		}

		/// <summary>
		/// Selects a given upgrade.
		/// </summary>
		/// <param name="index"> The index of the upgrade portrait. </param>
		public void SelectUpgrade ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < upgradePortraits.Length; i++ )
			{
				upgradePortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected upgrade
			DisplayUpgrade ( index );
		}

		/// <summary>
		/// Previews a given upgrade.
		/// </summary>
		/// <param name="index"> The index of the upgrade portrait. </param>
		public void PreviewUpgrade ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				upgradePortraits [ index ].ToggleHover ( true );

				// Display previewed upgrade
				DisplayUpgrade ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given upgrade.
		/// </summary>
		/// <param name="index"> The index of the upgrade portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				upgradePortraits [ index ].ToggleHover ( false );

				// Display selected upgrade
				DisplayUpgrade ( portraitIndex );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the data for given upgrade.
		/// </summary>
		/// <param name="index"> The index of the upgrade portrait. </param>
		private void DisplayUpgrade ( int index )
		{
			// Get upgrade
			Upgrades.UpgradeScriptableObject upgrade = upgradePortraits [ index ].Upgrade;

			// Display upgrade
			upgradeImage.sprite = upgrade.Icon;

			// Get stats
			Progression.UpgradeStatsModel stats = Progression.ProgressManager.Progress.GetUpgradeStats ( upgrade.ID );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display upgrade
				upgradeImage.color = Color.white;

				// Display upgrade info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = upgrade.Title;
				descriptionText.text = upgrade.Description;

				// Display stats
				ownStatText.text = stats.Owns.ToString ( );
				winStatText.text = stats.Wins.ToString ( );
				difficultyStatText.text = stats.HighestDifficultyWin > 0 ? $"Prestige {Utils.ToRomanNumeral ( stats.HighestDifficultyWin )}" : "N/A";
			}
			else
			{
				// Display silhouette
				upgradeImage.color = Color.black;

				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		#endregion // Private Functions
	}
}