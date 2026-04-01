namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This struct contains the key values for identifying a line in a poem.
	/// </summary>
	public struct LineKey
	{
		#region Line Data

		/// <summary>
		/// The index of the stanza containing this line.
		/// </summary>
		public int Stanza;

		/// <summary>
		/// The index of the line in the stanza.
		/// </summary>
		public int Line;

		#endregion // Line Data
	}
}