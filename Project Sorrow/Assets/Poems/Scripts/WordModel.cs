namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class stores the data for a word in a poem.
	/// </summary>
	[System.Serializable]
	public class WordModel
	{
		#region Word Data

		/// <summary>
		/// The index of the stanza that contains this word.
		/// </summary>
		public int Stanza;

		/// <summary>
		/// The index of the line that contains this word.
		/// </summary>
		public int Line;

		/// <summary>
		/// The index of the first letter of this word in the line.
		/// </summary>
		public int StartIndex;

		/// <summary>
		/// The number of letters contained in this word.
		/// </summary>
		public int Length;

		/// <summary>
		/// The type of modifier for this word.
		/// </summary>
		public Enums.WordModifierType Modifier;

		#endregion // Word Data
	}
}