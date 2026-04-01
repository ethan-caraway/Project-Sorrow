using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the First Draft item.
	/// </summary>
	public class FirstDraft : Item
	{
		#region Class Constructors

		public FirstDraft ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private int modifierCount = 0;

		#endregion // Item Data

		#region Item Override Functions

		public override void OnInitPerformance ( PerformanceModel model )
		{
			// Reset count
			modifierCount = 0;
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for modified word
			if ( modifier != Enums.WordModifierType.NONE )
			{
				modifierCount++;
			}

			// Return no additional applause
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override ApplauseModel OnApplause ( PerformanceModel model, int total )
		{
			// Check the amount of modified words in the performance
			if ( modifierCount >= 12 )
			{
				// Return the multiplied applause
				return new ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = total * 2
				};
			}
			else
			{
				// Return no applause
				return base.OnApplause ( model, total );
			}
		}

		#endregion // Item Override Functions
	}
}