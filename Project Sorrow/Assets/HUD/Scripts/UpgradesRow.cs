using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of a row of upgrades in the run info.
	/// </summary>
	public class UpgradesRow : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Shop.UpgradeDisplay [ ] upgrades;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Displays the information for an upgrade.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		/// <param name="index"> The index of the upgrade in the row. </param>
		public void SetUpgrade ( int id, int index )
		{
			// Get upgrade
			Upgrades.UpgradeScriptableObject upgrade = Upgrades.UpgradeUtility.GetUpgrade ( id );

			// Check for upgrade
			if ( upgrade != null )
			{
				// Display upgrade
				upgrades [ index ].gameObject.SetActive ( true );
				upgrades [ index ].SetUpgrade ( upgrade );
			}
			else
			{
				// Hide upgrade
				upgrades [ index ].gameObject.SetActive ( false );
			}
		}

		#endregion // Public Functions
	}
}