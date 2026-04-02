using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Startup
{
	/// <summary>
	/// This class controls the startup sequence of the game.
	/// </summary>
	public class StartupManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text percentageText;

		[SerializeField]
		private Slider loadingBar;

		[SerializeField]
		private TMP_Text messageText;

		#endregion // UI Elements

		#region Startup Data

		[SerializeField]
		private bool isAllUnlocked;

		[SerializeField]
		private Difficulty.DifficultyScriptableObject difficulty;

		[SerializeField]
		private Poets.PoetScriptableObject forcePoet;

		[SerializeField]
		private Poems.PoemScriptableObject [ ] forcePoems;

		[SerializeField]
		private Items.ItemScriptableObject [ ] forceItems;

		[SerializeField]
		private Consumables.ConsumableScriptableObject [ ] forceConsumables;

		[SerializeField]
		private Enums.StatusEffectType [ ] forceStatusEffects;

		[SerializeField]
		private Upgrades.UpgradeScriptableObject [ ] forceUpgrades;

		[SerializeField]
		private Encounters.EncounterScriptableObject forceEncounter;

		[SerializeField]
		private Judges.JudgeScriptableObject forceJudge;

		[SerializeField]
		private bool isForceLastPerformance;

		#endregion // Startup Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Begin startup
			Init ( );
		}

		private void Update ( )
		{
			// Get percentage
			float percentage = GetLoadPercentage ( );

			// Display percentage
			percentageText.text = $"{percentage * 100:##0}%";
			loadingBar.value = percentage;
		}

		#endregion // MonoBehaviour Functions

		#region Private Functions

		/// <summary>
		/// Begins the startup sequence for the game.
		/// </summary>
		private async void Init ( )
		{
			// Wait for poems to load
			messageText.text = "Loading poems...";
			await Poems.PoemUtility.Load ( );

			// Wait for poets to load
			messageText.text = "Loading poets...";
			await Poets.PoetUtility.Load ( );

			// Wait for items to load
			messageText.text = "Loading items...";
			await Items.ItemUtility.Load ( );

			// Wait for consumables to load
			messageText.text = "Loading consumables...";
			await Consumables.ConsumableUtility.Load ( );

			// Wait for status effects to load
			messageText.text = "Loading status effects...";
			await StatusEffects.StatusEffectUtility.Load ( );

			// Wait for tags to load
			messageText.text = "Loading tags...";
			await Tags.TagUtility.Load ( );

			// Wait for encounters to load
			messageText.text = "Loading encounters...";
			await Encounters.EncounterUtility.Load ( );

			// Wait for judges to load
			messageText.text = "Loading judges...";
			await Judges.JudgeUtility.Load ( );

			// Wait for upgrades to load
			messageText.text = "Loading upgrades...";
			await Upgrades.UpgradeUtility.Load ( );

			// Load save data
			messageText.text = "Loading save data...";
			Memory.MemoryManager.Load ( );

			// Initialize the save data
			Progression.ProgressManager.Progress.Init ( );

			// HACK: Set whether or not everything is unlocked
			Progression.ProgressManager.Progress.IsAllUnlocked = isAllUnlocked;

			// HACK: Check if run is being forced
			if ( IsForceRun ( ) )
			{
				// Begin run
				ForceRun ( );
			}
			else
			{
				// Load main menu
				SceneManager.LoadScene ( GameManager.MAIN_MENU_SCENE );
			}
		}

		/// <summary>
		/// Gets the current percentage of loading progress.
		/// </summary>
		/// <returns> The current load percentage. </returns>
		private float GetLoadPercentage ( )
		{
			return ( Poems.PoemUtility.PercentageComplete 
				+ Poets.PoetUtility.PercentageComplete
				+ Items.ItemUtility.PercentageComplete 
				+ Consumables.ConsumableUtility.PercentageComplete 
				+ StatusEffects.StatusEffectUtility.PercentageComplete
				+ Tags.TagUtility.PercentageComplete
				+ Encounters.EncounterUtility.PercentageComplete
				+ Judges.JudgeUtility.PercentageComplete
				+ Upgrades.UpgradeUtility.PercentageComplete ) 
				/ 9f;

		}

		/// <summary>
		/// Check if a run is being forced from dev tools.
		/// </summary>
		/// <returns> Whether or not to start a run immediately. </returns>
		private bool IsForceRun ( )
		{
			return forcePoet != null || // Check for valid poet to force
				( forcePoems != null && forcePoems.Length > 0 && forcePoems [ 0 ] != null ) || // Check for one valid poem to force
				( forceItems != null && forceItems.Length > 0 && forceItems [ 0 ] != null ) || // Check for one valid item to force
				( forceConsumables != null && forceConsumables.Length > 0 && forceConsumables [ 0 ] != null ) || // Check for one valid consumable to force
				( forceStatusEffects != null && forceStatusEffects.Length > 0 && forceStatusEffects [ 0 ] != Enums.StatusEffectType.NONE ) || // Check for one valid status effect to force
				( forceUpgrades != null && forceUpgrades.Length > 0 && forceUpgrades [ 0 ] != null ) || // Check for one valid upgrade to force
				forceEncounter != null || // Check for valid encounter to force
				forceJudge != null || // Check for valid judge to force
				isForceLastPerformance; // Check for forcing the last performance of the round
		}

		/// <summary>
		/// Starts a run immediately for testing one of the forced settings.
		/// </summary>
		private void ForceRun ( )
		{
			// Start run
			GameManager.IsRunActive = true;

			// Set run data
			GameManager.Run = new RunModel ( );
			GameManager.Difficulty = difficulty;

			// Set poet
			GameManager.Run.PoetID = forcePoet != null ? forcePoet.ID : 1;
			GameManager.Run.Init ( );

			// HACK: Force ownership of items
			if ( forceItems != null && forceItems.Length > 0 )
			{
				for ( int i = 0; i < forceItems.Length; i++ )
				{
					if ( forceItems [ i ] != null )
					{
						Items.ItemModel itemData =  GameManager.Run.AddItem ( forceItems [ i ].ID );
						itemData.Item.OnAdd ( null );
					}
				}
			}

			// HACK: Force ownership of consumables
			if ( forceConsumables != null && forceConsumables.Length > 0 )
			{
				for ( int i = 0; i < forceConsumables.Length; i++ )
				{
					if ( forceConsumables [ i ] != null )
					{
						GameManager.Run.AddConsumable ( forceConsumables [ i ].ID, 1 );
					}
				}
			}

			// HACK: Force status effects
			if ( forceStatusEffects != null && forceStatusEffects.Length > 0 )
			{
				for ( int i = 0; i < forceStatusEffects.Length; i++ )
				{
					if ( forceStatusEffects [ i ] != Enums.StatusEffectType.NONE )
					{
						GameManager.Run.AddStatusEffect ( forceStatusEffects [ i ], 1 );
					}
				}
			}

			// HACK: Force ownership of upgrades
			if ( forceUpgrades != null && forceUpgrades.Length > 0 )
			{
				for ( int i = 0; i < forceUpgrades.Length; i++ )
				{
					if ( forceUpgrades [ i ] != null )
					{
						GameManager.Run.AddUpgrade ( forceUpgrades [ i ].ID );
					}
				}
			}

			// Start first round
			GameManager.Run.NewRun ( );

			// HACK: Force poems into draft
			if ( forcePoems != null && forcePoems.Length > 0 )
			{
				for ( int i = 0; i < forcePoems.Length; i++ )
				{
					if ( forcePoems [ i ] != null && i < GameManager.Run.RoundData [ 0 ].DraftIDs.Length )
					{
						GameManager.Run.RoundData [ 0 ].DraftIDs [ i ] = forcePoems [ i ].ID;
					}
				}
			}

			// HACK: Force last performance of the round
			if ( isForceLastPerformance )
			{
				// Set performance
				GameManager.Run.Performance = Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1;

				// Automatically draft poems
				for ( int i = 0; i < GameManager.Run.CurrentRound.Poems.Length; i++ )
				{
					GameManager.Run.CurrentRound.Poems [ i ] = new Poems.PoemModel
					{
						ID = GameManager.Run.CurrentRound.DraftIDs [ i ]
					};
				}
			}

			// HACK: Force encounter for first round
			if ( forceEncounter != null )
			{
				GameManager.Run.CurrentRound.EncounterID = forceEncounter.ID;
			}

			// HACK: Force judge for first round
			if ( forceJudge != null )
			{
				GameManager.Run.CurrentRound.JudgeID = forceJudge.ID;

				// Check for The Minimalist judge
				if ( forceJudge.ID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Set poem for The Minimalist judge
					GameManager.Run.CurrentRound.Poems [ Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 ] = new Poems.PoemModel
					{
						ID = Judges.JudgeHelper.GetMinimalistPoem ( )
					};
				}
			}

			// Load run
			SceneManager.LoadScene ( GameManager.SETLIST_SCENE );
		}

		#endregion // Private Functions
	}
}