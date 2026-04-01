namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Style Guide item.
	/// </summary>
	public class StyleGuide : Item
	{
		#region Class Constructors

		public StyleGuide ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for punctuation
			if ( IsPunctuation ( text ) )
			{
				// Return additional snaps
				return 5;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not the character is punctuation.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <returns> Whether or not the character is a punctuation. </returns>
		private bool IsPunctuation ( string text )
		{
			// Get character
			char c = text [ 0 ];

			// Check for consonant
			return
				c == ',' ||
				c == '.' ||
				c == '?' ||
				c == '!' ||
				c == ';' ||
				c == ':' ||
				c == '*' ||
				c == '\'' ||
				c == '\"' ||
				c == '(' ||
				c == ')' ||
				c == '-';
		}

		#endregion // Private Functions
	}
}