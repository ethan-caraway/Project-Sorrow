using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the status effect tab in the collections menu.
	/// </summary>
	public class StatusEffectCollection : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private StatusEffectPortrait [ ] statusEffectPortraits;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text applyStatText;

		[SerializeField]
		private TMP_Text stacksStatText;

		[SerializeField]
		private TMP_Text triggerPromptText;

		[SerializeField]
		private TMP_Text triggerStatText;

		[SerializeField]
		private GameObject undiscoveredContainer;

		#endregion // UI Elements

		#region Status Effect Data

		private int portraitIndex;

		#endregion // Status Effect Data

		#region Public Functions

		/// <summary>
		/// Initializes the status effect collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display each status effects
			Enums.StatusEffectType [ ] statusEffects =
			{
				Enums.StatusEffectType.STUBBORN,
				Enums.StatusEffectType.GREEDY,
				Enums.StatusEffectType.DRAMATIC,
				Enums.StatusEffectType.POPULAR,
				Enums.StatusEffectType.EXCITED,
				Enums.StatusEffectType.SERIOUS
			};
			for ( int i = 0; i < statusEffectPortraits.Length; i++ )
			{
				// Display status effect
				statusEffectPortraits [ i ].SetStatusEffect ( statusEffects [ i ] );
			}

			// Display the first status effect
			SelectStatusEffect ( 0 );
		}

		/// <summary>
		/// Selects a given status effect.
		/// </summary>
		/// <param name="index"> The index of the status effect portrait. </param>
		public void SelectStatusEffect ( int index )
		{
			// Store item
			portraitIndex = index;

			// Update portraits
			for ( int i = 0; i < statusEffectPortraits.Length; i++ )
			{
				statusEffectPortraits [ i ].ToggleSelect ( i == index );
			}

			// Display selected status effect
			DisplayStatusEffect ( index );
		}

		/// <summary>
		/// Previews a given status effect.
		/// </summary>
		/// <param name="index"> The index of the status effect portrait. </param>
		public void PreviewStatusEffect ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Highlight portrait
				statusEffectPortraits [ index ].ToggleHover ( true );

				// Display previewed status effect
				DisplayStatusEffect ( index );
			}
		}

		/// <summary>
		/// Ends the preview of a given status effect.
		/// </summary>
		/// <param name="index"> The index of the status effect portrait. </param>
		public void EndPreview ( int index )
		{
			// Check if selected
			if ( index != portraitIndex )
			{
				// Unhighlight portrait
				statusEffectPortraits [ index ].ToggleHover ( false );

				// Display selected status effect
				DisplayStatusEffect ( portraitIndex );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the data for given status effect.
		/// </summary>
		/// <param name="index"> The index of the status effect portrait. </param>
		private void DisplayStatusEffect ( int index )
		{
			// Get status effect
			Enums.StatusEffectType statusEffect = statusEffectPortraits [ index ].StatusEffect;
			StatusEffects.StatusEffectScriptableObject data = StatusEffects.StatusEffectUtility.GetStatusEffect ( statusEffect );
			Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( data.Tag );

			// Get stats
			Progression.StatusEffectStatsModel stats = Progression.ProgressManager.Progress.GetStatusEffectStats ( (int)statusEffect );

			// Check if discovered
			if ( stats.IsDiscovered )
			{
				// Display status effect info
				infoContainer.SetActive ( true );
				undiscoveredContainer.SetActive ( false );
				titleText.text = tag.Title;
				descriptionText.text = tag.Description;

				// Display stats
				applyStatText.text = stats.Applies.ToString ( );
				stacksStatText.text = stats.Stacks.ToString ( );
				triggerPromptText.text = GetTriggerStatPrompt ( statusEffect );
				triggerStatText.text = FormetTriggerStat ( statusEffect, stats.Triggered );
			}
			else
			{
				// Display discovery info
				infoContainer.SetActive ( false );
				undiscoveredContainer.SetActive ( true );
			}
		}

		/// <summary>
		/// Gets the unique prompt for the trigger stat for the status effect.
		/// </summary>
		/// <param name="statusEffect"> The status effect displayed. </param>
		/// <returns> The prompt for the trigger stat. </returns>
		private string GetTriggerStatPrompt ( Enums.StatusEffectType statusEffect )
		{
			// Check modifier
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.STUBBORN:
					return "Arrogance Earned:";

				case Enums.StatusEffectType.GREEDY:
					return "Money Earned:";

				case Enums.StatusEffectType.DRAMATIC:
					return "Snaps Earned:";

				case Enums.StatusEffectType.POPULAR:
					return "Snaps Earned:";

				case Enums.StatusEffectType.EXCITED:
					return "Consumables Earned:";

				case Enums.StatusEffectType.SERIOUS:
					return "Item Retriggers:";
			}

			// Return no prompt
			return string.Empty;
		}

		/// <summary>
		/// Formats the display of the trigger stat value for the status effect.
		/// </summary>
		/// <param name="statusEffect"> The status effect displayed. </param>
		/// <param name="value"> The value of the trigger stat. </param>
		/// <returns> The formated trigger stat. </returns>
		private string FormetTriggerStat ( Enums.StatusEffectType statusEffect, int value )
		{
			// Check modifier
			switch ( statusEffect )
			{
				case Enums.StatusEffectType.STUBBORN:
					return $"<color=purple>{value}</color>";

				case Enums.StatusEffectType.GREEDY:
					return $"<color=green>${value}</color>";
			}

			// Return value by default
			return value.ToString ( );
		}

		#endregion // Private Functions
	}
}