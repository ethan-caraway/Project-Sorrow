using System.Collections.Generic;

namespace FlightPaper.ProjectSorrow.Progression
{
	/// <summary>
	/// This class stores the progression stats for the player.
	/// </summary>
	[System.Serializable]
	public class ProgressModel
	{
		#region Progress Data Constants

		private const int TOTAL_ITEMS = 150;
		private const int TOTAL_CONSUMABLES = 60;
		private const int TOTAL_POETS = 20;
		private const int TOTAL_JUDGES = 40;
		private const int TOTAL_UPGRADES = 25;
		private const int TOTAL_MODIFIERS = 6;
		private const int TOTAL_STATUS_EFFECTS = 6;
		private const int TOTAL_DIFFICULTIES = 12;

		#endregion // Progress Data Constants

		#region Progress Data

		/// <summary>
		/// Whether or not all progress unlocks should be ignored.
		/// </summary>
		public bool IsAllUnlocked;

		/// <summary>
		/// The stats for item usage by the player.
		/// </summary>
		public ItemStatsModel [ ] ItemStats = new ItemStatsModel [ TOTAL_ITEMS ];

		/// <summary>
		/// The stats for consumable usage by the player.
		/// </summary>
		public ConsumableStatsModel [ ] ConsumableStats = new ConsumableStatsModel [ TOTAL_CONSUMABLES ];

		/// <summary>
		/// The stats for poet usage by the player.
		/// </summary>
		public PoetStatsModel [ ] PoetStats = new PoetStatsModel [ TOTAL_POETS ];

		/// <summary>
		/// The stats for judges performed against by the player.
		/// </summary>
		public JudgeStatsModel [ ] JudgeStats = new JudgeStatsModel [ TOTAL_JUDGES ];

		/// <summary>
		/// The stats for upgrade usage by the player.
		/// </summary>
		public UpgradeStatsModel [ ] UpgradeStats = new UpgradeStatsModel [ TOTAL_UPGRADES ];

		/// <summary>
		/// The stats for modifier usage by the player.
		/// </summary>
		public ModifierStatsModel [ ] ModifierStats = new ModifierStatsModel [ TOTAL_MODIFIERS ];

		/// <summary>
		/// The stats for status effect usage by the player.
		/// </summary>
		public StatusEffectStatsModel [ ] StatusEffectStats = new StatusEffectStatsModel [ TOTAL_STATUS_EFFECTS ];

		/// <summary>
		/// The stats for the difficulty of runs by the player.
		/// </summary>
		public DifficultyStatsModel [ ] DifficultyStats = new DifficultyStatsModel [ TOTAL_DIFFICULTIES ];

		/// <summary>
		/// The stats for challenge progression by the player.
		/// </summary>
		public ChallengeStatsModel ChallengeStats = new ChallengeStatsModel ( );

		/// <summary>
		/// The total number of successful performances completed.
		/// </summary>
		public int TotalSuccessfulPerformances = 0;

		/// <summary>
		/// The total number of runs won.
		/// </summary>
		public int TotalWins = 0;

		/// <summary>
		/// The highest difficulty the player has won a run on.
		/// </summary>
		public int HighestDifficultyWin = 0;

		private Dictionary<int, int> itemStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> consumableStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> poetStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> judgeStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> upgradeStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> modifierStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> statusEffectStatIndexes = new Dictionary<int, int> ( );
		private Dictionary<int, int> difficultyStatIndexes = new Dictionary<int, int> ( );

		#endregion // Progress Data

		#region Public Functions

