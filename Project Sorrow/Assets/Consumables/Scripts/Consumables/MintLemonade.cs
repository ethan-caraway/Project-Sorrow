namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Mint Lemonade consumable.
	/// </summary>
	public class MintLemonade : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return new StatusEffects.StatusEffectModel
			{
				Type = Enums.StatusEffectType.POPULAR,
				Count = 6 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}