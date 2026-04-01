using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the character selection menu.
	/// </summary>
	public class CharacterSelectMenu : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private PoetSelect poetSelect;

		[SerializeField]
		private DifficultySelect difficultySelect;

		[SerializeField]
		private Button startButton;

		#endregion // UI Elements

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Setup menu
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets whether or not a run can be started based on the selected poet and difficulty.
		/// </summary>
		public void CanStartRun ( )
		{
			// Enable/disable the start run button
			startButton.interactable = Progression.ProgressManager.Progress.IsAllUnlocked || // Check for all unlocks
				( Poets.PoetHelper.IsUnlocked ( poetSelect.SelectedPoet.ID ) && // Check for unlocked poet
				difficultySelect.SelectedDifficulty.ID <= Progression.ProgressManager.Progress.HighestDifficultyWin + 1 ); // Check for unlocked difficulty
		}

		/// <summary>
		/// Starts a new run with the selected poet and difficulty.
		/// </summary>
		public void StartRun ( )
		{
			// Start run
			GameManager.IsRunActive = true;

			// Create run data
			GameManager.Run = new RunModel ( );

			// Store selected poet
			GameManager.Run.PoetID = poetSelect.SelectedPoet.ID;
			GameManager.Run.Init ( );

			// Store difficulty
			GameManager.Difficulty = difficultySelect.SelectedDifficulty;

			// Begin run
			GameManager.Run.NewRun ( );
			SceneManager.LoadScene ( GameManager.SETLIST_SCENE );
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
		/// Initializes the character selection menu.
		/// </summary>
		private void Init ( )
		{
			// Setup poet selection
			poetSelect.Init ( );

			// Setup difficulty selection
			difficultySelect.Init ( );
		}

		#endregion // Private Functions
	}
}