		/// <summary>
		/// Initialize the progress data.
		/// </summary>
		public void Init ( )
		{
			// Check for item stats
			if ( ItemStats == null || ItemStats.Length != TOTAL_ITEMS || ItemStats [ 0 ] == null || ItemStats [ 0 ].ID == Items.ItemModel.NO_ITEM_ID )
			{
				InitItemStats ( );
			}

			// Store item dictionary
			itemStatIndexes.Clear ( );
			for ( int i = 0; i < ItemStats.Length; i++ )
			{
				itemStatIndexes.Add ( ItemStats [ i ].ID, i );
			}

			// Check for consumable stats
			if ( ConsumableStats == null || ConsumableStats.Length != TOTAL_CONSUMABLES || ConsumableStats [ 0 ] == null || ConsumableStats [ 0 ].ID == Consumables.ConsumableModel.NO_CONSUMABLE_ID )
			{
				InitConsumableStats ( );
			}

			// Store consumable dictionary
			consumableStatIndexes.Clear ( );
			for ( int i = 0; i < ConsumableStats.Length; i++ )
			{
				consumableStatIndexes.Add ( ConsumableStats [ i ].ID, i );
			}

			// Check for poet stats
			if ( PoetStats == null || PoetStats.Length != TOTAL_POETS || PoetStats [ 0 ] == null || PoetStats [ 0 ].ID == 0 )
			{
				InitPoetStats ( );
			}

			// Store poet dictionary
			poetStatIndexes.Clear ( );
			for ( int i = 0; i < PoetStats.Length; i++ )
			{
				poetStatIndexes.Add ( PoetStats [ i ].ID, i );
			}

			// Check for judge stats
			if ( JudgeStats == null || JudgeStats.Length != TOTAL_JUDGES || JudgeStats [ 0 ] == null || JudgeStats [ 0 ].ID == 0 )
			{
				InitJudgeStats ( );
			}

			// Store judge dictionary
			judgeStatIndexes.Clear ( );
			for ( int i = 0; i < JudgeStats.Length; i++ )
			{
				judgeStatIndexes.Add ( JudgeStats [ i ].ID, i );
			}

			// Check for upgrade stats
			if ( UpgradeStats == null || UpgradeStats.Length != TOTAL_UPGRADES || UpgradeStats [ 0 ] == null || UpgradeStats [ 0 ].ID == Upgrades.UpgradeModel.NO_UPGRADE_ID )
			{
				InitUpgradeStats ( );
			}

			// Store upgrade dictionary
			upgradeStatIndexes.Clear ( );
			for ( int i = 0; i < UpgradeStats.Length; i++ )
			{
				upgradeStatIndexes.Add ( UpgradeStats [ i ].ID, i );
			}

			// Check for modifier stats
			if ( ModifierStats == null || ModifierStats.Length != TOTAL_MODIFIERS || ModifierStats [ 0 ] == null || ModifierStats [ 0 ].ID == 0 )
			{
				InitModifierStats ( );
			}

			// Store modifier dictionary
			modifierStatIndexes.Clear ( );
			for ( int i = 0; i < ModifierStats.Length; i++ )
			{
				modifierStatIndexes.Add ( ModifierStats [ i ].ID, i );
			}

			// Check for status effect stats
			if ( StatusEffectStats == null || StatusEffectStats.Length != TOTAL_STATUS_EFFECTS || StatusEffectStats [ 0 ] == null || StatusEffectStats [ 0 ].ID == 0 )
			{
				InitStatusEffectStats ( );
			}

			// Store status effect dictionary
			statusEffectStatIndexes.Clear ( );
			for ( int i = 0; i < StatusEffectStats.Length; i++ )
			{
				statusEffectStatIndexes.Add ( StatusEffectStats [ i ].ID, i );
			}

			// Check for difficulty stats
			if ( DifficultyStats == null || DifficultyStats.Length != TOTAL_DIFFICULTIES || DifficultyStats [ 0 ] == null || DifficultyStats [ 0 ].ID == 0 )
			{
				InitDifficultyStats ( );
			}

			// Store modifier dictionary
			difficultyStatIndexes.Clear ( );
			for ( int i = 0; i < DifficultyStats.Length; i++ )
			{
				difficultyStatIndexes.Add ( DifficultyStats [ i ].ID, i );
			}
		}

