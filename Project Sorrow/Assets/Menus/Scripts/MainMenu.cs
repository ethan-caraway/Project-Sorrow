using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the main menu.
	/// </summary>
	public class MainMenu : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject continueButton;

		[SerializeField]
		private GameObject newRunPopup;

		[SerializeField]
		private TMP_Text versionText;

		#endregion // UI Elements

		#region Menu Data

		[SerializeField]
		private Difficulty.DifficultyScriptableObject [ ] difficulties;

		[SerializeField]
		private Difficulty.DifficultyScriptableObject tutorialDifficulty;

		#endregion // Menu Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Set up the main menu
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Continues the previous run.
		/// </summary>
		public void ContinueRun ( )
		{
			// Start run
			GameManager.IsRunActive = true;

			// Get run data
			GameManager.Run = Memory.MemoryManager.GetPreviousRun ( );
			GameManager.Run.Init ( );

			// Get difficulty
			GameManager.Difficulty = GetDifficulty ( Memory.MemoryManager.GetPreviousDifficulty ( ) );

			// Load run at checkpoint
			SceneManager.LoadScene ( GameManager.Run.Checkpoint );
		}

		/// <summary>
		/// Begins a new run.
		/// </summary>
		public void StartRun ( )
		{
			// Check for previous run
			if ( Memory.MemoryManager.HasPreviousRun )
			{
				// Display confirmation popup
				newRunPopup.SetActive ( true );
			}
			else
			{
				// Load character select
				SceneManager.LoadScene ( GameManager.CHARACTER_SELECT_SCENE );
			}
		}

		/// <summary>
		/// Cancels starting a new run.
		/// </summary>
		public void CancelNewRun ( )
		{
			// Hide popup
			newRunPopup.SetActive ( false );
		}

		/// <summary>
		/// Confirms starting a new run.
		/// </summary>
		public void ConfirmNewRun ( )
		{
			// Load character select
			SceneManager.LoadScene ( GameManager.CHARACTER_SELECT_SCENE );
		}

		/// <summary>
		/// Begins the tutorial.
		/// </summary>
		public void StartTutorial ( )
		{
			// Start run
			GameManager.IsRunActive = true;

			// Activate the tutorial
			GameManager.IsTutorial = true;

			// Set the tutorial difficulty
			GameManager.Difficulty = tutorialDifficulty;

			// Set up the tutorial data
			GameManager.Run = new RunModel ( );
			GameManager.Run.PoetID = 1;
			GameManager.Run.Perk = Perks.PerkHelper.GetPerk ( GameManager.Run.PoetID );
			GameManager.Run.RoundData = new RoundModel [ 1 ];
			GameManager.Run.RoundData [ 0 ] = new RoundModel ( );
			GameManager.Run.RoundData [ 0 ].Poems = new Poems.PoemModel [ 2 ] {	new Poems.PoemModel ( ), null };

			// Begin tutorial performance
			SceneManager.LoadScene ( GameManager.PERFORMANCE_SCENE );
		}

		/// <summary>
		/// Opens the collection menu.
		/// </summary>
		public void OpenCollection ( )
		{
			// Open collection scene
			SceneManager.LoadScene ( GameManager.COLLECTION_SCENE );
		}

		/// <summary>
		/// Quits the game.
		/// </summary>
		public void QuitToDesktop ( )
		{
			// Quit game
			Application.Quit ( );
		}

		#endregion // Public Funtions

		#region Private Functions

		/// <summary>
		/// Initializes the main menu.
		/// </summary>
		private void Init ( )
		{
			// Check for previous run
			continueButton.SetActive ( Memory.MemoryManager.HasPreviousRun );

			// Display version number
			versionText.text = Application.version;
		}

		private Difficulty.DifficultyScriptableObject GetDifficulty ( int id )
		{
			// Find difficulty with matching ID
			for ( int i = 0; i < difficulties.Length; i++ )
			{
				// Check ID
				if ( difficulties [ i ].ID == id )
				{
					return difficulties [ i ];
				}
			}

			// Return default difficulty
			return difficulties [ 0 ];
		}

		#endregion // Private Functions
	}
}