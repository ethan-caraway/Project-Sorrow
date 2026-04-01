using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the encounter screen during intermission.
	/// </summary>
	public class EncounterManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ConfidenceHUD confidenceHUD;

		[SerializeField]
		private HUD.MoneyHUD moneyHUD;

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private HUD.StatusEffectHUD statusEffectHUD;

		[SerializeField]
		private HUD.PoetHUD poetHUD;

		[SerializeField]
		private HUD.TimerHUD timerHUD;

		[SerializeField]
		private HUD.ItemsHUD itemsHUD;

		[SerializeField]
		private HUD.RunInfoHUD runInfoHUD;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private GameObject encounterButton;

		[SerializeField]
		private GameObject skipButton;

		[SerializeField]
		private OptionDisplay [ ] optionDisplays;

		[SerializeField]
		private GameObject continueButton;

		[SerializeField]
		private GameObject skipShopText;

		#endregion // UI Elements

		#region Encounter Data

		[SerializeField]
		private string startTitle;

		[TextArea]
		[SerializeField]
		private string startDescription;

		private EncounterScriptableObject encounterData;
		private Encounter encounter;
		private bool isShopSkipped = false;

		#endregion // Encounter Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Initialize the screen
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Begins the encounter for the player.
		/// </summary>
		public void StartEncounter ( )
		{
			// Get the encounter data
			encounterData = EncounterUtility.GetEncounter ( GameManager.Run.CurrentRound.EncounterID );
			encounter = EncounterHelper.GetEncounter ( GameManager.Run.CurrentRound.EncounterID );

			// Display encounter prompt
			titleText.text = encounterData.Title;
			descriptionText.text = encounterData.Description;

			// Display options
			for ( int i = 0; i < optionDisplays.Length; i++ )
			{
				// Check for option
				if ( i < encounterData.Options.Length )
				{
					// Get if the option is available
					bool isAvailable = encounter.IsOptionAvailable ( i, encounterData.Options [ i ] );
					bool isDisplayed = !encounterData.Options [ i ].IsHidden || isAvailable;

					// Display if the option available
					optionDisplays [ i ].gameObject.SetActive ( isDisplayed );
					if ( isDisplayed )
					{
						optionDisplays [ i ].SetOption ( encounterData.Options [ i ], isAvailable );
					}
				}
				else
				{
					// Hide option
					optionDisplays [ i ].gameObject.SetActive ( false );
				}
			}

			// Hide initial option buttons
			encounterButton.SetActive ( false );
			skipButton.SetActive ( false );
		}

		/// <summary>
		/// Skips the encounter and continues to the shop.
		/// </summary>
		public void SkipEncounter ( )
		{
			// Proceed to the shop
			SceneManager.LoadScene ( GameManager.SHOP_SCENE );
		}

		/// <summary>
		/// Selects an option in the encounter.
		/// </summary>
		/// <param name="index"> The index of the option. </param>
		public void SelectOption ( int index )
		{
			// Display conclusion of encounter
			descriptionText.text = encounterData.Options [ index ].Conclusion;

			// Get results of encounter
			ResultModel results = encounter.GetResults ( index, encounterData.Options [ index ] );
			isShopSkipped = results.IsShopSkipped;

			// Check for money earned or lost
			if ( results.Money != 0 )
			{
				// Update money
				moneyHUD.ApplyMoney ( results.Money, GameManager.Run.Money, GameManager.Run.Debt );
				GameManager.Run.ApplyMoney ( results.Money );
			}

			// Update encounter bonuses
			GameManager.Run.EncounterData.Add ( results.Bonus );

			// Update confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Update timer
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Update consumables
			consumablesHUD.RefreshConsumables ( );

			// Update items
			itemsHUD.SetItems ( null, null );

			// Update status effects
			statusEffectHUD.SetStatusEffects ( );

			// Hide option buttons
			for ( int i = 0; i < optionDisplays.Length; i++ )
			{
				optionDisplays [ i ].gameObject.SetActive ( false );
			}

			// Display continue button
			continueButton.SetActive ( true );
			skipShopText.SetActive ( isShopSkipped );
		}

		/// <summary>
		/// Continues the run after the encounter.
		/// </summary>
		public void Continue ( )
		{
			// Check if the shop is being skipped
			if ( isShopSkipped )
			{
				// Load setlist
				SceneManager.LoadScene ( GameManager.SETLIST_SCENE );
			}
			else
			{
				// Load shop
				SceneManager.LoadScene ( GameManager.SHOP_SCENE );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Initializes the encounter screen for choosing if to have an encounter.
		/// </summary>
		private void Init ( )
		{
			// Save game
			GameManager.Run.Checkpoint = GameManager.ENCOUNTER_SCENE;
			Memory.MemoryManager.Save ( );

			// Set confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Set money
			moneyHUD.SetMoney ( GameManager.Run.Money, GameManager.Run.Debt );

			// Set consumables
			consumablesHUD.SetConsumables ( null, null, null );

			// Set status effects
			statusEffectHUD.SetStatusEffects ( );

			// Setup poet
			poetHUD.SetPoet ( Poets.PoetUtility.GetPoet ( GameManager.Run.PoetID ) );

			// Set timer
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Set items
			itemsHUD.SetItems ( null, null );

			// Display initial encounter prompt
			titleText.text = startTitle;
			descriptionText.text = startDescription;

			// Display initial encounter options
			encounterButton.SetActive ( true );
			skipButton.SetActive ( true );
			for ( int i = 0; i < optionDisplays.Length; i++ )
			{
				optionDisplays [ i ].gameObject.SetActive ( false );
			}
			continueButton.SetActive ( false );
		}

		#endregion // Private Functions
	}
}