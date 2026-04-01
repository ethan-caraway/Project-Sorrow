using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the status effects displayed in the HUD.
	/// </summary>
	public class StatusEffectHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject container;

		[SerializeField]
		private StatusEffectDisplay [ ] statusEffectDisplays;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Displays all of the player's current status effects in the HUD.
		/// </summary>
		public void SetStatusEffects ( )
		{
			// Track if there is at least one status effect
			bool hasStatusEffect = false;

			// Display each status effect
			for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( GameManager.Run.IsValidStatusEffect ( i ) )
				{
					// Store that there is at least one status effect
					hasStatusEffect = true;

					// Display status effect
					statusEffectDisplays [ i ].gameObject.SetActive ( true );
					statusEffectDisplays [ i ].SetStatusEffect ( GameManager.Run.StatusEffectData [ i ] );
				}
				else
				{
					// Hide status effect
					statusEffectDisplays [ i ].gameObject.SetActive ( false );
				}
			}

			// Display status effects
			container.SetActive ( hasStatusEffect );
		}

		#endregion // Public Functions
	}
}