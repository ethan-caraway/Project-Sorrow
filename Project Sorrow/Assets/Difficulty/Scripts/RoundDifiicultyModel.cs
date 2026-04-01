using UnityEngine;

namespace FlightPaper.ProjectSorrow.Difficulty
{
	/// <summary>
	/// This class stores the data for a round of a run.
	/// </summary>
	[System.Serializable]
	public class RoundDifficultyModel
	{
		#region Round Data

		[Tooltip ( "The rating for the poems in a round." )]
		[Range ( 1, 5 )]
		[SerializeField]
		private int rating;

		[Tooltip ( "The starting amount of snaps required for a performance in a round." )]
		[SerializeField]
		private int startingSnaps;

		[Tooltip ( "The amount of snaps that the requirement will increase for a performance in a round." )]
		[SerializeField]
		private int snapsIncrement;

		#endregion // Round Data

		#region Public Properties

		/// <summary>
		/// The rating for the poems in a round.
		/// </summary>
		public int Rating
		{
			get
			{
				return rating;
			}
		}

		/// <summary>
		/// The starting amount of snaps required for a performance in a round.
		/// </summary>
		public int StartingSnaps
		{
			get
			{
				return startingSnaps;
			}
		}

		/// <summary>
		/// The amount of snaps that the requirement will increase for a performance in a round.
		/// </summary>
		public int SnapsIncrement
		{
			get
			{
				return snapsIncrement;
			}
		}

		#endregion // Public Properties
	}
}