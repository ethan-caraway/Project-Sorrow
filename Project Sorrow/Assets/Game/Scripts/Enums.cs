namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class stores enums.
	/// </summary>
	public static class Enums
	{
		/// <summary>
		/// The available rarities for items and consumables.
		/// </summary>
		public enum Rarity
		{
			COMMON,
			UNCOMMON,
			RARE,
			LEGENDARY
		}

		/// <summary>
		/// The types of scaling values for items.
		/// </summary>
		public enum ScaleType
		{
			NONE,
			SNAPS,
			MULTIPLIER,
			TIME,
			COUNT
		}

		/// <summary>
		/// The types of modifiers for words.
		/// </summary>
		public enum WordModifierType
		{
			NONE,
			BOLD,
			ITALICS,
			STRIKETHROUGH,
			UNDERLINE,
			CAPS,
			SMALL,
			REDACTED,
			HIGHLIGHT
		}

		/// <summary>
		/// The types of status effects.
		/// </summary>
		public enum StatusEffectType
		{
			NONE,
			STUBBORN,
			GREEDY,
			DRAMATIC,
			POPULAR,
			EXCITED,
			SERIOUS,
			FOCUSED,
			ANXIOUS,
			IMPAIRED
		}

		/// <summary>
		/// The types of consumables.
		/// </summary>
		public enum ConsumableType
		{
			LOAN,
			MODIFIER,
			ENHANCEMENT,
			STATUS_EFFECT
		}

		/// <summary>
		/// The types of color backgrounds for item trigger splashes.
		/// </summary>
		public enum SplashColorType
		{
			NONE,
			SNAPS_GOLD,
			CONFIDENCE_BLUE,
			ARROGANCE_PURPLE,
			TIME_YELLOW,
			MONEY_GREEN,
			PENALTY_RED,
			EXCITED_CYAN,
			SERIOUS_GREY,
			COMMON,
			UNCOMMON,
			RARE,
			LEGENDARY
		}

		/// <summary>
		/// The Latinate series of ordinal number words (e.g. primary, secondary, tertiary, etc.).
		/// </summary>
		public enum LatinateOrdinalNumbers
		{
			PRIMARY,
			SECONDAY,
			TERTIARY
		}
	}
}