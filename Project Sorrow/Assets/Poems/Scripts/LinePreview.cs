using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class controls the display of a line in a poem preview.
	/// </summary>
	public class LinePreview : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text lineText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Displays a given line of a poem with its word modifiers.
		/// </summary>
		/// <param name="line"> The line of the poem. </param>
		/// <param name="wordModifiers"> The word modifiers for the line. </param>
		public void SetLine ( string line, WordModel [ ] wordModifiers )
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
					word = PoemHelper.FormatWordModification ( word, wordModifiers [ i ].Modifier, false, true );

					// Insert formatted word into the line
					line = line.Remove ( wordModifiers [ i ].StartIndex, wordModifiers [ i ].Length ).Insert ( wordModifiers [ i ].StartIndex, word );
				}
			}

			// Display the formatted line
			lineText.text = line;
		}

		#endregion // Public Functions
	}
}