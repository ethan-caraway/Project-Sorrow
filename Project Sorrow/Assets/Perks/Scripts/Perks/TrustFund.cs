using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Trust Fund perk.
	/// </summary>
	public class TrustFund : Perk
	{
		#region Class Constructors

		public TrustFund ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override void OnStartRun ( )
		{
			// Start with money
			GameManager.Run.Money = 15;
		}

		#endregion // Perk Override Functions
	}
}