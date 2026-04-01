namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class store the data for the current performance.
	/// </summary>
	[System.Serializable]
	public class PerformanceModel
	{
		#region Performance Data Constants

		/// <summary>
		/// The default commission for a performance.
		/// </summary>
		public const int DEFAULT_COMMISSION = 3;

		/// <summary>
		/// The default interest cap for a performance.
		/// </summary>
		public const int DEFAULT_INTEREST_CAP = 5;

		#endregion // Performance Data Constants

		#region Performance Data

		/// <summary>
		/// The total amount of money to be earned upon a successful performance.
		/// </summary>
		public int Commission = DEFAULT_COMMISSION;

		/// <summary>
		/// The total amount of money that can be earned from interest upon a successful performance.
		/// </summary>
		public int InterestCap = DEFAULT_INTEREST_CAP;
		
		/// <summary>
		/// The amount of money needed to earn one dollar in interest.
		/// </summary>
		public int InterestRate = 5;

		/// <summary>
		/// The current amount of confidence remaining during this performance.
		/// </summary>
		public int ConfidenceRemaining = 0;

		/// <summary>
		/// The current amount of arrogance remaining during this performance.
		/// </summary>
		public int ArroganceRemaining = 0;

		/// <summary>
		/// The total amount of time in seconds required for a flub.
		/// </summary>
		public float FlubDuration = 0.5f;

		/// <summary>
		/// The total amount of time in seconds remaining during this performance.
		/// </summary>
		public float TimeRemaining = 0;

		/// <summary>
		/// The increment of time in seconds that earns money at the end of the performance.
		/// </summary>
		public float TimeIncrement = 30f;

		/// <summary>
		/// The amount of money earned per increment of time remaining at the end of the performance.
		/// </summary>
		public int MoneyPerTime = 1;

		/// <summary>
		/// The amount of snaps required for this performance.
		/// </summary>
		public int SnapsGoal = 200;

		/// <summary>
		/// The amount of snaps lost for a flub.
		/// </summary>
		public int FlubPenalty = 0;

		/// <summary>
		/// The amount of snaps earned per character.
		/// </summary>
		public int BaseSnaps = 1;

		/// <summary>
		/// The current amount of snaps earned during this performance.
		/// </summary>
		public int SnapsCount = 0;

		/// <summary>
		/// The amount of time in seconds for waiting during stanza breaks.
		/// </summary>
		public float BreakDuration = 2f;

		#endregion // Performance Data
	}
}