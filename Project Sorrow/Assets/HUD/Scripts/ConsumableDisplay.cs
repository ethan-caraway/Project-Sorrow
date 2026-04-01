using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of a consumable.
	/// </summary>
	public class ConsumableDisplay : MonoBehaviour
	{
		#region Consumable Data Constants

		private const float HIGHLIGHT_DURATION = 1f;
		private const float DISSOLVE_DURATION = 1f;
		private const string DISSOLVE_AMOUNT_REFERENCE = "_DissolveAmount";

		private const float BOUNCE_DURATION = 2f;

		#endregion // Consumable Data Constants

		#region UI Elements

		[SerializeField]
		private Button consumableButton;

		[SerializeField]
		private RawImage rarityImage;

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private Image dissolveImage;

		[SerializeField]
		private GameObject countContainer;

		[SerializeField]
		private TMP_Text countText;

		[SerializeField]
		private PointerBillboard billboard;

		[SerializeField]
		private ConsumableInfoDisplay infoDisplay;

		[SerializeField]
		private TagInfoDisplay tagDisplay;

		#endregion // UI Elements

		#region Consumable Data

		[SerializeField]
		private bool isInteractable = true;

		[Tooltip ( "Whether or not the small icon for the item should be used." )]
		[SerializeField]
		private bool isSmall;

		[SerializeField]
		private Color32 disabledRarityColor;

		[SerializeField]
		private Color32 disabledIconColor;

		[SerializeField]
		private Color32 highlightColor;

		private Consumables.ConsumableScriptableObject currentConsumable;
		private bool canHover;
		private bool isInfoLocked;
		private Vector2 startAnchorPos;

		private Tween rarityHighlightTween;
		private Tween iconShakeTween;
		private Tween firstBounceTween;
		private Sequence bounceSequence = null;

		#endregion // Consumable Data

		#region MonoBehaviour Functions

		private void Awake ( )
		{
			// Store starting position
			startAnchorPos = ( iconImage.transform as RectTransform ).anchoredPosition;
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets the display of an consumable in the HUD.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="count"> The number of instances of the consumable. </param>
		/// <param name="isEnabled"> Whether or not the consumable is enabled. </param>
		public void SetConsumable ( Consumables.ConsumableScriptableObject consumable, int count, bool isEnabled )
		{
			// Set consumable
			currentConsumable = consumable;
			DisplayConsumable ( consumable, isEnabled );

			// Set count
			countContainer.SetActive ( count > 1 );
			countText.text = $"x{count}";

			// Set interaction of the button
			consumableButton.gameObject.SetActive ( consumable != null );
			consumableButton.interactable = isInteractable && consumable != null;
			canHover = true;
			isInfoLocked = false;

			// Check for consumable info
			if ( infoDisplay != null )
			{
				// Check for consumable
				if ( consumable != null )
				{
					// Set consumable info
					infoDisplay.SetConsumable ( consumable );
				}

				// Hide consumable info
				infoDisplay.gameObject.SetActive ( false );
			}

			// Check for tag info
			if ( tagDisplay != null )
			{
				// Check for consumable and tag
				if ( consumable != null && consumable.HasTag )
				{
					tagDisplay.SetTag ( Tags.TagUtility.GetTag ( consumable.Tag ) );
				}

				// Hide tag info
				tagDisplay.gameObject.SetActive ( false );
			}
		}

		/// <summary>
		/// Previews an consumable display.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="count"> The number of instances of the consumable. </param>
		/// <param name="isEnabled"> Whether or not the consumable is enabled. </param>
		public void PreviewConsumable ( Consumables.ConsumableScriptableObject consumable, int count, bool isEnabled )
		{
			// Display the preview
			DisplayConsumable ( consumable, isEnabled );

			// Set count
			countContainer.SetActive ( count > 1 );
			countText.text = $"x{count}";
		}

		/// <summary>
		/// Updates the number of instances of the consumable.
		/// </summary>
		/// <param name="count"> The number of instances of the consumable. </param>
		public void UpdateCount ( int count )
		{
			// Set count
			countContainer.SetActive ( count > 1 );
			countText.text = $"x{count}";
		}

		/// <summary>
		/// Shows the information panel when hovering.
		/// </summary>
		/// /// <param name="isLocked"> Whether or not the consumable info should be locked and not hidden when not hovered. </param>
		public void ShowInfo ( bool isLocked )
		{
			// Check for consumable
			if ( currentConsumable != null )
			{
				// Check if locked
				if ( isLocked )
				{
					isInfoLocked = true;
				}

				// Check for consumable display
				if ( infoDisplay != null )
				{
					// Display info
					infoDisplay.gameObject.SetActive ( true );
				}

				// Check for tag display
				if ( tagDisplay != null )
				{
					// Display tag
					tagDisplay.gameObject.SetActive ( currentConsumable.HasTag );
				}
			}
		}

		/// <summary>
		/// Hides the information panel when no longer hovering.
		/// </summary>
		public void HideInfo ( )
		{
			// Remove lock
			isInfoLocked = false;
			
			// Check for consumable display
			if ( infoDisplay != null )
			{
				// Hide info
				infoDisplay.gameObject.SetActive ( false );
			}

			// Check for tag display
			if ( tagDisplay != null )
			{
				// Hide tag
				tagDisplay.gameObject.SetActive ( false );
			}
		}

		/// <summary>
		/// The callback for when the mouse pointer begins hovering over the consumable.
		/// </summary>
		public void OnHoverEnter ( )
		{
			// Mark that the item is being hovered
			if ( billboard != null )
			{
				billboard.OnHoverEnter ( );
			}

			// Check if the item can be hovered
			if ( canHover )
			{
				// Display item info
				ShowInfo ( false );
			}
		}

		/// <summary>
		/// The callback for when the mouse pointer ends hovering over the consumable.
		/// </summary>
		public void OnHoverExit ( )
		{
			// Mark that the item is no longer being hovered
			if ( billboard != null )
			{
				billboard.OnHoverExit ( );
			}

			// Check if the item can be hovered
			if ( canHover )
			{
				// Check if locked
				if ( !isInfoLocked )
				{
					// Hide item info
					HideInfo ( );
				}
			}
		}

		/// <summary>
		/// Toggles whether or not the item can be hovered.
		/// </summary>
		/// <param name="hover"> Whether or not the item can be hovered. </param>
		public void ToggleCanHover ( bool hover )
		{
			// Store state
			canHover = hover;
		}

		/// <summary>
		/// Highlights a consumable being used.
		/// </summary>
		public void HighlightConsumable ( )
		{
			// End previous rarity animation if playing
			if ( rarityHighlightTween != null && !rarityHighlightTween.IsComplete ( ) )
			{
				rarityHighlightTween.Kill ( );
			}

			// Highlight rarity
			rarityImage.color = highlightColor;
			rarityHighlightTween = rarityImage.DOColor ( Utils.GetRarityColor ( currentConsumable.Rarity ), HIGHLIGHT_DURATION ).SetEase ( Ease.InExpo );

			// End previous icon animation if playing
			if ( iconShakeTween != null && !iconShakeTween.IsComplete ( ) )
			{
				iconShakeTween.Kill ( );
			}

			// Shake icon
			iconShakeTween = iconImage.transform.DOShakeRotation ( HIGHLIGHT_DURATION, Vector3.forward * 50 ).SetEase ( Ease.InExpo );
		}

		/// <summary>
		/// Dissolves the current consumable being displayed.
		/// </summary>
		/// <param name="isFullDissolve"> Whether or not the entire consumable will dissolve or just partially. </param>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void DissolveConsumable ( bool isFullDissolve, System.Action onComplete )
		{
			// Hide info panel
			if ( infoDisplay != null )
			{
				infoDisplay.gameObject.SetActive ( false );
			}

			// Hide tag panel
			if ( tagDisplay != null )
			{
				tagDisplay.gameObject.SetActive ( false );
			}

			// Dissable button
			consumableButton.interactable = false;

			// Check for full dissolve
			if ( isFullDissolve )
			{
				// Hide actual icon
				iconImage.color = Color.clear;

				// Fade rarity
				consumableButton.image.DOFade ( 0f, DISSOLVE_DURATION );
				rarityImage.DOColor ( Color.clear, DISSOLVE_DURATION ).SetEase ( Ease.OutExpo );
			}

			// Dissolve consumable
			dissolveImage.gameObject.SetActive ( true );
			dissolveImage.material.SetFloat ( DISSOLVE_AMOUNT_REFERENCE, 0f );
			dissolveImage.material.DOFloat ( 1f, DISSOLVE_AMOUNT_REFERENCE, DISSOLVE_DURATION ).SetEase ( Ease.OutExpo ).OnComplete ( ( ) =>
			{
				// Trigger callback
				if ( onComplete != null )
				{
					onComplete ( );
				}
			} );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the consumable in the HUD.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="isEnabled"> Whether or not the consumable is enabled. </param>
		private void DisplayConsumable ( Consumables.ConsumableScriptableObject consumable, bool isEnabled )
		{
			// Check for consumable
			if ( consumable != null )
			{
				// Check if enabled
				if ( isEnabled )
				{
					// Set rarity
					consumableButton.colors = Utils.GetRarityColors ( consumable.Rarity, isInteractable );
					rarityImage.color = Utils.GetRarityColor ( consumable.Rarity );

					// Set icon as enabled
					iconImage.color = Color.white;
				}
				else
				{
					// Set as disabled
					consumableButton.colors = new ColorBlock
					{
						normalColor = disabledRarityColor,
						highlightedColor = disabledRarityColor,
						pressedColor = disabledRarityColor,
						selectedColor = disabledRarityColor,
						disabledColor = disabledRarityColor,
						colorMultiplier = 1f
					};
					rarityImage.color = disabledRarityColor;
					iconImage.color = disabledIconColor;
				}

				// Reset icon
				consumableButton.image.color = Color.white;
				rarityImage.gameObject.SetActive ( true );
				iconImage.gameObject.SetActive ( true );
				dissolveImage.gameObject.SetActive ( false );
				dissolveImage.material.SetFloat ( DISSOLVE_AMOUNT_REFERENCE, 0f );
				if ( billboard != null )
				{
					billboard.ResetBillboard ( );
				}

				// Set icon
				iconImage.sprite = isSmall ? consumable.SmallIcon : consumable.Icon;
				dissolveImage.sprite = isSmall ? consumable.SmallIcon : consumable.Icon;

				// Animate bounce
				if ( bounceSequence == null )
				{
					BounceConsumable ( );
				}
			}
			else
			{
				// Set container
				consumableButton.colors = new ColorBlock
				{
					normalColor = Color.clear,
					highlightedColor = Color.clear,
					pressedColor = Color.clear,
					selectedColor = Color.clear,
					disabledColor = Color.clear,
					colorMultiplier = 1f
				};

				// Hide consumable
				rarityImage.gameObject.SetActive ( false );
				iconImage.gameObject.SetActive ( false );

				// End bounce
				if ( bounceSequence != null )
				{
					bounceSequence.Kill ( );
					bounceSequence = null;
					( iconImage.transform as RectTransform ).anchoredPosition = startAnchorPos;
				}
			}
		}

		/// <summary>
		/// Animates a subtle bounce to the consumable.
		/// </summary>
		private void BounceConsumable ( )
		{
			// Get consumable recttransform
			RectTransform consumableRect = iconImage.transform as RectTransform;

			// Kill previous animtion
			if ( firstBounceTween != null && !firstBounceTween.IsComplete ( ) )
			{
				firstBounceTween.Kill ( );
				firstBounceTween = null;
			}
			if ( bounceSequence != null )
			{
				bounceSequence.Kill ( );
				firstBounceTween = null;
			}

			// Get bounce height
			float bounceHeight = ( ( ( consumableButton.transform as RectTransform ).rect.height - consumableRect.rect.height ) / 2f ) - 6f;
			float maxHeight = startAnchorPos.y + bounceHeight;
			float minHeight = startAnchorPos.y - bounceHeight;

			// Give the consumable a random start position
			float startPercentage = Random.Range ( 0f, 1f );
			Vector2 startPos = new Vector2 ( consumableRect.anchoredPosition.x, Mathf.Lerp ( minHeight, maxHeight, startPercentage ) );
			consumableRect.anchoredPosition = startPos;

			// Animate the consumable bouncing
			firstBounceTween = consumableRect.DOAnchorPosY ( maxHeight, BOUNCE_DURATION * ( 1f - startPercentage ) ).SetEase ( Ease.OutQuad ).OnComplete ( ( ) =>
			{
				bounceSequence = DOTween.Sequence ( );
				bounceSequence.Append ( consumableRect.DOAnchorPosY ( minHeight, BOUNCE_DURATION ).SetEase ( Ease.InOutQuad ) );
				bounceSequence.Append ( consumableRect.DOAnchorPosY ( maxHeight, BOUNCE_DURATION ).SetEase ( Ease.InOutQuad ) );
				bounceSequence.SetLoops ( -1 );
			} );
		}

		#endregion // Private Functions
	}
}