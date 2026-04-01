using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Dual Performance perk.
	/// </summary>
	public class DualPerformance : Perk
	{
		#region Class Constructors

		public DualPerformance ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override void OnFlub ( PerformanceModel model )
		{
			// Check for The Futurist judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetFuturistId ( ) )
			{
				return;
			}

			// Decrement confidence
			model.ConfidenceRemaining--;
		}

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier )
		{
			// Return additional snap
			return 1;
		}

		#endregion // Perk Override Functions
	}
}