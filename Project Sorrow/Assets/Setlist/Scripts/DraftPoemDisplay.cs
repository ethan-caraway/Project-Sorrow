using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Setlist
{
	/// <summary>
	/// This class controls the display of poem to be drafted.
	/// </summary>
	public class DraftPoemDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private GameObject inspectButton;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text authorText;

		[SerializeField]
		private TMP_Text levelText;

		[SerializeField]
		private GameObject [ ] ratingImages;

		#endregion // UI Elements

		#region Poem Data

		private Poems.PoemModel model;

		#endregion // Poem Data

		#region Public Functions

		/// <summary>
		/// Sets the poem to be displayed.
		/// </summary>
		/// <param name="poem"> The data for the poem. </param>
		public void SetPoem ( Poems.PoemModel poem )
		{
			// Store the poem data
			model = poem;

			// Check for poem
			if ( poem != null )
			{
				// Display poem info
				DisplayPoem ( Poems.PoemUtility.GetPoem ( poem.ID ), poem.Level );
			}
			else
			{
				// Hide poem
				DisplayPoem ( null, 0 );
			}
		}

		/// <summary>
		/// Toggles whether or not the poem information is displayed.
		/// </summary>
		/// <param name="isDisplayed"> Whether or not the poem information is displayed. </param>
		public void ToggleDisplay ( bool isDisplayed )
		{
			// Check for poem
			if ( isDisplayed && model != null )
			{
				// Display poem info
				DisplayPoem ( Poems.PoemUtility.GetPoem ( model.ID ), model.Level );
			}
			else
			{
				// Hide poem
				DisplayPoem ( null, 0 );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the information for a poem.
		/// </summary>
		/// <param name="poem"> The data for the poem. </param>
		/// <param name="level"> The level of the poem. </param>
		private void DisplayPoem ( Poems.PoemScriptableObject poem, int level )
		{
			// Check for a poem
			if ( poem != null )
			{
				// Display inspect button
				inspectButton.SetActive ( true );

				// Display title
				titleText.gameObject.SetActive ( true );
				titleText.text = poem.Title;

				// Display author
				authorText.gameObject.SetActive ( true );
				authorText.text = $"By {poem.Author}";

				// Display level
				levelText.gameObject.SetActive ( level > 0 );
				if ( level > 0 )
				{
					levelText.text = $"Lv. {Utils.ToRomanNumeral ( level )}";
				}

				// Display rating
				for ( int i = 0; i < ratingImages.Length; i++ )
				{
					ratingImages [ i ].SetActive ( i < poem.Rating );
				}
			}
			else
			{
				// Hide poem
				inspectButton.SetActive ( false );
				titleText.gameObject.SetActive ( false );
				authorText.gameObject.SetActive ( false );
				levelText.gameObject.SetActive ( false );
				for ( int i = 0; i < ratingImages.Length; i++ )
				{
					ratingImages [ i ].SetActive ( false );
				}
			}
		}

		#endregion // Private Functions
	}
}