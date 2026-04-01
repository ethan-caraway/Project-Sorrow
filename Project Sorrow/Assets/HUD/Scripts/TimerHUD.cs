using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the timer elements in the HUD.
	/// </summary>
	public class TimerHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text timerText;

		[SerializeField]
		private Slider leftSlider;

		[SerializeField]
		private Slider rightSlider;

		#endregion // UI Elements

		#region Timer Data

		[SerializeField]
		private Color32 noHighlightColor;

		[SerializeField]
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		#endregion // Timer Data

		#region Public Functions

		/// <summary>
		/// Updates the display of the timer.
		/// </summary>
		/// <param name="total"> The total amount of time in seconds for the timer. </param>
		/// <param name="current"> The remaining amount of time in seconds for the timer. </param>
		public void UpdateTimer ( float total, float current )
		{
			// Display time remaining
			timerText.text = Utils.FormatTime ( current );

			// Get percentage of remaining time
			float percentage = current / total;

			// Update sliders
			leftSlider.value = percentage;
			rightSlider.value = percentage;

			// Check for increased time
			if ( total > GameManager.Difficulty.TimeAllowance )
			{
				// Set positive highlight
				timerText.color = positiveHighlightColor;			
			}
			// Check for reduced time
			else if ( total < GameManager.Difficulty.TimeAllowance )
			{
				// Set negative highlight
				timerText.color = negativeHighlightColor;
			}
			else
			{
				// Set no highlight
				timerText.color = noHighlightColor;
			}
		}

		#endregion // Public Functions
	}
}