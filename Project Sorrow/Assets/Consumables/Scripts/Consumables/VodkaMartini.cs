namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Vodka Martini consumable.
	/// </summary>
	public class VodkaMartini : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.DRAMATIC,
				Count = 9 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}