namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class stores the data for the result of an encounter.
	/// </summary>
	public class ResultModel
	{
		#region Encounter Data

		/// <summary>
		/// The amount of money gains from the encounter.
		/// </summary>
		public int Money = 0;

		/// <summary>
		/// Whether or not the shop should be skipped due to the encounter.
		/// </summary>
		public bool IsShopSkipped = false;

		/// <summary>
		/// The bonuses accrued from the encounter.
		/// </summary>
		public EncounterBonusModel Bonus = new EncounterBonusModel ( );

		#endregion // Encounter Data
	}
}