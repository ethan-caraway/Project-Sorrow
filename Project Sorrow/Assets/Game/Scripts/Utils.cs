using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This static class contains utility functions
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Linearly interpolates from one signed integer to another.
		/// </summary>
		/// <param name="from"> The minimum starting value. </param>
		/// <param name="to"> The maximum ending value. </param>
		/// <param name="percentage"> The percentage (between 0f and 1f) of progress between the two values. </param>
		/// <returns> The interpolated signed integer value. </returns>
		public static int Lerp ( int from, int to, float percentage )
		{
			// Check for valid percentage
			if ( percentage < 0f )
			{
				percentage = 0f;
			}
			else if ( percentage > 1f )
			{
				percentage = 1f;
			}

			// Calculate lerp
			return (int)( ( ( 1f - percentage ) * from ) + ( percentage * to ) );
		}

		/// <summary>
		/// Linearly interpolates from one unsigned integer to another.
		/// </summary>
		/// <param name="from"> The minimum starting value. </param>
		/// <param name="to"> The maximum ending value. </param>
		/// <param name="percentage"> The percentage (between 0f and 1f) of progress between the two values. </param>
		/// <returns> The interpolated unsigned integer value. </returns>
		public static uint Lerp ( uint from, uint to, float percentage )
		{
			// Check for valid percentage
			if ( percentage < 0f )
			{
				percentage = 0f;
			}
			else if ( percentage > 1f )
			{
				percentage = 1f;
			}

			// Calculate lerp
			return (uint)( ( 1f - percentage ) * from ) + (uint)( percentage * to );
		}

		/// <summary>
		/// Linearly interpolates from an empty string to a provided string value.
		/// </summary>
		/// <param name="text"> The full string value. </param>
		/// <param name="percentage"> The percentage (between 0f and 1f) of progress between an empty string and the full string value. </param>
		/// <returns> The interpolated substring. </returns>
		public static string Lerp ( string text, float percentage )
		{
			// Check for valid percentage
			if ( percentage < 0f )
			{
				percentage = 0f;
			}
			else if ( percentage > 1f )
			{
				percentage = 1f;
			}

			// Calculate lerp length
			int length = Lerp ( 0, text.Length, percentage );

			// Return lerp substring
			return text.Substring ( 0, length );
		}

		/// <summary>
		/// Formats time in seconds to be displayed as 00:00.00.
		/// </summary>
		/// <param name="time"> The time in seconds to be displayed. </param>
		/// <returns> The formated time as a string. </returns>
		public static string FormatTime ( float time )
		{
			// Get minutes
			int minutes = (int)time / 60;

			// Get seconds
			float seconds = time % 60f;

			// Return the formated string 00:00.00
			return $"{minutes:00}:{seconds:00.00}";
		}

		/// <summary>
		/// Gets the button colors for the given item rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item. </param>
		/// <returns> The button colors. </returns>
		public static ColorBlock GetRarityColorsOld ( Enums.Rarity rarity )
		{
			// Check rarity
			switch ( rarity )
			{
				case Enums.Rarity.COMMON:
					return new ColorBlock
					{
						normalColor = new Color32 ( 210, 210, 210, 255 ),
						highlightedColor = new Color32 ( 145, 145, 145, 255 ),
						pressedColor = new Color32 ( 145, 145, 145, 255 ),
						selectedColor = new Color32 ( 145, 145, 145, 255 ),
						disabledColor = new Color32 ( 210, 210, 210, 255 ),
						colorMultiplier = 1f
					};

				case Enums.Rarity.UNCOMMON:
					return new ColorBlock
					{
						normalColor = new Color32 ( 0, 195, 0, 255 ),
						highlightedColor = new Color32 ( 0, 145, 0, 255 ),
						pressedColor = new Color32 ( 0, 145, 0, 255 ),
						selectedColor = new Color32 ( 0, 145, 0, 255 ),
						disabledColor = new Color32 ( 0, 195, 0, 255 ),
						colorMultiplier = 1f
					};

				case Enums.Rarity.RARE:
					return new ColorBlock
					{
						normalColor = new Color32 ( 0, 80, 200, 255 ),
						highlightedColor = new Color32 ( 0, 60, 145, 255 ),
						pressedColor = new Color32 ( 0, 60, 145, 255 ),
						selectedColor = new Color32 ( 0, 60, 145, 255 ),
						disabledColor = new Color32 ( 0, 80, 200, 255 ),
						colorMultiplier = 1f
					};

				case Enums.Rarity.LEGENDARY:
					return new ColorBlock
					{
						normalColor = new Color32 ( 110, 0, 200, 255 ),
						highlightedColor = new Color32 ( 75, 0, 125, 255 ),
						pressedColor = new Color32 ( 75, 0, 125, 255 ),
						selectedColor = new Color32 ( 75, 0, 125, 255 ),
						disabledColor = new Color32 ( 110, 0, 200, 255 ),
						colorMultiplier = 1f
					};

				default:
					return new ColorBlock
					{
						normalColor = new Color32 ( 210, 210, 210, 255 ),
						highlightedColor = new Color32 ( 145, 145, 145, 255 ),
						pressedColor = new Color32 ( 145, 145, 145, 255 ),
						selectedColor = new Color32 ( 145, 145, 145, 255 ),
						disabledColor = new Color32 ( 210, 210, 210, 255 ),
						colorMultiplier = 1f
					};
			}
		}

		/// <summary>
		/// Gets the color for a given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item or consumable. </param>
		/// <returns> The color for the rarity. </returns>
		public static Color32 GetRarityColor ( Enums.Rarity rarity )
		{
			// Check rarity
			switch ( rarity )
			{
				case Enums.Rarity.COMMON:
					return new Color32 ( 175, 175, 175, 255 );

				case Enums.Rarity.UNCOMMON:
					return new Color32 ( 0, 195, 0, 255 );

				case Enums.Rarity.RARE:
					return new Color32 ( 0, 80, 200, 255 );

				case Enums.Rarity.LEGENDARY:
					return new Color32 ( 110, 0, 200, 255 );

				default:
					return Color.black;
			}
		}

		/// <summary>
		/// Gets the alternative color for a given rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item or consumable. </param>
		/// <returns> The alternative color for the rarity. </returns>
		public static Color32 GetRarityAltColor ( Enums.Rarity rarity )
		{
			// Check rarity
			switch ( rarity )
			{
				case Enums.Rarity.COMMON:
					return new Color32 ( 145, 145, 145, 255 );

				case Enums.Rarity.UNCOMMON:
					return new Color32 ( 0, 145, 0, 255 );

				case Enums.Rarity.RARE:
					return new Color32 ( 0, 60, 145, 255 );

				case Enums.Rarity.LEGENDARY:
					return new Color32 ( 75, 0, 125, 255 );

				default:
					return Color.black;
			}
		}

		/// <summary>
		/// Gets the button colors for a given item rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item or consumable. </param>
		/// <param name="isDisabledAltColor"> Whether the alternative rarity color or the base rarity color should be used for the disabled color. </param>
		/// <returns> The button colors for the rarity. </returns>
		public static ColorBlock GetRarityColors ( Enums.Rarity rarity, bool isDisabledAltColor = true )
		{
			// Get rarity colors
			Color32 color = GetRarityColor ( rarity );
			Color32 altColor = GetRarityAltColor ( rarity );

			// Return button colors
			return new ColorBlock
			{
				normalColor = color,
				highlightedColor = altColor,
				pressedColor = altColor,
				selectedColor = altColor,
				disabledColor = isDisabledAltColor ? altColor : color,
				colorMultiplier = 1f
			};
		}

		/// <summary>
		/// Gets the color associated with a modifier.
		/// </summary>
		/// <param name="modifier"> The modifier to get the color for. </param>
		/// <returns> The color associated with the modifier. </returns>
		public static Color32 GetModifierColor ( Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					return new Color32 ( 0, 255, 0, 255 );

				case Enums.WordModifierType.ITALICS:
					return new Color32 ( 255, 225, 0, 255 );

				case Enums.WordModifierType.STRIKETHROUGH:
					return new Color32 ( 0, 255, 255, 255 );

				case Enums.WordModifierType.UNDERLINE:
					return new Color32 ( 0, 0, 255, 255 );

				case Enums.WordModifierType.CAPS:
					return new Color32 ( 161, 116, 14, 255 );

				case Enums.WordModifierType.SMALL:
					return new Color32 ( 255, 175, 255, 255 );
			}

			// Return white as default
			return Color.white;
		}

		/// <summary>
		/// Gets the color associated with a status effect.
		/// </summary>
		/// <param name="statusEffect"> The status effect to get the color for. </param>
		/// <returns> The color associated with the status effect. </returns>
		public static Color32 GetStatusEffectColor ( Enums.StatusEffectType statusEffect )
		{
			// Check modifier
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.STUBBORN:
					return new Color32 ( 128, 0, 128, 255 );

				case Enums.StatusEffectType.GREEDY:
					return new Color32 ( 0, 225, 0, 255 );

				case Enums.StatusEffectType.DRAMATIC:
					return new Color32 ( 255, 225, 0, 255 );

				case Enums.StatusEffectType.POPULAR:
					return new Color32 ( 161, 116, 14, 255 );

				case Enums.StatusEffectType.EXCITED:
					return new Color32 ( 0, 255, 255, 255 );

				case Enums.StatusEffectType.SERIOUS:
					return new Color32 ( 255, 255, 255, 255 );
			}

			// Return white as default
			return Color.white;
		}

		/// <summary>
		/// Converts an integer into a roman numeral.
		/// </summary>
		/// <param name="number"> The integer to be converted. </param>
		/// <returns> The converted roman numberal. </returns>
		public static string ToRomanNumeral ( int number )
		{
			// Check for positive value
			if ( number <= 0 )
			{
				return string.Empty;
			}

			// Store roman numberal data
			int [ ] increments =
			{
				1000, // M
				900,  // CM
				500,  // D
				400,  // CD
				100,  // C
				90,   // XC
				50,   // L
				40,   // XL
				10,   // X
				9,    // IX
				5,    // V
				4,    // IV
				1     // I
			};
			string [ ] romanNumerals =
			{
				"M",  // 1000
				"CM", // 900
				"D",  // 500
				"CD", // 400
				"C",  // 100
				"XC", // 90
				"L",  // 50
				"XL", // 40
				"X",  // 10
				"IX", // 9
				"V",  // 5
				"IV", // 4
				"I"   // 1
			};

			// Construct roman numeral
			string value = string.Empty;
			for ( int i = 0; i < increments.Length && number > 0; i++ )
			{
				// Subtract each increment from the number until 0
				while ( number >= increments [ i ] )
				{
					number -= increments [ i ];
					value += romanNumerals [ i ];
				}
			}

			// Return the roman numberal
			return value;
		}

		/// <summary>
		/// Converts this modifier enum to its corrisponding ID.
		/// </summary>
		/// <param name="modifier"> The enum this method is extending. </param>
		/// <returns> The corrisponding ID. </returns>
		public static string ToID ( this Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					return "Bold";

				case Enums.WordModifierType.ITALICS:
					return "Italics";

				case Enums.WordModifierType.STRIKETHROUGH:
					return "Strikethrough";

				case Enums.WordModifierType.UNDERLINE:
					return "Underline";

				case Enums.WordModifierType.CAPS:
					return "CAPS";

				case Enums.WordModifierType.SMALL:
					return "small";

				case Enums.WordModifierType.REDACTED:
					return "Redacted";

				case Enums.WordModifierType.HIGHLIGHT:
					return "Highlight";
			}

			// Return no ID
			return string.Empty;
		}
	}
}