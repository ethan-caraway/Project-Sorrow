using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the item displays in the HUD.
	/// </summary>
	public class ItemsHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private ItemSlot [ ] itemDisplays;

		[SerializeField]
		private TMP_Text countText;

		[SerializeField]
		private ItemDisplay dragDisplay;

		#endregion // UI Elements

		#region Item Data

		[SerializeField]
		private Color32 noHighlightColor;

		[SerializeField]
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		private Shop.PriceModel itemPrices;
		private System.Action<int> onSellItem;

		private bool isDragging;
		private int dragIndex;

		#endregion // Item Data

		#region Public Functions

		/// <summary>
		/// Displays all of the player's current items in the HUD.
		/// </summary>
		/// <param name="prices"> The price data for items. </param>
		/// <param name="onSell"> The callback for when an item is sold. Parameter is the index of the item in the data array. </param>
		public void SetItems ( Shop.PriceModel prices, System.Action<int> onSell )
		{
			// Store prices
			itemPrices = prices;

			// Store callback
			onSellItem = onSell;

			// Check for full price sale
			bool isFullPrice = false;
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for full price
					if ( GameManager.Run.ItemData [ i ].Item.OnItemSellPrice ( ) )
					{
						// Set full price
						isFullPrice = true;
						break;
					}
				}
			}

			// Display the items
			for ( int i = 0; i < itemDisplays.Length; i++ )
			{
				// Get item
				Items.ItemScriptableObject item = null;
				string instanceID = string.Empty;
				bool isEnabled = false;
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					item = Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID );
					instanceID = GameManager.Run.ItemData [ i ].InstanceID;
					isEnabled = GameManager.Run.ItemData [ i ].Item.IsEnabled;
				}
				else if ( i >= GameManager.Run.MaxItems )
				{
					// Hide unavailable slot
					itemDisplays [ i ].gameObject.SetActive ( false );
					continue;
				}

				// Get sell price
				int price = 0;
				if ( item != null && prices != null )
				{
					price = prices.GetItemSellPrice ( item.Rarity, isFullPrice );
				}

				// Display item
				itemDisplays [ i ].gameObject.SetActive ( true );
				itemDisplays [ i ].SetItem ( item, instanceID, isEnabled, price );
			}

			// Set item count
			countText.text = $"{GameManager.Run.ItemCount}/{GameManager.Run.MaxItems}";
			if ( GameManager.Run.MaxItems > GameManager.Difficulty.MaxItems )
			{
				countText.color = positiveHighlightColor;
			}
			else if ( GameManager.Run.MaxItems < GameManager.Difficulty.MaxItems )
			{
				countText.color = negativeHighlightColor;
			}
			else
			{
				countText.color = noHighlightColor;
			}
		}

		/// <summary>
		/// Updates the prices of the items.
		/// </summary>
		/// <param name="prices"> The price data for the items. </param>
		public void UpdatePrices ( Shop.PriceModel prices )
		{
			// Store prices
			itemPrices = prices;

			// Check for full price sale
			bool isFullPrice = false;
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for full price
					if ( GameManager.Run.ItemData [ i ].Item.OnItemSellPrice ( ) )
					{
						// Set full price
						isFullPrice = true;
						break;
					}
				}
			}

			// Display the items
			for ( int i = 0; i < itemDisplays.Length; i++ )
			{
				// Get item
				Items.ItemScriptableObject item = null;
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					item = Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID );
				}
				else if ( i >= GameManager.Run.MaxItems )
				{
					// Skip unavailable slot
					continue;
				}

				// Get sell price
				int price = 0;
				if ( item != null && prices != null )
				{
					price = prices.GetItemSellPrice ( item.Rarity, isFullPrice );
				}

				// Update price
				itemDisplays [ i ].UpdatePrice ( price );
			}
		}

		/// <summary>
		/// Sells the selected item.
		/// </summary>
		/// <param name="index"> The index of the item. </param>
		public void SellItem ( int index )
		{
			// Trigger item being sold
			onSellItem ( index );

			// Disable items
			PreventHover ( );

			// Dissolve item being sold
			itemDisplays [ index ].DissolveItem ( ( ) =>
			{
				// Update items
				SetItems ( itemPrices, onSellItem );
			} );
		}

		/// <summary>
		/// Prevents the ability to hover item displays when an item is focused.
		/// </summary>
		public void PreventHover ( )
		{
			// Disable hover in each item display
			for ( int i = 0; i < itemDisplays.Length; i++ )
			{
				itemDisplays [ i ].PreventHover ( OnRegainHover );
			}
		}

		/// <summary>
		/// Highlights an item being used.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="isPositive"> Whether or not the item is highlighted positively. </param>
		public void HighlightItem ( int id, string instance, bool isPositive )
		{
			// Check for valid ID
			if ( id == Items.ItemModel.NO_ITEM_ID )
			{
				return;
			}

			// Check for item
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for matching item ID
				if ( GameManager.Run.ItemData [ i ] != null && GameManager.Run.ItemData [ i ].ID == id && GameManager.Run.ItemData [ i ].InstanceID == instance )
				{
					// Highlight item
					itemDisplays [ i ].HighlightItem ( isPositive );
					return;
				}
			}
		}

		/// <summary>
		/// Highlights an item being used.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <param name="model"> The data for the highlight. </param>
		public void HighlightItem ( int id, string instance, ItemHighlightModel model )
		{
			// Check for valid ID
			if ( id == Items.ItemModel.NO_ITEM_ID )
			{
				return;
			}

			// Check for item
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for matching item ID
				if ( GameManager.Run.ItemData [ i ] != null && GameManager.Run.ItemData [ i ].ID == id && GameManager.Run.ItemData [ i ].InstanceID == instance )
				{
					// Highlight item
					itemDisplays [ i ].HighlightItem ( model );
					return;
				}
			}
		}

		/// <summary>
		/// Begins dragging an item in the HUD.
		/// </summary>
		/// <param name="index"> The index of the item. </param>
		public void BeginDragItem ( int index )
		{
			// Check for item
			if ( GameManager.Run.ItemData [ index ] != null )
			{
				// Set dragging
				isDragging = true;
				dragIndex = index;

				// Get item
				Items.ItemScriptableObject item = Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ index ].ID );

				// Display dragging item
				dragDisplay.SetItem ( item, GameManager.Run.ItemData [ index ].InstanceID, GameManager.Run.ItemData [ index ].Item.IsEnabled );
				dragDisplay.gameObject.SetActive ( true );

				// Hide item being dragged
				itemDisplays [ index ].PreviewItem ( null, false );
				itemDisplays [ index ].HideInfo ( );
				PreventHover ( );
			}
		}

		/// <summary>
		/// Drags an item in the HUD.
		/// </summary>
		/// <param name="eventData"> The event data for the drag. </param>
		public void DragItem ( BaseEventData eventData )
		{
			// Check for drag
			if ( isDragging )
			{
				// Get pointer data
				PointerEventData pointerEventData = eventData as PointerEventData;

				// Get rect transform
				RectTransform rectTransform = dragDisplay.transform as RectTransform;

				// Get valid mouse position
				Vector3 mousePosition;
				if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out mousePosition ) )
				{
					// Set position
					rectTransform.position = mousePosition;
				}
			}
		}

		/// <summary>
		/// Ends dragging an item in the HUD.
		/// </summary>
		public void EndDragItem ( )
		{
			// Check for drag
			if ( isDragging )
			{
				// Stop drag
				isDragging = false;

				// Hide dragging item
				dragDisplay.gameObject.SetActive ( false );

				// Reset items
				SetItems ( itemPrices, onSellItem );
			}
		}

		/// <summary>
		/// Previews the position of all items.
		/// </summary>
		/// <param name="index"> The index of the new position for the dragged item. </param>
		public void PreviewItems ( int index )
		{
			// Check for drag
			if ( isDragging )
			{
				// Check if moving item right
				if ( dragIndex < index )
				{
					// Move each item
					for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
					{
						// Check for unmoved items
						if ( i < dragIndex || i > index )
						{
							// Show unmoved item
							Items.ItemScriptableObject item = GameManager.Run.ItemData [ i ] != null ? Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID ) : null;
							itemDisplays [ i ].PreviewItem ( item, item != null && GameManager.Run.ItemData [ i ].Item.IsEnabled );
						}
						// Check for new position of moved item
						else if ( i == index )
						{
							// Show empty space
							itemDisplays [ i ].PreviewItem ( null, false );
						}
						else
						{
							// Move item left
							Items.ItemScriptableObject item = GameManager.Run.ItemData [ i + 1 ] != null ? Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i + 1 ].ID ) : null;
							itemDisplays [ i ].PreviewItem ( item, item != null && GameManager.Run.ItemData [ i + 1 ].Item.IsEnabled );
						}
					}
				}
				else
				{
					// Move each item
					for ( int i = 0; i < itemDisplays.Length; i++ )
					{
						// Check for unmoved items
						if ( i < index || i > dragIndex )
						{
							// Show unmoved item
							Items.ItemScriptableObject item = GameManager.Run.ItemData [ i ] != null ? Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID ) : null;
							itemDisplays [ i ].PreviewItem ( item, item != null && GameManager.Run.ItemData [ i ].Item.IsEnabled );
						}
						// Check for new position of moved item
						else if ( i == index )
						{
							// Show empty space
							itemDisplays [ i ].PreviewItem ( null, false );
						}
						else
						{
							// Move item right
							Items.ItemScriptableObject item = GameManager.Run.ItemData [ i - 1 ] != null ? Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i - 1 ].ID ) : null;
							itemDisplays [ i ].PreviewItem ( item, item != null && GameManager.Run.ItemData [ i - 1 ].Item.IsEnabled );
						}
					}
				}
			}
		}

		/// <summary>
		/// Ends the preview of items.
		/// </summary>
		public void EndPreview ( )
		{
			// Check for drag
			if ( isDragging )
			{
				// Reset previews
				for ( int i = 0; i < itemDisplays.Length; i++ )
				{
					// Check for dragged item
					if ( i == dragIndex )
					{
						// Show empty space
						itemDisplays [ i ].PreviewItem ( null, false );
					}
					else
					{
						// Reset preview
						Items.ItemScriptableObject item = GameManager.Run.ItemData [ i ] != null ? Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ i ].ID ) : null;
						itemDisplays [ i ].PreviewItem ( item, item != null && GameManager.Run.ItemData [ i ].Item.IsEnabled );
					}
				}
			}
		}

		/// <summary>
		/// Drops an item into a new position in the HUD.
		/// </summary>
		/// <param name="index"> The index of the new position for the dragged item. </param>
		public void DropItem ( int index )
		{
			// Check for drag
			if ( isDragging )
			{
				// Move item to new positions
				GameManager.Run.RearrangeItems ( dragIndex, index );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// The callback for when item display can regain hover.
		/// </summary>
		private void OnRegainHover ( )
		{
			// Reenable hover in each item display
			for ( int i = 0; i < itemDisplays.Length; i++ )
			{
				itemDisplays [ i ].RegainHover ( );
			}
		}

		#endregion // Private Functions
	}
}