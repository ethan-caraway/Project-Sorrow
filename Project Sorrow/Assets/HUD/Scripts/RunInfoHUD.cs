using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of the current run info in the HUD.+
	/// </summary>
	public class RunInfoHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private PauseHUD pauseHUD;

		[SerializeField]
		private Button [ ] tabButtons;

		[SerializeField]
		private GameObject [ ] tabContainers;

		[SerializeField]
		private SetlistHUD setlistHUD;

		[SerializeField]
		private PoemsHUD poemsHUD;

		[SerializeField]
		private UpgradesHUD upgradesHUD;

		[SerializeField]
		private DifficultyHUD difficultyHUD;

		#endregion // UI Elements

		#region Run Data

		/// <summary>
		/// Whether or not the setlist is currently being drafted.
		/// </summary>
		public bool IsDrafting = false;

		/// <summary>
		/// The callback for when the run info panel is opened.
		/// </summary>
		public System.Action OnOpen;

		/// <summary>
		/// The callback for when the run info panel is closed.
		/// </summary>
		public System.Action OnClose;

		#endregion // Run Data

		#region Public Functions

		/// <summary>
		/// Opens the run info panel.
		/// </summary>
		public void Open ( )
		{
			// Disable pausing
			pauseHUD.CanPause = false;

			// Display panel
			gameObject.SetActive ( true );

			// Initialize tabs
			setlistHUD.Init ( IsDrafting );
			poemsHUD.Init ( );
			upgradesHUD.Init ( );
			difficultyHUD.Init ( );

			// Open the setlist tab by default
			OpenTab ( 0 );

			// Trigger callback
			if ( OnOpen != null )
			{
				OnOpen ( );
			}
		}

		/// <summary>
		/// Opens a tab in the run info panel.
		/// </summary>
		/// <param name="index"> The index of the tab. </param>
		public void OpenTab ( int index )
		{
			// Set tabs
			for ( int i = 0; i < tabButtons.Length; i++ )
			{
				// Display only the selected tab
				tabButtons [ i ].interactable = i != index;
				tabContainers [ i ].SetActive ( i == index );
			}
		}

		/// <summary>
		/// Closes the run info panel.
		/// </summary>
		public void Close ( )
		{
			// Enable pausing
			pauseHUD.CanPause = true;

			// Hide panel
			gameObject.SetActive ( false );

			// Trigger callback
			if ( OnClose != null )
			{
				OnClose ( );
			}
		}

		#endregion // Public Functions
	}
}