namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This struct stores the data for applause earned for a performance to be displayed in the summary.
	/// </summary>
	public struct ApplauseModel
	{
		/// <summary>
		/// The ID of the item giving the bonus.
		/// </summary>
		public int ItemID;

		/// <summary>
		/// The ID of the instance of the item giving the bonus.
		/// </summary>
		public string ItemInstanceID;

		/// <summary>
		/// The amount of applause earned for this bonus.
		/// </summary>
		public int Applause;
	}
}