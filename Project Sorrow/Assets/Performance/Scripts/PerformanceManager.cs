using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the setup and progression of the performance.
	/// </summary>
	public class PerformanceManager : MonoBehaviour
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
		private HUD.RunInfoHUD runInfoHUD;

		[SerializeField]
		private GameObject runInfoButton;

		[SerializeField]
		private HUD.TimerHUD timerHUD;

		[SerializeField]
		private HUD.ItemsHUD itemsHUD;

		[SerializeField]
		private HUD.SnapsHUD snapsHUD;

		[SerializeField]
		private HUD.PauseHUD pauseHUD;

		[SerializeField]
		private JudgeHUD judgeHUD;

		[SerializeField]
		private CreditsHUD creditsHUD;

		[SerializeField]
		private SummaryHUD summaryHUD;

		[SerializeField]
		private PoemManager poemManager;

		#endregion // UI Elements

		#region Performance Data

		private PerformanceModel model;

		private bool isTimerActive;
		private bool isPerforming;

		private System.Action<bool> onPause;

		#endregion // Performance Data

		#region Public Properties

		/// <summary>
		/// The performance data.
		/// </summary>
		public PerformanceModel Model
		{
			get
			{
				return model;
			}
		}

		/// <summary>
		/// Whether or not the performance is currently in progress.
		/// </summary>
		public bool IsPerforming
		{
			get
			{
				return isPerforming;
			}
		}

		#endregion // Public Properties

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Initialize performance
			Init ( );

			// Disable the run info button if tutorial
			runInfoButton.SetActive ( !GameManager.IsTutorial );

			// Set callbacks
			runInfoHUD.OnOpen += OnPause;
			runInfoHUD.OnClose += OnResume;
			pauseHUD.OnOpen += OnPause;
			pauseHUD.OnClose += OnResume;
		}

		private void Update ( )
		{
			// Check if timer is active
			if ( isTimerActive && !GameManager.Run.HasStatusEffect ( Enums.StatusEffectType.FOCUSED ) )
			{
				// Decrement time
				model.TimeRemaining -= Time.deltaTime;

				// Check for completion
				if ( model.TimeRemaining <= 0f )
				{
					// Set time to 0
					model.TimeRemaining = 0;

					// Disable timer
					isTimerActive = false;

					// Trigger end of timer
					OnTimerExpire ( );
				}

				// Update timer display
				timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, model.TimeRemaining );
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Updates the display of items in the HUD.
		/// </summary>
		public void UpdateItems ( )
		{
			// Setup items
			itemsHUD.SetItems ( null, null );
		}

		/// <summary>
		/// Updates the display of consumables in the HUD.
		/// </summary>
		public void UpdateConsumables ( )
		{
			// Refresh HUD
			consumablesHUD.RefreshConsumables ( );
		}

		/// <summary>
		/// Updates the display of status effects in the HUD.
		/// </summary>
		public void UpdateStatusEffects ( )
		{
			// Refresh HUD
			statusEffectHUD.SetStatusEffects ( );
		}

		/// <summary>
		/// Highlights an item in use.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="isPositive"> Whether or not the item is being highlighted positively. </param>
		public void HighlightItem ( int id, string instance, bool isPositive )
		{
			// Highlight an item in use
			itemsHUD.HighlightItem ( id, instance, isPositive );
		}

		/// <summary>
		/// Highlights an consumable in use.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		public void HighlightConsumable ( int id )
		{
			// Highlight an consumable in use
			consumablesHUD.HighlightConsumable ( id );
		}

		/// <summary>
		/// Applies money earned during the performance.
		/// </summary>
		/// <param name="money"> The amount of money being earned. </param>
		public void EarnMoney ( int money )
		{
			// Update money
			moneyHUD.ApplyMoney ( money, GameManager.Run.Money, GameManager.Run.Debt );

			// Earn money
			GameManager.Run.ApplyMoney ( money );
		}

		/// <summary>
		/// Check whether or not the player has any arrogance.
		/// </summary>
		/// <returns></returns>
		public bool HasArrogance ( )
		{
			return model.ArroganceRemaining > 0;
		}

		/// <summary>
		/// Consumes arrogance upon an ignored flub.
		/// </summary>
		public void LoseArrogance ( )
		{
			// Decrement arrogance
			model.ArroganceRemaining--;

			// Update confidence display
			confidenceHUD.LoseArrogance ( GameManager.Run.MaxConfidence, model.ConfidenceRemaining, model.ArroganceRemaining );
		}

		/// <summary>
		/// Adds confidence.
		/// </summary>
		public void GainConfidence ( )
		{
			// Increment confidence
			model.ConfidenceRemaining++;

			// Update confidence display
			confidenceHUD.GainConfidence ( GameManager.Run.MaxConfidence, model.ConfidenceRemaining, model.ArroganceRemaining );
		}

		/// <summary>
		/// Consumes confidence upon a flub.
		/// </summary>
		public void LoseConfidence ( )
		{
			// Check for judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Check for The Futurist judge
				if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetFuturistId ( ) )
				{
					// Deduct time instead of confidence
					model.TimeRemaining -= Judges.JudgeHelper.GetFuturistFlubPenalty ( );
					return;
				}
				// Check for the Business Man judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetBusinessManId ( ) )
				{
					// Get penalty
					int penalty = Judges.JudgeHelper.GetBusinessManFlubPenalty ( );

					// Check current money
					moneyHUD.ApplyMoney ( penalty, GameManager.Run.Money, GameManager.Run.Debt );

					// Deduct money
					GameManager.Run.ApplyMoney ( penalty );
				}
				// Check for the Bartender judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetBartenderId ( ) )
				{
					// Apply Impaired
					StatusEffects.StatusEffectModel statusEffect = Judges.JudgeHelper.GetBartenderStatusEffects ( );

					// Check for end of line
					if ( poemManager.IsEndOfLine ( ) )
					{
						// Queue up status effect
						poemManager.QueueStatusEffect ( statusEffect );
					}
					else
					{
						// Apply status effect
						GameManager.Run.AddStatusEffect ( statusEffect );

						// Update status effect HUD
						statusEffectHUD.SetStatusEffects ( );
					}
				}
			}

			// Decrement confidence
			model.ConfidenceRemaining--;

			// Trigger any on confidence lost effects from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnLoseConfidence ( model ) )
					{
						// Highlight item
						HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}

			// Update confidence display
			confidenceHUD.LoseConfidence ( GameManager.Run.MaxConfidence, model.ConfidenceRemaining, model.ArroganceRemaining );

			// Check for remaining confidence
			if ( model.ConfidenceRemaining <= 0 )
			{
				// Trigger failure
				OnNoConfidenceRemaining ( );
			}
		}

		/// <summary>
		/// Earns snaps for completing a line in the poem.
		/// </summary>
		/// <param name="snaps"> The amount of snaps earned from the line. </param>
		public void EarnSnaps ( int snaps )
		{
			// Add snaps earned
			model.SnapsCount += snaps;

			// Update snaps display
			snapsHUD.AddSnaps ( snaps );
		}

		/// <summary>
		/// Marks the end of the performance when the poem has been completed
		/// </summary>
		public void CompletePerformance ( )
		{
			// Create summary stats
			SummaryStatsModel stats = new SummaryStatsModel ( );

			// Store money from confidence
			stats.Confidence = GetSnapsForConfidence ( model.ConfidenceRemaining );
			stats.Reputation = GameManager.Run.Reputation;
			model.SnapsCount += stats.Confidence;

			// Store applause
			stats.Applause = GetApplause ( );
			for ( int i = 0; i < stats.Applause.Length; i++ )
			{
				model.SnapsCount += stats.Applause [ i ].Applause;
			}

			// Store snaps from time
			stats.Time = GetMoneyForTimeRemaining ( model.TimeRemaining );

			// Store interest
			stats.Interest = GetMoneyForInterest ( GameManager.Run.Money );
			stats.InterestCap = model.InterestCap;

			// Store commission
			stats.Commission = model.Commission;

			// Trigger any on complete performance effect from the player's perk
			GameManager.Run.Perk.OnCompletePerformance ( model, stats );

			// Trigger any on complete performance effects from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Trigger item
					GameManager.Run.ItemData [ i ].Item.OnCompletePerformance ( model, stats );
				}
			}

			// Check snaps
			if ( model.SnapsCount >= model.SnapsGoal )
			{
				// Store success
				stats.IsSuccess = true;

				// Trigger success
				OnPerformanceSuccess ( stats );
			}
			else
			{
				// Store failure
				stats.IsSuccess = false;

				// Trigger failure
				OnPerformanceFail ( );
			}

			// Display summary
			summaryHUD.ShowSummary ( stats );

			// Add money earned
			// Adding the money after the summary is called allows the summary to accurately display money and debt changes
			GameManager.Run.ApplyMoney ( stats.Time + stats.Commission + stats.Interest );
		}

		/// <summary>
		/// Adds a callback for the pause event.
		/// </summary>
		/// <param name="pause"> The callback for the pause event. </param>
		public void AddOnPause ( System.Action<bool> pause )
		{
			// Check for callback
			if ( pause != null )
			{
				onPause += pause;
			}
		}

		public void Skip ( )
		{
			model.SnapsCount = model.SnapsGoal;
			CompletePerformance ( );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Initializes the performance.
		/// </summary>
		private void Init ( )
		{
			// Initialize data
			GetPerformanceModel ( );

			// Get poem
			Poems.PoemModel poemModel = GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ];
			Poems.PoemScriptableObject poem = Poems.PoemUtility.GetPoem ( poemModel.ID );

			// Get enhancements
			ApplyEnhancements ( poemModel );

			// Create stanzas
			Poems.Stanza [ ] stanzas = poemModel.ToStanzas ( );

			// Get word modifiers
			Poems.WordModel [ ] wordModifiers = GetWordModifiers ( stanzas, poemModel.Modifiers );

			// Set base snaps per character
			model.BaseSnaps = poemModel.BaseSnaps;

			// Setup poem
			poemManager.SetPoem ( stanzas, wordModifiers );

			// Set money
			moneyHUD.SetMoney ( GameManager.Run.Money, GameManager.Run.Debt );

			// Setup confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Setup consumables
			consumablesHUD.SetConsumables ( null, null, null );

			// Setup status effects
			statusEffectHUD.SetStatusEffects ( );

			// Setup poet
			poetHUD.SetPoet ( Poets.PoetUtility.GetPoet ( GameManager.Run.PoetID ) );

			// Setup timer
			isTimerActive = false;
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, model.TimeRemaining );

			// Setup items
			itemsHUD.SetItems ( null, null );

			// Setup snaps
			snapsHUD.SetGoal ( model.SnapsGoal, model.SnapsCount );

			// Check for final performance of the round
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Setup judge
				judgeHUD.SetJudge ( Judges.JudgeUtility.GetJudge ( GameManager.Run.CurrentRound.JudgeID ) );
			}
			else
			{
				// Hide judge
				judgeHUD.HideJudge ( );
			}

			// Disable pausing for the intro
			pauseHUD.CanPause = false;

			// Play credits
			creditsHUD.PlayIntro ( poem.Title, poem.Author, poem.Rating, GameManager.Run.IsLastPerformanceOfRound ( ), BeginPerformance );
		}

		/// <summary>
		/// Initializes the performance data.
		/// </summary>
		private void GetPerformanceModel ( )
		{
			// Initialize default performance data
			model = new PerformanceModel ( );

			// Set snaps goal
			model.SnapsGoal = GameManager.Difficulty.GetSnapsRequirement ( GameManager.Run.Round, GameManager.Run.Performance );

			// Set commission
			model.Commission = GameManager.Difficulty.GetCommission ( GameManager.Run.Performance );

			// Set flub penalty
			model.FlubPenalty = GameManager.Difficulty.FlubPenatly;

			// Check for judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Check for Public Speaker judge
				if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetPublicSpeakerId ( ) )
				{
					// Increase flub penalty
					model.FlubPenalty = Judges.JudgeHelper.GetPublicSpeakerFlubPenalty ( model.FlubPenalty );
				}
				// Check for Literary Critic judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetLiteraryCriticId ( ) )
				{
					// Increase snaps goal
					model.SnapsGoal = Judges.JudgeHelper.GetLiteraryCriticSnapsGoal ( GameManager.Difficulty.Rounds [ GameManager.Run.Round ] );
				}
				// Check for Clown judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetClownId ( ) )
				{
					// Disable random item
					Judges.JudgeHelper.SetClownItemsEnabled ( false );
				}
				// Check for Therapist judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetTherapistId ( ) )
				{
					// Remove status effects
					Judges.JudgeHelper.SetTherapistStatusEffects ( );
				}
				// Check for Celebrity judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetCelebrityId ( ) )
				{
					// Apply anxious
					Judges.JudgeHelper.SetCelebrityStatusEffects ( );
				}
				// Check for Bartender judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetBartenderId ( ) )
				{
					// Apply Impaired
					GameManager.Run.AddStatusEffect ( Judges.JudgeHelper.GetBartenderStatusEffects ( ) );
				}
			}

			// Set confidence
			model.ConfidenceRemaining = GameManager.Run.MaxConfidence;

			// Set arrogance
			model.ArroganceRemaining = GameManager.Run.MaxArrogance;

			// Set time
			model.TimeRemaining = GameManager.Run.TimeAllowance;

			// Apply perk modifications
			GameManager.Run.Perk.OnInitPerformance ( model );

			// Apply upgrade modifications
			for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( GameManager.Run.IsValidUpgrade ( i ) )
				{
					// Apply upgrade effect
					GameManager.Run.UpgradeData [ i ].Upgrade.OnInitPerformance ( model );
				}
			}

			// Apply item modifications
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Apply item effect
					GameManager.Run.ItemData [ i ].Item.OnInitPerformance ( model );
				}
			}

			// Apply encounter modifications
			model.Commission += GameManager.Run.EncounterData.Commission;
			if ( model.Commission < 0 )
			{
				model.Commission = 0;
			}
			model.InterestCap += GameManager.Run.EncounterData.InterestCap;
			if ( model.InterestCap < 0 )
			{
				model.InterestCap = 0;
			}
		}

		/// <summary>
		/// Applies any enhancements from the poem.
		/// </summary>
		/// <param name="poemModel"> The data for the poem. </param>
		private void ApplyEnhancements ( Poems.PoemModel poemModel )
		{
			// Check for Health Inspector judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) )
			{
				return;
			}

			// Add enhancements
			model.Commission += poemModel.Commission;

			// Apply any enhancements from items
			Poems.PoemModel itemEnhancements = new Poems.PoemModel ( );
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Get any enhancements
					itemEnhancements.Add ( GameManager.Run.ItemData [ i ].Item.OnEnhancePoem ( ) );
				}
			}

			// Check for any additional enhancements
			if ( itemEnhancements.HasEnhancements )
			{
				// Update performance data
				model.ConfidenceRemaining += itemEnhancements.Confidence;
				model.ArroganceRemaining += itemEnhancements.Arrogance;
				model.TimeRemaining += itemEnhancements.TimeAllowance;
				model.Commission += itemEnhancements.Commission;

				// Update poem data
				GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].Add ( itemEnhancements );

				// Check for permanent poem
				if ( poemModel.Level > 0 )
				{
					// Enhancement permanent draft poem
					GameManager.Run.EnhancePermanentDraftPoem ( poemModel.ID, itemEnhancements );
				}
			}
		}

		/// <summary>
		/// Gets the words to modify for the performance.
		/// </summary>
		/// <param name="stanzas"> The array of stanzas for the poem. </param>
		/// <returns> The array of modified words in the poem. </returns>
		private Poems.WordModel [ ] GetWordModifiers ( Poems.Stanza [ ] stanzas, Poems.WordModel [ ] oldWordModifiers )
		{
			// Check for item modifiers
			bool isAddingPermanentModifiers = false;

			// Check if items add modifiers
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Check for modifiers
					if ( GameManager.Run.ItemData [ i ].Item.IsModifyingWords ( ) )
					{
						isAddingPermanentModifiers = true;
						break;
					}
				}
			}

			// Check for judge modifiers
			bool isAddingTempModifiers = false;

			// Check for the Censor judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetCensorId ( ) )
			{
				isAddingTempModifiers = true;
			}
			// Check for The Imagist judge
			else if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetImagistId ( ) )
			{
				isAddingTempModifiers = true;
			}

			// Check if modifiers are being added
			if ( isAddingPermanentModifiers || isAddingTempModifiers )
			{
				// Store word modifiers
				List<Poems.WordModel> wordModifiers = new List<Poems.WordModel> ( );
				if ( oldWordModifiers != null )
				{
					wordModifiers.AddRange ( oldWordModifiers );
				}

				// Get available words
				Dictionary<Poems.LineKey, List<Poems.WordModel>> availableWords = Poems.PoemHelper.GetWords ( stanzas, oldWordModifiers );

				// Get keys for each line
				List<Poems.LineKey> keys = availableWords.Keys.ToList ( );

				// Check if adding item modifiers
				if ( isAddingPermanentModifiers )
				{
					// Get new modifiers
					List<Enums.WordModifierType> newModifiers = new List<Enums.WordModifierType> ( );
					for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
						{
							// Get new modifiers
							Enums.WordModifierType [ ] itemModifiers = GameManager.Run.ItemData [ i ].Item.OnModifyWords ( );

							// Check for modifiers
							if ( itemModifiers != null )
							{
								// Highlight item
								itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );

								// Store modifiers
								newModifiers.AddRange ( itemModifiers );
							}
						}
					}

					// Check for new modifiers
					if ( newModifiers.Count > 0 )
					{
						// Store new word modifiers
						List<Poems.WordModel> newWordModifiers = new List<Poems.WordModel> ( );

						// Apply each new word modifier
						for ( int i = 0; i < newModifiers.Count; i++ )
						{
							// Get random line
							int keyIndex = 0;
							if ( keys.Count > 1 )
							{
								keyIndex = Random.Range ( 0, keys.Count );
							}
							Poems.LineKey key = keys [ keyIndex ];

							// Check for available words
							if ( availableWords [ key ] != null && availableWords [ key ].Count > 0 )
							{
								// Get random word
								Poems.WordModel word = availableWords [ key ] [ Random.Range ( 0, availableWords [ key ].Count ) ];

								// Remove word availability
								availableWords [ key ].Remove ( word );

								// Add modifier
								word.Modifier = newModifiers [ i ];
								newWordModifiers.Add ( word );
								wordModifiers.Add ( word );

								// Update stats
								GameManager.Run.Stats.OnApplyModifier ( word.Modifier );

								// Check for remaining words
								if ( availableWords [ key ].Count < 1 )
								{
									availableWords.Remove ( key );
									break;
								}
							}
						}

						// Permanently add new word modifiers to the poem
						GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].AddModifiers ( newWordModifiers.ToArray ( ) );
						if ( GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].Level > 0 )
						{
							// Get poem ID
							int id = GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].ID;
							for ( int i = 0; i < GameManager.Run.PermanentDraftPoems.Length; i++ )
							{
								// Check for poem
								if ( GameManager.Run.PermanentDraftPoems [ i ] != null && GameManager.Run.PermanentDraftPoems [ i ].ID == id )
								{
									// Add modifiers
									GameManager.Run.PermanentDraftPoems [ i ].AddModifiers ( newWordModifiers.ToArray ( ) );
									break;
								}
							}
						}
					}
				}

				// Check if adding judge modifiers
				if ( isAddingTempModifiers )
				{
					// Apply word modifiers to each line
					for ( int i = 0; i < keys.Count; i++ )
					{
						// Get key
						Poems.LineKey key = keys [ i ];

						// Get new modifiers
						Enums.WordModifierType [ ] newModifiers = null;

						// Check for the Censor judge
						if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetCensorId ( ) )
						{
							// Get modifiers
							newModifiers = Judges.JudgeHelper.GetCensorWordModifier ( );
						}
						// Check for The Imagist judge
						else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetImagistId ( ) )
						{
							// Get modifiers
							newModifiers = Judges.JudgeHelper.GetImagistWordModifier ( );
						}

						// Check for modifiers
						if ( newModifiers != null )
						{
							// Apply modifiers to the line
							for ( int j = 0; j < newModifiers.Length; j++ )
							{
								// Check for available words
								if ( availableWords [ key ] != null && availableWords [ key ].Count > 0 )
								{
									// Get random word
									Poems.WordModel word = availableWords [ key ] [ Random.Range ( 0, availableWords [ key ].Count ) ];

									// Remove word availability
									availableWords [ key ].Remove ( word );

									// Add modifier
									word.Modifier = newModifiers [ j ];
									wordModifiers.Add ( word );

									// Check for remaining words
									if ( availableWords [ key ].Count < 1 )
									{
										availableWords.Remove ( key );
										break;
									}
								}
							}
						}
					}
				}

				// Return list of new and old modifiers
				return wordModifiers.OrderBy ( x => x.StartIndex ).ToArray ( );
			}
			else
			{
				// Return existing modifiers
				return oldWordModifiers;
			}
		}

		/// <summary>
		/// Starts the performance of the poem.
		/// </summary>
		private void BeginPerformance ( )
		{
			// Start timer
			isPerforming = true;
			isTimerActive = true;

			// Enable pausing
			pauseHUD.CanPause = true;

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnInitPerformance ( );
			}

			// Begins poem
			poemManager.BeginPoem ( );
		}

		/// <summary>
		/// The callback for when the performance is paused by a menu.
		/// </summary>
		private void OnPause ( )
		{
			// Pause timer
			isTimerActive = false;

			// Trigger callback
			if ( onPause != null )
			{
				onPause ( true );
			}
		}

		/// <summary>
		/// The callback for when the performance is resumed by a menu.
		/// </summary>
		private void OnResume ( )
		{
			// Unpause if performance is in progress
			if ( isPerforming )
			{
				// Resume timer
				isTimerActive = true;

				// Trigger callback
				if ( onPause != null )
				{
					onPause ( false );
				}
			}
		}

		/// <summary>
		/// The callback for when the performance is successfully completed.
		/// </summary>
		/// <param name="stats"> The stats calculated for the summary. </param>
		private void OnPerformanceSuccess ( SummaryStatsModel stats )
		{
			// Stop timer
			isPerforming = false;
			isTimerActive = false;

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnCompletePerformance ( GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].ID, model, stats );
			}
		}

		/// <summary>
		/// The callback for when the performance is 
		/// </summary>
		private void OnPerformanceFail ( )
		{
			// Stop timer
			isPerforming = false;
			isTimerActive = false;

			// Stop poem
			poemManager.OnPoemFail ( );
		}

		/// <summary>
		/// The callback for when no confidence remaings during the performance.
		/// </summary>
		private void OnNoConfidenceRemaining ( )
		{
			// Fail the performance
			OnPerformanceFail ( );

			// Display failure screen
			summaryHUD.ShowOutOfConfidence ( );
		}

		/// <summary>
		/// The callback for when time expires during the performance.
		/// </summary>
		private void OnTimerExpire ( )
		{
			// Fail the performance
			OnPerformanceFail ( );

			// Display failure screen
			summaryHUD.ShowOutOfTime ( );
		}

		/// <summary>
		/// Calculates the amount of snaps earned from the remaining confidence.
		/// </summary>
		/// <param name="confidence"> The amount of confidence remaining. </param>
		/// <returns> The amount of money earned. </returns>
		private int GetSnapsForConfidence ( int confidence )
		{
			// Return money earned
			return GameManager.Run.Reputation * confidence;
		}

		/// <summary>
		/// Calculates the applause earned from the performance.
		/// </summary>
		/// <returns> The data for the bonuses. </returns>
		private ApplauseModel [ ] GetApplause ( )
		{
			// Store applause
			List<ApplauseModel> applause = new List<ApplauseModel> ( );
			int total = 0;

			// Get perk applause
			ApplauseModel perkApplause = GameManager.Run.Perk.OnApplause ( model, total );

			// Check for applause
			if ( perkApplause.Applause > 0 )
			{
				// Increment applause earned
				total += perkApplause.Applause;

				// Store applause
				applause.Add ( perkApplause );
			}

			// Get enhancement applause
			ApplauseModel enhancementModel = new ApplauseModel
			{
				Applause = GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].Applause
			};

			// Check for applause and the Health Inspector judge
			if ( enhancementModel.Applause > 0 && !( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) ) )
			{
				// Increment applause earned
				total += enhancementModel.Applause;

				// Store applause
				applause.Add ( enhancementModel );
			}

			// Get applause from upgrades
			for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( GameManager.Run.IsValidUpgrade ( i ) )
				{
					// Get applause
					ApplauseModel upgradeApplause = GameManager.Run.UpgradeData [ i ].Upgrade.OnApplause ( model, total );

					// Check for applause
					if ( upgradeApplause.Applause > 0 )
					{
						// Increment applause earned
						total += upgradeApplause.Applause;

						// Store applause
						applause.Add ( upgradeApplause );
					}
				}
			}

			// Get applause from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Get applause
					ApplauseModel itemApplause = GameManager.Run.ItemData [ i ].Item.OnApplause ( model, total );

					// Check for applause
					if ( itemApplause.ItemID != Items.ItemModel.NO_ITEM_ID && itemApplause.Applause > 0 )
					{
						// Increment applause earned
						total += itemApplause.Applause;

						// Store applause
						applause.Add ( itemApplause );
					}
				}
			}

			// Check for the Librarian judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetLibrarianId ( ) )
			{
				// Check for applause
				if ( total > 0 )
				{
					// Add applause penalty
					applause.Add ( Judges.JudgeHelper.GetLibrarianApplause ( total ) );
				}
			}

			// Return applause
			return applause.ToArray ( );
		}

		/// <summary>
		/// Calculates the amount of money earned from the time remaining at the end of the performance.
		/// </summary>
		/// <param name="timeRemaining"> The amount of time remaining in seconds. </param>
		/// <returns> The amount of money earned. </returns>
		private int GetMoneyForTimeRemaining ( float timeRemaining )
		{
			// Get increments of time
			int increments = (int)( timeRemaining / model.TimeIncrement );

			// Return snaps earned
			return model.MoneyPerTime * increments;
		}

		/// <summary>
		/// Calculates the amount of money earned from interest at the end of the performance.
		/// </summary>
		/// <param name="money"> The amount of money owned at the end of the performance. </param>
		/// <returns> The amount of money earned. </returns>
		private int GetMoneyForInterest ( int money )
		{
			// Get increments of interest
			int increments = money / model.InterestRate;

			// Check for interest cap
			if ( increments > model.InterestCap )
			{
				increments = model.InterestCap;
			}

			// Check for negative interest
			if ( increments < 0 )
			{
				increments = 0;
			}
			
			// Return money earned
			return increments;
		}

		#endregion // Private Functions
	}
}