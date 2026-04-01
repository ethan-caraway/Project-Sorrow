namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Mineral Water consumable.
	/// </summary>
	public class MineralWater : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Confidence = 1 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}