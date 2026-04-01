namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Milk Coffee consumable.
	/// </summary>
	public class MilkCoffee : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				TimeAllowance = 40f * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}