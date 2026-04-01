namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Light Beer consumable.
	/// </summary>
	public class LightBeer : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Arrogance = 3 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}