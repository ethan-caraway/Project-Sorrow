using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls displaying the upgraded poems in the run info.
	/// </summary>
	public class PoemsHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject poemsContainer;

		[SerializeField]
		private Shop.PoemUpgradeDisplay [ ] poems;

		[SerializeField]
		private Scrollbar scrollbar;

		[SerializeField]
		private GameObject promptText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Initializes the poems HUD.
		/// </summary>
		public void Init ( )
		{
			// Check for poems
			if ( GameManager.Run.PermanentDraftPoems [ 0 ] != null && GameManager.Run.PermanentDraftPoems [ 0 ].ID != 0 )
			{
				// Display poems
				poemsContainer.SetActive ( true );
				promptText.SetActive ( false );

				// Display each poem
				for ( int i = 0; i < poems.Length; i++ )
				{
					// Check for poem
					if ( GameManager.Run.PermanentDraftPoems [ i ] != null && GameManager.Run.PermanentDraftPoems [ i ].ID != 0 )
					{
						// Display poem
						poems [ i ].gameObject.SetActive ( true );
						poems [ i ].SetUpgrade ( GameManager.Run.PermanentDraftPoems [ i ] );
					}
					else
					{
						// Hide poem
						poems [ i ].gameObject.SetActive ( false );
					}
				}

				// Reset scroll
				scrollbar.value = 1;
			}
			else
			{
				// Hide poems
				poemsContainer.SetActive ( false );
				promptText.SetActive ( true );
			}
		}

		#endregion // Public Functions
	}
}