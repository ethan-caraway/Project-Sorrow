using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of a consumable slot in the HUD.
	/// </summary>
	public class ConsumableSlot : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private ConsumableDisplay consumableDisplay;

		[SerializeField]
		private GameObject consumableButton;

		[SerializeField]
		private GameObject applyButton;

		[SerializeField]
		private GameObject applyAllButton;

		[SerializeField]
		private GameObject sellButton;

		[SerializeField]
		private TMP_Text sellText;

		[SerializeField]
		private GameObject sellAllButton;

		[SerializeField]
		private TMP_Text sellAllText;

		[SerializeField]
		private GameObject inputFieldOverride;

		#endregion // UI Elements

		#region Consumable Data

		[SerializeField]
		private Color32 emptyContainerColor;

		[SerializeField]
		private Color32 highlightColor;

		[SerializeField]
		private bool canSell;

		private bool canApplyToPoem = false;

		private Consumables.ConsumableScriptableObject currentConsumable;
		private int currentCount;
		private bool isFocused;
		private System.Action onStopFocusPrevention;

		#endregion // Consumable Data

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

					// Only hide the buttons if it isn't actively being clicked as it will be hidden later
					if ( EventSystem.current.currentSelectedGameObject != sellButton && 
						EventSystem.current.currentSelectedGameObject != sellAllButton &&
						EventSystem.current.currentSelectedGameObject != applyButton && 
						EventSystem.current.currentSelectedGameObject != applyAllButton )
					{
						// Hide sell button
						sellButton.SetActive ( false );

						// Hide sell all button
						sellAllButton.SetActive ( false );

						// Hide info
						HideInfo ( );
					}
				}
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Sets the display of an consumable in the HUD.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="count"> The number of instances of the consumable. </param>
		/// <param name="isEnabled"> Whether or not the consumable is enabled. </param>
		/// <param name="sellPrice"> The price the consumable is sold for. </param>
		public void SetConsumable ( Consumables.ConsumableScriptableObject consumable, int count, bool isEnabled, int sellPrice )
		{
			// Hide apply buttons
			applyButton.SetActive ( false );
			applyAllButton.SetActive ( false );

			// Set consumable
			consumableDisplay.SetConsumable ( consumable, count, isEnabled );
			currentConsumable = consumable;
			currentCount = count;

			// Display price
			UpdatePrice ( sellPrice );

			// Set interaction of the button
			consumableButton.gameObject.SetActive ( consumable != null );

			// Set as not focused
			isFocused = false;
		}

		/// <summary>
		/// Updates the sell price of the consumable
		/// </summary>
		/// <param name="sellPrice"> The price the consumable is sold for. </param>
		public void UpdatePrice ( int sellPrice )
		{
			// Set sell button
			sellButton.SetActive ( false );
			sellText.text = $"Sell ${sellPrice:N0}";

			// Set sell all button
			sellAllButton.SetActive ( false );
			sellAllText.text = $"Sell All ${( sellPrice * currentCount ):N0}";
		}

		/// <summary>
		/// Toggles whether or not the consumable can currently be applied.
		/// </summary>
		/// <param name="canApply"> Whether or not the consumable can be applied. </param>
		public void ToggleApplyToPoem ( bool canApply )
		{
			// Store whether or not the consumable can be applied
			canApplyToPoem = canApply;
		}

		/// <summary>
		/// Displays the sell button on click.
		/// </summary>
		public void ShowButtons ( )
		{
			// Set focus
			isFocused = true;

			// Show apply button
			applyButton.SetActive ( canApplyToPoem || currentConsumable.Type == Enums.ConsumableType.STATUS_EFFECT );

			// Show apply all button
			applyAllButton.SetActive ( ( canApplyToPoem || currentConsumable.Type == Enums.ConsumableType.STATUS_EFFECT ) && currentCount > 1 );

			// Show sell button
			sellButton.SetActive ( canSell );

			// Show sell all button
			sellAllButton.SetActive ( canSell && currentCount > 1 );

			// Force display info
			consumableDisplay.ShowInfo ( true );
		}

		/// <summary>
		/// Hides the information panel when no longer hovering.
		/// </summary>
		public void HideInfo ( )
		{
			// Hide info
			consumableDisplay.HideInfo ( );

			// Hide apply button
			applyButton.SetActive ( false );

			// Hide apply all button
			applyAllButton.SetActive ( false );

			// Hide sell button
			sellButton.SetActive ( false );

			// Hide sell all button
			sellAllButton.SetActive ( false );
		}

		/// <summary>
		/// Prevents showing the information panel when one consumable display is focused.
		/// </summary>
		/// <param name="onStopPrevention"> The callback when the consumable display no longer has focus. </param>
		public void PreventHover ( System.Action onStopPrevention )
		{
			// Prevent hover
			consumableDisplay.ToggleCanHover ( false );

			// Store callback
			onStopFocusPrevention = onStopPrevention;
		}

		/// <summary>
		/// Regains the ability to show the information panel on hover.
		/// </summary>
		public void RegainHover ( )
		{
			// Regain hover
			consumableDisplay.ToggleCanHover ( true );
			onStopFocusPrevention = null;
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
			consumableDisplay.PreviewConsumable ( consumable, count, isEnabled );
		}

		/// <summary>
		/// Highlights a consumable being used.
		/// </summary>
		public void HighlightConsumable ( )
		{
			// Animate consumable
			consumableDisplay.HighlightConsumable ( );
		}

		/// <summary>
		/// Dissolves the consumable in the HUD.
		/// </summary>
		/// <param name="isFullDissolve"> Whether or not the entire consumable will dissolve or just partially. </param>
		/// <param name="onComplete"> The callback for when the animation completes. </param>
		public void DissolveConsumable ( bool isFullDissolve, System.Action onComplete )
		{
			// Animate consumable
			consumableDisplay.DissolveConsumable ( isFullDissolve, onComplete );
		}

		/// <summary>
		/// Updates the number of instances of the consumable.
		/// </summary>
		/// <param name="count"> The number of instances of the consumable. </param>
		/// <param name="sellPrice"> The price the consumable is sold for. </param>
		public void UpdateCount ( int count, int sellPrice )
		{
			// Set count
			currentCount = count;
			consumableDisplay.UpdateCount ( count );

			// Set sell all button
			sellAllText.text = $"Sell All ${( sellPrice * count ):N0}";
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Check if focus of this consumable slot has been lost to a different element.
		/// </summary>
		/// <returns> Whether or not the focus has been lost. </returns>
		private bool IsFocusLost ( )
		{
			// Check for input field during performances
			if ( inputFieldOverride != null )
			{
				// Check for focus a different element from this slot or the input field override
				return EventSystem.current.currentSelectedGameObject != consumableButton.gameObject && EventSystem.current.currentSelectedGameObject != inputFieldOverride;
			}
			else
			{
				// Check for focus on a different element from this slot
				return EventSystem.current.currentSelectedGameObject != consumableButton.gameObject;
			}
		}

		#endregion // Private Functions
	}
}