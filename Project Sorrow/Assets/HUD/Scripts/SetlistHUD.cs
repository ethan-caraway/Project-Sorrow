using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls displaying the current setlist in the run info.
	/// </summary>
	public class SetlistHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject setlistContainer;

		[SerializeField]
		private Setlist.PerformanceDisplay [ ] performanceDisplays;

		[SerializeField]
		private Image judgeIcon;

		[SerializeField]
		private TMP_Text judgeText;

		[SerializeField]
		private TagInfoDisplay judgeTagDisplay;

		[SerializeField]
		private GameObject promptText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Initializes the setlist HUD.
		/// </summary>
		/// <param name="isDrafting"> Whether or not a setlist is currently being drafted. </param>
		public void Init ( bool isDrafting )
		{
			// Check for setlist
			if ( isDrafting || GameManager.Run.CurrentRound.Poems [ 0 ] == null || GameManager.Run.CurrentRound.Poems [ 0 ].ID == 0 )
			{
				// Display prompt
				setlistContainer.SetActive ( false );
				promptText.SetActive ( true );
			}
			else
			{
				// Display setlist
				setlistContainer.SetActive ( true );
				promptText.SetActive ( false );
				SetupSetlist ( );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the setlist.
		/// </summary>
		private void SetupSetlist ( )
		{
			// Display performances
			for ( int i = 0; i < performanceDisplays.Length; i++ )
			{
				// Get snaps requirement
				int snaps = GameManager.Difficulty.GetSnapsRequirement ( GameManager.Run.Round, i );

				// Check for Literary Critic judge
				if ( i == performanceDisplays.Length - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetLiteraryCriticId ( ) )
				{
					// Increase snaps requirement
					snaps = Judges.JudgeHelper.GetLiteraryCriticSnapsGoal ( GameManager.Difficulty.Rounds [ GameManager.Run.Round ] );
				}

				// Display performance info
				performanceDisplays [ i ].SetPerformance (
					GameManager.Run.CurrentRound.Poems [ i ],
					snaps,
					GameManager.Run.Performance );
			}

			// Get judge data
			Judges.JudgeScriptableObject judge = Judges.JudgeUtility.GetJudge ( GameManager.Run.CurrentRound.JudgeID );

			// Display judge
			judgeIcon.sprite = judge.Icon;
			judgeText.text = $"<b>{judge.Title}</b>: {judge.Description}";

			// Display judge tag
			if ( judge.HasTag )
			{
				judgeTagDisplay.gameObject.SetActive ( true );
				judgeTagDisplay.SetTag ( Tags.TagUtility.GetTag ( judge.Tag ) );
			}
			else
			{
				judgeTagDisplay.gameObject.SetActive ( false );
			}
		}

		#endregion // Private Functions
	}
}