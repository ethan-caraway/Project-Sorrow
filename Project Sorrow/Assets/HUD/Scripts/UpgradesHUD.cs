using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of upgrades in the run info.
	/// </summary>
	public class UpgradesHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject upgradesContainer;

		[SerializeField]
		private Transform rowsContainer;

		[SerializeField]
		private UpgradesRow rowPrefab;

		[SerializeField]
		private Scrollbar scrollbar;

		[SerializeField]
		private GameObject promptText;

		#endregion // UI Elements

		#region Upgrades Data

		private List<UpgradesRow> rows = new List<UpgradesRow> ( );

		#endregion // Upgrades Data

		#region Public Functions

		/// <summary>
		/// Initializes the upgrades HUD.
		/// </summary>
		public void Init ( )
		{
			// Check for upgrades
			if ( GameManager.Run.UpgradeData [ 0 ] != null && GameManager.Run.UpgradeData [ 0 ].ID != Upgrades.UpgradeModel.NO_UPGRADE_ID )
			{
				// Display upgrades
				upgradesContainer.SetActive ( true );
				promptText.SetActive ( false );

				// Display each upgrade
				for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( GameManager.Run.UpgradeData [ i ] != null && GameManager.Run.UpgradeData [ i ].ID != Upgrades.UpgradeModel.NO_UPGRADE_ID )
					{
						// Check for row
						if ( rows.Count <= i / 2 )
						{
							// Create new row
							UpgradesRow row = Instantiate ( rowPrefab, rowsContainer );
							row.gameObject.SetActive ( true );

							// Hide second upgrade in row in case one is not available to populate it
							row.SetUpgrade ( 0, 1 );

							// Store row
							rows.Add ( row );
						}

						// Display upgrade
						rows [ i / 2 ].SetUpgrade ( GameManager.Run.UpgradeData [ i ].ID, i % 2 );
					}
				}

				// Reset scroll
				scrollbar.value = 1;
			}
			else
			{
				// Hide upgrades
				upgradesContainer.SetActive ( false );
				promptText.SetActive ( true );
			}
		}

		#endregion // Public Functions
	}
}