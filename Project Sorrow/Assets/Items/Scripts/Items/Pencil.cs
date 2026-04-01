namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Pencil item.
	/// </summary>
	public class Pencil : Item
	{
		#region Class Constructors

		public Pencil ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for consonant
			if ( IsConsonant ( text ) )
			{
				// Return additional snaps
				return 1;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not the character is a consonant.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <returns> Whether or not the character is a consonant. </returns>
		private bool IsConsonant ( string text )
		{
			// Get lowercase letter
			char c = text.ToLower ( ) [ 0 ];

			// Check for consonant
			return
				c == 'b' ||
				c == 'c' ||
				c == 'd' ||
				c == 'f' ||
				c == 'g' ||
				c == 'h' ||
				c == 'j' ||
				c == 'k' ||
				c == 'l' ||
				c == 'm' ||
				c == 'n' ||
				c == 'p' ||
				c == 'q' ||
				c == 'r' ||
				c == 's' ||
				c == 't' ||
				c == 'v' ||
				c == 'w' ||
				c == 'x' ||
				c == 'y' ||
				c == 'z';
		}

		#endregion // Private Functions
	}
}