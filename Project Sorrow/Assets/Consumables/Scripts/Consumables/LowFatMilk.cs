namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Low-Fat Milk consumable.
	/// </summary>
	public class LowFatMilk : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.STUBBORN,
				Count = 8 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}