		/// <summary>
		/// Gets the stats for an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The stat data for the item. </returns>
		public ItemStatsModel GetItemStats ( int id )
		{
			// Check for ID
			if ( itemStatIndexes.ContainsKey ( id ) )
			{
				// Return item stats data
				return ItemStats [ itemStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a consumable.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <returns> The stat data for the consumable. </returns>
		public ConsumableStatsModel GetConsumableStats ( int id )
		{
			// Check for ID
			if ( consumableStatIndexes.ContainsKey ( id ) )
			{
				// Return consumable stats data
				return ConsumableStats [ consumableStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a poet.
		/// </summary>
		/// <param name="id"> The ID of the poet. </param>
		/// <returns> The stat data for the poet. </returns>
		public PoetStatsModel GetPoetStats ( int id )
		{
			// Check for ID
			if ( poetStatIndexes.ContainsKey ( id ) )
			{
				// Return poet stats data
				return PoetStats [ poetStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a judge.
		/// </summary>
		/// <param name="id"> The ID of the judge. </param>
		/// <returns> The stat data for the judge. </returns>
		public JudgeStatsModel GetJudgeStats ( int id )
		{
			// Check for ID
			if ( judgeStatIndexes.ContainsKey ( id ) )
			{
				// Return judge stats data
				return JudgeStats [ judgeStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for an upgrade.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		/// <returns> The stat data for the upgrade. </returns>
		public UpgradeStatsModel GetUpgradeStats ( int id )
		{
			// Check for ID
			if ( upgradeStatIndexes.ContainsKey ( id ) )
			{
				// Return upgrade stats data
				return UpgradeStats [ upgradeStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a modifier.
		/// </summary>
		/// <param name="id"> The ID of the modifier. </param>
		/// <returns> The stat data for the modifier. </returns>
		public ModifierStatsModel GetModifierStats ( int id )
		{
			// Check for ID
			if ( modifierStatIndexes.ContainsKey ( id ) )
			{
				// Return modifier stats data
				return ModifierStats [ modifierStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a status effect.
		/// </summary>
		/// <param name="id"> The ID of the status effect. </param>
		/// <returns> The stat data for the status effect. </returns>
		public StatusEffectStatsModel GetStatusEffectStats ( int id )
		{
			// Check for ID
			if ( statusEffectStatIndexes.ContainsKey ( id ) )
			{
				// Return status effect stats data
				return StatusEffectStats [ statusEffectStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		/// <summary>
		/// Gets the stats for a difficulty.
		/// </summary>
		/// <param name="id"> The ID of the difficulty. </param>
		/// <returns> The stat data for the difficulty. </returns>
		public DifficultyStatsModel GetDifficultyStats ( int id )
		{
			// Check for ID
			if ( difficultyStatIndexes.ContainsKey ( id ) )
			{
				// Return difficulty stats data
				return DifficultyStats [ difficultyStatIndexes [ id ] ];
			}

			// Return no data
			return null;
		}

		#endregion // Public Functions

		#region Public Event Functions

		/// <summary>
		/// The callback for when a new run is started.
		/// </summary>
		/// <param name="poetID"> The ID of the poet for the run. </param>
		/// <param name="difficultyID"> The ID of the difficulty for the run. </param>
		public void OnStartRun ( int poetID, int difficultyID )
		{
			// Check for poet
			if ( poetStatIndexes.ContainsKey ( poetID ) )
			{
				// Increment runs
				PoetStats [ poetStatIndexes [ poetID ] ].Runs++;
			}

			// Check for difficulty
			if ( difficultyStatIndexes.ContainsKey ( difficultyID ) )
			{
				// Increment runs
				DifficultyStats [ difficultyStatIndexes [ difficultyID ] ].Runs++;
			}
		}

		/// <summary>
		/// The callback for when an item is acquired during a run.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		public void OnAddItem ( int id )
		{
			// Check for ID
			if ( itemStatIndexes.ContainsKey ( id ) )
			{
				// Increment owned stat
				ItemStats [ itemStatIndexes [ id ] ].Owns++;
			}
		}

		/// <summary>
		/// The callback for when a consumable is acquired during a run.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <param name="count"> The number of instances of the consumable being added. </param>
		public void OnAddConsumable ( int id, int count )
		{
			// Check for ID
			if ( consumableStatIndexes.ContainsKey ( id ) )
			{
				// Increment owned stat
				ConsumableStats [ consumableStatIndexes [ id ] ].Owns += count;
			}
		}

		/// <summary>
		/// The callback for when an upgrade is acquired during a run.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		public void OnAddUpgrade ( int id )
		{
			// Check for ID
			if ( upgradeStatIndexes.ContainsKey ( id ) )
			{
				// Increment owned stat
				UpgradeStats [ upgradeStatIndexes [ id ] ].Owns++;
			}
		}

		/// <summary>
		/// The callback for when a consumable is consumed by the player during a run.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <param name="instances"> The number of instances of the consumable being consumed. </param>
		public void OnConsumeConsumable ( int id, int instances )
		{
			// Check for consumable
			if ( consumableStatIndexes.ContainsKey ( id ) )
			{
				// Increment consumable consume stat
				ConsumableStats [ consumableStatIndexes [ id ] ].Consumed += instances;
			}
		}

		/// <summary>
		/// The callback for when a modifier is applied to a word during a run.
		/// </summary>
		/// <param name="modifier"> The modifier being applied. </param>
		public void OnApplyModifier ( Enums.WordModifierType modifier )
		{
			// Check for modifier
			if ( modifierStatIndexes.ContainsKey ( (int)modifier ) )
			{
				// Increment modifier apply stat
				ModifierStats [ modifierStatIndexes [ (int)modifier ] ].Applies++;
			}
		}

		/// <summary>
		/// The callback for when a status is applied during a run.
		/// </summary>
		/// <param name="statusEffect"> The status effect being applied. </param>
		/// <param name="count"> The number of stacks of the status effect being applied. </param>
		/// <param name="total"> The current total stacks of the status effect owned. </param>
		public void OnApplyStatusEffect ( Enums.StatusEffectType statusEffect, int count, int total )
		{
			// Check for status effect
			if ( statusEffectStatIndexes.ContainsKey ( (int)statusEffect ) )
			{
				// Increment status effect apply stat
				StatusEffectStats [ statusEffectStatIndexes [ (int)statusEffect ] ].Applies += count;

				// Check for new record
				if ( total > StatusEffectStats [ statusEffectStatIndexes [ (int)statusEffect ] ].Stacks )
				{
					// Store new status effect stack stat
					StatusEffectStats [ statusEffectStatIndexes [ (int)statusEffect ] ].Stacks = total;
				}
			}
		}

		/// <summary>
		/// The callback for when a performance is initialized.
		/// </summary>
		/// <param name="performance"> The index of the performance in the round. </param>
		/// <param name="round"> The index of the round in the run. </param>
		/// <param name="poetID"> The ID of the poet in the run. </param>
		/// <param name="judgeID"> The ID of the judge of for the performance. 0 if no judge. </param>
		public virtual void OnInitPerformance ( int performance, int round, int poetID, int judgeID )
		{
			// Check for poet
			if ( poetStatIndexes.ContainsKey ( poetID ) )
			{
				// Check for new highest round
				if ( round + 1 > PoetStats [ poetStatIndexes [ poetID ] ].HighestRound )
				{
					PoetStats [ poetStatIndexes [ poetID ] ].HighestRound = round + 1;
				}

				// Check for new highest performance
				if ( performance + 1 + ( round * Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND ) > PoetStats [ poetStatIndexes [ poetID ] ].HighestPerformance )
				{
					PoetStats [ poetStatIndexes [ poetID ] ].HighestPerformance = performance + 1 + ( round * Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND );
				}
			}

			// Check for judge
			if ( judgeStatIndexes.ContainsKey ( judgeID ) )
			{
				// Increment judge performance stat
				JudgeStats [ judgeStatIndexes [ judgeID ] ].Performances++;
			}
		}
		
		/// <summary>
		/// The callback for when a word modifier triggers.
		/// </summary>
		/// <param name="modifier"> The modifier being triggered. </param>
		/// <param name="value"> The value of the stat to add. </param>
		public void OnModifierTrigger ( Enums.WordModifierType modifier, int value )
		{
			// Check for modifier
			if ( modifierStatIndexes.ContainsKey ( (int)modifier ) )
			{
				// Increment modifier trigger stat
				ModifierStats [ modifierStatIndexes [ (int)modifier ] ].Triggered += value;
			}
		}

		/// <summary>
		/// The callback for when a status effect triggers.
		/// </summary>
		/// <param name="statusEffect"> The status effect being triggered. </param>
		/// <param name="value"> The value of the stat to add. </param>
		public void OnStatusEffectTrigger ( Enums.StatusEffectType statusEffect, int value )
		{
			// Check for status effect
			if ( statusEffectStatIndexes.ContainsKey ( (int)statusEffect ) )
			{
				// Increment status effect trigger stat
				StatusEffectStats [ statusEffectStatIndexes [ (int)statusEffect ] ].Triggered += value;
			}
		}

		/// <summary>
		/// The callback for applying snaps earned for completing a word of a poem.
		/// </summary>
		/// <param name="modifier"> The modifier applied to the word. </param>
		public void OnWordComplete ( Enums.WordModifierType modifier )
		{
			// Check for modifier
			if ( modifierStatIndexes.ContainsKey ( (int)modifier ) )
			{
				// Increment modifier apply stat
				ModifierStats [ modifierStatIndexes [ (int)modifier ] ].Performed++;
			}
		}

		/// <summary>
		/// The callback for when a performance is completed.
		/// </summary>
		/// <param name="isSuccess"> Whether or not the performance was completed successfully. </param>
		/// <param name="snaps"> The total amount of snaps earned for the performance. </param>
		/// <param name="poetID"> The ID of the poet used for the run. </param>
		/// <param name="judgeID"> The ID of the judge of for the performance. 0 if no judge. </param>
		/// <param name="difficultyID"> The ID of the difficulty for the run. </param>
		public virtual void OnCompletePerformance ( bool isSuccess, int snaps, int poetID, int judgeID, int difficultyID )
		{
			// Check for success
			if ( isSuccess )
			{
				// Check for poet
				if ( poetStatIndexes.ContainsKey ( poetID ) )
				{
					// Check for new high score
					if ( snaps > PoetStats [ poetStatIndexes [ poetID ] ].HighScore )
					{
						PoetStats [ poetStatIndexes [ poetID ] ].HighScore = snaps;
					}
				}

				// Check for judge
				if ( judgeStatIndexes.ContainsKey ( judgeID ) )
				{
					// Increment judge success stat
					JudgeStats [ judgeStatIndexes [ judgeID ] ].Successes++;

					// Check for new high score
					if ( snaps > JudgeStats [ judgeStatIndexes [ judgeID ] ].HighScore )
					{
						JudgeStats [ judgeStatIndexes [ judgeID ] ].HighScore = snaps;
					}
				}

				// Check for difficulty
				if ( difficultyStatIndexes.ContainsKey ( difficultyID ) )
				{
					// Check for new high score
					if ( snaps > DifficultyStats [ difficultyStatIndexes [ difficultyID ] ].HighScore )
					{
						DifficultyStats [ difficultyStatIndexes [ difficultyID ] ].HighScore = snaps;
					}
				}
			}
		}

		/// <summary>
		/// The callback for when a run is won.
		/// </summary>
		/// <param name="poetID"> The ID of the poet used for the run. </param>
		/// <param name="difficultyID"> The ID of the difficulty for the run. </param>
		/// <param name="items"> The data for items owned on the win. </param>
		/// <param name="upgrades"> The data for upgrades owned on the win. </param>
		public void OnWin ( int poetID, int difficultyID, Items.ItemModel [ ] items, Upgrades.UpgradeModel [ ] upgrades )
		{
			// Increment wins
			TotalWins++;

			// Check for winning new highest difficulty
			if ( difficultyID > HighestDifficultyWin )
			{
				HighestDifficultyWin = difficultyID;
			}

			// Check for poet
			if ( poetStatIndexes.ContainsKey ( poetID ) )
			{
				// Increment win
				PoetStats [ poetStatIndexes [ poetID ] ].Wins++;

				// Check for winning new highest difficulty
				if ( difficultyID > PoetStats [ poetStatIndexes [ poetID ] ].HighestDifficultyWin )
				{
					PoetStats [ poetStatIndexes [ poetID ] ].HighestDifficultyWin = difficultyID;
				}
			}

			// Check for difficulty
			if ( difficultyStatIndexes.ContainsKey ( difficultyID ) )
			{
				// Increment win
				DifficultyStats [ difficultyStatIndexes [ difficultyID ] ].Wins++;
			}

			// Get item count
			int itemCount = 0;
			if ( items != null )
			{
				for ( int i = 0; i < items.Length; i++ )
				{
					// Check for item
					if ( items [ i ] != null && items [ i ].Item != null )
					{
						// Increment count
						itemCount++;

						// Update item stats
						if ( itemStatIndexes.ContainsKey ( items [ i ].ID ) )
						{
							// Increment wins
							ItemStats [ itemStatIndexes [ items [ i ].ID ] ].Wins++;

							// Check for winning new highest difficulty
							if ( difficultyID > ItemStats [ itemStatIndexes [ items [ i ].ID ] ].HighestDifficultyWin )
							{
								ItemStats [ itemStatIndexes [ items [ i ].ID ] ].HighestDifficultyWin = difficultyID;
							}
						}
					}
				}
			}

			// Check for least items
			if ( itemCount < ChallengeStats.LeastItemsWin )
			{
				ChallengeStats.LeastItemsWin = itemCount;
			}

			// Check for upgrades
			if ( upgrades != null )
			{
				for ( int i = 0; i < upgrades.Length; i++ )
				{
					// Check for upgrade
					if ( upgrades [ i ] != null && upgrades [ i ].Upgrade != null )
					{
						// Update upgrade stats
						if ( upgradeStatIndexes.ContainsKey ( upgrades [ i ].ID ) )
						{
							// Increment wins
							UpgradeStats [ upgradeStatIndexes [ upgrades [ i ].ID ] ].Wins++;

							// Check for winning new highest difficulty
							if ( difficultyID > UpgradeStats [ upgradeStatIndexes [ upgrades [ i ].ID ] ].HighestDifficultyWin )
							{
								UpgradeStats [ upgradeStatIndexes [ upgrades [ i ].ID ] ].HighestDifficultyWin = difficultyID;
							}
						}
					}
				}
			}
		}

		#endregion // Public Events Functions

		#region Private Functions

		/// <summary>
		/// Initializes the data for item stats.
		/// </summary>
		private void InitItemStats ( )
		{
			// Create item stat data
			ItemStatsModel [ ] temp = new ItemStatsModel [ TOTAL_ITEMS ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing data
				if ( ItemStats != null && i < ItemStats.Length && ItemStats [ i ] != null && ItemStats [ i ].ID != Items.ItemModel.NO_ITEM_ID )
				{
					// Store existing data
					temp [ i ] = ItemStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new ItemStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			ItemStats = temp;
		}

		/// <summary>
		/// Initializes the data for consumable stats.
		/// </summary>
		private void InitConsumableStats ( )
		{
			// Create consumable stat data
			ConsumableStatsModel [ ] temp = new ConsumableStatsModel [ TOTAL_CONSUMABLES ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing data
				if ( ConsumableStats != null && i < ConsumableStats.Length && ConsumableStats [ i ] != null && ConsumableStats [ i ].ID != Consumables.ConsumableModel.NO_CONSUMABLE_ID )
				{
					// Store existing data
					temp [ i ] = ConsumableStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new ConsumableStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			ConsumableStats = temp;
		}

		/// <summary>
		/// Initializes the data for poet stats.
		/// </summary>
		private void InitPoetStats ( )
		{
			// Create poet stat data
			PoetStatsModel [ ] temp = new PoetStatsModel [ TOTAL_POETS ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing data
				if ( PoetStats != null && i < PoetStats.Length && PoetStats [ i ] != null && PoetStats [ i ].ID != 0 )
				{
					// Store existing data
					temp [ i ] = PoetStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new PoetStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			PoetStats = temp;
		}

		/// <summary>
		/// Initializes the data for judge stats.
		/// </summary>
		private void InitJudgeStats ( )
		{
			// Create judge stat data
			JudgeStatsModel [ ] temp = new JudgeStatsModel [ TOTAL_JUDGES ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing
				if ( JudgeStats != null && i < JudgeStats.Length && JudgeStats [ i ] != null && JudgeStats [ i ].ID != 0 )
				{
					// Store existing data
					temp [ i ] = JudgeStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new JudgeStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			JudgeStats = temp;
		}

		/// <summary>
		/// Initializes the data for upgrade stats.
		/// </summary>
		private void InitUpgradeStats ( )
		{
			// Create upgrade stat data
			UpgradeStatsModel [ ] temp = new UpgradeStatsModel [ TOTAL_UPGRADES ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing
				if ( UpgradeStats != null && i < UpgradeStats.Length && UpgradeStats [ i ] != null && UpgradeStats [ i ].ID != Upgrades.UpgradeModel.NO_UPGRADE_ID )
				{
					// Store existing data
					temp [ i ] = UpgradeStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new UpgradeStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			UpgradeStats = temp;
		}

		/// <summary>
		/// Initializes the data for modifier stats.
		/// </summary>
		private void InitModifierStats ( )
		{
			// Create modifier stat data
			ModifierStatsModel [ ] temp = new ModifierStatsModel [ TOTAL_MODIFIERS ];
			Enums.WordModifierType [ ] ids =
			{
				Enums.WordModifierType.BOLD,
				Enums.WordModifierType.ITALICS,
				Enums.WordModifierType.STRIKETHROUGH,
				Enums.WordModifierType.UNDERLINE,
				Enums.WordModifierType.CAPS,
				Enums.WordModifierType.SMALL
			};
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing
				if ( ModifierStats != null && i < ModifierStats.Length && ModifierStats [ i ] != null && ModifierStats [ i ].ID == (int)ids [ i ] )
				{
					// Store existing data
					temp [ i ] = ModifierStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new ModifierStatsModel
					{
						ID = (int)ids [ i ]
					};
				}
			}

			// Store data
			ModifierStats = temp;
		}

		/// <summary>
		/// Initializes the data for status effect stats.
		/// </summary>
		private void InitStatusEffectStats ( )
		{
			// Create status effect stat data
			StatusEffectStatsModel [ ] temp = new StatusEffectStatsModel [ TOTAL_STATUS_EFFECTS ];
			Enums.StatusEffectType [ ] ids =
			{
				Enums.StatusEffectType.STUBBORN,
				Enums.StatusEffectType.GREEDY,
				Enums.StatusEffectType.DRAMATIC,
				Enums.StatusEffectType.POPULAR,
				Enums.StatusEffectType.EXCITED,
				Enums.StatusEffectType.SERIOUS
			};
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing
				if ( StatusEffectStats != null && i < StatusEffectStats.Length && StatusEffectStats [ i ] != null && StatusEffectStats [ i ].ID == (int)ids [ i ] )
				{
					// Store existing data
					temp [ i ] = StatusEffectStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new StatusEffectStatsModel
					{
						ID = (int)ids [ i ]
					};
				}
			}

			// Store data
			StatusEffectStats = temp;
		}

		/// <summary>
		/// Initializes the data for difficulty stats.
		/// </summary>
		private void InitDifficultyStats ( )
		{
			// Create difficulty stat data
			DifficultyStatsModel [ ] temp = new DifficultyStatsModel [ TOTAL_DIFFICULTIES ];
			for ( int i = 0; i < temp.Length; i++ )
			{
				// Check for existing
				if ( DifficultyStats != null && i < DifficultyStats.Length && DifficultyStats [ i ] != null && DifficultyStats [ i ].ID != 0 )
				{
					// Store existing data
					temp [ i ] = DifficultyStats [ i ];
				}
				else
				{
					// Create new data
					temp [ i ] = new DifficultyStatsModel
					{
						ID = i + 1
					};
				}
			}

			// Store data
			DifficultyStats = temp;
		}

		#endregion // Private Functions
	}
}