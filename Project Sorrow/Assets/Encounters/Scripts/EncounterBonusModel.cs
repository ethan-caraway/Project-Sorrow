namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class stores the bonuses accrued from encounters during a run.
	/// </summary>
	[System.Serializable]
	public class EncounterBonusModel
	{
		#region Encounter Data

		/// <summary>
		/// The confidence bonus from encounters.
		/// </summary>
		public int MaxConfidence = 0;

		/// <summary>
		/// The arrogance bonus from encounters.
		/// </summary>
		public int MaxArrogance = 0;

		/// <summary>
		/// The time allowance bonus from encounters.
		/// </summary>
		public float TimeAllowance = 0f;

		/// <summary>
		/// The reputation bonus from encounters.
		/// </summary>
		public int Reputation = 0;

		/// <summary>
		/// The commission bonus from encounters.
		/// </summary>
		public int Commission = 0;

		/// <summary>
		/// The interest cap bonus from encounters.
		/// </summary>
		public int InterestCap = 0;

		#endregion // Encounter Data

		#region Public Functions

		/// <summary>
		/// Adds new bonuses to the run. 
		/// </summary>
		/// <param name="addModel"> The data for bonuses gained. </param>
		public void Add ( EncounterBonusModel addModel )
		{
			// Check for data
			if ( addModel == null )
			{
				return;
			}

			// Add bonuses
			MaxConfidence += addModel.MaxConfidence;
			MaxArrogance += addModel.MaxArrogance;
			TimeAllowance += addModel.TimeAllowance;
			Reputation += addModel.Reputation;
			Commission += addModel.Commission;
			InterestCap += addModel.InterestCap;
		}

		#endregion // Public Functions
	}
}