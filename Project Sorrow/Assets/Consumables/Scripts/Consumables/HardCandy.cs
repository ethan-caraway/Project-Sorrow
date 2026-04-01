namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Hard Candy consumable.
	/// </summary>
	public class HardCandy : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.EXCITED,
				Count = 4 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}