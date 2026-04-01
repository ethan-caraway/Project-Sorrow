using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the modifiers tab in the collections menu.
	/// </summary>
	public class ModifiersCollection : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private ModifierPortrait [ ] modifierPortraits;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text applyStatText;

		[SerializeField]
		private TMP_Text performStatText;

		[SerializeField]
		private TMP_Text triggerPromptText;

		[SerializeField]
		private TMP_Text triggerStatText;

		[SerializeField]
		private GameObject undiscoveredContainer;

		#endregion // UI Elements

		#region Modifier Data

		private int portraitIndex;

		#endregion // Modifier Data

		#region Public Functions

		/// <summary>
		/// Initializes the modifier collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display each modifiers
			Enums.WordModifierType [ ] modifiers =
			{
				Enums.WordModifierType.BOLD,
				Enums.WordModifierType.ITALICS,
				Enums.WordModifierType.STRIKETHROUGH,
				Enums.WordModifierType.UNDERLINE,
				Enums.WordModifierType.CAPS,
				Enums.WordModifierType.SMALL
			};
			for ( int i = 0; i < modifierPortraits.Length; i++ )
			{
				// Display modifier
				modifierPortraits [ i ].SetModifier ( modifiers [ i ] );
			}

			// Display the first modifier
			SelectModifier ( 0 );
		}

		/// <summary>
		/// Selects a given modifier.
		/// </summary>
		/// <param name="index"> The index of the modifier portrait. </param>
		public void SelectModifier ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < modifierPortraits.Length; i++ )
			{
				modifierPortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected modifier
			DisplayModifier ( index );
		}

		/// <summary>
		/// Previews a given modifier.
		/// </summary>
		/// <param name="index"> The index of the modifier portrait. </param>
		public void PreviewModifier ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				modifierPortraits [ index ].ToggleHover ( true );

				// Display previewed modifier
				DisplayModifier ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given modifier.
		/// </summary>
		/// <param name="index"> The index of the modifier portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				modifierPortraits [ index ].ToggleHover ( false );

				// Display selected modifier
				DisplayModifier ( portraitIndex );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the data for given modifier.
		/// </summary>
		/// <param name="index"> The index of the modifier portrait. </param>
		private void DisplayModifier ( int index )
		{
			// Get modifier
			Enums.WordModifierType modifier = modifierPortraits [ index ].Modifier;
			Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( modifier.ToID ( ) );

			// Get stats
			Progression.ModifierStatsModel stats = Progression.ProgressManager.Progress.GetModifierStats ( (int)modifier );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display modifier info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = tag.Title;
				descriptionText.text = tag.Description;

				// Display stats
				applyStatText.text = stats.Applies.ToString ( );
				performStatText.text = stats.Performed.ToString ( );
				triggerPromptText.text = GetTriggerStatPrompt ( modifier );
				triggerStatText.text = FormetTriggerStat ( modifier, stats.Triggered );
			}
			else
			{
				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		/// <summary>
		/// Gets the unique prompt for the trigger stat for the modifier.
		/// </summary>
		/// <param name="modifier"> The modifier displayed. </param>
		/// <returns> The prompt for the trigger stat. </returns>
		private string GetTriggerStatPrompt ( Enums.WordModifierType modifier )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					return "Money Earned:";

				case Enums.WordModifierType.ITALICS:
					return "Time Earned:";

				case Enums.WordModifierType.STRIKETHROUGH:
					return "Flubs Ignored:";

				case Enums.WordModifierType.UNDERLINE:
					return "Confidence Earned:";

				case Enums.WordModifierType.CAPS:
					return "Snaps Earned:";

				case Enums.WordModifierType.SMALL:
					return "Snaps Earned:";
			}

			// Return no prompt
			return string.Empty;
		}

		/// <summary>
		/// Formats the display of the trigger stat value for the modifier.
		/// </summary>
		/// <param name="modifier"> The modifier displayed. </param>
		/// <param name="value"> The value of the trigger stat. </param>
		/// <returns> The formated trigger stat. </returns>
		private string FormetTriggerStat ( Enums.WordModifierType modifier, int value )
		{
			// Check modifier
			switch ( modifier )
			{
				case Enums.WordModifierType.BOLD:
					return $"<color=green>${value}</color>";

				case Enums.WordModifierType.ITALICS:
					return $"<color=#FFE100>{Utils.FormatTime ( value )}</color>";

				case Enums.WordModifierType.STRIKETHROUGH:
					return $"<color=red>{value}</color>";

				case Enums.WordModifierType.UNDERLINE:
					return $"<color=blue>{value}</color>";
			}

			// Return value by default
			return value.ToString ( );
		}

		#endregion // Private Functions
	}
}