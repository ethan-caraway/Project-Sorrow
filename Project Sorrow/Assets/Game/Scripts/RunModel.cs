using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class stores the data for the current run.
	/// </summary>
	[System.Serializable]
	public class RunModel
	{
		#region Run Data Constants

		/// <summary>
		/// The maximum number of items that can be own at once.
		/// </summary>
		public const int MAX_ITEMS = 8;

		/// <summary>
		/// The maximum number of consumable slots for owning unique consumables.
		/// </summary>
		public const int MAX_CONSUMABLES = 6;

		/// <summary>
		/// The maximum number of unique status effects that can had at once.
		/// </summary>
		public const int MAX_STATUS_EFFECTS = 9;

		private const int DEFAULT_REPUTATION = 25;

		#endregion // Run Data Constants

		#region Run Data

		/// <summary>
		/// The ID of the poet the player is using in the run.
		/// </summary>
		public int PoetID = 0;

		/// <summary>
		/// The data of the perk for the poet the player is using in the run.
		/// </summary>
		[System.NonSerialized]
		public Perks.Perk Perk;

		/// <summary>
		/// The amount of money the player currently has in the run.
		/// </summary>
		public int Money = 0;

		/// <summary>
		/// The amount of debt the player currently has in the run.
		/// </summary>
		public int Debt = 0;

		/// <summary>
		/// The data of the items the player currently owns in the run.
		/// </summary>
		public Items.ItemModel [ ] ItemData = new Items.ItemModel [ MAX_ITEMS ];

		/// <summary>
		/// The data of the consumables the player currently owns in the run.
		/// </summary>
		public Consumables.ConsumableModel [ ] ConsumableData = new Consumables.ConsumableModel [ MAX_CONSUMABLES ];

		/// <summary>
		/// The data of the status effects the player currently has in the run.
		/// </summary>
		public StatusEffects.StatusEffectModel [ ] StatusEffectData = new StatusEffects.StatusEffectModel [ MAX_STATUS_EFFECTS ];

		/// <summary>
		/// The data of the upgrades the player currently owns in the run.
		/// </summary>
		public Upgrades.UpgradeModel [ ] UpgradeData = new Upgrades.UpgradeModel [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN - 1 ];

		/// <summary>
		/// The data of the bonus received from encounters in the run.
		/// </summary>
		public Encounters.EncounterBonusModel EncounterData = new Encounters.EncounterBonusModel ( );

		/// <summary>
		/// The current round of the run.
		/// </summary>
		public int Round = 0;

		/// <summary>
		/// The current performance in the round for the run.
		/// </summary>
		public int Performance = 0;

		/// <summary>
		/// The data for each round of the run.
		/// </summary>
		public RoundModel [ ] RoundData = new RoundModel [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN ];

		/// <summary>
		/// The data for each permanently added poem to the player's draft.
		/// </summary>
		public Poems.PoemModel [ ] PermanentDraftPoems = new Poems.PoemModel [ RoundModel.MAX_DRAFTS ];

		/// <summary>
		/// The stats for the run.
		/// </summary>
		public RunStatsModel Stats = new RunStatsModel ( );

		/// <summary>
		/// The save point for continuing a run.
		/// </summary>
		public string Checkpoint;

		#endregion // Run Data

		#region Public Properties

		/// <summary>
		/// The maximum amount of confidence during a performance.
		/// </summary>
		public int MaxConfidence
		{
			get
			{
				// Get default confidence
				int confidence = GameManager.Difficulty.MaxConfidence;

				// Check for max confidence effect from poet perk
				confidence += Perk.OnMaxConfidence ( confidence );

				// Check for max confidence effects from encounters
				confidence += EncounterData.MaxConfidence;

				// Check for max confidence effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						// Trigger for each upgrade
						confidence += UpgradeData [ i ].Upgrade.OnMaxConfidence ( confidence );
					}
				}

				// Check for max confidence effects from items
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						confidence += ItemData [ i ].Item.OnMaxConfidence ( confidence );
					}
				}

				// Check for max confidence enhancements from the poem and the Health Inspector judge
				if ( IsPerforming ( ) && !( IsLastPerformanceOfRound ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) ) )
				{
					confidence += CurrentRound.Poems [ Performance ].Confidence;
				}

				// Check for the Parents judge
				if ( IsLastPerformanceOfRound ( ) && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetParentsId ( ) )
				{
					// Reduce confidence
					confidence = Judges.JudgeHelper.GetParentsConfidence ( confidence );
				}
				// Check for The Futurist judge
				else if ( IsLastPerformanceOfRound () && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetFuturistId ( ) )
				{
					// Reduce confidence
					return Judges.JudgeHelper.GetFuturistConfidence ( );
				}

				// Return confidence
				return confidence < 0 ? 0 : confidence;
			}
		}

		/// <summary>
		/// The maximum amount of arrogance during a performance.
		/// </summary>
		public int MaxArrogance
		{
			get
			{
				// Get default arrogance
				int arrogance = 0;

				// Check for max arrogance effect from poet perk
				arrogance += Perk.OnMaxArrogance ( arrogance );

				// Check for max arrogance effects from encounters
				arrogance += EncounterData.MaxArrogance;

				// Check for max arrogance effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						// Trigger for each upgrade
						arrogance += UpgradeData [ i ].Upgrade.OnMaxArrogance ( arrogance );
					}
				}

				// Check for max arrogance effects from items
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						arrogance += ItemData [ i ].Item.OnMaxArrogance ( arrogance );
					}
				}

				// Check for max arrogance enhancements from the poem and the Health Inspector judge
				if ( IsPerforming ( ) && !( IsLastPerformanceOfRound ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) ) )
				{
					arrogance += CurrentRound.Poems [ Performance ].Arrogance;
				}

				// Return arrogance
				return arrogance < 0 ? 0 : arrogance;
			}
		}

		/// <summary>
		/// The amount of time in seconds allowed during a performance.
		/// </summary>
		public float TimeAllowance
		{
			get
			{
				// Get default time allowance
				float time = GameManager.Difficulty.TimeAllowance;

				// Check for time allowance effect from poet perk
				time += Perk.OnTimeAllowance ( time );

				// Check for time allowance effects from encounters
				time += EncounterData.TimeAllowance;

				// Check for time allowance effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						// Trigger for each upgrade
						time += UpgradeData [ i ].Upgrade.OnTimeAllowance ( time );
					}
				}

				// Check for time allowance effects from items
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						time += ItemData [ i ].Item.OnTimeAllowance ( time );
					}
				}

				// Check for max time enhancements from the poem and the Health Inspector judge
				if ( IsPerforming ( ) && !( IsLastPerformanceOfRound ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) ) )
				{
					time += CurrentRound.Poems [ Performance ].TimeAllowance;
				}

				// Check for the Gym Coach judge
				if ( IsLastPerformanceOfRound ( ) && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetGymCoachId ( ) )
				{
					// Reduce time
					time = Judges.JudgeHelper.GetGymCoachTime ( time );
				}
				// Check for The Afro-Surrealist judge
				else if ( IsLastPerformanceOfRound ( ) && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetAfroSurrealistId ( ) )
				{
					return Judges.JudgeHelper.GetAfroSurrealistTimeAllowance ( );
				}

				// Return time allowance
				return time < 0f ? 0f : time;
			}
		}

		/// <summary>
		/// The amount of reputation the player has during a run.
		/// </summary>
		public int Reputation
		{
			get
			{
				// Get default reputation
				int reputation = DEFAULT_REPUTATION;

				// Check for reputation effect from poet perk
				reputation += Perk.OnReputation ( reputation );

				// Check for reputation effects from encounters
				reputation += EncounterData.Reputation;

				// Check for reputation effects from items
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						reputation += ItemData [ i ].Item.OnReputation ( reputation );
					}
				}

				// Check for reputation effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						// Trigger for each upgrade
						reputation += UpgradeData [ i ].Upgrade.OnReputation ( reputation );
					}
				}

				// Check for max time enhancements from the poem and the Health Inspector judge
				if ( IsPerforming ( ) && !( IsLastPerformanceOfRound ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetHealthInspectorId ( ) ) )
				{
					reputation += CurrentRound.Poems [ Performance ].Reputation;
				}

				// Check for the Hermit judge
				if ( IsLastPerformanceOfRound ( ) && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetHermitId ( ) )
				{
					// Reduce reputation
					reputation = Judges.JudgeHelper.GetHermitReputation ( reputation );
				}

				// Return reputaiton
				return reputation < 0 ? 0 : reputation;
			}
		}

		/// <summary>
		/// The total amount of item slots available.
		/// </summary>
		public int MaxItems
		{
			get
			{
				// Get default item slots
				int slots = GameManager.Difficulty.MaxItems;

				// Get any item slots effects from the poet's perk
				slots += Perk.OnMaxItems ( );

				// Check for item slots effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						slots += UpgradeData [ i ].Upgrade.OnMaxItems ( );
					}
				}

				// Return max item slots
				return slots;
			}
		}

		/// <summary>
		/// The current number of items owned.
		/// </summary>
		public int ItemCount
		{
			get
			{
				// Track the count
				int count = 0;

				// Count until an empty slot is found
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						// Increment count
						count++;
					}
					else
					{
						// End count
						break;
					}
				}

				// Return count
				return count;
			}
		}

		/// <summary>
		/// The total amount of consumable slots available.
		/// </summary>
		public int MaxConsumables
		{
			get
			{
				// Get default consumable slots
				int slots = GameManager.Difficulty.MaxConsumables;

				// Check for consumable slots effects from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						slots += UpgradeData [ i ].Upgrade.OnMaxConsumables ( );
					}
				}

				// Return max consumable slots
				return slots;
			}
		}

		/// <summary>
		/// The current number of consumable slot occupied by unique consumables owned.
		/// </summary>
		public int ConsumableSlotCount
		{
			get
			{
				// Track the count
				int count = 0;

				// Count until an empty slot is found
				for ( int i = 0; i < ConsumableData.Length; i++ )
				{
					// Check for consumable
					if ( IsValidConsumable ( i ) )
					{
						// Increment count
						count++;
					}
					else
					{
						// End count
						break;
					}
				}

				// Return count
				return count;
			}
		}

		/// <summary>
		/// The current number of consumable instances owned.
		/// </summary>
		public int ConsumableCount
		{
			get
			{
				// Track the count
				int count = 0;

				// Count until an empty slot is found
				for ( int i = 0; i < ConsumableData.Length; i++ )
				{
					// Check for consumable
					if ( IsValidConsumable ( i ) )
					{
						// Increment count
						count += ConsumableData [ i ].Count;
					}
					else
					{
						// End count
						break;
					}
				}

				// Return count
				return count;
			}
		}

		/// <summary>
		/// The rarity data for the run.
		/// </summary>
		public Shop.RarityModel RarityData
		{
			get
			{
				// Get default rarity data
				Shop.RarityModel model = new Shop.RarityModel ( );

				// Get any rarity alterations from upgrades
				for ( int i = 0; i < UpgradeData.Length; i++ )
				{
					// Check for upgrade
					if ( IsValidUpgrade ( i ) )
					{
						// Trigger upgrade
						model = UpgradeData [ i ].Upgrade.OnRarity ( model );
					}
				}

				// Return rarity data
				return model;
			}
		}

		/// <summary>
		/// The data for the current round of the run.
		/// </summary>
		public RoundModel CurrentRound
		{
			get
			{
				return RoundData [ Round ];
			}
		}

		#endregion // Public Properties

		#region Public Poem Functions

		/// <summary>
		/// Checks whether or not there is room for another permanent draft poem.
		/// </summary>
		/// <returns> Whether or not there is room for another permanent draft poem. </returns>
		public bool CanAddPermanentDraftPoem ( )
		{
			// Track the count
			int count = 0;

			// Count until an empty slot is found
			for ( int i = 0; i < PermanentDraftPoems.Length; i++ )
			{
				// Check for poem
				if ( PermanentDraftPoems [ i ] != null && PermanentDraftPoems [ i ].ID != 0 )
				{
					// Increment count
					count++;
				}
				else
				{
					// End count
					break;
				}
			}

			// Return if there is room
			return count < RoundModel.MAX_DRAFTS;
		}

		/// <summary>
		/// Adds a poem to the permanent draft list.
		/// </summary>
		/// <param name="poem"> The data for the poem. </param>
		public void AddPermanentDraftPoem ( Poems.PoemModel poem )
		{
			// Find open slot
			for ( int i = 0; i < PermanentDraftPoems.Length; i++ )
			{
				// Check for poem
				if ( PermanentDraftPoems [ i ] == null || PermanentDraftPoems [ i ].ID == 0 )
				{
					// Add poem
					PermanentDraftPoems [ i ] = poem;
					return;
				}
			}
		}

		/// <summary>
		/// Upgrades an existing permanent draft poem to the next level.
		/// </summary>
		/// <param name="id"> The ID of the poem. </param>
		public void UpgradePermanentDraftPoem ( int id )
		{
			// Get the poem
			for ( int i = 0; i < PermanentDraftPoems.Length; i++ )
			{
				// Check for poem
				if ( PermanentDraftPoems [ i ] != null && PermanentDraftPoems [ i ].ID == id )
				{
					// Upgrade poem
					PermanentDraftPoems [ i ].Level++;
					return;
				}
			}
		}

		/// <summary>
		/// Enhances an existing permanent draft poem.
		/// </summary>
		/// <param name="id"> The ID of the poem. </param>
		/// <param name="enhancements"> The new enhancements to apply to the poem. </param>
		public void EnhancePermanentDraftPoem ( int id, Poems.PoemModel enhancements )
		{
			// Get the poem
			for ( int i = 0; i < PermanentDraftPoems.Length; i++ )
			{
				// Check for poem
				if ( PermanentDraftPoems [ i ] != null && PermanentDraftPoems [ i ].ID == id )
				{
					// Enhancement poem
					PermanentDraftPoems [ i ].Add ( enhancements );
					return;
				}
			}
		}

		#endregion // Public Poem Functions

		#region Public Item Functions

		/// <summary>
		/// Checks whether or not player item data is valid.
		/// </summary>
		/// <param name="index"> The index of the item to validate. </param>
		/// <returns> Whether or not the item data is valid. </returns>
		public bool IsValidItem ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= ItemData.Length )
			{
				// Return that index is out of range
				return false;
			}

			// Validate item data
			return ItemData [ index ] != null && ItemData [ index ].IsValid ( );
		}

		/// <summary>
		/// Checks whether or not an item is owned.
		/// </summary>
		/// <param name="id"> The ID of the item to be checked. </param>
		/// <returns> Whether or not the item is owned. </returns>
		public bool HasItem ( int id )
		{
			// Check for ID
			for ( int i = 0; i < ItemData.Length; i++ )
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id )
					return true;

			// Return that ID was not found
			return false;
		}

		/// <summary>
		/// Gets the data for an owned item by its ID.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The data for the item. </returns>
		public Items.ItemModel GetItem ( int id )
		{
			// Check for ID
			for ( int i = 0; i < ItemData.Length; i++ )
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id )
					return ItemData [ i ];

			// Return that ID was not found
			return null;
		}

		/// <summary>
		/// Checks whether or not there is room for another item.
		/// </summary>
		/// <returns> Whether or not there is room for another item. </returns>
		public bool CanAddItem ( )
		{
			// Return if the number of owned items is less than the max
			return ItemCount < MAX_ITEMS;
		}

		/// <summary>
		/// Adds a new owned item.
		/// </summary>
		/// <param name="id"> The ID of the item to add. </param>
		public Items.ItemModel AddItem ( int id )
		{
			// Store new item data
			Items.ItemModel item = null;

			// Find first empty index
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check index
				if ( !IsValidItem ( i ) )
				{
					// Get instance id
					string instanceID = System.Guid.NewGuid ( ).ToString ( );

					// Store item data
					ItemData [ i ] = new Items.ItemModel
					{
						ID = id,
						InstanceID = instanceID,
						Item = Items.ItemHelper.GetItem ( id, instanceID )
					};
					item = ItemData [ i ];
					break;
				}
			}

			// Trigger item being added
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item
				if ( IsValidItem ( i ) )
				{
					// Trigger item
					ItemData [ i ].Item.OnAddAnyItem ( );
				}
			}

			// Update stats
			Stats.OnAddItem ( id );

			// Return the created item data
			return item;
		}

		/// <summary>
		/// Removes the specific instance of a given item.
		/// </summary>
		/// <param name="id"> The ID of the item to remove.</param>
		/// <param name="instance"> The ID of the instance of the item to remove. </param>
		public void RemoveItem ( int id, string instance )
		{
			// Track if item is removed
			bool isRemoved = false;

			// Find ID
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item to remove
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Remove item
					ItemData [ i ] = null;
					isRemoved = true;
				}
				
				// Check for removed item
				if ( isRemoved )
				{
					// Shift item left
					ItemData [ i ] = i + 1 < ItemData.Length ? ItemData [ i + 1 ] : null;
				}
			}

			// Trigger item being removed
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item
				if ( IsValidItem ( i ) )
				{
					// Trigger item
					ItemData [ i ].Item.OnRemoveAnyItem ( );
				}
			}
		}

		/// <summary>
		/// Removes the first instance of a given item.
		/// </summary>
		/// <param name="id"> The ID of the item to remove.</param>
		public void RemoveItemFirstInstance ( int id )
		{
			// Track if item is removed
			bool isRemoved = false;

			// Find ID
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item to remove
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id )
				{
					// Remove item
					ItemData [ i ] = null;
					isRemoved = true;
				}
				
				// Check for removed item
				if ( isRemoved )
				{
					// Shift item left
					ItemData [ i ] = i + 1 < ItemData.Length ? ItemData [ i + 1 ] : null;
				}
			}

			// Trigger item being removed
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item
				if ( IsValidItem ( i ) )
				{
					// Trigger item
					ItemData [ i ].Item.OnRemoveAnyItem ( );
				}
			}
		}

		/// <summary>
		/// Removes an item at a specific index.
		/// </summary>
		/// <param name="index"> The index of the item to remove. </param>
		public void RemoveItemAtIndex ( int index )
		{
			// Check for valid index
			if ( index >= 0 && index < ItemData.Length )
			{
				// Remove item
				ItemData [ index ] = null;

				// Shift items left
				for ( int i = index; i < ItemData.Length; i++ )
				{
					ItemData [ i ] = i + 1 < ItemData.Length ? ItemData [ i + 1 ] : null;
				}

				// Trigger item being removed
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						// Trigger item
						ItemData [ i ].Item.OnRemoveAnyItem ( );
					}
				}
			}
		}

		/// <summary>
		/// Rearranges the order of items.
		/// </summary>
		/// <param name="from"> The index from the item being moved. </param>
		/// <param name="to"> The index of where the item is being moved to. </param>
		public void RearrangeItems ( int from, int to )
		{
			// Get item being moved
			Items.ItemModel item = ItemData [ from ];

			// Track available
			Items.ItemModel [ ] tempItems = new Items.ItemModel [ MAX_ITEMS ];

			// Check if moving item right
			if ( from < to )
			{
				// Move each item
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for unmoved items
					if ( i < from || i > to )
					{
						// Store unmoved item
						tempItems [ i ] = ItemData [ i ];
					}
					// Check for new position of moved item
					else if ( i == to )
					{
						// Store moved item
						tempItems [ i ] = item;
					}
					else
					{
						// Move item left
						tempItems [ i ] = ItemData [ i + 1 ];
					}
				}
			}
			else
			{
				// Move each item
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for unmoved items
					if ( i < to || i > from )
					{
						// Store unmoved item
						tempItems [ i ] = ItemData [ i ];
					}
					// Check for new position of moved item
					else if ( i == to )
					{
						// Store moved item
						tempItems [ i ] = item;
					}
					else
					{
						// Move item right
						tempItems [ i ] = ItemData [ i - 1 ];
					}
				}
			}

			// Store updated item order
			ItemData = tempItems;
		}

		/// <summary>
		/// Gets the description of an item with the variable value(s) inserted.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="description"> The base description of the item. </param>
		/// <returns> The description of the item with the variable value(s). </returns>
		public string GetItemVariableDescription ( int id, string instance, string description )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Return variable description
					return ItemData [ i ].Item.GetVariableDescription ( description );
				}
			}

			// Return base description by default
			return description;
		}

		/// <summary>
		/// Gets the description of an item with the variable value(s) inserted of an unowned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="description"> The base description of the item. </param>
		/// <returns> The description of the item with the variable value(s). </returns>
		public string GetWouldBeItemVariableDescription ( int id, string description )
		{
			// Get item
			Items.Item item = Items.ItemHelper.GetItem ( id, string.Empty );

			// Check for item
			if ( item != null )
			{
				return item.GetWouldBeVariableDescription ( description );
			}

			// Return base description by default
			return description;
		}

		/// <summary>
		/// Gets the current integer scale value of an owned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <returns> The current integer scale value of the item. </returns>
		public int GetItemIntScaleValue ( int id, string instance )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Return scale value
					return ItemData [ i ].Item.GetIntScaleValue ( ItemData [ i ].IntScaleValue );
				}
			}

			// Return 0 by default
			return 0;
		}

		/// <summary>
		/// Gets the current float scale value of an owned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <returns> The current float scale value of the item. </returns>
		public float GetItemFloatScaleValue ( int id, string instance )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Return scale value
					return ItemData [ i ].FloatScaleValue;
				}
			}

			// Return 0 by default
			return 0f;
		}

		/// <summary>
		/// Gets the current string scale value of an owned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item</param>
		/// <returns> The current string scale value of the item. </returns>
		public string GetItemStringScaleValue ( int id, string instance )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Return scale value
					return ItemData [ i ].StringScaleValue;
				}
			}

			// Return empty string by default
			return string.Empty;
		}

		/// <summary>
		/// Gets the integer scale value of an unowned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The integer scale value of the item. </returns>
		public int GetWouldBeItemIntScaleValue ( int id )
		{
			// Get item
			Items.Item item = Items.ItemHelper.GetItem ( id, string.Empty );

			// Check for item
			if ( item != null )
			{
				// Return snaps
				return item.GetWouldBeIntScaleValue ( );
			}

			// Return 0 by default
			return 0;
		}

		/// <summary>
		/// Gets the float scale value of an unowned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The float scale value of the item. </returns>
		public float GetWouldBeItemFloatScaleValue ( int id )
		{
			// Get item
			Items.Item item = Items.ItemHelper.GetItem ( id, string.Empty );

			// Check for item
			if ( item != null )
			{
				// Return snaps
				return item.GetWouldBeFloatScaleValue ( );
			}

			// Return 0 by default
			return 0f;
		}

		/// <summary>
		/// Gets the string scale value of an unowned item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <returns> The string scale value of the item. </returns>
		public string GetWouldBeItemStringScaleValue ( int id )
		{
			// Get item
			Items.Item item = Items.ItemHelper.GetItem ( id, string.Empty );

			// Check for item
			if ( item != null )
			{
				// Return snaps
				return item.GetWouldBeStringScaleValue ( );
			}

			// Return empty string by default
			return string.Empty;
		}

		/// <summary>
		/// Sets the current integer scale value of an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="value"> The current integer scale value of the item. </param>
		public void SetItemIntScaleValue ( int id, string instance, int value )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Store new scale value
					ItemData [ i ].IntScaleValue = value;
					return;
				}
			}
		}

		/// <summary>
		/// Sets the current float scale value of an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="value"> The current float scale value of the item. </param>
		public void SetItemFloatScaleValue ( int id, string instance, float value )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Store new scale value
					ItemData [ i ].FloatScaleValue = value;
					return;
				}
			}
		}

		/// <summary>
		/// Sets the current string scale value of an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="value"> The current string scale value of the item. </param>
		public void SetItemStringScaleValue ( int id, string instance, string value )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Store new scale value
					ItemData [ i ].StringScaleValue = value;
					return;
				}
			}
		}

		/// <summary>
		/// Adds to the current integer scale value of an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="value"> The amount to add to the integer scale value of the item. </param>
		/// <returns> The total integer scale value of the item. </returns>
		public int AddItemIntScaleValue ( int id, string instance, int value )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Store new scale value
					ItemData [ i ].IntScaleValue += value;
					return ItemData [ i ].IntScaleValue;
				}
			}

			// Return no value
			return 0;
		}

		/// <summary>
		/// Adds to the current float scale value of an item.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="value"> The amount to add to the float scale value of the item. </param>
		/// <returns> The total float scale value of the item. </returns>
		public float AddItemFloatScaleValue ( int id, string instance, float value )
		{
			// Check for item
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				if ( ItemData [ i ] != null && ItemData [ i ].ID == id && ItemData [ i ].InstanceID == instance )
				{
					// Store new scale value
					ItemData [ i ].FloatScaleValue += value;
					return ItemData [ i ].FloatScaleValue;
				}
			}

			// Return no value
			return 0;
		}

		#endregion // Public Item Functions

		#region Public Consumable Functions

		/// <summary>
		/// Checks whether or not player consumable data is valid.
		/// </summary>
		/// <param name="index"> The index of the consumable to validate. </param>
		/// <returns> Whether or not the consumable data is valid. </returns>
		public bool IsValidConsumable ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= ConsumableData.Length )
			{
				// Return that index is out of range
				return false;
			}

			// Validate consumable data
			return ConsumableData [ index ] != null && ConsumableData [ index ].IsValid ( );
		}

		/// <summary>
		/// Checks whether or not an instance of a consumable is owned.
		/// </summary>
		/// <param name="id"> The ID of the consumable to be checked. </param>
		/// <returns> Whether or not the consumable is owned. </returns>
		public bool HasConsumable ( int id )
		{
			// Check for ID
			for ( int i = 0; i < ConsumableData.Length; i++ )
				if ( ConsumableData [ i ] != null && ConsumableData [ i ].ID == id )
					return true;

			// Return that ID was not found
			return false;
		}

		/// <summary>
		/// Checks whether or not there is room for another unique consumable.
		/// </summary>
		/// <returns> Whether or not there is room for another unique consumable. </returns>
		public bool CanAddConsumable ( )
		{
			// Return if the number of owned consumables is less than the max
			return ConsumableSlotCount < MaxConsumables;
		}

		/// <summary>
		/// Adds a new owned consumable.
		/// </summary>
		/// <param name="id"> The ID of the consumable to add. </param>
		/// <param name="count"> The number of instances of the consumable to add. </param>
		public void AddConsumable ( int id, int count )
		{
			// Search consumables
			for ( int i = 0; i < ConsumableData.Length; i++ )
			{
				// Check for empty slot
				if ( !IsValidConsumable ( i ) )
				{
					// Store ID
					ConsumableData [ i ] = new Consumables.ConsumableModel
					{
						ID = id,
						Count = count,
						Consumable = Consumables.ConsumableHelper.GetConsumable ( id )
					};
					break;
				}
				// Check for owned consumable
				else if ( ConsumableData [ i ].ID == id )
				{
					// Increment count
					ConsumableData [ i ].Count += count;
					break;
				}
			}

			// Trigger consumable being added
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item
				if ( IsValidItem ( i ) )
				{
					// Trigger item
					ItemData [ i ].Item.OnAddAnyConsumable ( count );
				}
			}

			// Update stats
			Stats.OnAddConsumable ( id, count );
		}

		/// <summary>
		/// Removes a consumable at a specific index.
		/// </summary>
		/// <param name="index"> The index of the consumable to remove. </param>
		/// <param name="count"> The number of instances of the consumable to remove. </param>
		public void RemoveConsumableAtIndex ( int index, int count )
		{
			// Check for valid index
			if ( index >= 0 && index < ConsumableData.Length && IsValidConsumable ( index ) )
			{
				// Remove instance(s)
				ConsumableData [ index ].Count -= count;

				// Check if the consumable was completely removed
				if ( ConsumableData [ index ].Count <= 0 )
				{
					// Clear consumable
					ConsumableData [ index ] = null;

					// Shift consumables left
					for ( int i = index; i < ConsumableData.Length; i++ )
					{
						ConsumableData [ i ] = i + 1 < ConsumableData.Length ? ConsumableData [ i + 1 ] : null;
					}
				}

				// Trigger consumable being removed
				for ( int i = 0; i < ItemData.Length; i++ )
				{
					// Check for item
					if ( IsValidItem ( i ) )
					{
						// Trigger item
						ItemData [ i ].Item.OnRemoveAnyConsumable ( );
					}
				}
			}
		}

		/// <summary>
		/// Rearranges the order of consumables.
		/// </summary>
		/// <param name="from"> The index from the consumable being moved. </param>
		/// <param name="to"> The index of where the consumable is being moved to. </param>
		public void RearrangeConsumables ( int from, int to )
		{
			// Get consumable being moved
			Consumables.ConsumableModel consumable = ConsumableData [ from ];

			// Track available
			Consumables.ConsumableModel [ ] tempConsumables = new Consumables.ConsumableModel [ MAX_CONSUMABLES ];

			// Check if moving consumable right
			if ( from < to )
			{
				// Move each consumable
				for ( int i = 0; i < ConsumableData.Length; i++ )
				{
					// Check for unmoved consumables
					if ( i < from || i > to )
					{
						// Store unmoved item
						tempConsumables [ i ] = ConsumableData [ i ];
					}
					// Check for new position of moved consumable
					else if ( i == to )
					{
						// Store moved consumable
						tempConsumables [ i ] = consumable;
					}
					else
					{
						// Move consumable left
						tempConsumables [ i ] = ConsumableData [ i + 1 ];
					}
				}
			}
			else
			{
				// Move each consumable
				for ( int i = 0; i < ConsumableData.Length; i++ )
				{
					// Check for unmoved consumables
					if ( i < to || i > from )
					{
						// Store unmoved consumable
						tempConsumables [ i ] = ConsumableData [ i ];
					}
					// Check for new position of moved consumable
					else if ( i == to )
					{
						// Store moved consumable
						tempConsumables [ i ] = consumable;
					}
					else
					{
						// Move consumable right
						tempConsumables [ i ] = ConsumableData [ i - 1 ];
					}
				}
			}

			// Store updated consumable order
			ConsumableData = tempConsumables;
		}

		#endregion // Public Consumable Functions

		#region Public Status Effect Functions

		/// <summary>
		/// Checks whether or not player status effect data is valid.
		/// </summary>
		/// <param name="index"> The index of the status effect to validate. </param>
		/// <returns> Whether or not the status effect data is valid. </returns>
		public bool IsValidStatusEffect ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= StatusEffectData.Length )
			{
				// Return that index is out of range
				return false;
			}

			// Validate status effect data
			return StatusEffectData [ index ] != null && StatusEffectData [ index ].IsValid ( );
		}

		/// <summary>
		/// Checks whether or not a the player has a given status effect.
		/// </summary>
		/// <param name="type"> The type of the status effect to be checked. </param>
		/// <returns> Whether or not the player has the status effect. </returns>
		public bool HasStatusEffect ( Enums.StatusEffectType type )
		{
			// Check for status effect
			for ( int i = 0; i < StatusEffectData.Length; i++ )
				if ( StatusEffectData [ i ] != null && StatusEffectData [ i ].Type == type )
					return true;

			// Return that status effect was not found
			return false;
		}

		/// <summary>
		/// Adds a new stack(s) of a status effect.
		/// </summary>
		/// <param name="type"> The type of the status effect to add. </param>
		/// <param name="count"> The number of stacks of the status effect to add. </param>
		public void AddStatusEffect ( Enums.StatusEffectType type, int count )
		{
			// Create status effect data
			AddStatusEffect ( new StatusEffects.StatusEffectModel
			{
				Type = type,
				Count = count
			} );
		}

		/// <summary>
		/// Adds a new stack(s) of a status effect.
		/// </summary>
		/// <param name="statusEffect"> The data of the status effect to add. </param>
		public void AddStatusEffect ( StatusEffects.StatusEffectModel statusEffect )
		{
			// Check for status effect
			if ( statusEffect == null || statusEffect.Type == Enums.StatusEffectType.NONE || statusEffect.Count == 0 )
			{
				return;
			}

			// Check for Therapist judge
			if ( IsLastPerformanceOfRound ( ) && IsPerforming ( ) && CurrentRound.JudgeID == Judges.JudgeHelper.GetTherapistId ( ) )
			{
				// Prevent status effects from being applied
				return;
			}

			// Track total count
			int total = 0;

			// Search status effects
			for ( int i = 0; i < StatusEffectData.Length; i++ )
			{
				// Check for empty slot
				if ( !IsValidStatusEffect ( i ) )
				{
					// Store status effect
					StatusEffectData [ i ] = statusEffect;
					total = statusEffect.Count;
					break;
				}
				// Check for owned status effect
				else if ( StatusEffectData [ i ].Type == statusEffect.Type )
				{
					// Increment stacks
					StatusEffectData [ i ].Count += statusEffect.Count;
					total = StatusEffectData [ i ].Count;
					break;
				}
			}

			// Update stats
			Stats.OnApplyStatusEffect ( statusEffect.Type, statusEffect.Count, total );
		}

		/// <summary>
		/// Expires a stack of each current status effect.
		/// </summary>
		public void ExpireStatusEffects ( )
		{
			// Track first available index
			int availableIndex = 0;

			// Search status effects
			for ( int i = 0; i < StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( IsValidStatusEffect ( i ) )
				{
					// Decrement stack
					StatusEffectData [ i ].Count--;

					// Check for remaining stacks
					if ( StatusEffectData [ i ].Count > 0 )
					{
						// Check if the status effect needs to be moved
						if ( availableIndex < i )
						{
							// Move status effect to first available slot
							StatusEffectData [ availableIndex ] = StatusEffectData [ i ];

							// Clear status effect slot
							StatusEffectData [ i ] = null;
						}

						// Increment first available slot
						availableIndex++;
					}
					else
					{
						// Clear expired status effect
						StatusEffectData [ i ] = null;
					}
				}
			}
		}

		#endregion // Public Status Effect Functions

		#region Public Upgrade Functions

		/// <summary>
		/// Checks whether or not player upgrade data is valid.
		/// </summary>
		/// <param name="index"> The index of the upgrade to validate. </param>
		/// <returns> Whether or not the upgrade data is valid. </returns>
		public bool IsValidUpgrade ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= UpgradeData.Length )
			{
				// Return that index is out of range
				return false;
			}

			// Validate upgrade data
			return UpgradeData [ index ] != null && UpgradeData [ index ].IsValid ( );
		}

		/// <summary>
		/// Adds a new owned upgrade.
		/// </summary>
		/// <param name="id"> The ID of the upgrade to add. </param>
		public void AddUpgrade ( int id )
		{
			// Find first empty index
			bool hasRoom = false;
			for ( int i = 0; i < UpgradeData.Length; i++ )
			{
				// Check index
				if ( !IsValidUpgrade ( i ) )
				{
					// Store ID
					UpgradeData [ i ] = new Upgrades.UpgradeModel
					{
						ID = id,
						Upgrade = Upgrades.UpgradeHelper.GetUpgrade ( id )
					};
					hasRoom = true;
					break;
				}
			}

			// Check if no room was found
			if ( !hasRoom )
			{
				// Expand array
				List<Upgrades.UpgradeModel> temp = new List<Upgrades.UpgradeModel> ( );
				temp.AddRange ( UpgradeData );

				// Add new upgrade
				temp.Add ( new Upgrades.UpgradeModel
				{
					ID = id,
					Upgrade = Upgrades.UpgradeHelper.GetUpgrade ( id )
				} );

				// Store expanded array
				UpgradeData = temp.ToArray ( );
			}

			// Update stats
			Stats.OnAddUpgrade ( id );
		}

		#endregion // Public Upgrade Functions

		#region Public Functions

		/// <summary>
		/// Initializes the data when starting or loading a run.
		/// </summary>
		public void Init ( )
		{
			// Get poet perk
			Perk = Perks.PerkHelper.GetPerk ( PoetID );

			// Get items
			for ( int i = 0; i < ItemData.Length; i++ )
			{
				// Check for item
				if ( ItemData [ i ] != null && ItemData [ i ].ID != Items.ItemModel.NO_ITEM_ID )
				{
					ItemData [ i ].Item = Items.ItemHelper.GetItem ( ItemData [ i ].ID, ItemData [ i ].InstanceID );
				}
				else
				{
					ItemData [ i ] = null;
				}
			}

			// Get consumables
			for ( int i = 0; i < ConsumableData.Length; i++ )
			{
				// Check for consumable
				if ( ConsumableData [ i ] != null && ConsumableData [ i ].ID != Consumables.ConsumableModel.NO_CONSUMABLE_ID )
				{
					ConsumableData [ i ].Consumable = Consumables.ConsumableHelper.GetConsumable ( ConsumableData [ i ].ID );
				}
				else
				{
					ConsumableData [ i ] = null;
				}
			}

			// Get upgrades
			for ( int i = 0; i < UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( UpgradeData [ i ] != null && UpgradeData [ i ].ID != Upgrades.UpgradeModel.NO_UPGRADE_ID )
				{
					UpgradeData [ i ].Upgrade = Upgrades.UpgradeHelper.GetUpgrade ( UpgradeData [ i ].ID );
				}
				else
				{
					UpgradeData [ i ] = null;
				}
			}
		}

		/// <summary>
		/// Sets the data for a new run.
		/// </summary>
		public void NewRun ( )
		{
			// Reset round
			Round = 0;
			Performance = 0;

			// Track history
			int [ ] poemHistory = new int [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN * RoundModel.MAX_DRAFTS ];
			int [ ] encounterHistory = new int [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN ];
			int [ ] judgeHistory = new int [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN ];

			// Generate the data for each round
			RoundData = new RoundModel [ Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN ];
			for ( int round = 0; round < RoundData.Length; round++ )
			{
				// Create round data
				RoundData [ round ] = new RoundModel ( );

				// Check for boss round
				bool isBossRound = ( round + 1 ) % Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN == 0;

				// Generate encounter
				RoundData [ round ].EncounterID = Encounters.EncounterUtility.GetEncounterByRound ( round, encounterHistory ).ID;
				encounterHistory [ round ] = RoundData [ round ].EncounterID;

				// Generate judge
				RoundData [ round ].JudgeID = Judges.JudgeUtility.GetJudgeByRound ( isBossRound ? Judges.JudgeScriptableObject.BOSS_MIN_ROUND : round, judgeHistory ).ID;
				judgeHistory [ round ] = RoundData [ round ].JudgeID;

				// Store rating
				int rating = GameManager.Difficulty.Rounds [ round ].Rating;

				// Generate poems
				for ( int i = 0; i < CurrentRound.DraftIDs.Length; i++ )
				{
					// Get poem
					int id = Poems.PoemUtility.GetPoemByRating ( rating, poemHistory ).ID;

					// Store poem
					RoundData [ round ].DraftIDs [ i ] = id;
					poemHistory [ ( round * RoundModel.MAX_DRAFTS ) + i ] = id;
				}

				// Check for The Minimalist judge
				if ( isBossRound && RoundData [ round ].JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Set poem for The Minimalist judge
					RoundData [ round ].Poems [ Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 ] = new Poems.PoemModel
					{
						ID = Judges.JudgeHelper.GetMinimalistPoem ( )
					};
				}
			}

			// Trigger start of run effect from the poet's perk
			Perk.OnStartRun ( );

			// Update the stats
			if ( !GameManager.IsTutorial )
			{
				Stats.OnStartRun ( );
			}
		}

		/// <summary>
		/// Sets the data for a new round in the run.
		/// </summary>
		public void NextRound ( )
		{
			// Store current round
			Round++;
			Performance = 0;
		}

		/// <summary>
		/// Increments to the next peformance in the round.
		/// </summary>
		public void NextPerformance ( )
		{
			// Increment performance
			Performance++;

			// Check for new round
			if ( Performance >= Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND )
			{
				// Check round
				if ( Round + 1 < GameManager.Difficulty.Rounds.Length )
				{
					// Start new round
					NextRound ( );
				}
			}
		}

		/// <summary>
		/// Checks whether or not the current performance is the last in the round.
		/// </summary>
		/// <returns> Whether or not it is the last performance of the round. </returns>
		public bool IsLastPerformanceOfRound ( )
		{
			// Check for last performance of round
			return Performance == Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1;
		}

		/// <summary>
		/// Checks whether or not the current performance is the last in the run.
		/// </summary>
		/// <returns> Whether or not it is the last performacne of the run. </returns>
		public bool IsLastPerformanceOfRun ( )
		{
			// Check for last performance of run
			return IsLastPerformanceOfRound ( ) && Round == Difficulty.DifficultyScriptableObject.ROUNDS_PER_RUN - 1;
		}

		/// <summary>
		/// Checks whether or not if the player is currently performing (aka in the performance scene).
		/// </summary>
		/// <returns> Whether or not the player is performing. </returns>
		public bool IsPerforming ( )
		{
			// Check for the performance scene
			return SceneManager.GetActiveScene ( ).name == GameManager.PERFORMANCE_SCENE;
		}

		/// <summary>
		/// Applies an amount of money to the player's debt or money. 
		/// </summary>
		/// <param name="money"> The amount of money being applied. </param>
		public void ApplyMoney ( int money )
		{
			// Check for adding money
			if ( money > 0 )
			{
				// Check for debt
				if ( Debt < 0 )
				{
					// Apply money to debt first
					Debt += money;

					// Check if debt is paid
					if ( Debt > 0 )
					{
						// Keep extra money
						Money += Debt;

						// Reset debt
						Debt = 0;
					}
				}
				else
				{
					// Add money
					Money += money;
				}
			}
			else
			{
				// Subtract money
				Money += money;

				// Check for new debt
				if ( Money < 0 )
				{
					// Add to debt
					Debt += Money;

					// Reset money
					Money = 0;
				}
			}

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnApplyMoney ( );
			}
		}

		/// <summary>
		/// Check whether or not the player can afford to buy something of a given price.
		/// </summary>
		/// <param name="price"> The price to be checked. </param>
		/// <returns> Whether or not the player can currently afford the price. </returns>
		public bool CanPlayerAffordPrice ( int price )
		{
			// Get minimum budget
			int minimumBudget = 0;

			// Check for any item triggers
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Get minimum budget
					minimumBudget += GameManager.Run.ItemData [ i ].Item.OnMinBudget ( minimumBudget );
				}
			}

			// Check for negative min budget
			if ( minimumBudget < 0 )
			{
				// Check debt
				if ( GameManager.Run.Debt > minimumBudget )
				{
					// Calculate the minimum budget deducting debt
					minimumBudget -= GameManager.Run.Debt;
				}
				else
				{
					// Reset minimum budget since the debt exceeds it
					minimumBudget = 0;
				}
			}

			// Check if player has enough money
			return GameManager.Run.Money - price >= minimumBudget;
		}

		#endregion // Public Functions
	}
}