using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Setlist
{
	/// <summary>
	/// This class controls the display of the performance information in a setlist.
	/// </summary>
	public class PerformanceDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Button draftButton;

		[SerializeField]
		private GameObject poemContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text authorText;

		[SerializeField]
		private TMP_Text levelText;

		[SerializeField]
		private GameObject [ ] ratingIcons;

		[SerializeField]
		private GameObject slotContainer;

		[SerializeField]
		private Button promptButton;

		[SerializeField]
		private TMP_Text promptText;

		#endregion // UI Elements

		#region Performance Data

		[SerializeField]
		private int performance;

		#endregion // Performance Data

		#region Public Functions

		/// <summary>
		/// Displays the information for this performance to be drafted into.
		/// </summary>
		/// <param name="snaps"> The snaps requirement for the performance. </param>
		public void SetDraft ( int snaps )
		{
			// Hide poem
			poemContainer.SetActive ( false );

			// Display draft slot
			slotContainer.SetActive ( true );

			// Display button
			promptButton.gameObject.SetActive ( false );
			promptText.text = $"Earn\n{snaps:N0}";
		}

		/// <summary>
		/// Displays the information for a poem.
		/// </summary>
		/// <param name="model"> The data for the poem. </param>
		public void SetPoem ( Poems.PoemModel model )
		{
			// Checck for data
			if ( model != null && model.ID != 0 )
			{
				// Get poem data
				Poems.PoemScriptableObject poem = Poems.PoemUtility.GetPoem ( model.ID );

				// Display poem
				poemContainer.SetActive ( true );
				slotContainer.SetActive ( false );
				titleText.text = poem.Title;
				authorText.text = $"By {poem.Author}";
				levelText.gameObject.SetActive ( model.Level > 0 );
				if ( model.Level > 0 )
				{
					levelText.text = $"Lv. {Utils.ToRomanNumeral ( model.Level )}";
				}
				for ( int i = 0; i < ratingIcons.Length; i++ )
				{
					ratingIcons [ i ].SetActive ( i < poem.Rating );
				}
			}
			else
			{
				// Display draft slot
				poemContainer.SetActive ( false );
				slotContainer.SetActive ( true );
			}
		}

		/// <summary>
		/// Displays the information for this performance.
		/// </summary>
		/// <param name="model"> The data for the poem. </param>
		/// <param name="snaps"> The snaps requirement for the performance. </param>
		/// <param name="currentPerformance"> The current performance in the setlist. </param>
		public void SetPerformance ( Poems.PoemModel model, int snaps, int currentPerformance )
		{
			// Display the poem
			SetPoem ( model );

			// Disable draft button
			if ( draftButton != null )
			{
				draftButton.interactable = false;
			}

			// Display button
			promptButton.gameObject.SetActive ( performance <= currentPerformance ); // Hide the button for future performances
			promptButton.interactable = performance == currentPerformance; // Only the current performance can be clicked
			promptText.text = performance < currentPerformance ? "Completed" : $"Earn\n{snaps:N0}"; // Display if the performance is completed or display the snap requirement
		}

		#endregion // Public Functions
	}
}