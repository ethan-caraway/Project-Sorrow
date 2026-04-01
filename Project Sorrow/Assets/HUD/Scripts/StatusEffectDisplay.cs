using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of a status effect.
	/// </summary>
	public class StatusEffectDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Image icon;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TagInfoDisplay tagDisplay;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Sets the status effect to display.
		/// </summary>
		/// <param name="model"> The data of the status effect to display. </param>
		public void SetStatusEffect ( StatusEffects.StatusEffectModel model )
		{
			// Get status effect
			StatusEffects.StatusEffectScriptableObject statusEffect = StatusEffects.StatusEffectUtility.GetStatusEffect ( model.Type );

			// Get tag data
			Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( statusEffect.Tag );

			// Display icon
			icon.sprite = statusEffect.Icon;

			// Get stack count
			string count = model.Count > 1 ? $" <color=#828282>(x{model.Count})</color>" : string.Empty;

			// Display status effect
			titleText.text = $"{tag.Title.Substring ( "Status Effect:  ".Length )}{count}";

			// Set tag
			tagDisplay.SetTag ( tag );
			tagDisplay.gameObject.SetActive ( false );
		}

		/// <summary>
		/// Display the status effect information.
		/// </summary>
		public void ShowInfo ( )
		{
			// Display tag
			tagDisplay.gameObject.SetActive ( true );
		}

		/// <summary>
		/// Hide the status effect information.
		/// </summary>
		public void HideInfo ( )
		{
			// Hide tag
			tagDisplay.gameObject.SetActive ( false );
		}

		#endregion // Public Functions
	}
}