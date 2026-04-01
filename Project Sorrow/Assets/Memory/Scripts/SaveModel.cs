namespace FlightPaper.ProjectSorrow.Memory
{
	/// <summary>
	/// This class stores the save data for the player.
	/// </summary>
	[System.Serializable]
	public class SaveModel
	{
		#region Save Data

		/// <summary>
		/// The save data for an in-progress run. Null when a run is not in progress.
		/// </summary>
		public RunModel Run;

		/// <summary>
		/// The ID of the difficulty for an in-progress run. 0 when a run is not in progress.
		/// </summary>
		public int Difficulty = 0;

		/// <summary>
		/// The save data for the player's progression.
		/// </summary>
		public Progression.ProgressModel Progress = new Progression.ProgressModel ( );

		#endregion // Save Data
	}
}