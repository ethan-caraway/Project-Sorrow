using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Balloons item.
	/// </summary>
	public class Balloons : Item
	{
		#region Class Constructors

		public Balloons ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get 5% of the performance goal
			int target = (int)( GameManager.Difficulty.GetSnapsRequirement ( GameManager.Run.Round, GameManager.Run.Performance ) * 0.05f );

			// Check if target is met
			if ( total >= target )
			{
				// Apply Excited
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.EXCITED_CYAN,
						SplashText = "Excited"
					},
					StatusEffect = new StatusEffects.StatusEffectModel
					{
						Type = Enums.StatusEffectType.EXCITED,
						Count = 2
					}
				};
			}

			// Return that the item was not triggered
			return base.OnLineComplete ( total );
		}

		#endregion // Item Override Functions
	}
}