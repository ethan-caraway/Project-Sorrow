using UnityEngine;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the display of the lines in the poem.
	/// </summary>
	public class LineManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private InputLine inputLinePrefab;

		[SerializeField]
		private StanzaBreak stanzaBreakPrefab;

		[SerializeField]
		private Transform lineContainer;

		private InputLine currentLine;
		private StanzaBreak currentBreak;
		private InputLine nextLine;
		private StanzaBreak nextBreak;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Initializes the upcoming line in the poem to be displayed.
		/// </summary>
		/// <param name="text"> The text for the line. </param>
		/// <param name="wordModifiers"> The word modifiers for the line. </param>
		/// <param name="isAnimatingIn"> Whether or not the line should animate in and push other lines up. True by default. </param>
		public void SetNextLine ( string text, Poems.WordModel [ ] wordModifiers, bool isAnimatingIn = true )
		{
			// Create line
			nextLine = Instantiate ( inputLinePrefab, lineContainer );

			// Setup line
			nextLine.SetLine ( FormatPreview ( text, wordModifiers ), isAnimatingIn );
		}

		/// <summary>
		/// Initializes the upcoming stanza break in the poem to be displayed.
		/// </summary>
		public void SetNextBreak ( )
		{
			// Create break
			nextBreak = Instantiate ( stanzaBreakPrefab, lineContainer );

			// Animate break
			nextBreak.AnimateIn ( );
		}

		/// <summary>
		/// Sets the next line as the current line and begins editing.
		/// </summary>
		public void SetCurrentLine ( )
		{
			// Store current line
			currentLine = nextLine;

			// Begin editing
			currentLine.EditLine ( );
		}

		/// <summary>
		/// Sets the next break as the current break and begins waiting.
		/// </summary>
		/// <param name="time"> The duration of time in seconds to wait for the break. </param>
		/// <param name="onComplete"> The callback for when the stanza break finishes waiting. </param>
		public void SetCurrentBreak ( float time, System.Action onComplete )
		{
			// Store current break
			currentBreak = nextBreak;

			// Begin timer
			currentBreak.StartTimer ( time, onComplete );
		}

		/// <summary>
		/// Progresses the display of the line.
		/// </summary>
		/// <param name="text"> The next character input by the user. </param>
		/// <param name="modifier"> The modification to be applied to the character. </param>
		/// <param name="isCorrect"> Whether or not the input for the next character in the line is accurate. </param>
		/// <param name="snaps"> The amount of snaps earned. </param>
		public void AppendLine ( string text, Enums.WordModifierType modifier, bool isCorrect, int snaps )
		{
			// Format the text
			text = Poems.PoemHelper.FormatWordModification ( text, modifier, false, isCorrect );

			// Append correct text
			currentLine.AppendLine ( text, snaps );
		}

		/// <summary>
		/// Applies additional snaps to be earned for completing a word.
		/// </summary>
		/// <param name="snaps"> The number of additional snaps earned. </param>
		public void AddSnaps ( int snaps )
		{
			currentLine.AddSnaps ( snaps );
		}

		/// <summary>
		/// Animate the line for when a mistake is made.
		/// </summary>
		/// <param name="duration"> The amount of time in seconds the animation should play for. </param>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void FlubLine ( float duration, System.Action onComplete )
		{
			// Play animation
			currentLine.PlayFlubAnimation ( duration, onComplete );
		}

		/// <summary>
		/// Displays that the line has been completed.
		/// </summary>
		/// <param name="snaps"> The number of additional snaps earned for completing the line. </param>
		public void CompleteLine ( int snaps )
		{
			// Display the completed line
			currentLine.CompleteLine ( snaps );
		}

		/// <summary>
		/// Prevents progression on the current line or break.
		/// </summary>
		public void StopLine ( )
		{
			// Stop line
			if ( currentLine != null )
			{
				currentLine.EndEditing ( );
			}

			// Stop break
			if ( currentBreak != null )
			{
				currentBreak.StopTimer ( );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Sets the formatted preview of the line.
		/// </summary>
		/// <param name="line"> The text of the line. </param>
		/// <param name="wordModifiers"> The word modifiers for the line. </param>
		/// <returns> The formatted preview of the line. </returns>
		private string FormatPreview ( string line, Poems.WordModel [ ] wordModifiers )
		{
			// Check for word modifiers
			if ( wordModifiers != null )
			{
				// Apply each modification
				for ( int i = wordModifiers.Length - 1; i >= 0; i-- )
				{
					// Get word
					string word = line.Substring ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length );

					// Apply formatting
					word = Poems.PoemHelper.FormatWordModification ( word, wordModifiers [ i ].Modifier, true, true );

					// Insert formatted word into the line
					line = line.Remove ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length ).Insert ( wordModifiers [ i ].StartIndex, word );
				}
			}
			
			// Return the formatted line
			return line;
		}

		#endregion // Private Functions
	}
}