namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Black Rice consumable.
	/// </summary>
	public class BlackRice : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.SERIOUS,
				Count = 6 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}