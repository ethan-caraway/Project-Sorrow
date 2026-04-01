namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Personal Loan consumable.
	/// </summary>
	public class PersonalLoan : Consumable
	{
		#region Consumable Override Functions

		public override int GetLoanMoney ( )
		{
			return 30;
		}

		public override int GetLoanDebt ( )
		{
			return -30;
		}

		#endregion // Consumable Override Functions
	}
}