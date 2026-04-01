namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Publishing Deal perk.
	/// </summary>
	public class PublishingDeal : Perk
	{
		#region Class Constructors

		public PublishingDeal ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override float OnTimeAllowance ( float current )
		{
			return 30f;
		}

		#endregion // Perk Override Functions
	}
}