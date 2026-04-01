using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of an integer that would count up or down.
	/// </summary>
	public class IntegerCounter : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text numberText;

		[SerializeField]
		private TMP_Text addTextPrefab;

		#endregion // UI Elements

		#region Counter Data

		[SerializeField]
		private string prependText;

		[SerializeField]
		private bool isPlusDisplayed = true;

		[SerializeField]
		private Color32 positiveColor;

		[SerializeField]
		private Color32 negativeColor;

		[SerializeField]
		private Color32 addTextPrimaryPositiveColor;

		[SerializeField]
		private Color32 addTextSecondaryPositiveColor;

		[SerializeField]
		private Color32 addTextTertiaryPositiveColor;

		[SerializeField]
		private Color32 addTextNegativeColor;

		private int targetCount = 0;
		private int startCount = 0;
		private int displayCount = 0;

		private float countingTimer;
		private float countingDuration = 0.75f;

		#endregion // Counter Data

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check if display needs to count up to the actual total
			if ( displayCount < targetCount )
			{
				// Increment timer
				countingTimer += Time.deltaTime;

				// Check for completion
				if ( countingTimer >= countingDuration )
				{
					// Display actual total
					displayCount = targetCount;
				}
				else
				{
					// Lerp display
					displayCount = Utils.Lerp ( startCount, targetCount, countingTimer / countingDuration );
				}

				// Display number
				SetText ( numberText, displayCount, isPlusDisplayed, positiveColor, negativeColor );
			}
			// Check if display needs to count down to the actual total
			else if ( displayCount > targetCount )
			{
				// Increment timer
				countingTimer += Time.deltaTime;

				// Check for completion
				if ( countingTimer >= countingDuration )
				{
					// Display actual total
					displayCount = targetCount;
				}
				else
				{
					// Lerp display
					displayCount = Utils.Lerp ( targetCount, startCount, ( countingDuration - countingTimer ) / countingDuration );
				}

				// Display number
				SetText ( numberText, displayCount, isPlusDisplayed, positiveColor, negativeColor );
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets initial number amount to display.
		/// </summary>
		/// <param name="number"> The starting amount of snaps to display. </param>
		/// <param name="isZeroDisplayed"> Whether or not the text is displayed when set to zero snaps. </param>
		public void SetNumber ( int number, bool isZeroDisplayed )
		{
			// Set counts
			targetCount = number;
			startCount = targetCount;
			displayCount = targetCount;

			// Set display
			if ( number == 0 && !isZeroDisplayed )
			{
				numberText.text = string.Empty;
			}
			else
			{
				SetText ( numberText, number, isPlusDisplayed, positiveColor, negativeColor );
			}
		}

		/// <summary>
		/// Adds a number of snaps to the current total.
		/// </summary>
		/// <param name="number"> The number being added to the total. </param>
		/// <param name="isAddTextAnimated"> Whether or not to animate the add text. </param>
		/// <param name="addColor"> The color to use for the add text. </param>
		public void AddNumber ( int number, bool isAddTextAnimated = true, Enums.LatinateOrdinalNumbers addColor = Enums.LatinateOrdinalNumbers.PRIMARY )
		{
			// Check for no value added
			if ( number == 0 )
			{
				return;
			}

			// Store current number being displayed
			startCount = displayCount;

			// Add number to total
			targetCount += number;

			// Reset timer
			countingTimer = 0f;

			// Create add text
			if ( isAddTextAnimated && addTextPrefab != null )
			{
				AnimateAddText ( number, addColor );
			}
		}

		#endregion Public Functions

		#region Private Functions

		/// <summary>
		/// Sets a text element to display a formatted number.
		/// </summary>
		/// <param name="text"> The text element for display. </param>
		/// <param name="number"> The number amount to display. </param>
		/// <param name="hasPlus"> Whether or not a plus is displayed with the number. </param>
		/// <param name="positive"> The text color for positive numbers. </param>
		/// <param name="negative"> The text color for negative numbers. </param>
		private void SetText ( TMP_Text text, int number, bool hasPlus, Color32 positive, Color32 negative )
		{
			// Check for negative snaps
			if ( number < 0 )
			{
				// Display negative snaps
				text.color = negative;

				// Check for prepend text
				if ( !string.IsNullOrEmpty ( prependText ) )
				{
					// Get a positive number for display
					text.text = $"-{prependText}{(number * -1):N0}";
				}
				else
				{
					text.text = $"{number:N0}";
				}
			}
			else
			{
				// Display positive snaps
				text.color = positive;
				if ( hasPlus )
				{
					text.text = $"+{prependText}{number:N0}";
				}
				else
				{
					text.text = $"{prependText}{number:N0}";
				}
			}
		}

		/// <summary>
		/// Animates the add text.
		/// </summary>
		/// <param name="number"> The number being added to the total. </param>
		/// <param name="addColor"> The color of the add text. </param>
		private void AnimateAddText ( int number, Enums.LatinateOrdinalNumbers addColor )
		{
			// Create text
			TMP_Text addText = Instantiate ( addTextPrefab, transform );
			addText.rectTransform.anchoredPosition = addTextPrefab.rectTransform.anchoredPosition;
			addText.rectTransform.sizeDelta = addTextPrefab.rectTransform.sizeDelta;
			addText.gameObject.SetActive ( true );

			// Get the positive color
			Color32 positiveColor = addTextPrimaryPositiveColor;
			if ( addColor == Enums.LatinateOrdinalNumbers.SECONDAY )
			{
				positiveColor = addTextSecondaryPositiveColor;
			}
			else if ( addColor == Enums.LatinateOrdinalNumbers.TERTIARY )
			{
				positiveColor = addTextTertiaryPositiveColor;
			}

			// Display amount
			SetText ( addText, number, true, positiveColor, addTextNegativeColor );

			// Get offset
			Vector2 offset = new Vector2 ( Random.Range ( -10f, 10f ), 15f );

			// Create animation
			addText.rectTransform.DOAnchorPos ( addText.rectTransform.anchoredPosition + offset, countingDuration ).OnComplete ( ( ) =>
			{
				// Delete add text when finished
				Destroy ( addText.gameObject );
			} );
		}

		#endregion // Private Functions
	}
}