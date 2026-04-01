using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls displaying the difficulty in the run info.
	/// </summary>
	public class DifficultyHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text [ ] penaltyTexts;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Initializes the difficulty HUD.
		/// </summary>
		public void Init ( )
		{
			// Display difficulty title
			titleText.text = GameManager.Difficulty.Title;

			// Display difficulty description
			descriptionText.text = GameManager.Difficulty.Description;

			// Display penalties
			for ( int i = 0; i < penaltyTexts.Length; i++ )
			{
				// Check for penalty
				if ( i < GameManager.Difficulty.Penalties.Length )
				{
					// Display penalty
					penaltyTexts [ i ].gameObject.SetActive ( true );
					penaltyTexts [ i ].text = $"\u2022<indent=1em>{GameManager.Difficulty.Penalties [ i ]}";
				}
				else
				{
					// Hide penalty
					penaltyTexts [ i ].gameObject.SetActive ( false );
				}
			}
		}

		#endregion // Public Functions
	}
}