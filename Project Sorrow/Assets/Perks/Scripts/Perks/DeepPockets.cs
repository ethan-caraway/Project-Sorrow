namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Deep Pockets perk.
	/// </summary>
	public class DeepPockets : Perk
	{
		#region Class Constructors

		public DeepPockets ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override int OnMaxItems ( )
		{
			// Increase item slots
			return 1;
		}

		public override float OnModifyPrices ( )
		{
			// Increase prices by 50%
			return 1.5f;
		}

		#endregion // Perk Override Functions
	}
}