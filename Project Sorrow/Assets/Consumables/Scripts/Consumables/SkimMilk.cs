namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Skim Milk consumable.
	/// </summary>
	public class SkimMilk : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.STUBBORN,
				Count = 4 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}