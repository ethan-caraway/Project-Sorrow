using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the display of a consumable in the shop.
	/// </summary>
	public class ShopConsumableDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ConsumableDisplay consumableDisplay;

		[SerializeField]
		private Button consumableButton;

		[SerializeField]
		private GameObject priceContainer;

		[SerializeField]
		private TMP_Text priceText;

		#endregion // UI Elements

		#region Display Data

		[SerializeField]
		private Color32 canPurchaseColor;

		[SerializeField]
		private Color32 cannotPurchaseColor;

		#endregion // Display Data

		#region Public Functions

		/// <summary>
		/// Sets the display of a consumable in the shop.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="canPurchase"> Whether or not the player can currently purchase the consumable. </param>
		public void SetConsumable ( ShopConsumableModel consumable, bool canPurchase )
		{
			// Display consumable
			consumableDisplay.gameObject.SetActive ( true );
			consumableDisplay.SetConsumable ( consumable.Consumable, 1, true );

			// Set price
			priceContainer.SetActive ( true );
			priceText.text = $"${consumable.Price}";
			RefreshDisplay ( canPurchase );
		}

		/// <summary>
		/// Make no consumable appear in this slot in the shop.
		/// </summary>
		public void HideConsumable ( )
		{
			// Hide consumable
			consumableDisplay.gameObject.SetActive ( false );
		}

		/// <summary>
		/// Updates the price displayed for the consumable.
		/// </summary>
		/// <param name="price"> The new price for the consumable. </param>
		/// <param name="canPurchase"> Whether or not the player can purchase the consumable. </param>
		public void UpdatePrice ( int price, bool canPurchase )
		{
			// Set new price
			priceText.text = $"${price}";
			RefreshDisplay ( canPurchase );
		}

		/// <summary>
		/// Sets the price display to reflect whether or not the player can currently afford the consumable.
		/// </summary>
		/// <param name="canPurchase"> Whether or not the player can purchase the consumable. </param>
		public void RefreshDisplay ( bool canPurchase )
		{
			// Set the button as enabled or disabled based on whether or not the consumable can be purchased
			consumableButton.interactable = canPurchase;

			// Set the color of the price based on whether or not the player can currently purchase the consumable
			priceText.color = canPurchase ? canPurchaseColor : cannotPurchaseColor;
		}

		#endregion // Public Functions
	}
}