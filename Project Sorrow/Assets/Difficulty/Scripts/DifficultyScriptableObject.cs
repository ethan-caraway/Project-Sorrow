using UnityEngine;

namespace FlightPaper.ProjectSorrow.Difficulty
{
	/// <summary>
	/// This class is a scriptable object that stores the data for the difficulty of a run.
	/// </summary>
	[CreateAssetMenu ( fileName = "Difficulty", menuName = "Scriptable Objects/Difficulty" )]
	public class DifficultyScriptableObject : ScriptableObject
	{
		#region Difficulty Data Constants

		/// <summary>
		/// The total number of performances in a round.
		/// </summary>
		public const int PERFORMANCES_PER_ROUND = 4;

		/// <summary>
		/// The total number of rounds in a round.
		/// </summary>
		public const int ROUNDS_PER_RUN = 5;

		private const int SNAP_MULTIPLIER_0 = 0;
		private const int SNAP_MULTIPLIER_1 = 1;
		private const int SNAP_MULTIPLIER_2 = 3;
		private const int SNAP_MULTIPLIER_3 = 7;

		private const float SECONDS_PER_MINUTE = 60f;

		#endregion // Difficulty Data Constants

		#region Difficulty Data

		[Tooltip ( "The ID of the difficulty" )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the difficulty." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the difficulty." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The description of the penalties of the difficulty." )]
		[SerializeField]
		private string [ ] penalties;

		[Tooltip ( "The unlock criteria for the difficulty." )]
		[TextArea]
		[SerializeField]
		private string unlockCriteria;

		[Tooltip ( "The data for each round in the run." )]
		[SerializeField]
		private RoundDifficultyModel [ ] rounds;

		[Tooltip ( "The amount of commission earned for each performance in a round." )]
		[SerializeField]
		private int [ ] commission;

		[Tooltip ( "The maximum amount of confidence for each performance in the run." )]
		[SerializeField]
		private int maxConfidence;

		[Tooltip ( "The total amount of time in minutes allowed for each performance in the run." )]
		[SerializeField]
		private float timeAllowanceMinutes;

		[Tooltip ( "The total amount of item slots available." )]
		[SerializeField]
		private int maxItems;

		[Tooltip ( "The total amount of consumable slots available." )]
		[SerializeField]
		private int maxConsumables;

		[Tooltip ( "The total amount of snaps lost for a flub." )]
		[SerializeField]
		private int flubPenalty;

		#endregion // Difficulty Data

		#region Public Properties

		/// <summary>
		/// The ID of the difficulty.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the difficulty.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the difficulty.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The description of the penalties of the difficulty.
		/// </summary>
		public string [ ] Penalties
		{
			get
			{
				return penalties;
			}
		}

		/// <summary>
		/// The unlock criteria of the difficulty.
		/// </summary>
		public string UnlockCriteria
		{
			get
			{
				return unlockCriteria;
			}
		}

		/// <summary>
		/// The data for each round in the run.
		/// </summary>
		public RoundDifficultyModel [ ] Rounds
		{
			get
			{
				return rounds;
			}
		}

		/// <summary>
		/// The maximum amount of confidence for each performance in the run.
		/// </summary>
		public int MaxConfidence
		{
			get
			{
				return maxConfidence;
			}
		}

		/// <summary>
		/// The total amount of time in seconds allowed for each performance in the run.
		/// </summary>
		public float TimeAllowance
		{
			get
			{
				return timeAllowanceMinutes * SECONDS_PER_MINUTE;
			}
		}

		/// <summary>
		/// The total amount of items slots available.
		/// </summary>
		public int MaxItems
		{
			get
			{
				return maxItems;
			}
		}

		/// <summary>
		/// The total amount of consumable slots available.
		/// </summary>
		public int MaxConsumables
		{
			get
			{
				return maxConsumables;
			}
		}

		/// <summary>
		/// The total amount of snaps lost for a flub.
		/// </summary>
		public int FlubPenatly
		{
			get
			{
				return flubPenalty;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Gets the requirement amount of snaps for a given performance.
		/// </summary>
		/// <param name="round"> The given round of the run. </param>
		/// <param name="performance"> The given performance of the round. </param>
		/// <returns> The snaps requirement for the performance. </returns>
		public int GetSnapsRequirement ( int round, int performance )
		{
			// Check for valid round
			if ( round < 0 || round >= rounds.Length )
			{
				round = Mathf.Clamp ( round, 0, rounds.Length );
			}

			// Check for valid performance
			if ( performance < 0 || performance >= PERFORMANCES_PER_ROUND )
			{
				performance = Mathf.Clamp ( performance, 0, PERFORMANCES_PER_ROUND );
			}

			// Get the starting amount of snaps for the round
			int snaps = rounds [ round ].StartingSnaps;

			// Increase snaps based on the current performance in the round
			snaps += rounds [ round ].SnapsIncrement * GetSnapsMultiplier ( performance );

			// Return the snaps requirements for the performance
			return snaps;
		}

		/// <summary>
		/// Gets the commission for a given performance.
		/// </summary>
		/// <param name="performance"> The given performance of the round. </param>
		/// <returns> The commission for the performance. </returns>
		public int GetCommission ( int performance )
		{
			// Check for valid performance
			if ( performance < 0 || performance >= PERFORMANCES_PER_ROUND )
			{
				performance = Mathf.Clamp ( performance, 0, PERFORMANCES_PER_ROUND );
			}

			// Return the commission for the performance
			return commission [ performance ];
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Gets the snaps multiplier for the performance of the round.
		/// </summary>
		/// <param name="performance"> The performance of the round. </param>
		/// <returns> The snaps multiplier. </returns>
		private int GetSnapsMultiplier ( int performance )
		{
			// Check performance
			switch ( performance )
			{
				case 0:
					return SNAP_MULTIPLIER_0;

				case 1:
					return SNAP_MULTIPLIER_1;

				case 2:
					return SNAP_MULTIPLIER_2;

				case 3:
					return SNAP_MULTIPLIER_3;

				default:
					return 0;
			}
		}

		#endregion // Private Functions
	}
}