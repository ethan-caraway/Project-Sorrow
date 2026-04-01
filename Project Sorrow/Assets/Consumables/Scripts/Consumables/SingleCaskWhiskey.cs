namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Single Cask Whiskey consumable.
	/// </summary>
	public class SingleCaskWhiskey : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Reputation = 10 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}