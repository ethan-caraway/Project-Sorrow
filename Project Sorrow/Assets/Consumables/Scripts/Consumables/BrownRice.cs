namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Brown Rice consumable.
	/// </summary>
	public class BrownRice : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.SERIOUS,
				Count = 4 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}