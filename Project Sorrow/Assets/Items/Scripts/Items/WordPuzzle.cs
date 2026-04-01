namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Word Puzzle item.
	/// </summary>
	public class WordPuzzle : Item
	{
		#region Class Constructors

		public WordPuzzle ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for one of the least common letters
			if ( IsUncommonLetter ( text ) )
			{
				// Return additional snaps
				return 15;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not the character is one of the least common letters (J, K, Q, V, X, Z).
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <returns> Whether or not the character is one of the least common letters. </returns>
		private bool IsUncommonLetter ( string text )
		{
			// Get lowercase letter
			char c = text.ToLower ( ) [ 0 ];

			// Check for uncommon letter
			return
				c == 'j' ||
				c == 'k' ||
				c == 'q' ||
				c == 'v' ||
				c == 'x' ||
				c == 'z';
		}

		#endregion // Private Functions
	}
}