using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the consumable displays in the HUD.
	/// </summary>
	public class ConsumablesHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private ConsumableSlot [ ] consumableSlots;

		[SerializeField]
		private TMP_Text countText;

		[SerializeField]
		private ConsumableDisplay dragDisplay;

		[SerializeField]
		private StatusEffectHUD statusEffectHUD;

		#endregion // UI Elements

		#region Consumable Data

		[SerializeField]
		private Color32 noHighlightColor;

		[SerializeField]
		private Color32 positiveHighlightColor;

		[SerializeField]
		private Color32 negativeHighlightColor;

		private System.Action<int> onApplyConsumableToPoem;
		private System.Action<int> onApplyAllConsumableToPoem;

		private Shop.PriceModel consumablePrices;
		private System.Action<int> onSellConsumable;
		private System.Action<int> onSellAllConsumable;

		private bool isDragging;
		private int dragIndex;

		#endregion // Consumable Data

		#region Public Functions

		/// <summary>
		/// Displays all of the player's current consumables in the HUD.
		/// </summary>
		/// <param name="prices"> The price data for consumables. </param>
		/// <param name="onSell"> The callback for when a consumable is sold. Parameter is the index of the consumable in the data array. </param>
		/// <param name="onSellAll"> The callback for when all instances of a consumable are sold. Parameter is the index of the consumable in the data array. </param>
		public void SetConsumables ( Shop.PriceModel prices, System.Action<int> onSell, System.Action<int> onSellAll )
		{
			// Store prices
			consumablePrices = prices;

			// Store callbacks
			onSellConsumable = onSell;
			onSellAllConsumable = onSellAll;

			// Display consumables
			RefreshConsumables ( );
		}

		/// <summary>
		/// Updates the display of consumables in the HUD.
		/// </summary>
		/// <param name="resetSlots"> Whether or not the slot displays should be reset. </param>
		public void RefreshConsumables ( )
		{
			// Check for full price sale
			bool isFullPrice = false;
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for full price
					if ( GameManager.Run.ItemData [ i ].Item.OnConsumableSellPrice ( ) )
					{
						// Set full price
						isFullPrice = true;
						break;
					}
				}
			}

			// Display the consumables
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				// Get consumable
				Consumables.ConsumableScriptableObject consumable = null;
				int count = 0;
				if ( GameManager.Run.IsValidConsumable ( i ) )
				{
					consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i ].ID );
					count = GameManager.Run.ConsumableData [ i ].Count;
				}
				else if ( i >= GameManager.Run.MaxConsumables )
				{
					// Hide unavailable slots
					consumableSlots [ i ].gameObject.SetActive ( false );
					continue;
				}

				// Get sell price
				int price = 0;
				if ( consumable != null && consumablePrices != null )
				{
					price = consumablePrices.GetConsumableSellPrice ( consumable.Rarity, isFullPrice );
				}

				// Display consumable
				consumableSlots [ i ].gameObject.SetActive ( true );
				consumableSlots [ i ].SetConsumable ( consumable, count, true, price );
			}

			// Set consumable count
			countText.text = $"{GameManager.Run.ConsumableSlotCount}/{GameManager.Run.MaxConsumables}";
			if ( GameManager.Run.MaxConsumables > GameManager.Difficulty.MaxConsumables )
			{
				countText.color = positiveHighlightColor;
			}
			else if ( GameManager.Run.MaxConsumables < GameManager.Difficulty.MaxConsumables )
			{
				countText.color = negativeHighlightColor;
			}
			else
			{
				countText.color = noHighlightColor;
			}
		}

		/// <summary>
		/// Updates the prices of the consumables.
		/// </summary>
		/// <param name="prices"> The price data for the consumables. </param>
		public void UpdatePrices ( Shop.PriceModel prices )
		{
			// Store prices
			consumablePrices = prices;

			// Check for full price sale
			bool isFullPrice = false;
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for full price
					if ( GameManager.Run.ItemData [ i ].Item.OnConsumableSellPrice ( ) )
					{
						// Set full price
						isFullPrice = true;
						break;
					}
				}
			}

			// Display the consumables
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				// Get consumable
				Consumables.ConsumableScriptableObject consumable = null;
				if ( GameManager.Run.IsValidConsumable ( i ) )
				{
					consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i ].ID );
				}
				else if ( i >= GameManager.Run.MaxConsumables )
				{
					// Skip unavailable slots
					continue;
				}

				// Get sell price
				int price = 0;
				if ( consumable != null && consumablePrices != null )
				{
					price = consumablePrices.GetConsumableSellPrice ( consumable.Rarity, isFullPrice );
				}

				// Update price
				consumableSlots [ i ].UpdatePrice ( price );
			}
		}

		/// <summary>
		/// Enables application of consumables to a poem.
		/// </summary>
		/// <param name="onApply"> The callback for when a consumable is applied to a poem. Parameter is the index of the consumable in the data array. </param>
		/// <param name="onApplyAll"> The callback for when all instances of a consumable are applied to a poem. Parameter is the index of the consumable in the data array. </param>
		public void EnableApplyToPoem ( System.Action<int> onApply, System.Action<int> onApplyAll )
		{
			// Store the callbacks
			onApplyConsumableToPoem = onApply;
			onApplyAllConsumableToPoem = onApplyAll;

			// Enable apply on each consumable
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				consumableSlots [ i ].ToggleApplyToPoem ( true );
			}
		}

		/// <summary>
		/// Disables application of consumables to a poem.
		/// </summary>
		public void DisableApplyToPoem ( )
		{
			// Disable apply on each consumable
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				consumableSlots [ i ].ToggleApplyToPoem ( false );
			}
		}

		/// <summary>
		/// Applies the selected consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		public void ApplyConsumable ( int index )
		{
			// Get count before removal
			int count = GameManager.Run.ConsumableData [ index ].Count;

			// Check consumable type
			if ( Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID ).IsApplyToPoem )
			{
				// Trigger consumable being applied
				onApplyConsumableToPoem ( index );
			}
			else
			{
				// Trigger an instance of consumable
				ApplyStatusEffect ( index, 1 );
			}

			// Disable consumables
			PreventHover ( );

			// Dissolve item being sold
			consumableSlots [ index ].DissolveConsumable ( count == 1, ( ) =>
			{
				// Update consumables
				RefreshConsumables ( );
			} );
		}

		/// <summary>
		/// Applies all instances of the selected consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		public void ApplyAllConsumable ( int index )
		{
			// Check consumable type
			if ( Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID ).IsApplyToPoem )
			{
				// Trigger all instances of consumable being applied
				onApplyAllConsumableToPoem ( index );
			}
			else
			{
				// Trigger all instances of consumable
				ApplyStatusEffect ( index, GameManager.Run.ConsumableData [ index ].Count );
			}

			// Disable consumables
			PreventHover ( );

			// Dissolve item being sold
			consumableSlots [ index ].DissolveConsumable ( true, ( ) =>
			{
				// Update consumables
				RefreshConsumables ( );
			} );
		}

		/// <summary>
		/// Sells the selected consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		public void SellConsumable ( int index )
		{
			// Get count before removal
			int count = GameManager.Run.ConsumableData [ index ].Count;

			// Trigger consumable being sold
			onSellConsumable ( index );

			// Disable consumables
			PreventHover ( );

			// Dissolve item being sold
			consumableSlots [ index ].DissolveConsumable ( count == 1, ( ) =>
			{
				// Update consumables
				RefreshConsumables ( );
			} );
		}

		/// <summary>
		/// Sells all instances of the selected consumable.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		public void SellAllConsumable ( int index )
		{
			// Trigger all instances of consumable being sold
			onSellAllConsumable ( index );

			// Disable consumables
			PreventHover ( );

			// Dissolve item being sold
			consumableSlots [ index ].DissolveConsumable ( true, ( ) =>
			{
				// Update consumables
				RefreshConsumables ( );
			} );
		}

		/// <summary>
		/// Prevents the ability to hover consumable displays when a consumable is focused.
		/// </summary>
		public void PreventHover ( )
		{
			// Disable hover in each consumable display
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				consumableSlots [ i ].PreventHover ( OnRegainHover );
			}
		}

		/// <summary>
		/// Highlights a consumable being used.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		public void HighlightConsumable ( int id )
		{
			// Check for consumable
			for ( int i = 0; i < GameManager.Run.ConsumableData.Length; i++ )
			{
				// Check for matching consumable ID
				if ( GameManager.Run.ConsumableData [ i ] != null && GameManager.Run.ConsumableData [ i ].ID == id )
				{
					// Highlight consumable
					consumableSlots [ i ].HighlightConsumable ( );
					return;
				}
			}
		}

		/// <summary>
		/// Begins dragging a consumable in the HUD.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		public void BeginDragConsumable ( int index )
		{
			// Check for consumable
			if ( GameManager.Run.IsValidConsumable ( index ) )
			{
				// Set dragging
				isDragging = true;
				dragIndex = index;
				
				// Get consumable
				Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID );

				// Display dragging consumable
				dragDisplay.gameObject.SetActive ( true );
				dragDisplay.SetConsumable ( consumable, GameManager.Run.ConsumableData [ index ].Count, true );

				// Hide consumable being dragged
				consumableSlots [ index ].PreviewConsumable ( null, 0, false );
				consumableSlots [ index ].HideInfo ( );
				PreventHover ( );
			}
		}

		/// <summary>
		/// Drags a consumable in the HUD.
		/// </summary>
		/// <param name="eventData"> The event data for the drag. </param>
		public void DragConsumable ( BaseEventData eventData )
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
		/// Ends dragging a consumable in the HUD.
		/// </summary>
		public void EndDragConsumable ( )
		{
			// Check for drag
			if ( isDragging )
			{
				// Stop drag
				isDragging = false;
				
				// Hide dragging consumable
				dragDisplay.gameObject.SetActive ( false );

				// Reset consumables
				RefreshConsumables ( );
			}
		}

		/// <summary>
		/// Previews the position of all consumables.
		/// </summary>
		/// <param name="index"> The index of the new position for the dragged consumable. </param>
		public void PreviewConsumables ( int index )
		{
			// Check for drag
			if ( isDragging )
			{
				// Check if moving consumable right
				if ( dragIndex < index )
				{
					// Move each consumable
					for ( int i = 0; i < GameManager.Run.ConsumableData.Length; i++ )
					{
						// Check for unmoved consumables
						if ( i < dragIndex || i > index )
						{
							// Show unmoved consumable
							Consumables.ConsumableScriptableObject consumable = GameManager.Run.ConsumableData [ i ] != null ? Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i ].ID ) : null;
							consumableSlots [ i ].PreviewConsumable ( consumable, consumable != null ? GameManager.Run.ConsumableData [ i ].Count : 0, consumable != null );
						}
						// Check for new position of moved consumable
						else if ( i == index )
						{
							// Show empty space
							consumableSlots [ i ].PreviewConsumable ( null, 0, false );
						}
						else
						{
							// Move consumable left
							Consumables.ConsumableScriptableObject consumable = GameManager.Run.ConsumableData [ i + 1 ] != null ? Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i + 1 ].ID ) : null;
							consumableSlots [ i ].PreviewConsumable ( consumable, consumable != null ? GameManager.Run.ConsumableData [ i + 1 ].Count : 0, consumable != null );
						}
					}
				}
				else
				{
					// Move each consumable
					for ( int i = 0; i < consumableSlots.Length; i++ )
					{
						// Check for unmoved consumable
						if ( i < index || i > dragIndex )
						{
							// Show unmoved consumable
							Consumables.ConsumableScriptableObject consumable = GameManager.Run.ConsumableData [ i ] != null ? Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i ].ID ) : null;
							consumableSlots [ i ].PreviewConsumable ( consumable, consumable != null ? GameManager.Run.ConsumableData [ i ].Count : 0, consumable != null );
						}
						// Check for new position of moved consumable
						else if ( i == index )
						{
							// Show empty space
							consumableSlots [ i ].PreviewConsumable ( null, 0, false );
						}
						else
						{
							// Move consumable right
							Consumables.ConsumableScriptableObject consumable = GameManager.Run.ConsumableData [ i - 1 ] != null ? Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i - 1 ].ID ) : null;
							consumableSlots [ i ].PreviewConsumable ( consumable, consumable != null ? GameManager.Run.ConsumableData [ i - 1 ].Count : 0, consumable != null );
						}
					}
				}
			}
		}

		/// <summary>
		/// Ends the preview of consumables.
		/// </summary>
		public void EndPreview ( )
		{
			// Check for drag
			if ( isDragging )
			{
				// Reset previews
				for ( int i = 0; i < consumableSlots.Length; i++ )
				{
					// Check for dragged consumable
					if ( i == dragIndex )
					{
						// Show empty space
						consumableSlots [ i ].PreviewConsumable ( null, 0, false );
					}
					else
					{
						// Reset preview
						Consumables.ConsumableScriptableObject consumable = GameManager.Run.ConsumableData [ i ] != null ? Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ i ].ID ) : null;
						consumableSlots [ i ].PreviewConsumable ( consumable, consumable != null ? GameManager.Run.ConsumableData [ i ].Count : 0, consumable != null );
					}
				}
			}
		}

		/// <summary>
		/// Drops a consumable into a new position in the HUD.
		/// </summary>
		/// <param name="index"> The index of the new position for the dragged consumable. </param>
		public void DropConsumable ( int index )
		{
			// Check for drag
			if ( isDragging )
			{
				// Move consumable to new positions
				GameManager.Run.RearrangeConsumables ( dragIndex, index );
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// The callback for when consumable display can regain hover.
		/// </summary>
		private void OnRegainHover ( )
		{
			// Reenable hover in each consumable display
			for ( int i = 0; i < consumableSlots.Length; i++ )
			{
				consumableSlots [ i ].RegainHover ( );
			}
		}

		/// <summary>
		/// Applies status effects from a consumable to the player.
		/// </summary>
		/// <param name="index"> The index of the consumable in the array. </param>
		/// <param name="instances"> The number of instances of the consumable to consume. </param>
		private void ApplyStatusEffect ( int index, int instances )
		{
			// Get status effect from the consumable
			StatusEffects.StatusEffectModel model = GameManager.Run.ConsumableData [ index ].Consumable.OnStatusEffects ( instances );

			// Apply status effect
			GameManager.Run.AddStatusEffect ( model.Type, model.Count );

			// Update stats
			GameManager.Run.Stats.OnConsumeConsumable ( GameManager.Run.ConsumableData [ index ].ID, instances );

			// Remove instance of the consumable
			GameManager.Run.RemoveConsumableAtIndex ( index, instances );

			// Update status effect HUD
			statusEffectHUD.SetStatusEffects ( );
		}

		#endregion // Private Functions
	}
}