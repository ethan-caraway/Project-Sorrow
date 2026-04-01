namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Sparkling Wine consumable.
	/// </summary>
	public class SparklingWine : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Commission = 6 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}