using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the credits and countdown splash in the HUD for the performance.
	/// </summary>
	public class CreditsHUD : MonoBehaviour
	{
		#region Credits Data Constants

		private const float CREDITS_FADE = 0.5f;
		private const float CREDITS_INTERVAL = 0.25f;
		private const float CREDITS_DURATION = 2.5f;
		private const float QUOTE_DURATION = 6f;

		#endregion // Credits Data Constants

		#region UI Elements

		[SerializeField]
		private Image backgroundImage;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text authorText;

		[SerializeField]
		private Image [ ] starIcons;

		[SerializeField]
		private RectTransform container;

		[SerializeField]
		private TMP_Text countdownText;

		[SerializeField]
		private JudgeHUD judgeHUD;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Plays the intro animation for the performance.
		/// </summary>
		/// <param name="title"> The title of the poem. </param>
		/// <param name="author"> The author of the poem. </param>
		/// <param name="rating"> The difficulty rating of the poem. </param>
		/// <param name="isLastPerformance"> Whether or not this is the last performance of the round. </param>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void PlayIntro ( string title, string author, int rating, bool isLastPerformance, System.Action onComplete )
		{
			// Set title
			titleText.text = title;
			titleText.alpha = 0f;

			// Set author
			authorText.text = $"By {author}";
			authorText.alpha = 0f;

			// Set rating
			for ( int i = 0; i < starIcons.Length; i++ )
			{
				// Check rating amount
				if ( i < rating )
				{
					// Set star to be displayed
					starIcons [ i ].gameObject.SetActive ( true );
					starIcons [ i ].color = new Color ( 1f, 1f, 1f, 0f );
				}
				else
				{
					// Hide star
					starIcons [ i ].gameObject.SetActive ( false );
				}
			}

			// Create animation
			Sequence sequence = DOTween.Sequence ( );

			// Fade in title
			sequence.AppendInterval ( CREDITS_INTERVAL );
			sequence.Append ( titleText.DOFade ( 1f, CREDITS_FADE ).SetEase ( Ease.InQuad ) );
			sequence.AppendInterval ( CREDITS_INTERVAL );

			// Fade in author
			sequence.Append ( authorText.DOFade ( 1f, CREDITS_FADE ).SetEase ( Ease.InQuad ) );
			sequence.AppendInterval ( CREDITS_INTERVAL );

			// Fade in stars
			for ( int i = 0; i < rating; i++ )
			{
				// Check for first
				if ( i == 0 )
				{
					sequence.Append ( starIcons [ i ].DOFade ( 1f, CREDITS_FADE ).SetEase ( Ease.InQuad ) );
				}
				else
				{
					sequence.Join ( starIcons [ i ].DOFade ( 1f, CREDITS_FADE ).SetEase ( Ease.InQuad ) );
				}
			}
			sequence.AppendInterval ( CREDITS_DURATION );

			// Fade out credits
			sequence.Append ( titleText.DOFade ( 0f, CREDITS_FADE ).SetEase ( Ease.OutQuad ) );
			sequence.Join ( authorText.DOFade ( 0f, CREDITS_FADE ).SetEase ( Ease.OutQuad ) );
			for ( int i = 0; i < rating; i++ )
			{
				sequence.Join ( starIcons [ i ].DOFade ( 0f, CREDITS_FADE ).SetEase ( Ease.OutQuad ) );
			}

			// Fade out background
			sequence.AppendCallback ( ( ) =>
			{
				// Play animation separate from sequence timing
				backgroundImage.DOFade ( 0f, 3f ).SetEase ( Ease.OutQuad );

				// Display countdown = 3s
				countdownText.gameObject.SetActive ( true );
				countdownText.text = "3";
			} );

			// Check for last performance
			if ( isLastPerformance )
			{
				// Animate judge quote
				sequence.AppendCallback ( ( ) =>
				{
					judgeHUD.AnimateQuote ( );
				} );
				sequence.AppendInterval ( judgeHUD.QuoteDuration );
			}

			// Begin countdown
			sequence.Append ( countdownText.DOFade ( 0f, 1f ).SetEase ( Ease.InExpo ) );
			sequence.AppendCallback ( ( ) =>
			{
				// Countdown = 2s
				countdownText.color = Color.white;
				countdownText.text = "2";
			} );
			sequence.Append ( countdownText.DOFade ( 0f, 1f ).SetEase ( Ease.InExpo ) );
			sequence.AppendCallback ( ( ) =>
			{
				// Countdown = 1s
				countdownText.color = Color.white;
				countdownText.text = "1";
			} );
			sequence.Append ( countdownText.DOFade ( 0f, 1f ).SetEase ( Ease.InExpo ) );
			sequence.Join ( container.DOSizeDelta ( container.sizeDelta * Vector2.up, 1f ).SetEase ( Ease.InSine ) );
			sequence.AppendCallback ( ( ) =>
			{
				// Trigger completion
				onComplete ( );

				// Hide splash
				gameObject.SetActive ( false );
			} );
		}

		#endregion // Public Functions
	}
}