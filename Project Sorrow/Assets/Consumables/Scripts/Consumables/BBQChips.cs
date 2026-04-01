namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the BBQ Chips consumable.
	/// </summary>
	public class BBQChips : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Applause = 250 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}