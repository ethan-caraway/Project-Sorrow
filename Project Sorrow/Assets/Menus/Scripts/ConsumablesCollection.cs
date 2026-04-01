using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the consumables tab in the collections menu.
	/// </summary>
	public class ConsumablesCollection : MonoBehaviour
	{
		#region Consumable Data Constants

		private const int TOTAL_PAGES = 3;

		#endregion // Consumable Data Constants

		#region UI Elements

		[SerializeField]
		private ConsumablePortrait [ ] consumablePortraits;

		[SerializeField]
		private TMP_Text pageText;

		[SerializeField]
		private Button prevButton;

		[SerializeField]
		private Button nextButton;

		[SerializeField]
		private HUD.ConsumableDisplay consumableDisplay;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text ownStatText;

		[SerializeField]
		private TMP_Text consumeStatText;

		[SerializeField]
		private GameObject undiscoveredContainer;

		#endregion // UI Elements

		#region Consumable Data

		[SerializeField]
		private Color undiscoveredColor;

		private int portraitIndex;
		private int pageIndex;

		#endregion // Consumable Data

		#region Public Functions

		/// <summary>
		/// Initializes the consumable collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display the first page of consumables
			DisplayPage ( 0 );
		}

		/// <summary>
		/// Selects a given consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable portrait. </param>
		public void SelectConsumable ( int index )
		{
			// Store consumable
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < consumablePortraits.Length; i++ )
			{
				consumablePortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected consumable
			DisplayConsumable ( index );
		}

		/// <summary>
		/// Previews a given consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable portrait. </param>
		public void PreviewConsumable ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				consumablePortraits [ index ].ToggleHover ( true );

				// Display previewed consumable
				DisplayConsumable ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				consumablePortraits [ index ].ToggleHover ( false );

				// Display selected consumable
				DisplayConsumable ( portraitIndex );
			}
		}

		/// <summary>
		/// Display the previous page of consumables.
		/// </summary>
		public void PrevPage ( )
		{
			// Load page
			DisplayPage ( pageIndex - 1 );
		}

		/// <summary>
		/// Display the next page of consumables.
		/// </summary>
		public void NextPage ( )
		{
			// Load page
			DisplayPage ( pageIndex + 1 );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the consumables for a given page.
		/// </summary>
		/// <param name="index"> The index of the page. </param>
		private void DisplayPage ( int index )
		{
			// Store page
			pageIndex = index;

			// Display each consumable
			for ( int i = 0; i < consumablePortraits.Length; i++ )
			{
				// Display item
				consumablePortraits [ i ].SetConsumable ( i + 1 + ( index * consumablePortraits.Length ) );
			}

			// Display page
			pageText.text = $"{index + 1}/{TOTAL_PAGES}";
			prevButton.interactable = index > 0;
			nextButton.interactable = index + 1 < TOTAL_PAGES;

			// Select first consumable on the page
			SelectConsumable ( 0 );
		}

		/// <summary>
		/// Displays the data for given consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable portrait. </param>
		private void DisplayConsumable ( int index )
		{
			// Get consumable
			Consumables.ConsumableScriptableObject consumable = consumablePortraits [ index ].Consumable;

			// Get stats
			Progression.ConsumableStatsModel stats = Progression.ProgressManager.Progress.GetConsumableStats ( consumable.ID );

			// Display consumable
			consumableDisplay.SetConsumable ( consumable, 1, stats.IsDiscovered );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display consumable info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = consumable.Title;
				descriptionText.text = consumable.Description;

				// Display stats
				ownStatText.text = stats.Owns.ToString ( );
				consumeStatText.text = stats.Consumed.ToString ( );
			}
			else
			{
				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		#endregion // Private Functions
	}
}