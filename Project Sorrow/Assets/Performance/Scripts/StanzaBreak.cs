using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the stanza break in a poem.
	/// </summary>
	public class StanzaBreak : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Slider backerSlider;

		[SerializeField]
		private Slider slider;

		#endregion // UI Elements

		#region Break Data

		private bool isTimerActive = false;
		private float timer;
		private float duration;

		private System.Action onTimerComplete = null;

		#endregion // Break Data

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check for timer
			if ( isTimerActive )
			{
				// Increment timer
				timer += Time.deltaTime;

				// Update slider
				slider.value = timer / duration;

				// Check for timer completion
				if ( timer > duration )
				{
					// Trigger completion
					OnTimerComplete ( );
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Animates the stanza break appearing and pushing the lines up.
		/// </summary>
		public void AnimateIn ( )
		{
			// Disable timer
			isTimerActive = false;

			// Hide slider
			slider.gameObject.SetActive ( false );

			// Create animation
			RectTransform rect = GetComponent<RectTransform> ( );
			rect.DOSizeDelta ( Vector2.right * rect.sizeDelta.x, 0.25f ).From ( ).SetEase ( Ease.InQuad );
		}

		/// <summary>
		/// Begins the timer of the stanza break.
		/// </summary>
		/// <param name="time"> The duration of time in seconds to wait for the break. </param>
		/// <param name="onComplete"> The callback for when the timer completes. </param>
		public void StartTimer ( float time, System.Action onComplete )
		{
			// Animate backer
			backerSlider.direction = Slider.Direction.TopToBottom;
			backerSlider.DOValue ( 1, 0.25f ).SetEase ( Ease.InQuad );

			// Store duration
			duration = time;

			// Reset timer
			isTimerActive = true;
			timer = 0f;

			// Display slider
			slider.gameObject.SetActive ( true );
			slider.value = 0f;

			// Store callback
			onTimerComplete = onComplete;
		}

		/// <summary>
		/// Stops the timer of the stanza break before the timer completes.
		/// </summary>
		public void StopTimer ( )
		{
			// Deactivate timer
			isTimerActive = false;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Completes the wait for the stanza break.
		/// </summary>
		private void OnTimerComplete ( )
		{
			// End timer
			isTimerActive = false;

			// Animate backer
			backerSlider.direction = Slider.Direction.BottomToTop;
			backerSlider.DOValue ( 0, 0.25f ).SetEase ( Ease.InQuad );

			// Hide slider
			slider.gameObject.SetActive ( false );

			// Progress poem
			onTimerComplete ( );
		}

		#endregion // Private Functions
	}
}