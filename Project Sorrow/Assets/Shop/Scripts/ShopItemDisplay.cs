using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the display of an item in the shop.
	/// </summary>
	public class ShopItemDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ItemDisplay itemDisplay;

		[SerializeField]
		private Button itemButton;

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
		/// Sets the display of an item in the shop.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="canPurchase"> Whether or not the player can currently purchase the item. </param>
		public void SetItem ( ShopItemModel item, bool canPurchase )
		{
			// Display item
			itemDisplay.gameObject.SetActive ( true );
			itemDisplay.SetItem ( item.Item, string.Empty, true );

			// Set price
			priceContainer.SetActive ( true );
			priceText.text = $"${item.Price}";
			RefreshDisplay ( canPurchase );
		}

		/// <summary>
		/// Make no item appear in this slot in the shop.
		/// </summary>
		public void HideItem ( )
		{
			// Hide item
			itemDisplay.gameObject.SetActive ( false );
		}

		/// <summary>
		/// Updates the price displayed for the item.
		/// </summary>
		/// <param name="price"> The new price for the item. </param>
		/// <param name="canPurchase"> Whether or not the player can purchase the item. </param>
		public void UpdatePrice ( int price, bool canPurchase )
		{
			// Set new price
			priceText.text = $"${price}";
			RefreshDisplay ( canPurchase );
		}

		/// <summary>
		/// Sets the price display to reflect whether or not the player can currently afford the item.
		/// </summary>
		/// <param name="canPurchase"> Whether or not the player can purchase the item. </param>
		public void RefreshDisplay ( bool canPurchase )
		{
			// Set the button as enabled or disabled based on whether or not the item can be purchased
			itemButton.interactable = canPurchase;

			// Set the color of the price based on whether or not the player can currently purchase the item
			priceText.color = canPurchase ? canPurchaseColor : cannotPurchaseColor;
		}

		#endregion // Public Functions
	}
}