namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Chocolate Candy consumable.
	/// </summary>
	public class ChocolateCandy : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.EXCITED,
				Count = 2 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}