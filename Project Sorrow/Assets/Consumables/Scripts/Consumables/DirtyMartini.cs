namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Dirty Martini consumable.
	/// </summary>
	public class DirtyMartini : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.DRAMATIC,
				Count = 6 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}