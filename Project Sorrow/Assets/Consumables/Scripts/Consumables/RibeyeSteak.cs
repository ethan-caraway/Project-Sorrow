namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Ribeye Steak consumable.
	/// </summary>
	public class RibeyeSteak : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.GREEDY,
				Count = 8 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}