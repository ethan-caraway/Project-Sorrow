namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Salt & Vinegar Chips consumable.
	/// </summary>
	public class SaltAndVinegarChips : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Applause = 100 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}