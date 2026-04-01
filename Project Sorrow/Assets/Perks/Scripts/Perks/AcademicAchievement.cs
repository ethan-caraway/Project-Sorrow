namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Academic Achievement perk.
	/// </summary>
	public class AcademicAchievement : Perk
	{
		#region Class Constructors

		public AcademicAchievement ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override int OnMaxConfidence ( int current )
		{
			return 1;
		}

		#endregion // Perk Override Functions
	}
}