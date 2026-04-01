namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Whole Milk consumable.
	/// </summary>
	public class WholeMilk : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.STUBBORN,
				Count = 12 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}