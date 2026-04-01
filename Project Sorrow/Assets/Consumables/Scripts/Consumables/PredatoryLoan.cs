namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Predatory Loan consumable.
	/// </summary>
	public class PredatoryLoan : Consumable
	{
		#region Consumable Override Functions

		public override int GetLoanMoney ( )
		{
			return 10;
		}

		public override int GetLoanDebt ( )
		{
			return -20;
		}

		#endregion // Consumable Override Functions
	}
}