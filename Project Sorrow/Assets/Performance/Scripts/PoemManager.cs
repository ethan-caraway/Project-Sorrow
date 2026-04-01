using System.Collections.Generic;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class manages the progress of the poem being performed.
	/// </summary>
	public class PoemManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private PerformanceManager performanceManager;

		[SerializeField]
		private LineManager lineManager;

		[SerializeField]
		private GameObject inputTextManager;

		#endregion // UI Elements

		#region Poem Data

		private Poems.Stanza [ ] stanzas;
		private Dictionary<Poems.LineKey, List<Poems.WordModel>> modifiers;

		private int stanzaIndex;
		private int lineIndex;
		private int characterIndex;

		private int wordSnaps = 0;
		private int wordIndex = -1;
		private bool hasFlubbedWord = false;
		private int wordCount = 0;

		private int lineSnaps = 0;
		private bool canLineEarnSnaps = true;
		private List<StatusEffects.StatusEffectModel> statusEffectQueue = new List<StatusEffects.StatusEffectModel> ( );

		private string currentLine;
		private Poems.WordModel [ ] currentModifiers;
		private int currentModifierIndex;
		private Poems.WordModel [ ] nextModifiers;

		private bool isPoemActive;
		private bool isInputActive;

		#endregion // Poem Data

		#region Public Functions

		/// <summary>
		/// Responds to text being input by the user.
		/// </summary>
		/// <param name="text"> The text from the user. Should be a character at a time. </param>
		public void OnInputText ( string text )
		{
			// Check if poem is active
			if ( !isPoemActive || !isInputActive )
			{
				return;
			}

			// Check if line is complete
			if ( characterIndex >= currentLine.Length )
			{
				return;
			}

			// Check for input
			if ( string.IsNullOrEmpty ( text ) )
			{
				return;
			}

			// Check if the text is correct
			if ( CompareCharacters ( currentLine [ characterIndex ], text [ 0 ] ) )
			{
				// Submit accurate text
				CharacterMatch ( currentLine [ characterIndex ].ToString ( ), true, OnInputTextComplete );
			}
			else
			{
				// Track if the flub can be prevented
				bool isPrevented = false;

				// Check for arrogance
				if ( performanceManager.HasArrogance ( ) )
				{
					// Prevent flub
					isPrevented = true;

					// Lose arrogance
					performanceManager.LoseArrogance ( );
				}

				// Check for flub prevention
				if ( isPrevented )
				{
					// Submit accurate text
					CharacterMatch ( currentLine [ characterIndex ].ToString ( ), false, OnInputTextComplete );
				}
				else
				{
					// Submit inaccurate text
					CharacterMismatch ( currentLine [ characterIndex ].ToString ( ), OnInputTextComplete );
				}
			}
		}

		/// <summary>
		/// Sets the poem to be performed.
		/// </summary>
		/// <param name="poem"> The structured stanzas for the poem. </param>
		/// <param name="wordModifiers"> The word modifiers for the poem. </param>
		public void SetPoem ( Poems.Stanza [ ] poem, Poems.WordModel [ ] wordModifiers )
		{
			// Store poem
			stanzas = poem;

			// Store modifiers
			modifiers = Poems.PoemHelper.GetModifiersByLine ( wordModifiers );

			// Reset indexes
			stanzaIndex = 0;
			lineIndex = 0;
			characterIndex = 0;

			// Create first line
			SetNextLine ( stanzaIndex, lineIndex, false );

			// Disable input until performance begins
			ToggleInput ( false );

			// Add callbacks
			performanceManager.AddOnPause ( OnPause );
		}

		/// <summary>
		/// Starts the performance of the poem.
		/// </summary>
		public void BeginPoem ( )
		{
			// Begin poem
			isPoemActive = true;
			ToggleInput ( true );

			// Start first line
			BeginLine ( );
		}

		/// <summary>
		/// The callback for when poem has failed to complete.
		/// </summary>
		public void OnPoemFail ( )
		{
			// Stop current line/break
			lineManager.StopLine ( );

			// Stop poem
			isPoemActive = false;
			ToggleInput ( false );
			inputTextManager.SetActive ( false );
		}

		/// <summary>
		/// Checks whether or not it is currently last character of the line.
		/// </summary>
		/// <returns> Whether or not it is the end of the line. </returns>
		public bool IsEndOfLine ( )
		{
			// Check for last character
			return characterIndex + 1 >= currentLine.Length;
		}

		/// <summary>
		/// Queues a status effect to be added at the end of the line after status effects expire.
		/// </summary>
		/// <param name="statusEffect"> The data for the status effect to queue. </param>
		public void QueueStatusEffect ( StatusEffects.StatusEffectModel statusEffect )
		{
			// Check for status effect
			if ( statusEffect != null )
			{
				// Queue the status effect for after the status effect expire
				statusEffectQueue.Add ( statusEffect );
			}
		}

		#endregion // Public Functions

		#region Private Poem Display Functions

		/// <summary>
		/// Begins the performance of the next line of the poem.
		/// </summary>
		private void BeginLine ( )
		{
			// Check for valid line
			if ( lineIndex < stanzas [ stanzaIndex ].Lines.Length )
			{
				// Store line
				currentLine = stanzas [ stanzaIndex ].Lines [ lineIndex ];

				// Store modifiers
				currentModifiers = nextModifiers;
				currentModifierIndex = 0;

				// Format line
				currentLine = FormatLine ( currentLine, currentModifiers );

				// Reset to starting character of line
				characterIndex = 0;

				// Reset line snaps
				lineSnaps = 0;
				canLineEarnSnaps = true;

				// Check for start of word
				if ( Poems.PoemHelper.IsStartOfWord ( currentLine [ characterIndex ] ) )
				{
					wordIndex = characterIndex;
					wordSnaps = 0;
					hasFlubbedWord = false;
				}

				// Start line
				lineManager.SetCurrentLine ( );

				// Check if last line in the stanza
				if ( lineIndex + 1 >= stanzas [ stanzaIndex ].Lines.Length )
				{
					// Set up next break
					SetNextBreak ( );
				}
				else
				{
					// Set up line in next stanza
					SetNextLine ( stanzaIndex, lineIndex + 1 );
				}
			}
		}

		/// <summary>
		/// Sets the formatting of a line.
		/// </summary>
		/// <param name="line"> The text of the line. </param>
		/// <param name="wordModifiers"> The word modifiers for the line. </param>
		/// <returns> The formatted preview of the line. </returns>
		private string FormatLine ( string line, Poems.WordModel [ ] wordModifiers )
		{
			// Check for word modifiers
			if ( wordModifiers != null )
			{
				// Apply each modification
				for ( int i = wordModifiers.Length - 1; i >= 0; i-- )
				{
					// Check for Caps modifier
					if ( wordModifiers [ i ].Modifier == Enums.WordModifierType.CAPS )
					{
						// Get word
						string word = line.Substring ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length );

						// Insert formatted word into the line
						line = line.Remove ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length ).Insert ( wordModifiers [ i ].StartIndex, word.ToUpper ( ) );
					}
					// Check for small modifier
					else if ( wordModifiers [ i ].Modifier == Enums.WordModifierType.SMALL )
					{
						// Get word
						string word = line.Substring ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length );

						// Insert formatted word into the line
						line = line.Remove ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length ).Insert ( wordModifiers [ i ].StartIndex, word.ToLower ( ) );
					}
				}
			}

			// Return the formatted line
			return line;
		}

		/// <summary>
		/// Begins the performance of the next break of the poem.
		/// </summary>
		private void BeginBreak ( )
		{
			// Start break
			lineManager.SetCurrentBreak ( performanceManager.Model.BreakDuration, OnBreakComplete );

			// Pause input
			ToggleInput ( false );

			// Set up line in next stanza
			SetNextLine ( stanzaIndex, 0 );
		}

		/// <summary>
		/// Sets up the next line in the poem after the current line.
		/// </summary>
		/// <param name="stanza"> The index of the current stanza. </param>
		/// <param name="line"> The index of the next line. </param>
		/// <param name="isAnimatingIn"> Whether or not the next line should animate in and push up the lines in the poem. </param>
		private void SetNextLine ( int stanza, int line, bool isAnimatingIn = true )
		{
			// Check for valid line
			if ( stanza < stanzas.Length && line < stanzas [ stanza ].Lines.Length )
			{
				// Get key
				Poems.LineKey key = new Poems.LineKey
				{
					Stanza = stanza,
					Line = line
				};

				// Get the word modifiers for the next line
				nextModifiers = null;
				if ( modifiers != null && modifiers.ContainsKey ( key ) )
				{
					nextModifiers = modifiers [ key ].ToArray ( );
				}

				// Get text
				string text = FormatLine ( stanzas [ stanza ].Lines [ line ], nextModifiers );

				// Create next line
				lineManager.SetNextLine ( text, nextModifiers, isAnimatingIn );
			}
		}

		/// <summary>
		/// Sets up the next break in the poem after the current line.
		/// </summary>
		private void SetNextBreak ( )
		{
			// Create next break
			lineManager.SetNextBreak ( );
		}

		/// <summary>
		/// The callback for when a stanza is completed.
		/// </summary>
		private void OnStanzaComplete ( )
		{
			// Increment to next stanza
			stanzaIndex++;

			// Check for end of poem
			if ( stanzaIndex >= stanzas.Length )
			{
				// Trigger end of poem
				OnPoemComplete ( );
			}
			else
			{
				// Begin next break
				BeginBreak ( );
			}
		}

		/// <summary>
		/// The callback for when a stanza break is completed.
		/// </summary>
		private void OnBreakComplete ( )
		{
			// Reset to starting line of stanza
			lineIndex = 0;

			// Resume input
			ToggleInput ( true );

			// Begin next line
			BeginLine ( );
		}

		/// <summary>
		/// The callback for when the poem is completed.
		/// </summary>
		private void OnPoemComplete ( )
		{
			// Animate in another break to move the last line
			SetNextBreak ( );

			// Stop poem
			isPoemActive = false;
			ToggleInput ( false );
			inputTextManager.SetActive ( false );

			// Trigger end of performance
			performanceManager.CompletePerformance ( );
		}

		#endregion // Private Poem Display Functions

		#region Private Character Functions

		/// <summary>
		/// Checks if the input character matches the character of the line.
		/// </summary>
		/// <param name="line"> The character of the line. </param>
		/// <param name="input"> The input character. </param>
		/// <returns></returns>
		private bool CompareCharacters ( char line, char input )
		{
			// Check if the characters match
			if ( line == input )
			{
				// Return match
				return true;
			}
			else
			{
				// Check for word modifier effect
				if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
				{
					// Check for trigger
					if ( GetWordModifierCompareCharactersEffect ( currentModifiers [ currentModifierIndex ].Modifier ) )
					{
						// Return match
						return true;
					}
				}

				// Check if item effects cause characters to match
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
					{
						// Check for item trigger
						if ( GameManager.Run.ItemData [ i ].Item.OnCompareCharacters ( line, input ) )
						{
							// Highlight item
							performanceManager.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );

							// Return match
							return true;
						}
					}
				}

				// Return mismatch
				return false;
			}
		}

		/// <summary>
		/// Submits an accurate character for the line.
		/// </summary>
		/// <param name="text"> The text to display. </param>
		/// <param name="isNaturallyCorrect"> Whether the charater match was natural (true) or was a flub prevention (false). </param>
		/// <param name="onComplete"> The callback for when the submission is complete. </param>
		private void CharacterMatch ( string text, bool isNaturallyCorrect, System.Action onComplete )
		{
			// Calculate snaps earned
			int snaps = CalculateSnaps ( text, true );

			// Add earned snaps
			lineSnaps += snaps;
			wordSnaps += snaps;

			// Get modification
			Enums.WordModifierType modifier = Enums.WordModifierType.NONE;
			if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
			{
				modifier = currentModifiers [ currentModifierIndex ].Modifier;
			}

			// Display accurate input
			lineManager.AppendLine ( text, modifier, isNaturallyCorrect, snaps );

			// Trigger completion
			onComplete ( );
		}

		/// <summary>
		/// Submits an inaccurate character for the line.
		/// </summary>
		/// <param name="text"> The text to display. </param>
		/// <param name="onComplete"> The callback for when the submission is complete. </param>
		private void CharacterMismatch ( string text, System.Action onComplete )
		{
			// Pause input
			ToggleInput ( false );

			// Mark flub
			hasFlubbedWord = true;

			// Calculate snaps earned
			int snaps = CalculateSnaps ( text, false );

			// Add earned snaps
			lineSnaps += snaps;
			wordSnaps += snaps;

			// Trigger any on flub perk effects
			GameManager.Run.Perk.OnFlub ( performanceManager.Model );

			// Trigger any on flub item effects
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnFlub ( ) )
					{
						// Highlight item use
						performanceManager.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, GameManager.Run.ItemData [ i ].Item.IsFlubEffectPositive ( ) );
					}
				}
			}

			// Get modification
			Enums.WordModifierType modifier = Enums.WordModifierType.NONE;
			if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
			{
				modifier = currentModifiers [ currentModifierIndex ].Modifier;
			}

			// Display inaccurate input
			lineManager.AppendLine ( text, modifier, false, snaps );

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnFlub ( );
			}

			// Consume confidence
			performanceManager.LoseConfidence ( );

			// Animate flub
			lineManager.FlubLine ( performanceManager.Model.FlubDuration, ( ) =>
			{
				// Resume input
				ToggleInput ( true );

				// Trigger completion
				onComplete ( );
			} );
		}

		/// <summary>
		/// Calculates the number snaps earned.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <param name="isCorrect"> Whether or not the text was correct. </param>
		/// <returns> The number of snaps earned. </returns>
		private int CalculateSnaps ( string text, bool isCorrect )
		{
			// Store snaps earned
			int snaps = 0;

			// Check if text is correct
			if ( isCorrect )
			{
				// Check if status effect will prevent character from earning snaps
				bool canCharacterEarnSnaps = true;
				for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
				{
					// Check for status effect
					if ( GameManager.Run.IsValidStatusEffect ( i ) )
					{
						// Get if character can earn snaps from status effect
						if ( GetStatusEffectPreventSnapsEffect ( GameManager.Run.StatusEffectData [ i ].Type ) )
						{
							// Prevent snaps from being earned
							canCharacterEarnSnaps = false;
							break;
						}
					}
				}

				// Check if can earn snaps
				if ( !canCharacterEarnSnaps || !canLineEarnSnaps )
				{
					// Return that no snaps can be earned
					return 0;
				}

				// Check for white space
				if ( text != " " )
				{
					// Score snaps
					snaps += performanceManager.Model.BaseSnaps;
				}

				// Check for The Afro-Surrealist judge
				if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetAfroSurrealistId ( ) )
				{
					// Earn time
					performanceManager.Model.TimeRemaining += Judges.JudgeHelper.GetAfroSurrealistTimeEarned ( );
				}

				// Get snaps from word modifier
				Enums.WordModifierType modifier = Enums.WordModifierType.NONE;
				if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
				{
					// Store modifier
					modifier = currentModifiers [ currentModifierIndex ].Modifier;

					// Add snaps from word modifier effects
					snaps += GetWordModifierCharacterMatchEffect ( modifier );

					// Check for Redacted word modifier
					if ( modifier == Enums.WordModifierType.REDACTED )
					{
						return 0;
					}
				}

				// Get staps from status effects
				for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
				{
					// Check for status effect
					if ( GameManager.Run.IsValidStatusEffect ( i ) )
					{
						// Add snaps from status effect
						snaps += GetStatusEffectCharacterMatchEffect ( GameManager.Run.StatusEffectData [ i ].Type );
					}
				}

				// Apply additional snaps from the player's perk
				snaps += GameManager.Run.Perk.OnScoreCharacter ( text, snaps, modifier );

				// Apply additional snaps from items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
					{
						// Get additional snaps
						int itemSnaps = GameManager.Run.ItemData [ i ].Item.OnScoreCharacter ( text, snaps, modifier, performanceManager.Model );

						// Check for snaps earned from item
						if ( itemSnaps > 0 )
						{
							// Add snaps
							snaps += itemSnaps;

							// Highlight item use
							performanceManager.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
						}
					}
				}
			}
			else
			{
				// Apply penalty
				snaps = performanceManager.Model.FlubPenalty;

				// Get snaps from word modifier
				if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
				{
					// Add snaps from word modifier effects
					snaps += GetWordModifierCharacterMismatchEffect ( currentModifiers [ currentModifierIndex ].Modifier );
				}

				// Get snaps from status effect
				for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
				{
					// Check for status effect
					if ( GameManager.Run.IsValidStatusEffect ( i ) )
					{
						// Add snaps from status effect
						snaps += GetStatusEffectCharacterMismatchEffect ( GameManager.Run.StatusEffectData [ i ].Type, snaps );
					}
				}
			}

			// Check for Editor judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetEditorId ( ) )
			{
				// Get total snaps
				snaps = Judges.JudgeHelper.GetEditorTotalSnaps ( snaps );
			}

			// Return the snaps earned
			return snaps;
		}

		/// <summary>
		/// The callback for when an input has completed its submission.
		/// </summary>
		private void OnInputTextComplete ( )
		{
			// Check if poem is still active
			if ( isPoemActive )
			{
				// Get modification
				Enums.WordModifierType modifier = Enums.WordModifierType.NONE;
				if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
				{
					modifier = currentModifiers [ currentModifierIndex ].Modifier;
				}

				// Trigger completion of a character
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
					{
						// Trigger item
						Items.ItemTriggerModel trigger = GameManager.Run.ItemData [ i ].Item.OnCharacterComplete ( modifier, performanceManager.Model );
						if ( trigger != null )
						{
							// Highlight item
							OnItemTrigger ( trigger, true );
						}
					}
				}

				// Increment to next character
				characterIndex++;

				// Check if still in the line
				if ( characterIndex < currentLine.Length )
				{
					// Check for current word
					if ( wordIndex == -1 )
					{
						// Check for start of a new word
						if ( Poems.PoemHelper.IsStartOfWord ( currentLine [ characterIndex ] ) )
						{
							// Store start of a new word
							wordIndex = characterIndex;
						}
					}
					else
					{
						// Check for the end of the current word
						if ( Poems.PoemHelper.IsEndOfWord ( currentLine, characterIndex ) )
						{
							// Mark end of word
							OnWordComplete ( characterIndex - wordIndex );

							// Reset word
							wordIndex = -1;
						}
					}
				}
				else
				{
					// Check for end of word
					if ( wordIndex != -1 && Poems.PoemHelper.IsEndOfWord ( currentLine, characterIndex ) )
					{
						// Mark end of word
						OnWordComplete ( characterIndex - wordIndex );

						// Reset word
						wordIndex = -1;
					}

					// Trigger the end of line
					OnLineComplete ( );
				}
			}
		}

		#endregion // Private Character Functions

		#region Private Word Functions

		/// <summary>
		/// The callback for when a word is completed.
		/// </summary>
		/// <param name="length"> The length of the word. </param>
		private void OnWordComplete ( int length )
		{
			// Increment words completed
			wordCount++;

			// Track additional snaps earned for the word
			int snaps = 0;

			// Get snaps from word modifier
			Enums.WordModifierType modifier = Enums.WordModifierType.NONE;
			if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && characterIndex >= currentModifiers [ currentModifierIndex ].StartIndex )
			{
				modifier = currentModifiers [ currentModifierIndex ].Modifier;
				snaps += GetWordModifierWordCompletionEffect ( modifier );
			}

			// Get snaps from status effects
			for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( GameManager.Run.IsValidStatusEffect ( i ) )
				{
					// Get snaps from status effect
					snaps += GetStatusEffectWordCompletionEffect ( GameManager.Run.StatusEffectData [ i ].Type, modifier );
				}
			}

			// Trigger any on word complete effects from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Trigger item
					Items.ItemTriggerModel trigger = GameManager.Run.ItemData [ i ].Item.OnWordComplete ( wordSnaps + snaps, length, modifier, performanceManager.Model );
					if ( trigger != null )
					{
						// Check if trigger is highlighted
						bool isHighlighted = true;
						if ( trigger.Snaps > 0 )
						{
							isHighlighted = canLineEarnSnaps;
						}

						// Highlight item
						OnItemTrigger ( trigger, isHighlighted );

						// Check it line can earn snaps
						if ( canLineEarnSnaps )
						{
							// Apply snaps
							snaps += trigger.Snaps;
						}
					}
				}
			}

			// Check for judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Check for the Pretentious Snob judge
				if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetPretentiousSnobId ( ) )
				{
					// Check word length
					if ( Judges.JudgeHelper.IsPretentiousSnobPenalty ( length ) )
					{
						// Apply penalty
						snaps += Judges.JudgeHelper.GetPretentiousSnobPenalty ( wordSnaps + snaps );
					}
				}
				// Check for the Bestselling Author judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetBestsellingAuthorId ( ) )
				{
					// Check word length
					if ( Judges.JudgeHelper.IsBestsellingAuthorPenalty ( length ) )
					{
						// Apply penalty
						snaps += Judges.JudgeHelper.GetBestsellingAuthorPenalty ( wordSnaps + snaps );
					}
				}
				// Check for the Celebrity judge
				else if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetCelebrityId ( ) )
				{
					// Check word count
					StatusEffects.StatusEffectModel statusEffect = Judges.JudgeHelper.GetCelebrityWordCount ( wordCount );

					// Check for status effect
					if ( statusEffect != null )
					{
						// Check for end of line
						if ( IsEndOfLine ( ) )
						{
							// Queue status effect
							statusEffectQueue.Add ( statusEffect );
						}
						else
						{
							// Apply status effect
							GameManager.Run.AddStatusEffect ( statusEffect );

							// Update status effects
							performanceManager.UpdateStatusEffects ( );
						}
					}
				}
			}

			// Apply any additional snaps earned
			if ( snaps != 0 && canLineEarnSnaps )
			{
				// Add snaps
				lineSnaps += snaps;

				// Display snaps earned
				lineManager.AddSnaps ( snaps );
			}

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnWordComplete ( snaps, length, modifier, performanceManager.Model );
			}

			// Check for word modifier
			if ( currentModifiers != null && currentModifierIndex < currentModifiers.Length && wordIndex == currentModifiers [ currentModifierIndex ].StartIndex )
			{
				// Increment to next word modifier
				currentModifierIndex++;
			}

			// Reset word
			wordIndex = -1;
			wordSnaps = 0;
			hasFlubbedWord = false;
		}

		#endregion // Private Word Functions

		#region Private Line Functions

		/// <summary>
		/// The callback for when a line is completed.
		/// </summary>
		private void OnLineComplete ( )
		{
			// Track snaps earned for completion
			int completionSnaps = 0;

			// Trigger perk effect
			StatusEffects.StatusEffectModel perkStatusEffect = GameManager.Run.Perk.OnLineComplete ( );
			if ( perkStatusEffect != null )
			{
				// Add status effect to the queue
				statusEffectQueue.Add ( perkStatusEffect );
			}

			// Trigger any status effects
			for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( GameManager.Run.IsValidStatusEffect ( i ) )
				{
					// Get snaps
					int snaps = GetStatusEffectLineCompletionEffect ( GameManager.Run.StatusEffectData [ i ].Type, lineSnaps + completionSnaps );

					// Check if the line can earn snaps
					if ( canLineEarnSnaps )
					{
						completionSnaps += snaps;
					}
				}
			}

			// Trigger any item effects
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Trigger item
					Items.ItemTriggerModel trigger = GameManager.Run.ItemData [ i ].Item.OnLineComplete ( lineSnaps + completionSnaps );
					if ( trigger != null )
					{
						// Check if trigger is highlighted
						bool isHighlighted = true;
						if ( trigger.Snaps > 0 )
						{
							isHighlighted = canLineEarnSnaps;
						}

						// Highlight item
						OnItemTrigger ( trigger, isHighlighted );

						// Check if the line can earn snaps
						if ( canLineEarnSnaps )
						{
							completionSnaps += trigger.Snaps;
						}
					}

					// Check for end of stanza
					if ( lineIndex + 1 >= stanzas [ stanzaIndex ].Lines.Length )
					{
						// Trigger item
						trigger = GameManager.Run.ItemData [ i ].Item.OnStanzaComplete ( performanceManager.Model );
						if ( trigger != null )
						{
							// Check if trigger is highlighted
							bool isHighlighted = true;
							if ( trigger.Snaps > 0 )
							{
								isHighlighted = canLineEarnSnaps;
							}

							// Highlight item
							OnItemTrigger ( trigger, isHighlighted );

							// Check if the line can earn snaps
							if ( canLineEarnSnaps )
							{
								completionSnaps += trigger.Snaps;
							}
						}
					}
				}
			}

			// Check for Thespian judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetThespianId ( ) )
			{
				// Clear additional snaps
				completionSnaps = Judges.JudgeHelper.GetThespianLineSnaps ( );
			}

			// Complete line
			lineManager.CompleteLine ( completionSnaps );

			// Add earned snaps
			lineSnaps += completionSnaps;
			performanceManager.EarnSnaps ( lineSnaps );

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnLineComplete ( );
			}

			// Increment to next line
			lineIndex++;

			// Expire status effects
			ExpireStatusEffects ( );

			// Check for Clown judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetClownId ( ) )
			{
				// Disable random item
				Judges.JudgeHelper.SetClownItemsEnabled ( false );

				// Update HUD
				performanceManager.UpdateItems ( );
			}

			// Update consumable HUD
			performanceManager.UpdateConsumables ( );

			// Check for end of stanza
			if ( lineIndex >= stanzas [ stanzaIndex ].Lines.Length )
			{
				// Trigger end of stanza
				OnStanzaComplete ( );
			}
			else
			{
				// Begin next line
				BeginLine ( );
			}
		}

		#endregion // Private Line Functions

		#region Private Word Modifier Functions

		/// <summary>
		/// Triggers the effect of a character match from a word modifier.
		/// </summary>
		/// <param name="modifier"> The modifier for the word. </param>
		/// <returns> The additional snaps earned from the modifier. </returns>
		private int GetWordModifierCharacterMatchEffect ( Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					return GetBoldEffect ( );

				case Enums.WordModifierType.ITALICS:
					return GetItalicsEffect ( );

				case Enums.WordModifierType.CAPS:
					return GetCapsEffect ( );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Triggers the effect of a character mismatch from a word modifier.
		/// </summary>
		/// <param name="modifier"> The modifier for the word. </param>
		/// <returns> The additional snaps earned from the modifier. </returns>
		private int GetWordModifierCharacterMismatchEffect ( Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.HIGHLIGHT:
					return GetHighlightEffect ( );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Triggers the effect of comparing characters from a word modifier.
		/// </summary>
		/// <param name="modifer"> The modifier for the word. </param>
		/// <returns> Whether or not the word modifier should override a flub. </returns>
		private bool GetWordModifierCompareCharactersEffect ( Enums.WordModifierType modifer )
		{
			// Check for modifier
			switch ( modifer )
			{
				case Enums.WordModifierType.STRIKETHROUGH:
					// Update stats
					GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.STRIKETHROUGH, 1 );
					return true;
			}

			// Return no change
			return false;
		}

		/// <summary>
		/// Triggers the effect of completing a word from a word modifier.
		/// </summary>
		/// <param name="modifier"> The modifier for the word. </param>
		/// <returns> The additional snaps earned from the modifier. </returns>
		private int GetWordModifierWordCompletionEffect ( Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.UNDERLINE:
					return GetUnderlineEffect ( );

				case Enums.WordModifierType.SMALL:
					return GetSmallEffect ( );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Bold word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetBoldEffect ( )
		{
			// Check for 20% trigger chance
			bool isTriggered = Random.Range ( 0f, 1f ) < 0.25f;
			if ( isTriggered )
			{
				// Earn $2
				performanceManager.EarnMoney ( 2 );

				// Update stats
				GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.BOLD, 2 );

				// Check for item effect triggers
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
					{
						// Check for item trigger
						if ( GameManager.Run.ItemData [ i ].Item.OnBoldTrigger ( ) )
						{
							// Highlight item
							performanceManager.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
						}
					}
				}
			}

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Italics word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetItalicsEffect ( )
		{
			// Add time
			performanceManager.Model.TimeRemaining += 3f;

			// Update stats
			GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.ITALICS, 3 );

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Underline word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetUnderlineEffect ( )
		{
			// Check for flub
			if ( !hasFlubbedWord )
			{
				// Gain confidence from not flubbing
				performanceManager.GainConfidence ( );

				// Update stats
				GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.UNDERLINE, 1 );
			}

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the snaps earned from the Caps word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetCapsEffect ( )
		{
			// Update stats
			GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.CAPS, 5 );

			// Return +5 snaps
			return 5;
		}

		/// <summary>
		/// Gets the snaps earned from the small word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetSmallEffect ( )
		{
			// Get snaps
			int snaps = hasFlubbedWord ? 0 : wordSnaps;

			// Update stats
			GameManager.Run.Stats.OnModifierTrigger ( Enums.WordModifierType.SMALL, snaps * 2 );

			// Double snaps earned if no flubs
			return snaps;
		}

		/// <summary>
		/// Gets the snaps earned from the Highlight word modifier.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetHighlightEffect ( )
		{
			// Check if snaps can be earned
			if ( canLineEarnSnaps )
			{
				// Set that snaps can no longer be earned for this line
				canLineEarnSnaps = false;

				// Negate any snaps earned
				int snaps = lineSnaps + performanceManager.Model.FlubPenalty;
				return snaps > 0 ? snaps * -1 : 0;
			}

			// Return no additional penalty
			return 0;
		}

		#endregion // Private Word Modifier Functions

		#region Private Status Effect Functions

		/// <summary>
		/// Expires a stack of each status effect.
		/// </summary>
		private void ExpireStatusEffects ( )
		{
			// Track first available index
			int availableIndex = 0;

			// Track total expirations
			int total = 0;

			// Search status effects
			for ( int i = 0; i < GameManager.Run.StatusEffectData.Length; i++ )
			{
				// Check for status effect
				if ( GameManager.Run.IsValidStatusEffect ( i ) )
				{
					// Check if the status effect should expire
					bool isExpiring = true;
					for ( int j = 0; j < GameManager.Run.ItemData.Length; j++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( j ) )
						{
							// Check for expire prevention
							if ( GameManager.Run.ItemData [ j ].Item.IsStatusEffectPreventExpire ( ) )
							{
								// Highlight item
								performanceManager.HighlightItem ( GameManager.Run.ItemData [ j ].ID, GameManager.Run.ItemData [ j ].InstanceID, true );

								// Store prevention
								isExpiring = false;
								break;
							}
						}
					}

					// Check if expiring
					if ( isExpiring )
					{
						// Decrement stack
						GameManager.Run.StatusEffectData [ i ].Count--;

						// Increment total expirations
						total++;
					}

					// Check for remaining stacks
					if ( GameManager.Run.StatusEffectData [ i ].Count > 0 )
					{
						// Check if the status effect needs to be moved
						if ( availableIndex < i )
						{
							// Move status effect to first available slot
							GameManager.Run.StatusEffectData [ availableIndex ] = GameManager.Run.StatusEffectData [ i ];

							// Clear status effect slot
							GameManager.Run.StatusEffectData [ i ] = null;
						}

						// Increment first available slot
						availableIndex++;
					}
					else
					{
						// Clear expired status effect
						GameManager.Run.StatusEffectData [ i ] = null;
					}
				}
			}

			// Check if status effects expired
			if ( total > 0 )
			{
				// Trigger effects from items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) )
					{
						// Trigger item
						if ( GameManager.Run.ItemData [ i ].Item.OnStatusEffectExpire ( total ) )
						{
							// Highlight item
							performanceManager.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
						}
					}
				}
			}

			// Check queue
			int queue = statusEffectQueue.Count;
			if ( queue > 0 )
			{
				// Add each status effect in the queue
				for ( int i = 0; i < statusEffectQueue.Count; i++ )
				{
					// Apply status effect
					GameManager.Run.AddStatusEffect ( statusEffectQueue [ i ] );
				}

				// Clear queue
				statusEffectQueue.Clear ( );
			}
			
			// Check is status effects changed
			if ( total > 0 || queue > 0 )
			{
				// Update HUD
				performanceManager.UpdateStatusEffects ( );
			}
		}

		/// <summary>
		/// Triggers the effect of whether or not a character is prevented from earn snaps.
		/// </summary>
		/// <param name="statusEffect"> The type of the status effect. </param>
		/// <returns> Whether or not the character is prevented from earn snaps. </returns>
		private bool GetStatusEffectPreventSnapsEffect ( Enums.StatusEffectType statusEffect )
		{
			// Check status effect
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.IMPAIRED:
					return GetImpairedEffect ( );
			}

			// Return that snaps cannot be earned
			return false;
		}

		/// <summary>
		/// Triggers the effect of matching a character from a status effect.
		/// </summary>
		/// <param name="statusEffect"> The type of the status effect. </param>
		/// <returns> The additional snaps earned from the status effect. </returns>
		private int GetStatusEffectCharacterMatchEffect ( Enums.StatusEffectType statusEffect )
		{
			// Check status effect
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.DRAMATIC:
					return GetDramaticEffect ( );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Triggers the effect of a character mismatch from a status effect.
		/// </summary>
		/// <param name="statusEffect"> The type of the status effect. </param>
		/// <param name="total"> The total amount of snaps earned for the flub. </param>
		/// <returns> The additional snaps earned from the status effect. </returns>
		private int GetStatusEffectCharacterMismatchEffect ( Enums.StatusEffectType statusEffect, int total )
		{
			// Check status effect
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.STUBBORN:
					return GetStubbornEffect ( );

				case Enums.StatusEffectType.FOCUSED:
					return GetFocusedEffect ( );

				case Enums.StatusEffectType.ANXIOUS:
					return GetAnxiousEffect ( total );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Triggers the effect of completing a word from a status effect.
		/// </summary>
		/// <param name="statusEffect"> The type of the status effect. </param>
		/// <param name="modifier"> The modifier for the word. </param>
		/// <returns> The additional snaps earned from the status effect. </returns>
		private int GetStatusEffectWordCompletionEffect ( Enums.StatusEffectType statusEffect, Enums.WordModifierType modifier )
		{
			// Check status effect
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.GREEDY:
					return GetGreedyEffect ( modifier );

				case Enums.StatusEffectType.POPULAR:
					return GetPopularEffect ( );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Triggers the effect of completing a line from a status effect.
		/// </summary>
		/// <param name="statusEffect"> The type of the status effect. </param>
		/// <param name="total"> The current total number of snaps earned for the line. </param>
		/// <returns> The additional snaps earned from the status effect. </returns>
		private int GetStatusEffectLineCompletionEffect ( Enums.StatusEffectType statusEffect, int total )
		{
			// Check status effect
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.EXCITED:
					return GetExcitedEffect ( );

				case Enums.StatusEffectType.SERIOUS:
					return GetSeriousEffect ( total );
			}

			// Return no snaps earned
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Stubborn status effect.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetStubbornEffect ( )
		{
			// Increase arrogance
			performanceManager.Model.ArroganceRemaining += 2;

			// Update stats
			GameManager.Run.Stats.OnStatusEffectTrigger ( Enums.StatusEffectType.STUBBORN, 2 );

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Greedy status effect.
		/// </summary>
		/// <param name="modifier"> The modifier for the word. </param>
		/// <returns> The additional snaps earned. </returns>
		private int GetGreedyEffect ( Enums.WordModifierType modifier )
		{
			// Check for modifier
			if ( modifier != Enums.WordModifierType.NONE )
			{
				// Earn $1
				performanceManager.EarnMoney ( 1 );

				// Update stats
				GameManager.Run.Stats.OnStatusEffectTrigger ( Enums.StatusEffectType.GREEDY, 1 );
			}

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Dramatic status effect.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetDramaticEffect ( )
		{
			// Get snaps earned per 30 seconds remaining
			int snaps = (int)( performanceManager.Model.TimeRemaining / 30f );

			// Update stats
			GameManager.Run.Stats.OnStatusEffectTrigger ( Enums.StatusEffectType.DRAMATIC, snaps );

			// Return snaps per 30 seconds remaining
			return snaps;
		}

		/// <summary>
		/// Gets the effect from the Popular status effect.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetPopularEffect ( )
		{
			// Update stats
			GameManager.Run.Stats.OnStatusEffectTrigger ( Enums.StatusEffectType.POPULAR, GameManager.Run.Reputation );

			// Return snaps per reputation
			return GameManager.Run.Reputation;
		}

		/// <summary>
		/// Gets the effect from the Excited status effect.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetExcitedEffect ( )
		{
			// Check if effect meets the 50% chance
			if ( Random.Range ( 0f, 1f ) < 0.5f )
			{
				// Check if new consumable can be added
				if ( GameManager.Run.CanAddConsumable ( ) )
				{
					// Generate new consumable
					Consumables.ConsumableScriptableObject consumable = GameManager.Run.RarityData.GenerateConsumable ( -1f );

					// Check for consumable
					if ( consumable != null )
					{
						// Add consumable
						GameManager.Run.AddConsumable ( consumable.ID, 1 );
					}
				}
				else
				{
					// Get index of existing consumable
					int index = Random.Range ( 0, GameManager.Run.ConsumableCount );

					// Add additional instance of the existing consumable
					GameManager.Run.AddConsumable ( GameManager.Run.ConsumableData [ index ].ID, 1 );
				}

				// Update stats
				GameManager.Run.Stats.OnStatusEffectTrigger ( Enums.StatusEffectType.EXCITED, 1 );
			}

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Serious status effect.
		/// </summary>
		/// <param name="total"> The current total number of snaps earned for the line. </param>
		/// <returns> The additional snaps earned. </returns>
		private int GetSeriousEffect ( int total )
		{
			// Track snaps earned
			int completionSnaps = 0;

			// Trigger end of line effects for items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
				{
					// Trigger item
					Items.ItemTriggerModel trigger = GameManager.Run.ItemData [ i ].Item.OnLineComplete ( total + completionSnaps );
					if ( trigger != null )
					{
						// Check if trigger is highlighted
						bool isHighlighted = true;
						if ( trigger.Snaps > 0 )
						{
							isHighlighted = canLineEarnSnaps;
						}

						// Highlight item
						OnItemTrigger ( trigger, isHighlighted );

						// Check if the line can earn snaps
						if ( canLineEarnSnaps )
						{
							completionSnaps += trigger.Snaps;
						}
					}
				}
			}

			// Return additional snaps earned
			return completionSnaps;
		}

		/// <summary>
		/// Gets the effect from the Focused status effect.
		/// </summary>
		/// <returns> The additional snaps earned. </returns>
		private int GetFocusedEffect ( )
		{
			// Check for The Futurist judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetFuturistId ( ) )
			{
				// Return no additional snaps
				return 0;
			}

			// Lose confidence
			performanceManager.Model.ConfidenceRemaining--;

			// Return no additional snaps
			return 0;
		}

		/// <summary>
		/// Gets the effect from the Anxious status effect.
		/// </summary>
		/// <param name="total"> The total number of snaps earned. </param>
		/// <returns> The additional snaps earned. </returns>
		private int GetAnxiousEffect ( int total )
		{
			// Double flub penalty
			return total;
		}

		/// <summary>
		/// Gets the effect from the Impaired status effect.
		/// </summary>
		/// <returns> Whether or not a character is prevented from earning snaps. </returns>
		private bool GetImpairedEffect ( )
		{
			// Check if character is prevented from earning snaps
			return Random.Range ( 0f, 1f ) < 0.3f;
		}

		#endregion // Private Status Effect Functions

		#region Private Functions

		/// <summary>
		/// The callback for when the performance is paused.
		/// </summary>
		/// <param name="isPaused"> Whether or not the performance is paused. </param>
		private void OnPause ( bool isPaused )
		{
			// Check if performance is in progress
			if ( performanceManager.IsPerforming )
			{
				// Enable/disable the input
				ToggleInput ( !isPaused );
			}
		}

		/// <summary>
		/// Toggles whether or not input is being read.
		/// </summary>
		/// <param name="isActive"> Whether or not input is being read (true). </param>
		private void ToggleInput ( bool isActive )
		{
			// Store state
			isInputActive = isActive;

			// Toggle input feed
			inputTextManager.SetActive ( isInputActive );
		}

		/// <summary>
		/// The callback for when an item is triggered.
		/// </summary>
		/// <param name="model"> The data for the item trigger. </param>
		/// <param name="isHighlighted"> Whether or not the item should be highlighted for being triggered. </param>
		private void OnItemTrigger ( Items.ItemTriggerModel model, bool isHighlighted )
		{
			// Check for valid model
			if ( model != null && model.IsValid ( ) )
			{
				// Check if highlighted
				if ( isHighlighted )
				{
					// Highlight the item being triggered
					performanceManager.HighlightItem ( model.ID, model.InstanceID, model.IsPositive );
				}

				// Check for status effects
				if ( model.StatusEffect != null )
				{
					// Check for the end of the line
					if ( IsEndOfLine ( ) )
					{
						// Queue up the status effect to be added after the current status effects expire
						statusEffectQueue.Add ( model.StatusEffect );
					}
					else
					{
						// Apply status effect
						GameManager.Run.AddStatusEffect ( model.StatusEffect );

						// Update HUD
						performanceManager.UpdateStatusEffects ( );
					}
				}
			}
		}

		#endregion // Private Functions
	}
}