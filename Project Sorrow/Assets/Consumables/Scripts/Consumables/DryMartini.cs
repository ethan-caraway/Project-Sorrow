namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Dry Martini consumable.
	/// </summary>
	public class DryMartini : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.DRAMATIC,
				Count = 3 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}