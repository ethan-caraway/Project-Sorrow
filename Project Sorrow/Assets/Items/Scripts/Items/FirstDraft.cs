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

		public override void OnInitPerformance ( Performance.PerformanceModel model )
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

				// Highlight count
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SERIOUS_GREY,
						SplashText = modifierCount >= 12 ? $"<color=#A1740E>{modifierCount}</color>" : $"{modifierCount}"
					}
				};
			}

			// Return no additional applause
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check the amount of modified words in the performance
			if ( modifierCount >= 12 )
			{
				// Return the multiplied applause
				return new Performance.ApplauseModel
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