namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Bank Loan consumable.
	/// </summary>
	public class BankLoan : Consumable
	{
		#region Consumable Override Functions

		public override int GetLoanMoney ( )
		{
			return 20;
		}

		public override int GetLoanDebt ( )
		{
			return -30;
		}

		#endregion // Consumable Override Functions
	}
}