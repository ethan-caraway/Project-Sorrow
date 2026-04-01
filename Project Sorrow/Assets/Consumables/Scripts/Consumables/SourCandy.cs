namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Sour Candy consumable.
	/// </summary>
	public class SourCandy : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.EXCITED,
				Count = 6 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}