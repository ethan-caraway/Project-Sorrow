using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the collections menu.
	/// </summary>
	public class CollectionsMenu : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Button [ ] tabButtons;

		[SerializeField]
		private GameObject [ ] tabPanels;

		[SerializeField]
		private StatsCollection statsTab;

		[SerializeField]
		private ItemsCollection itemsTab;

		[SerializeField]
		private ConsumablesCollection consumablesTab;

		[SerializeField]
		private PoetsCollection poetsTab;

		[SerializeField]
		private JudgesCollection judgesTab;

		[SerializeField]
		private UpgradesCollection upgradesTab;

		[SerializeField]
		private ModifiersCollection modifiersTab;

		[SerializeField]
		private StatusEffectCollection statusEffectsTab;

		[SerializeField]
		private DifficultiesCollection difficultiesTab;

		#endregion // UI Elements

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Initialize the menu
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Opens a tab panel in the collections menu.
		/// </summary>
		/// <param name="index"> The index of the tab. </param>
		public void OpenTab ( int index )
		{
			// Display tab
			for ( int i = 0; i < tabButtons.Length; i++ )
			{
				// Display the selected tab
				tabButtons [ i ].interactable = i != index;
				tabPanels [ i ].SetActive ( i == index );
			}

			// Initialize the tab
			switch ( index )
			{
				case 0:
					statsTab.Init ( );
					break;

				case 1:
					itemsTab.Init ( );
					break;

				case 2:
					consumablesTab.Init ( );
					break;

				case 3:
					poetsTab.Init ( );
					break;

				case 4:
					judgesTab.Init ( );
					break;

				case 5:
					upgradesTab.Init ( );
					break;

				case 6:
					modifiersTab.Init ( );
					break;

				case 7:
					statusEffectsTab.Init ( );
					break;

				case 8:
					difficultiesTab.Init ( );
					break;
			}
		}

		/// <summary>
		/// Returns to the main menu.
		/// </summary>
		public void Back ( )
		{
			// Load main menu
			SceneManager.LoadScene ( GameManager.MAIN_MENU_SCENE );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Initializes the collections menu.
		/// </summary>
		private void Init ( )
		{
			// Open stats tab as default
			OpenTab ( 0 );
		}

		#endregion // Private Functions
	}
}