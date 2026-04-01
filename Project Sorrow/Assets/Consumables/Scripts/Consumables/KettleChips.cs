namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Kettle Chips consumable.
	/// </summary>
	public class KettleChips : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return new Poems.PoemModel
			{
				Applause = 40 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}