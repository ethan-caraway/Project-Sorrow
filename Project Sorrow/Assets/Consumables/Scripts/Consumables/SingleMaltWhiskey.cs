namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Single Malt Whiskey consumable.
	/// </summary>
	public class SingleMaltWhiskey : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Reputation = 5 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}