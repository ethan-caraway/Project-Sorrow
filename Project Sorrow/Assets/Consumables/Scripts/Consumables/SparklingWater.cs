namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Sparkling Water consumable.
	/// </summary>
	public class SparklingWater : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Confidence = 3 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}