using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{

	/// <summary>
	/// This class controls the confidence slider in the HUD.
	/// </summary>
	public class ConfidenceHUD : MonoBehaviour
	{
		#region Confidence Data Constants

		private const float ANIMATION_DURATION = 0.3f;
		private const float DELAY_DURATION = 0.2f;

		#endregion // Confidence Data Constants

		#region UI Elements

		[SerializeField]
		private Slider arroganceSlider;

		[SerializeField]
		private Slider effectSlider;

		[SerializeField]
		private Image effectFill;

		[SerializeField]
		private Slider confidenceSlider;

		[SerializeField]
		private TMP_Text confidenceCountText;

		[SerializeField]
		private TMP_Text arroganceCountText;

		#endregion // UI Elements

		#region Confidence Data

		[SerializeField]
		private Color32 noHighlightColor;

		[SerializeField]
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		[SerializeField]
		private Color32 increaseEffectColor;

		[SerializeField]
		private Color32 decreaseEffectColor;

		#endregion // Confidence Data

		#region Public Functions

		/// <summary>
		/// Sets up the confidence display.
		/// </summary>
		/// <param name="confidence"> The maximum amount of confidence. </param>
		/// <param name="arrogance"> The maximum amount of arrogance. </param>
		public void SetConfidence ( int confidence, int arrogance )
		{
			// Reset effect
			effectSlider.value = 0;

			// Set arrogance
			arroganceSlider.value = arrogance > 0 ? 1 : 0;

			// Set confidence - confidence will only be partially full if arrogance is above 0
			confidenceSlider.value = arrogance > 0 ? (float)confidence / (float)( confidence + arrogance ) : 1;

			// Display confidence
			confidenceCountText.text = $"{confidence}/{confidence}";
			arroganceCountText.text = arrogance > 0 ? $"+{arrogance}" : string.Empty;

			// Check for increased confidence
			if ( confidence > GameManager.Difficulty.MaxConfidence )
			{
				// Set positive highlight
				confidenceCountText.color = positiveHighlightColor;
			}
			// Check for decreased confidence
			else if ( confidence < GameManager.Difficulty.MaxConfidence )
			{
				// Set negative highlight
				confidenceCountText.color = negativeHighlightColor;
			}
			else
			{
				// Set no highlight
				confidenceCountText.color = noHighlightColor;
			}
		}

		/// <summary>
		/// Updates and animates the display when gaining confidence.
		/// </summary>
		/// <param name="totalConfidence"> The maximum amount of confidence. </param>
		/// <param name="currentConfidence"> The current amount of confidence. </param>
		/// <param name="currentArrogance"> The current amount of arrogance. </param>
		public void GainConfidence ( int totalConfidence, int currentConfidence, int currentArrogance )
		{
			// Get total scale of slider
			int scale = totalConfidence;
			if ( currentArrogance > 0 && currentConfidence + currentArrogance > totalConfidence )
			{
				scale = currentConfidence + currentArrogance;
			}

			// Get percentage of confindence
			float percentage = (float)currentConfidence / (float)scale;

			// Display amounts
			confidenceCountText.text = $"{currentConfidence}/{totalConfidence}";
			arroganceCountText.text = currentArrogance > 0 ? $"+{currentArrogance}" : string.Empty;

			// Set arrogance
			arroganceSlider.value = 0;
			if ( currentArrogance > 0 )
			{
				arroganceSlider.value = (float)( currentConfidence + currentArrogance ) / (float)scale;
			}

			// Check for over max
			if ( percentage > 1f )
			{
				confidenceSlider.value = 0.95f;
			}

			// Set effect color
			effectFill.color = increaseEffectColor;

			// Set effect
			effectSlider.value = percentage;

			// Animate confidence
			confidenceSlider.DOValue ( percentage, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );
		}

		/// <summary>
		/// Updates and animates the display when losing confidence.
		/// </summary>
		/// <param name="totalConfidence"> The maximum amount of confidence. </param>
		/// <param name="currentConfidence"> The current amount of confidence. </param>
		/// <param name="currentArrogance"> The current amount of arrogance. </param>
		public void LoseConfidence ( int totalConfidence, int currentConfidence, int currentArrogance )
		{
			// Check for underflow
			if ( currentConfidence < 0 )
			{
				currentConfidence = 0;
			}

			// Hide arrogance
			arroganceSlider.value = 0;

			// Get total scale of slider
			int scale = totalConfidence;
			if ( currentArrogance > 0 && currentConfidence + currentArrogance > totalConfidence )
			{
				scale = currentConfidence + currentArrogance;
			}

			// Get percentage of confindence
			float percentage = (float)currentConfidence / (float)scale;

			// Display amounts
			confidenceCountText.text = $"{currentConfidence}/{totalConfidence}";
			arroganceCountText.text = currentArrogance > 0 ? $"+{currentArrogance}" : string.Empty;

			// Set effect color
			effectFill.color = decreaseEffectColor;

			// Check for over max
			if ( percentage >= 1f )
			{
				// Set confidence
				effectSlider.value = 1;
				confidenceSlider.value = 0.95f;

				// Animate effect
				confidenceSlider.DOValue ( 1f, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );
			}
			else
			{
				// Set confidence
				effectSlider.value = confidenceSlider.value;
				confidenceSlider.value = percentage;

				// Set arrogance
				if ( currentArrogance > 0 )
				{
					arroganceSlider.value = effectSlider.value;
				}

				// Animate effect
				effectSlider.DOValue ( percentage, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );
				if ( currentArrogance > 0 )
				{
					arroganceSlider.DOValue ( (float)( currentConfidence + currentArrogance ) / (float)scale, ANIMATION_DURATION ).SetEase ( Ease.InQuad );
				}
			}
		}

		/// <summary>
		/// Updates and animates the display when losing arrogance.
		/// </summary>
		/// <param name="totalConfidence"> The maximum amount of confidence. </param>
		/// <param name="currentConfidence"> The current amount of confidence. </param>
		/// <param name="currentArrogance"> The current amount of arrogance. </param>
		public void LoseArrogance ( int totalConfidence, int currentConfidence, int currentArrogance )
		{
			// Get total scale of slider
			int scale = totalConfidence;
			if ( currentConfidence + currentArrogance > totalConfidence )
			{
				scale = currentConfidence + currentArrogance;
			}

			// Get percentage of confindence
			float confidencePercentage = (float)currentConfidence / (float)scale;

			// Display amounts
			confidenceCountText.text = $"{currentConfidence}/{totalConfidence}";
			arroganceCountText.text = currentArrogance > 0 ? $"+{currentArrogance}" : string.Empty;

			// Set effect color
			effectFill.color = decreaseEffectColor;

			// Check for over max
			if ( currentConfidence + currentArrogance >= totalConfidence )
			{
				// Set arrogance
				arroganceSlider.value = 1f;

				// Set effect
				effectSlider.value = confidencePercentage;

				// Animate confidence
				confidenceSlider.DOValue ( confidencePercentage, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );
			}
			else
			{
				// Get percentage of arrogance
				float arrogancePercentage = (float)( currentConfidence + currentArrogance ) / (float)scale;

				// Set confidence
				confidenceSlider.value = confidencePercentage;

				// Set effect
				effectSlider.value = (float)( currentConfidence + 1 ) / (float)scale;

				// Animate arrogance
				arroganceSlider.DOValue ( arrogancePercentage, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );

				// Animate effect
				effectSlider.DOValue ( confidencePercentage, ANIMATION_DURATION ).SetEase ( Ease.InQuad ).SetDelay ( DELAY_DURATION );
			}
		}

		#endregion // Public Functions
	}
}