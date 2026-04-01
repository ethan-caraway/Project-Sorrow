using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the display of the judge information.
	/// </summary>
	public class JudgeHUD : MonoBehaviour
	{
		#region Judge Data Constants

		private const float FADE_DURATION = 0.5f;
		private const float QUOTE_RATE = 0.05f;
		private const float QUOTE_DELAY = 3f;

		#endregion // Judge Data Constants

		#region UI Elements

		[SerializeField]
		private CanvasGroup quoteContainer;

		[SerializeField]
		private TMP_Text quoteText;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private HUD.TagInfoDisplay tagDisplay;

		#endregion // UI Elements

		#region Judge Data

		private string quote;
		private bool isQuoteAnimating;
		private float quoteDuration;
		private float quoteTimer;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// The amount of time in seconds the quote will take.
		/// </summary>
		public float QuoteDuration
		{
			get
			{
				return FADE_DURATION + quoteDuration + QUOTE_DELAY + FADE_DURATION;
			}
		}

		#endregion // Public Properties

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check for quote animation
			if ( isQuoteAnimating )
			{
				// Increment timer
				quoteTimer += Time.deltaTime;

				// Lerp the quote text
				quoteText.text = $"\"{Utils.Lerp ( quote, quoteTimer / quoteDuration )}\"";

				// Check for completion
				if ( quoteTimer >= quoteDuration )
				{
					// End animation
					isQuoteAnimating = false;
					quoteText.text = $"\"{quote}\"";
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets the display of a judge.
		/// </summary>
		/// <param name="judge"> The data for the judge. </param>
		public void SetJudge ( Judges.JudgeScriptableObject judge )
		{
			// Display judge info
			infoContainer.SetActive ( true );
			titleText.text = judge.Title;
			descriptionText.text = judge.Description;

			// Display tag info
			if ( judge.HasTag )
			{
				tagDisplay.gameObject.SetActive ( true );
				tagDisplay.SetTag ( Tags.TagUtility.GetTag ( judge.Tag ) );
			}
			else
			{
				tagDisplay.gameObject.SetActive ( false );
			}

			// Grab random quote
			quote = judge.Quotes [ Random.Range ( 0, judge.Quotes.Length ) ];

			// Set quote
			quoteContainer.gameObject.SetActive ( false );
			quoteText.text = "";
			isQuoteAnimating = false;

			// Set duration
			quoteDuration = quote.Length * QUOTE_RATE;
		}

		/// <summary>
		/// Hides the display of the judge.
		/// </summary>
		public void HideJudge ( )
		{
			// Hide judge info
			infoContainer.SetActive ( false );

			// Hide tag info
			tagDisplay.gameObject.SetActive ( false );

			// Hide quote
			quoteContainer.gameObject.SetActive ( false );
		}

		/// <summary>
		/// Animates the judge's quote.
		/// </summary>
		public void AnimateQuote ( )
		{
			// Display quote
			quoteContainer.gameObject.SetActive ( true );
			quoteContainer.alpha = 0f;
			
			// Create animation
			Sequence sequence = DOTween.Sequence ( );

			// Fade in quote
			sequence.Append ( quoteContainer.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Begin quote animation
			sequence.AppendCallback ( ( ) =>
			{
				// Animate quote
				isQuoteAnimating = true;
				quoteTimer = 0f;
			} );
			sequence.AppendInterval ( quoteDuration );

			// Wait for the quote to be displayed to read
			sequence.AppendInterval ( QUOTE_DELAY );

			// Fade out quote
			sequence.Append ( quoteContainer.DOFade ( 0f, FADE_DURATION ).SetEase ( Ease.OutQuad ) );
			sequence.AppendCallback ( ( ) =>
			{
				// Hide quote
				quoteContainer.gameObject.SetActive ( false );
			} );
		}

		#endregion // Public Functions
	}
}