namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Red Wine consumable.
	/// </summary>
	public class RedWine : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Commission = 1 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}