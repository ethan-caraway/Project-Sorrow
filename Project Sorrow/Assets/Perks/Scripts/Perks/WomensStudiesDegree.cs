namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Women's Studies Degree perk.
	/// </summary>
	public class WomensStudiesDegree : Perk
	{
		#region Class Constructors

		public WomensStudiesDegree ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check if current poem is by a woman
			if ( Poems.PoemUtility.GetPoem ( GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].ID ).IsFemaleAuthor )
			{
				return new Performance.ApplauseModel
				{
					Applause = 150
				};
			}

			// Return no bonus
			return base.OnApplause ( model, total );
		}

		#endregion // Perk Override Functions
	}
}