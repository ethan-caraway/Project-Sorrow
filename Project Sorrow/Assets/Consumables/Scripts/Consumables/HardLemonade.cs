namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Hard Lemonade consumable.
	/// </summary>
	public class HardLemonade : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.POPULAR,
				Count = 9 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}