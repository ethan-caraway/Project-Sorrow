namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Tonic Water consumable.
	/// </summary>
	public class TonicWater : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Confidence = 5 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}