namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the White Wine consumable.
	/// </summary>
	public class WhiteWine : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Commission = 3 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}