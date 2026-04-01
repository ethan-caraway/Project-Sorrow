namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Credit Card item.
	/// </summary>
	public class CreditCard : Item
	{
		#region Class Constructors

		public CreditCard ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnMinBudget ( int current )
		{
			// Return reduced minimum budget
			return -25;
		}

		public override bool OnPurchase ( int money, int price )
		{
			return money - price < 0;
		}

		#endregion // Item Override Functions
	}
}