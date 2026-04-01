using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the snaps displayed in the HUD.
	/// </summary>
	public class SnapsHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text goalText;

		[SerializeField]
		private IntegerCounter snapsCounter;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Initializes the goal and snaps display.
		/// </summary>
		/// <param name="goal"> The total number of snaps required for this poem. </param>
		/// <param name="startingSnaps"> The total number of snaps the player starts with for this poem. </param>
		public void SetGoal ( int goal, int startingSnaps )
		{
			// Display goal
			goalText.text = $"{goal:N0}";

			// Display snaps
			snapsCounter.SetNumber ( startingSnaps, true );
		}

		/// <summary>
		/// Adds a number of snaps to the current total.
		/// </summary>
		/// <param name="snaps"> The number of snaps being added to the total. </param>
		public void AddSnaps ( int snaps )
		{
			// Add snaps to count
			snapsCounter.AddNumber ( snaps, false );
		}

		#endregion // Public Functions
	}
}