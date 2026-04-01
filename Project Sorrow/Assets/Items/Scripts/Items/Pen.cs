namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Pen item.
	/// </summary>
	public class Pen : Item
	{
		#region Class Constructors

		public Pen ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for vowel
			if ( IsVowel ( text ) )
			{
				// Return additional snaps
				return 3;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not the character is a vowel.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <returns> Whether or not the character is a vowel. </returns>
		private bool IsVowel ( string text )
		{
			// Get lowercase letter
			char c = text.ToLower ( ) [ 0 ];

			// Check for vowel
			return
				c == 'a' ||
				c == 'e' ||
				c == 'i' ||
				c == 'o' ||
				c == 'u';
		}

		#endregion // Private Functions
	}
}