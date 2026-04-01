namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Tenderloin Steak consumable.
	/// </summary>
	public class TenderloinSteak : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.GREEDY,
				Count = 12 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}