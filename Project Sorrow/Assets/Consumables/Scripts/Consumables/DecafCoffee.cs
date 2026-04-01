namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Decaf Coffee consumable.
	/// </summary>
	public class DecafCoffee : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				TimeAllowance = 20f * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}