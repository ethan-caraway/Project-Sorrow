namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Male Privilege perk.
	/// </summary>
	public class MalePrivilege : Perk
	{
		#region Class Constructors

		public MalePrivilege ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override int OnMaxArrogance ( int current )
		{
			return 2;
		}

		#endregion // Perk Override Functions
	}
}