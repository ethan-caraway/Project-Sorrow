namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the White Rice consumable.
	/// </summary>
	public class WhiteRice : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.SERIOUS,
				Count = 2 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}