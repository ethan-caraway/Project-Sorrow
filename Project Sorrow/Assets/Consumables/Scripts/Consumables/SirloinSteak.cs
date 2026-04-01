namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Sirloin Steak consumable.
	/// </summary>
	public class SirloinSteak : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.GREEDY,
				Count = 4 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}