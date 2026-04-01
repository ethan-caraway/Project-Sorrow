using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Signed First Edition item.
	/// </summary>
	public class SignedFirstEdition : Item
	{
		#region Class Constructors

		public SignedFirstEdition ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Check for permanent draft poem and chance of upgrade
			if ( GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].Level > 0 && Random.Range ( 0f, 1f ) <= 0.25f )
			{
				// Upgrade current poem
				GameManager.Run.UpgradePermanentDraftPoem ( GameManager.Run.CurrentRound.Poems [ GameManager.Run.Performance ].ID );
			}
		}

		#endregion // Item Override Functions
	}
}