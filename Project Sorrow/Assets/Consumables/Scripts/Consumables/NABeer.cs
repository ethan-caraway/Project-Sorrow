namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Non-Alcoholic Beer consumable.
	/// </summary>
	public class NABeer : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Arrogance = 1 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}