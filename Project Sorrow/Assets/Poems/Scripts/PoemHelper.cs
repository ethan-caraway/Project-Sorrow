using System.Collections.Generic;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class contains functions for displaying poems.
	/// </summary>
	public static class PoemHelper
	{
		#region Public Functions

		/// <summary>
		/// Separates a list of word modifiers by the line of poem they appear on.
		/// </summary>
		/// <param name="modifiers"> The list of word modifiers. </param>
		/// <returns> The list of word modifiers separated by each line. </returns>
		public static Dictionary<LineKey, List<WordModel>> GetModifiersByLine ( WordModel [ ] modifiers )
		{
			// Check for modifiers
			if ( modifiers == null )
			{
				// Return no modifiers
				return null;
			}

			// Check for the Historian judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetHistorianId ( ) )
			{
				// Return no modifiers
				return Judges.JudgeHelper.GetHistorianModifiersByLine ( );
			}

			// Store modifiers by line
			Dictionary<LineKey, List<WordModel>> modifiersByLine = new Dictionary<LineKey, List<WordModel>> ( );

			// Get each modifier and separate by line
			for ( int i = 0; i < modifiers.Length; i++ )
			{
				// Check for modifier
				if ( modifiers [ i ] != null )
				{
					// Create key
					LineKey key = new LineKey
					{
						Stanza = modifiers [ i ].Stanza,
						Line = modifiers [ i ].Line
					};

					// Check for modifiers for this line
					if ( !modifiersByLine.ContainsKey ( key ) )
					{
						modifiersByLine.Add ( key, new List<WordModel> ( ) );
					}

					// Add modifier
					modifiersByLine [ key ].Add ( modifiers [ i ] );
				}
			}

			// Return the separated modifiers
			return modifiersByLine;
		}

		/// <summary>
		/// Gets the data for each word of a poem.
		/// </summary>
		/// <param name="stanzas"> The data for the poem. </param>
		/// <param name="excludes"> The words to be excluded from the available words returned. </param>
		/// <returns> The available words of the poem. </returns>
		public static Dictionary<LineKey, List<WordModel>> GetWords ( Stanza [ ] stanzas, WordModel [ ] excludes )
		{
			// Store words by line
			Dictionary<LineKey, List<WordModel>> words = new Dictionary<LineKey, List<WordModel>> ( );

			// Get words from stanzas
			for ( int s = 0; s < stanzas.Length; s++ )
			{
				// Get words from each line
				for ( int l = 0; l < stanzas [ s ].Lines.Length; l++ )
				{
					// Track the data of the word
					int startIndex = -1;
					int length = 0;

					// Create key for the line
					LineKey key = new LineKey
					{
						Stanza = s,
						Line = l
					};

					// Check each character of the line
					for ( int c = 0; c < stanzas [ s ].Lines [ l ].Length; c++ )
					{
						// Check for current word
						if ( startIndex == -1 )
						{
							// Check for start of a new word
							if ( IsStartOfWord ( stanzas [ s ].Lines [ l ] [ c ] ) )
							{
								// Store start of a new word
								startIndex = c;
								length = 1;
							}
						}
						else
						{
							// Check for the end of the current word
							if ( IsEndOfWord ( stanzas [ s ].Lines [ l ], c ) )
							{
								// Check for words for this line
								if ( !words.ContainsKey ( key ) )
								{
									words.Add ( key, new List<WordModel> ( ) );
								}

								// Add word
								words [ key ].Add ( new WordModel
								{
									Stanza = s,
									Line = l,
									StartIndex = startIndex,
									Length = length
								} );

								// Reset word
								startIndex = -1;
								length = 0;
							}
							else
							{
								// Increment the length of the word
								length++;
							}
						}
					}

					// Check for last word
					if ( startIndex > -1 )
					{
						// Check for words for this line
						if ( !words.ContainsKey ( key ) )
						{
							words.Add ( key, new List<WordModel> ( ) );
						}

						// Add word
						words [ key ].Add ( new WordModel
						{
							Stanza = s,
							Line = l,
							StartIndex = startIndex,
							Length = length
						} );
					}
				}
			}

			// Remove all excludes
			if ( excludes != null )
			{
				for ( int i = 0; i < excludes.Length; i++ )
				{
					// Check for word
					if ( excludes [ i ] != null )
					{
						// Create key
						LineKey key = new LineKey
						{
							Stanza = excludes [ i ].Stanza,
							Line = excludes [ i ].Line
						};

						// Remove this word
						if ( words.ContainsKey ( key ) )
						{
							words [ key ].RemoveAll ( x => x.StartIndex == excludes [ i ].StartIndex );
						}
					}
				}
			}

			// Return the available words of this poem
			return words;
		}

		/// <summary>
		/// Checks whether or not a character is the start of a word.
		/// </summary>
		/// <param name="c"> The character being checked. </param>
		/// <returns> Whether or not the character is the start of a word. </returns>
		public static bool IsStartOfWord ( char c )
		{
			// Return if the character is not a non-word character
			return
				c != ' ' &&
				c != '.' &&
				c != '!' &&
				c != '?' &&
				c != ',' &&
				c != ';' &&
				c != ':' &&
				c != '*' &&
				c != '(' &&
				c != ')' &&
				c != '-' &&
				c != '\'' &&
				c != '\"';
		}

		/// <summary>
		/// Checks whether or not a character is the end of a word.
		/// </summary>
		/// <param name="text"> The text containing the potential word. </param>
		/// <param name="index"> The index of the character being checked. </param>
		/// <returns> Whether or not the character is the end of a word. </returns>
		public static bool IsEndOfWord ( string text, int index )
		{
			// Check for end of the text
			if ( index >= text.Length )
			{
				// Return that the end of the text is the end of the word
				return true;
			}

			// Get the character
			char c = text [ index ];

			// Check for - or '
			if ( c == '-' || c == '\'' )
			{
				// Check if the word continues after the - or '
				return IsEndOfWord ( text, index + 1 );
			}

			// Return that the character is the end of the word if the character is a non-word character
			return
				c == ' ' ||
				c == '.' ||
				c == '!' ||
				c == '?' ||
				c == ',' ||
				c == ';' ||
				c == ':' ||
				c == '*' ||
				c == '(' ||
				c == ')' ||
				c == '\"';
		}

		/// <summary>
		/// Formats a word or character to visually represent a word modification.
		/// </summary>
		/// <param name="text"> The text of the word or character. </param>
		/// <param name="modifier"> The modification to be applied. </param>
		/// <param name="isPreview"> Whether or not the text is for the preview. </param>
		/// <param name="isCorrect"> Whether or not the input for the next character in the line is accurate. </param>
		/// <returns> The formatted word. </returns>
		public static string FormatWordModification ( string text, Enums.WordModifierType modifier, bool isPreview, bool isCorrect )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					// Check for flub
					if ( !isCorrect )
					{
						return $"<b>{MarkIncorrect ( text )}</b>";
					}
					else
					{
						return isPreview ? $"<color=#009000><b>{text}</b></color>" : $"<color=green><b>{text}</b></color>";
					}

				case Enums.WordModifierType.ITALICS:
					// Check for flub
					if ( !isCorrect )
					{
						return $"<i>{MarkIncorrect ( text )}</i>";
					}
					else
					{
						return isPreview ? $"<color=#B9AF64><i>{text}</i></color>" : $"<color=#FFE100><i>{text}</i></color>";
					}

				case Enums.WordModifierType.STRIKETHROUGH:
					// Check for flub
					if ( !isCorrect )
					{
						return $"<s>{MarkIncorrect ( text )}</s>";
					}
					else
					{
						return isPreview ? $"<color=#009090><s>{text}</s></color>" : $"<color=#00FFFF><s>{text}</s></color>";
					}

				case Enums.WordModifierType.UNDERLINE:
					// Check for flub
					if ( !isCorrect )
					{
						return $"<u>{MarkIncorrect ( text )}</u>";
					}
					else
					{
						return isPreview ? $"<color=#323291><u>{text}</u></color>" : $"<color=blue><u>{text}</u></color>";
					}

				case Enums.WordModifierType.CAPS:
					// Ensure text is in caps
					text = text.ToUpper ( );

					// Check for flub
					if ( !isCorrect )
					{
						return MarkIncorrect ( text );
					}
					else
					{
						return isPreview ? $"<color=#87785A>{text}</color>" : $"<color=#A1740E>{text}</color>";
					}

				case Enums.WordModifierType.SMALL:
					// Ensure the text is in lowercase
					text = text.ToLower ( );

					// Check for flub
					if ( !isCorrect )
					{
						return $"<size=70%>{MarkIncorrect ( text )}</size>";
					}
					else
					{
						return isPreview ? $"<color=#AF73AF><size=70%>{text}</size></color>" : $"<color=#FFAFFF><size=70%>{text}</size></color>";
					}

				case Enums.WordModifierType.REDACTED:
					return isPreview ? text : $"<mark=#000000>{text}</mark>";

				case Enums.WordModifierType.HIGHLIGHT:
					// Check for flub
					if ( !isCorrect )
					{
						return MarkIncorrect ( text );
					}
					else
					{
						return isPreview ? $"<mark=#FFFF0016>{text}</mark>" : $"<color=yellow>{text}</color>";
					}
			}

			// Return no modification
			return isCorrect ? text : MarkIncorrect ( text );
		}

		/// <summary>
		/// Gets the base amount of snaps earned for each character in a poem based on its level.
		/// </summary>
		/// <param name="level"> The level of the poem. </param>
		/// <returns> The base amount of snaps. </returns>
		public static int GetBaseSnaps ( int level )
		{
			// Calculate base snaps
			return level == 0 ? 1 : ( level * 2 ) - 1;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Formats text for being incorrect.
		/// </summary>
		/// <param name="text"> The text to be formatted. </param>
		/// <returns> The formatted text. </returns>
		private static string MarkIncorrect ( string text )
		{
			// Check for white space
			if ( text == " " )
			{
				return $"<mark=#64000064><alpha=#00>_</color></mark>";
			}
			else
			{
				return $"<mark=#64000064><color=red>{text}</color></mark>";
			}
		}

		#endregion // Private Functions
	}
}