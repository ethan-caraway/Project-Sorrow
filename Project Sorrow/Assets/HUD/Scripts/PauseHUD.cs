using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of the pause menu in the HUD.
	/// </summary>
	public class PauseHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject menuContainer;

		[SerializeField]
		private GameObject controlsContainer;

		[SerializeField]
		private GameObject newRunButton;

		[SerializeField]
		private GameObject popupContainer;

		#endregion // UI Elements

		#region Pause Data

		/// <summary>
		/// Whether or not the run can currently be paused.
		/// </summary>
		public bool CanPause = true;

		/// <summary>
		/// The callback for when the pause menu is opened.
		/// </summary>
		public System.Action OnOpen;

		/// <summary>
		/// The callback for when the pause menu is closed.
		/// </summary>
		public System.Action OnClose;

		private bool isPaused = false;

		#endregion // Pause Data

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check for escape key press
			if ( CanPause && Keyboard.current.escapeKey.wasPressedThisFrame )
			{
				// Check if paused
				if ( isPaused )
				{
					// Close pause menu
					Close ( );
				}
				else
				{
					// Open pause menu
					Open ( );
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Closes the pause menu.
		/// </summary>
		public void Close ( )
		{
			// Set as no longer paused
			isPaused = false;

			// Hide menu
			menuContainer.SetActive ( false );

			// Trigger callback
			if ( OnClose != null )
			{
				OnClose ( );
			}
		}

		/// <summary>
		/// Opens the confirm new run popup.
		/// </summary>
		public void NewRun ( )
		{
			// Display confirm popup
			controlsContainer.SetActive ( false );
			popupContainer.SetActive ( true );
		}

		/// <summary>
		/// Begin a new run.
		/// </summary>
		public void ConfirmNewRun ( )
		{
			// End run
			GameManager.IsRunActive = false;

			// End tutorial if active
			GameManager.IsTutorial = false;

			// Clear run data
			GameManager.Run = null;
			GameManager.Difficulty = null;

			// Save erasing run data
			Memory.MemoryManager.Save ( );

			// Load character select
			SceneManager.LoadScene ( GameManager.CHARACTER_SELECT_SCENE );
		}

		/// <summary>
		/// Cancels starting a new run.
		/// </summary>
		public void CancelNewRun ( )
		{
			// Hide confirm popup
			controlsContainer.SetActive ( true );
			popupContainer.SetActive ( false );
		}

		/// <summary>
		/// Exits to the main menu.
		/// </summary>
		public void MainMenu ( )
		{
			// End run
			GameManager.IsRunActive = false;

			// End tutorial if active
			GameManager.IsTutorial = false;

			// Clear run data
			GameManager.Run = null;
			GameManager.Difficulty = null;

			// Load main menu
			SceneManager.LoadScene ( GameManager.MAIN_MENU_SCENE );
		}

		/// <summary>
		/// Quits the game.
		/// </summary>
		public void QuitToDesktop ( )
		{
			// Quit game
			Application.Quit ( );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Opens the pause menu.
		/// </summary>
		private void Open ( )
		{
			// Set as paused
			isPaused = true;

			// Display menu
			menuContainer.SetActive ( true );
			controlsContainer.SetActive ( true );
			popupContainer.SetActive ( false );

			// Display the new run button only if it is not the tutorial
			newRunButton.SetActive ( !GameManager.IsTutorial );

			// Trigger callback
			if ( OnOpen != null )
			{
				OnOpen ( );
			}
		}

		#endregion // Private Functions
	}
}