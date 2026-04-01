namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Craft Beer consumable.
	/// </summary>
	public class CraftBeer : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Arrogance = 5 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}