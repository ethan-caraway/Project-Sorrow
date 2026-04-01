namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class stores the data for a round of a run.
	/// </summary>
	[System.Serializable]
	public class RoundModel
	{
		#region Round Data Constants

		/// <summary>
		/// The maximum amount of poem the player's draft each round.
		/// </summary>
		public const int MAX_DRAFTS = 6;

		#endregion // Round Data Constants

		#region Round Data

		/// <summary>
		/// The IDs of the poems for the current round in the run.
		/// </summary>
		public Poems.PoemModel [ ] Poems = new Poems.PoemModel [ Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND ];

		/// <summary>
		/// The IDs of the poems available to draft for the current round in the run.
		/// </summary>
		public int [ ] DraftIDs = new int [ MAX_DRAFTS ];

		/// <summary>
		/// The ID of the encounter for the current round in the run.
		/// </summary>
		public int EncounterID = 0;

		/// <summary>
		/// The ID of the judge for the current round in the run.
		/// </summary>
		public int JudgeID = 0;

		#endregion // Round Data
	}
}