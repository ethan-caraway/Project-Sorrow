using UnityEngine;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class stores the data for the chance of items of different rarities to appear in the shop.
	/// </summary>
	[System.Serializable]
	public class RarityModel
	{
		#region Rarity Data Constants

		/// <summary>
		/// The default percentage chance of a common item.
		/// </summary>
		public const float DEFAULT_COMMON_CHANCE = 0.65f;

		/// <summary>
		/// The default percentage chance of an uncommon item.
		/// </summary>
		public const float DEFAULT_UNCOMMON_CHANCE = 0.25f;

		/// <summary>
		/// The default percentage chance of a rare item.
		/// </summary>
		public const float DEFAULT_RARE_CHANCE = 0.09f;

		/// <summary>
		/// The default percentage chance of a legendary item.
		/// </summary>
		public const float DEFAULT_LEGENDARY_CHANCE = 0.01f;

		#endregion // Rarity Data Constants

		#region Rarity Data

		/// <summary>
		/// The percentage chance for a common item.
		/// </summary>
		public float CommonChance = DEFAULT_COMMON_CHANCE;

		/// <summary>
		/// The percentage chance for an uncommon item.
		/// </summary>
		public float UncommonChance = DEFAULT_UNCOMMON_CHANCE;

		/// <summary>
		/// The percentage chance for a rare item.
		/// </summary>
		public float RareChance = DEFAULT_RARE_CHANCE;

		/// <summary>
		/// The percentage chance for a legendary item.
		/// </summary>
		public float LegendaryChance = DEFAULT_LEGENDARY_CHANCE;

		#endregion // Rarity Data

		#region Public Functions

		/// <summary>
		/// Generates a random rarity based on the chance values.
		/// </summary>
		/// <returns> The random rarity. </returns>
		public Enums.Rarity GenerateRarity ( )
		{
			// Get random number
			float chance = Random.Range ( 0f, CommonChance + UncommonChance + RareChance + LegendaryChance );

			// Check for common
			if ( chance <= CommonChance )
			{
				// Return common
				return Enums.Rarity.COMMON;
			}
			// Check for uncommon
			else if ( chance <= CommonChance + UncommonChance )
			{
				// Return uncommon
				return Enums.Rarity.UNCOMMON;
			}
			// Check for rare
			else if ( chance <= CommonChance + UncommonChance + RareChance )
			{
				// Return rare
				return Enums.Rarity.RARE;
			}
			// Check for legendary
			else if ( chance <= CommonChance + UncommonChance + RareChance + LegendaryChance )
			{
				// Return legendary
				return Enums.Rarity.LEGENDARY;
			}

			// Return common by default
			return Enums.Rarity.COMMON;
		}

		/// <summary>
		/// Generates a random item.
		/// </summary>
		/// <param name="excludes"> The IDs of items that should be excluded from generating. </param>
		/// <returns> The generated item. </returns>
		public Items.ItemScriptableObject GenerateItem ( int [ ] excludes )
		{
			// Generate starting rarity
			Enums.Rarity rarity = GenerateRarity ( );

			// Generate item
			return GenerateItem ( rarity, excludes );
		}

		/// <summary>
		/// Generates a random consumable.
		/// </summary>
		/// <param name="loanChance"> The chance to generate a loan specifically. A negative value will exclude the possibility of generating a loan. </param>
		/// <returns> The generated consumable. </returns>
		public Consumables.ConsumableScriptableObject GenerateConsumable ( float loanChance )
		{
			// Generate starting rarity
			Enums.Rarity rarity = GenerateRarity ( );
			
			// Generate consumable
			return GenerateConsumable ( rarity, loanChance );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Generates a random item of a given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item to generate. </param>
		/// <param name="excludes"> The IDs of items that should be excluded from generating. </param>
		/// <returns> The generated item. </returns>
		private Items.ItemScriptableObject GenerateItem ( Enums.Rarity rarity, int [ ] excludes )
		{
			// Store the item to generate
			Items.ItemScriptableObject item;

			// Check for legendary
			if ( rarity == Enums.Rarity.LEGENDARY )
			{
				// Generate legendary item
				item = Items.ItemUtility.GetItemByRarity ( rarity, excludes );

				// Check for item
				if ( item != null )
				{
					// Return item
					return item;
				}

				// Reattempt to generate item with a lower rarity
				rarity = Enums.Rarity.RARE;
			}

			// Check for rare
			if ( rarity == Enums.Rarity.RARE )
			{
				// Generate rare item
				item = Items.ItemUtility.GetItemByRarity ( rarity, excludes );

				// Check for item
				if ( item != null )
				{
					// Return item
					return item;
				}

				// Reattempt to generate item with a lower rarity
				rarity = Enums.Rarity.UNCOMMON;
			}

			// Check for uncommon
			if ( rarity == Enums.Rarity.UNCOMMON )
			{
				// Generate uncommon item
				item = Items.ItemUtility.GetItemByRarity ( rarity, excludes );

				// Check for item
				if ( item != null )
				{
					// Return item
					return item;
				}

				// Reattempt to generate item with a lower rarity
				rarity = Enums.Rarity.COMMON;
			}

			// Generate common item
			item = Items.ItemUtility.GetItemByRarity ( rarity, excludes );

			// Return item whether or not one was successfully generated
			return item;
		}

		/// <summary>
		/// Generates a random consumable of a given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the consumable to generate. </param>
		/// <param name="loanChance"> The chance to generate a loan specifically. A negative value will exclude the possibility of generating a loan. </param>
		/// <returns> The generated consumable. </returns>
		private Consumables.ConsumableScriptableObject GenerateConsumable ( Enums.Rarity rarity, float loanChance )
		{
			// Store the consumable to generate
			Consumables.ConsumableScriptableObject consumable;

			// Check for legendary
			if ( rarity == Enums.Rarity.LEGENDARY )
			{
				// Generate legendary consumable
				consumable = Consumables.ConsumableUtility.GetConsumableByRarity ( rarity, loanChance );

				// Check for consumable
				if ( consumable != null )
				{
					// Return consumable
					return consumable;
				}

				// Reattempt to generate consumable with a lower rarity
				rarity = Enums.Rarity.RARE;
			}

			// Check for rare
			if ( rarity == Enums.Rarity.RARE )
			{
				// Generate rare consumable
				consumable = Consumables.ConsumableUtility.GetConsumableByRarity ( rarity, loanChance );

				// Check for consumable
				if ( consumable != null )
				{
					// Return consumable
					return consumable;
				}

				// Reattempt to generate consumable with a lower rarity
				rarity = Enums.Rarity.UNCOMMON;
			}

			// Check for uncommon
			if ( rarity == Enums.Rarity.UNCOMMON )
			{
				// Generate uncommon consumable
				consumable = Consumables.ConsumableUtility.GetConsumableByRarity ( rarity, loanChance );

				// Check for consumable
				if ( consumable != null )
				{
					// Return consumable
					return consumable;
				}

				// Reattempt to generate item with a lower rarity
				rarity = Enums.Rarity.COMMON;
			}

			// Generate common consumable
			consumable = Consumables.ConsumableUtility.GetConsumableByRarity ( rarity, loanChance );

			// Return consumable whether or not one was successfully generated
			return consumable;
		}

		#endregion // Private Functions
	}
}