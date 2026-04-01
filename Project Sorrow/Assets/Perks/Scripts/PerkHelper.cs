namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class contains functions for creating perks.
	/// </summary>
	public static class PerkHelper
	{
		#region Perk Data Constants

		private const int TENURE_ID = 1;
		private const int MALE_PRIVILEGE_ID = 2;
		private const int PUBLISHING_DEAL_ID = 3;
		private const int WOMENS_STUDIES_DEGREE_ID = 4;
		private const int TRUST_FUND_ID = 5;
		private const int ART_GRANT_ID = 6;
		private const int ANTIMATERIALIST_ZINE_ID = 7;
		private const int DEEP_POCKETS_ID = 8;
		private const int DUAL_PERFORMANCE_ID = 9;
		private const int MC_PATCH_ID = 10;
		private const int ACADEMIC_ACHIEVEMENT_ID = 11;
		private const int FLOW_STATE_ID = 12;

		#endregion // Perk Data Constants

		#region Public Functions

		/// <summary>
		/// Gets an instance of a perk for a given ID.
		/// </summary>
		/// <param name="id"> The ID of the perk. </param>
		/// <returns> The instance of the perk. </returns>
		public static Perk GetPerk ( int id )
		{
			// Check ID
			switch ( id )
			{
				case TENURE_ID:
					return new Tenure ( id );

				case MALE_PRIVILEGE_ID:
					return new MalePrivilege ( id );

				case PUBLISHING_DEAL_ID:
					return new PublishingDeal ( id );

				case WOMENS_STUDIES_DEGREE_ID:
					return new WomensStudiesDegree ( id );

				case TRUST_FUND_ID:
					return new TrustFund ( id );

				case ART_GRANT_ID:
					return new ArtGrant ( id );

				case ANTIMATERIALIST_ZINE_ID:
					return new AntimaterialistZine ( id );

				case DEEP_POCKETS_ID:
					return new DeepPockets ( id );

				case DUAL_PERFORMANCE_ID:
					return new DualPerformance ( id );

				case MC_PATCH_ID:
					return new MCPatch ( id );

				case ACADEMIC_ACHIEVEMENT_ID:
					return new AcademicAchievement ( id );

				case FLOW_STATE_ID:
					return new FlowState ( id );
			}

			// Return that no perk was found
			return null;
		}

		#endregion // Public Functions
	}
}