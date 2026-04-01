namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class stores the data for the summary screen after a performance.
	/// </summary>
	public class SummaryStatsModel
	{
		#region Summary Data

		/// <summary>
		/// Whether or not the performance was successful.
		/// </summary>
		public bool IsSuccess;

		/// <summary>
		/// The amount of snaps earned from confidence remaining.
		/// </summary>
		public int Confidence;

		/// <summary>
		/// The amount of snaps earned per confidence remaining.
		/// </summary>
		public int Reputation;

		/// <summary>
		/// The data for the applause earned.
		/// </summary>
		public ApplauseModel [ ] Applause;

		/// <summary>
		/// The amount of money earned from commission.
		/// </summary>
		public int Commission;

		/// <summary>
		/// The amount of snaps earned from the time remaining.
		/// </summary>
		public int Time;

		/// <summary>
		/// The amount of money earned from interest.
		/// </summary>
		public int Interest;

		/// <summary>
		/// The max amount of money that can be earned from interest.
		/// </summary>
		public int InterestCap;

		#endregion // Summary Data
	}
}