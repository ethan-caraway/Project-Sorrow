using System.Collections.Generic;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the rewards screen.
	/// </summary>
	public class RewardsManager : MonoBehaviour
	{
		#region Upgrade Data Constants

		private const int MAX_UPGRADES = 2;
		private const int DEFAULT_UPGRADE_ID = 1;

		#endregion // Upgrade Data Constants

		#region UI Elements

		[SerializeField]
		private HUD.MoneyHUD moneyHUD;

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private GameObject container;

		[SerializeField]
		private PoemUpgradeDisplay [ ] poemDisplays;

		[SerializeField]
		private UpgradeDisplay [ ] upgradeDisplays;

		[SerializeField]
		private Poems.PoemPreview poemPreview;

		#endregion // UI Elements

		#region Upgrade Data

		[SerializeField]
		private Upgrades.UpgradeScriptableObject forceUpgrade;

		private Poems.PoemModel [ ] poems;
		private Upgrades.UpgradeScriptableObject [ ] upgrades = new Upgrades.UpgradeScriptableObject [ MAX_UPGRADES ];

		#endregion // Upgrade Data

		#region Public Functions

		/// <summary>
		/// Initializes the upgrade select screen.
		/// </summary>
		public void Init ( )
		{
			// Display screen
			container.SetActive ( true );
			poemPreview.gameObject.SetActive ( false );

			// Get poems
			poems = GameManager.Run.RoundData [ GameManager.Run.Round - 1 ].Poems;

			// Display poem upgrades
			bool canAddPoem = GameManager.Run.CanAddPermanentDraftPoem ( );
			for ( int i = 0; i < poemDisplays.Length; i++ )
			{
				poemDisplays [ i ].SetUpgrade ( poems [ i ], poems [ i ].Level > 0 || canAddPoem );
			}

			// Get and display upgrades
			List<int> excluedIDs = new List<int> ( );
			for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( GameManager.Run.UpgradeData [ i ] != null && GameManager.Run.UpgradeData [ i ].ID != Upgrades.UpgradeModel.NO_UPGRADE_ID )
				{
					excluedIDs.Add ( GameManager.Run.UpgradeData [ i ].ID );
				}	
			}
			for ( int i = 0; i < upgrades.Length; i++ )
			{
				// HACK: Force upgrade to appear in the rewards
				if ( i == 0 && forceUpgrade != null )
				{
					// Store forced upgrade
					upgrades [ i ] = forceUpgrade;
					excluedIDs.Add ( upgrades [ i ].ID );

					// Display upgrade
					upgradeDisplays [ i ].SetUpgrade ( upgrades [ i ] );
					continue;
				}

				// Get upgrade
				upgrades [ i ] = Upgrades.UpgradeUtility.GetRandomUpgrade ( excluedIDs.ToArray ( ) );
				if ( upgrades [ i ] == null )
				{
					upgrades [ i ] = Upgrades.UpgradeUtility.GetUpgrade ( DEFAULT_UPGRADE_ID );
				}
				else
				{
					excluedIDs.Add ( upgrades [ i ].ID );
				}

				// Display upgrade
				upgradeDisplays [ i ].SetUpgrade ( upgrades [ i ] );
			}
		}

		/// <summary>
		/// Displays the preview of a poem.
		/// </summary>
		/// <param name="index"> The index of the poem. </param>
		public void InspectPoem ( int index )
		{
			// Hide upgrades
			container.SetActive ( false );

			// Display poem
			poemPreview.gameObject.SetActive ( true );
			poemPreview.SetPoem ( poems [ index ], ( addModel ) =>
			{
				// Add modifiers and enhancements to the poem
				poems [ index ].Add ( addModel );

				// Check for permanent draft poem
				if ( poems [ index ].Level > 0 )
				{
					// Enhancement permanent draft poem
					GameManager.Run.EnhancePermanentDraftPoem ( poems [ index ].ID, addModel );
				}
			} );
		}

		/// <summary>
		/// Exits the preview of a poem.
		/// </summary>
		public void ExitPreview ( )
		{
			// Display upgrades
			container.SetActive ( true );

			// Hide preview
			poemPreview.gameObject.SetActive ( false );

			// Disable consumable application
			consumablesHUD.DisableApplyToPoem ( );
		}

		/// <summary>
		/// Upgrades a given poem.
		/// </summary>
		/// <param name="index"> The index of the poem. </param>
		public void UpgradePoem ( int index )
		{
			// Check for first upgrade
			if ( poems [ index ].Level == 0 )
			{
				// Upgrade poem
				poems [ index ].Level++;

				// Add poem
				GameManager.Run.AddPermanentDraftPoem ( poems [ index ] );
			}
			else
			{
				// Upgrade poem
				GameManager.Run.UpgradePermanentDraftPoem ( poems [ index ].ID );
			}
		}

		/// <summary>
		/// Adds an upgrade to the player's run.
		/// </summary>
		/// <param name="index"> The index of the upgrade. </param>
		public void AddUpgrade ( int index )
		{
			// Add upgrade
			GameManager.Run.AddUpgrade ( upgrades [ index ].ID );

			// Trigger on add effect of upgrade
			Upgrades.Upgrade upgrade = Upgrades.UpgradeHelper.GetUpgrade ( upgrades [ index ].ID );
			int money = upgrade.OnAdd ( );

			// Update HUD
			if ( money > 0 )
			{
				moneyHUD.ApplyMoney ( money, GameManager.Run.Money, GameManager.Run.Debt );
				GameManager.Run.ApplyMoney ( money );
			}
		}

		#endregion // Public Functions
	}
}