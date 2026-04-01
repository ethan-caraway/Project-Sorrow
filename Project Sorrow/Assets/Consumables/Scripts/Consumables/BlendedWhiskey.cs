namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Blended Whiskey consumable.
	/// </summary>
	public class BlendedWhiskey : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Reputation = 2 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}