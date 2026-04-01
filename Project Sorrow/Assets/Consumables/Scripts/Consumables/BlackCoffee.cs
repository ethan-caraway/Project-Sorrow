namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Black Coffee consumable.
	/// </summary>
	public class BlackCoffee : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				TimeAllowance = 120f * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}