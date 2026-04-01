using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of an item.
	/// </summary>
	public class ItemDisplay : MonoBehaviour
	{
		#region Item Data Constants

		private const float HIGHLIGHT_DURATION = 1f;
		private const float DISSOLVE_DURATION = 1f;
		private const string DISSOLVE_AMOUNT_REFERENCE = "_DissolveAmount";

		private const float BOUNCE_DURATION = 2f;

		#endregion // Item Data Constants

		#region UI Elements

		[SerializeField]
		private Button itemButton;

		[SerializeField]
		private RawImage rarityImage;

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private Image dissolveImage;

		[SerializeField]
		private PointerBillboard billboard;

		[SerializeField]
		private ItemInfoDisplay infoDisplay;

		[SerializeField]
		private TagInfoDisplay tagDisplay;

		#endregion // UI Elements

		#region Item Data

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
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		private Items.ItemScriptableObject currentItem;
		private string currentInstanceID;
		private bool canHover;
		private bool isInfoLocked;
		private Vector2 startAnchorPos;

		private Tween rarityHighlightTween;
		private Tween iconShakeTween;
		private Tween firstBounceTween;
		private Sequence bounceSequence = null;

		#endregion // Item Data

		#region MonoBehaviour Functions

		private void Awake ( )
		{
			// Store starting position
			startAnchorPos = ( iconImage.transform as RectTransform ).anchoredPosition;
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets the display of an item.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="instanceID"> The ID of the instance of the item. </param>
		/// <param name="isEnabled"> Whether or not the item is enabled. </param>
		public void SetItem ( Items.ItemScriptableObject item, string instanceID, bool isEnabled )
		{
			// Set item
			currentItem = item;
			currentInstanceID = instanceID;
			DisplayItem ( item, isEnabled );

			// Set interaction of the button
			itemButton.gameObject.SetActive ( item != null );
			itemButton.interactable = isInteractable && item != null;
			canHover = true;
			isInfoLocked = false;

			// Check for item info
			if ( infoDisplay != null )
			{
				// Check for item
				if ( item != null )
				{
					// Set item info
					infoDisplay.SetItem ( item, instanceID );
				}

				// Hide item info
				infoDisplay.gameObject.SetActive ( false );
			}

			// Check for item tag
			if ( tagDisplay != null )
			{
				// Set tag info
				if ( item != null && item.HasTag )
				{
					tagDisplay.SetTag ( Tags.TagUtility.GetTag ( item.Tag ) );
				}

				// Hide item info
				tagDisplay.gameObject.SetActive ( false );
			}
		}

		/// <summary>
		/// Previews the display of an item.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="isEnabled"> Whether or not the item is enabled. </param>
		public void PreviewItem ( Items.ItemScriptableObject item, bool isEnabled )
		{
			// Display the preview
			DisplayItem ( item, isEnabled );
		}

		/// <summary>
		/// Shows the information panel for the item if available.
		/// </summary>
		/// <param name="isLocked"> Whether or not the item info should be locked and not hidden when not hovered. </param>
		public void ShowInfo ( bool isLocked )
		{
			// Check for item
			if ( currentItem != null )
			{
				// Check if locked
				if ( isLocked )
				{
					isInfoLocked = true;
				}

				// Check for item display
				if ( infoDisplay != null )
				{
					// Display info
					infoDisplay.gameObject.SetActive ( true );

					// Update item info for scalable values or variable descriptions
					if ( currentItem.IsVariableDescription || currentItem.IsScalable )
					{
						infoDisplay.SetItem ( currentItem, currentInstanceID );
					}
				}

				// Check for tag display
				if ( tagDisplay != null )
				{
					// Display tag
					tagDisplay.gameObject.SetActive ( currentItem.HasTag );
				}
			}
		}

		/// <summary>
		/// Hides the information panel for the item if available.
		/// </summary>
		public void HideInfo ( )
		{
			// Remove lock
			isInfoLocked = false;

			// Check for item display
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
		/// The callback for when the mouse pointer begins hovering over the item.
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
		/// The callback for when the mouse pointer ends hovering over the item.
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
		/// Highlights an item being used.
		/// </summary>
		/// <param name="isPositive"> Whether or not the item is being positively highlighted. </param>
		public void HighlightItem ( bool isPositive )
		{
			// End previous rarity animation if playing
			if ( rarityHighlightTween != null && !rarityHighlightTween.IsComplete ( ) )
			{
				rarityHighlightTween.Kill ( );
			}

			// Highlight rarity
			rarityImage.color = isPositive ? positiveHighlightColor : negativeHighlightColor;
			rarityHighlightTween = rarityImage.DOColor ( Utils.GetRarityColor ( currentItem.Rarity ), HIGHLIGHT_DURATION ).SetEase ( Ease.InExpo );

			// End previous icon animation if playing
			if ( iconShakeTween != null && !iconShakeTween.IsComplete ( ) )
			{
				iconShakeTween.Kill ( );
			}

			// Shake icon
			iconShakeTween = iconImage.transform.DOShakeRotation ( HIGHLIGHT_DURATION, Vector3.forward * 50 ).SetEase ( Ease.InExpo );
		}

		/// <summary>
		/// Dissolves the current item being displayed.
		/// </summary>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void DissolveItem ( System.Action onComplete )
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
			itemButton.interactable = false;

			// Hide actual icon
			iconImage.color = Color.clear;

			// Fade rarity
			itemButton.image.DOFade ( 0f, DISSOLVE_DURATION );
			rarityImage.DOColor ( Color.clear, DISSOLVE_DURATION ).SetEase ( Ease.OutExpo );

			// Dissolve item
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
		/// Displays the item.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="isEnabled"> Whether or not the item is enabled. </param>
		private void DisplayItem ( Items.ItemScriptableObject item, bool isEnabled )
		{
			// Check for item
			if ( item != null )
			{
				// Check if enabled
				if ( isEnabled )
				{
					// Set rarity
					itemButton.colors = Utils.GetRarityColors ( item.Rarity, isInteractable );
					rarityImage.color = Utils.GetRarityColor ( item.Rarity );

					// Set icon as enabled
					iconImage.color = Color.white;
				}
				else
				{
					// Set as disabled
					itemButton.colors = new ColorBlock
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
				itemButton.image.color = Color.white;
				rarityImage.gameObject.SetActive ( true );
				iconImage.gameObject.SetActive ( true );
				dissolveImage.gameObject.SetActive ( false );
				dissolveImage.material.SetFloat ( DISSOLVE_AMOUNT_REFERENCE, 0f );
				if ( billboard != null )
				{
					billboard.ResetBillboard ( );
				}

				// Set icon
				iconImage.sprite = isSmall ? item.SmallIcon : item.Icon;
				dissolveImage.sprite = isSmall ? item.SmallIcon : item.Icon;

				// Animate bounce
				if ( bounceSequence == null )
				{
					BounceItem ( );
				}
			}
			else
			{
				// Set container
				itemButton.colors = new ColorBlock
				{
					normalColor = Color.clear,
					highlightedColor = Color.clear,
					pressedColor = Color.clear,
					selectedColor = Color.clear,
					disabledColor = Color.clear,
					colorMultiplier = 1f
				};

				// Hide item
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
		/// Animates a subtle bounce to the item.
		/// </summary>
		private void BounceItem ( )
		{
			// Get item recttransform
			RectTransform itemRect = iconImage.transform as RectTransform;

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
			float bounceHeight = ( ( ( itemButton.transform as RectTransform ).rect.height - itemRect.rect.height ) / 2f ) - 6f;
			float maxHeight = startAnchorPos.y + bounceHeight;
			float minHeight = startAnchorPos.y - bounceHeight;

			// Give the item a random start position
			float startPercentage = Random.Range ( 0f, 1f );
			Vector2 startPos = new Vector2 ( itemRect.anchoredPosition.x, Mathf.Lerp ( minHeight, maxHeight, startPercentage ) );
			itemRect.anchoredPosition = startPos;

			// Animate the item bouncing
			firstBounceTween = itemRect.DOAnchorPosY ( maxHeight, BOUNCE_DURATION * ( 1f - startPercentage ) ).SetEase ( Ease.OutQuad ).OnComplete ( ( ) =>
			{
				bounceSequence = DOTween.Sequence ( );
				bounceSequence.Append ( itemRect.DOAnchorPosY ( minHeight, BOUNCE_DURATION ).SetEase ( Ease.InOutQuad ) );
				bounceSequence.Append ( itemRect.DOAnchorPosY ( maxHeight, BOUNCE_DURATION ).SetEase ( Ease.InOutQuad ) );
				bounceSequence.SetLoops ( -1 );
			} );
		}

		#endregion // Private Functions
	}
}