namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Pink Lemonade consumable.
	/// </summary>
	public class PinkLemonade : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.POPULAR,
				Count = 3 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}