using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Flow State perk.
	/// </summary>
	public class FlowState : Perk
	{
		#region Class Constructors

		public FlowState ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Data

		private int linesWithoutFlubs = 0;

		#endregion // Perk Data

		#region Perk Override Functions

		public override float OnTimeAllowance ( float current )
		{
			return -60f;
		}

		public override void OnInitPerformance ( PerformanceModel model )
		{
			// Set line count
			linesWithoutFlubs = 0;
		}

		public override void OnFlub ( PerformanceModel model )
		{
			// Reset line count
			linesWithoutFlubs = -1;
		}

		public override StatusEffects.StatusEffectModel OnLineComplete ( )
		{
			// Increment count
			linesWithoutFlubs++;

			// Check for 3 completed lines
			if ( linesWithoutFlubs >= 3 )
			{
				// Reset count
				linesWithoutFlubs = 0;

				// Apply Focused
				return new StatusEffects.StatusEffectModel
				{
					Type = Enums.StatusEffectType.FOCUSED,
					Count = 1
				};
			}

			// Return no additional snaps
			return base.OnLineComplete ( );
		}

		#endregion // Perk Override Functions
	}
}