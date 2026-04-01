using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of an item slot in the HUD.
	/// </summary>
	public class ItemSlot : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private ItemDisplay itemDisplay;

		[SerializeField]
		private GameObject itemButton;

		[SerializeField]
		private GameObject sellButton;

		[SerializeField]
		private TMP_Text sellText;

		[SerializeField]
		private GameObject inputFieldOverride;

		#endregion // UI Elements

		#region Item Data

		[SerializeField]
		private Color32 emptyContainerColor;

		[SerializeField]
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		[SerializeField]
		private bool canSell;

		private bool isFocused;
		private System.Action onStopFocusPrevention;

		#endregion // Item Data

		#region MonoBehaviour Functions

		private void Update ( )
		{
			// Check for focus state
			if ( isFocused )
			{
				// Check if focus has been lost
				if ( IsFocusLost ( ) )
				{
					// Lose focus
					isFocused = false;

					// Trigger the focus prevention to cease
					onStopFocusPrevention ( );

					// Only hide the sell button if it isn't actively being clicked as it will be hidden later
					if ( EventSystem.current.currentSelectedGameObject != sellButton )
					{
						// Hide sell button
						sellButton.SetActive ( false );

						// Hide info
						itemDisplay.HideInfo ( );
					}
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets the display of an item in the HUD.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="instanceID"> The ID of the instance of the item. </param>
		/// <param name="isEnabled"> Whether or not the item is enabled. </param>
		/// <param name="sellPrice"> The price the item is sold for. </param>
		public void SetItem ( Items.ItemScriptableObject item, string instanceID, bool isEnabled, int sellPrice )
		{
			// Set sell button
			UpdatePrice ( sellPrice );

			// Set item
			itemDisplay.SetItem ( item, instanceID, isEnabled );

			// Set interaction of the button
			itemDisplay.gameObject.SetActive ( item != null );

			// Set as not focused
			isFocused = false;
		}

		/// <summary>
		/// Updates the sell price of the item.
		/// </summary>
		/// <param name="sellPrice"> The price of the item is sold for. </param>
		public void UpdatePrice ( int sellPrice )
		{
			// Set sell button
			sellButton.SetActive ( false );
			sellText.text = $"Sell ${sellPrice:N0}";
		}

		/// <summary>
		/// Displays the sell button on click.
		/// </summary>
		public void ShowSellButton ( )
		{
			// Set focus
			isFocused = true;

			// Show sell button
			sellButton.SetActive ( true );

			// Force display info
			itemDisplay.ShowInfo ( true );
		}

		/// <summary>
		/// Hides the information panel when no longer hovering.
		/// </summary>
		public void HideInfo ( )
		{
			// Check for focus
			if ( !isFocused )
			{
				// Hide info
				itemDisplay.HideInfo ( );
			}
		}
		
		/// <summary>
		/// Prevents showing the information panel when one item display is focused.
		/// </summary>
		/// <param name="onStopPrevention"> The callback when the item display no longer has focus. </param>
		public void PreventHover ( System.Action onStopPrevention )
		{
			// Prevent hover
			itemDisplay.ToggleCanHover ( false );

			// Store callback
			onStopFocusPrevention = onStopPrevention;
		}

		/// <summary>
		/// Regains the ability to show the information panel on hover.
		/// </summary>
		public void RegainHover ( )
		{
			// Regain hover
			itemDisplay.ToggleCanHover ( true );
			onStopFocusPrevention = null;
		}

		/// <summary>
		/// Previews an item display.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="isEnabled"> Whether or not the item is enabled. </param>
		public void PreviewItem ( Items.ItemScriptableObject item, bool isEnabled )
		{
			// Display the preview
			itemDisplay.PreviewItem ( item, isEnabled );
		}

		/// <summary>
		/// Highlights an item being used.
		/// </summary>
		/// <param name="isPositive"> Whether or not the item is being positively highlighted. </param>
		public void HighlightItem ( bool isPositive )
		{
			// Animate item
			itemDisplay.HighlightItem ( isPositive );
		}

		/// <summary>
		/// Dissolves the item in the HUD.
		/// </summary>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void DissolveItem ( System.Action onComplete )
		{
			// Animate item
			itemDisplay.DissolveItem ( onComplete );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Check if focus of this item slot has been lost to a different element.
		/// </summary>
		/// <returns> Whether or not the focus has been lost. </returns>
		private bool IsFocusLost ( )
		{
			// Check for input field during performances
			if ( inputFieldOverride != null )
			{
				// Check for focus a different element from this slot or the input field override
				return EventSystem.current.currentSelectedGameObject != itemButton.gameObject && EventSystem.current.currentSelectedGameObject != inputFieldOverride;
			}
			else
			{
				// Check for focus on a different element from this slot
				return EventSystem.current.currentSelectedGameObject != itemButton.gameObject;
			}
		}

		#endregion // Private Functions
	}
}