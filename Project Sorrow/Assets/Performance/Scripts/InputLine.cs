using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls display an input line.
	/// </summary>
	public class InputLine : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private RectTransform container;

		[SerializeField]
		private Slider backerSlider;

		[SerializeField]
		private TMP_Text previewText;

		[SerializeField]
		private TMP_Text inputText;

		[SerializeField]
		private TMP_Text cursorText;

		[SerializeField]
		private HUD.IntegerCounter lineSnapsCounter;

		#endregion // UI Elements

		#region Input Line Data

		private string inputLine;
		private bool isEditing;
		private float cursorTimer;

		private bool isFrameDelay;
		private int frameDelayCounter;

		#endregion // Input Line Data

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check for frame delay
			if ( isFrameDelay )
			{
				// Decrement delay
				frameDelayCounter--;

				// Check for complete delay
				if ( frameDelayCounter <= 0 )
				{
					// Set font sizes
					inputText.fontSize = previewText.fontSize;
					cursorText.fontSize = previewText.fontSize;

					// End delay
					isFrameDelay = false;
				}
			}
			
			// Check for cursor
			if ( isEditing )
			{
				// Increment timer
				cursorTimer += Time.deltaTime;

				// Check timing to blink cursor
				if ( cursorTimer > 0.5f )
				{
					// Blink cursor
					cursorText.gameObject.SetActive ( !cursorText.gameObject.activeSelf );

					// Reset timer
					cursorTimer = 0f;
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Initializes the line by storing and displaying the given text.
		/// </summary>
		/// <param name="line"> The text for this line. </param>
		/// <param name="isAnimatingIn"> Whether or not this line should animate into the poem. </param>
		public void SetLine ( string line, bool isAnimatingIn )
		{
			// Set preview
			previewText.text = line;

			// Add alignment text
			//previewText.text += "<alpha=#00>@</alpha>";

			// Reset input
			inputLine = string.Empty;
			inputText.text = inputLine;

			// Reset snaps
			lineSnapsCounter.SetNumber ( 0, false );

			// Prevent editing
			isEditing = false;

			// Hide cursor
			cursorText.gameObject.SetActive ( false );

			// Set frame delay
			isFrameDelay = true;
			frameDelayCounter = 2;

			// Check for animation
			if ( isAnimatingIn )
			{
				AnimateIn ( );
			}
		}

		/// <summary>
		/// Sets the line as editable and awaiting input.
		/// </summary>
		public void EditLine ( )
		{
			// Begin editing
			isEditing = true;

			// Animate backer
			backerSlider.direction = Slider.Direction.TopToBottom;
			backerSlider.DOValue ( 1, 0.25f ).SetEase ( Ease.InQuad );

			// Display cursor
			cursorText.gameObject.SetActive ( true );
			cursorText.text = "_";
			cursorTimer = 0f;
		}

		/// <summary>
		/// Appends the input line with the given text.
		/// </summary>
		/// <param name="text"> The text to append to the input line. </param>
		/// <param name="snaps"> The number of additional snaps earned from the text. </param>
		public void AppendLine ( string text, int snaps )
		{
			// Remove previous alignment text
			inputLine = inputLine.Replace ( "<alpha=#00>@</alpha>", "" );

			// Store the appended text
			inputLine += text;

			// Add alignment text
			inputLine += "<alpha=#00>@</alpha>";

			// Display updated input
			inputText.text = inputLine;

			// Set cursor
			cursorText.gameObject.SetActive ( true );
			cursorTimer = 0f;

			// Remove previous cursor
			cursorText.text = cursorText.text.Remove ( cursorText.text.Length - 1 );
			
			// Check for bold characters
			if ( text.Contains ( "<b>" ) )
			{
				// Add bold space to match spacing before cursor
				cursorText.text += "<alpha=#00><b>x</b></color>_";
			}
			// Check for small characters
			else if ( text.Contains ( "<size=70%>" ) )
			{
				// Add small space to match spacing before cursor
				cursorText.text += "<alpha=#00><size=70%>x</size></color>_";
			}
			else
			{
				cursorText.text += " _";
			}

			// Display snaps
			lineSnapsCounter.AddNumber ( snaps );
		}

		/// <summary>
		/// Applies additional snaps to be earned for completing a word.
		/// </summary>
		/// <param name="snaps"> The number of additional snaps earned. </param>
		public void AddSnaps ( int snaps )
		{
			// Display snaps
			lineSnapsCounter.AddNumber ( snaps, true, Enums.LatinateOrdinalNumbers.SECONDAY );
		}

		/// <summary>
		/// Sets the line as no longer being editable.
		/// </summary>
		public void EndEditing ( )
		{
			// Stop editing
			isEditing = false;
			cursorText.gameObject.SetActive ( false );

			// Animate backer
			backerSlider.direction = Slider.Direction.BottomToTop;
			backerSlider.DOValue ( 0, 0.25f ).SetEase ( Ease.InQuad );
		}

		/// <summary>
		/// Sets the line as completed and no longer awaiting input.
		/// </summary>
		/// <param name="snaps"> The number of additional snaps earned from completing the line. </param>
		public void CompleteLine ( int snaps )
		{
			// Display finalized line
			inputText.text = inputLine;

			// Stop editing
			EndEditing ( );

			// Display snaps
			lineSnapsCounter.AddNumber ( snaps, true, Enums.LatinateOrdinalNumbers.TERTIARY );
		}

		/// <summary>
		/// Plays the animation for when a mistake is made.
		/// </summary>
		/// <param name="duration"> The amount of time in seconds the animation should play for. </param>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void PlayFlubAnimation ( float duration, System.Action onComplete )
		{
			// Stop editing
			isEditing = false;
			cursorText.gameObject.SetActive ( false );

			// Create animation
			container.DOShakeAnchorPos ( duration, 5 ).OnComplete ( ( ) =>
			{
				// Reenable editing
				isEditing = true;
				cursorText.gameObject.SetActive ( true );
				cursorTimer = 0f;
					
				// Trigger animation completion
				onComplete ( );
			} );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Animates the line appearing and pushing the lines up.
		/// </summary>
		private void AnimateIn ( )
		{
			// Create animation
			RectTransform rect = GetComponent<RectTransform> ( );
			rect.DOSizeDelta ( Vector2.right * rect.sizeDelta.x, 0.25f ).From ( ).SetEase ( Ease.InQuad );
		}

		#endregion // Private Functions
	}
}