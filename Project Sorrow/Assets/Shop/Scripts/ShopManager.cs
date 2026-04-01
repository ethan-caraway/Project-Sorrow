using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class controls the setup and progression of the shop.
	/// </summary>
	public class ShopManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ConfidenceHUD confidenceHUD;

		[SerializeField]
		private HUD.MoneyHUD moneyHUD;

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private HUD.StatusEffectHUD statusEffectHUD;

		[SerializeField]
		private HUD.PoetHUD poetHUD;

		[SerializeField]
		private HUD.TimerHUD timerHUD;

		[SerializeField]
		private HUD.ItemsHUD itemsHUD;

		[SerializeField]
		private GameObject shopContainer;

		[SerializeField]
		private ShopItemDisplay [ ] shopItemDisplays;

		[SerializeField]
		private ShopConsumableDisplay [ ] shopConsumableDisplays;

		[SerializeField]
		private Button rerollButton;

		[SerializeField]
		private TMP_Text rerollPriceText;

		[SerializeField]
		private RewardsManager rewardsManager;

		[SerializeField]
		private GameObject rewardsContainer;

		#endregion // UI Elements

		#region Shop Data

		[SerializeField]
		private Color32 canAffordColor;

		[SerializeField]
		private Color32 cannotAffordColor;

		[SerializeField]
		private Items.ItemScriptableObject forceItem;

		[SerializeField]
		private Consumables.ConsumableScriptableObject forceConsumable;

		private ShopModel model;
		private ShopItemModel [ ] items;
		private ShopConsumableModel [ ] consumables;

		private bool isRewardsScreen = false;

		private int rerollPrice;

		#endregion // Shop Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Set up the shop
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Purchases an item on sale in the shop.
		/// </summary>
		/// <param name="index"> The index of the item slot being purchased. </param>
		public void PurchaseItem ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= items.Length )
			{
				return;
			}

			// Get item
			Items.ItemScriptableObject item = items [ index ].Item;

			// Trigger any on purchase effects for items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnPurchaseItem ( item ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}

			// Store new item
			Items.ItemModel itemData = GameManager.Run.AddItem ( item.ID );

			// Hide item from shop
			shopItemDisplays [ index ].HideItem ( );

			// Get item price in case prices get updated by items
			int price = items [ index ].Price;

			// Trigger item being added
			if ( itemData != null && itemData.IsValid ( ) )
			{
				itemData.Item.OnAdd ( model );
			}

			// Update prices
			SetPrices ( );

			// Update display of owned items
			itemsHUD.SetItems ( model.Prices, OnSellItem );

			// Update sell price displays for consumables
			consumablesHUD.SetConsumables ( model.Prices, OnSellConsumable, OnSellAllConsumable );

			// Trigger that a purchase has been made
			OnPurchase ( price );
		}

		/// <summary>
		/// Purchases a consumable on sale in the shop.
		/// </summary>
		/// <param name="index"> The index of the consumable slot being purchased. </param>
		public void PurchaseConsumable ( int index )
		{
			// Check for valid index
			if ( index < 0 || index >= consumables.Length )
			{
				return;
			}

			// Get consumable
			Consumables.ConsumableScriptableObject consumable = consumables [ index ].Consumable;

			// Trigger any on purchase effects for items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnPurchaseConsumable ( consumable ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}

			// Get consumable price
			int price = 0;

			// Check if consumable is a loan
			if ( consumable.Type == Enums.ConsumableType.LOAN )
			{
				// Trigger loan
				Consumables.Consumable consumableData = Consumables.ConsumableHelper.GetConsumable ( consumable.ID );
				if ( consumableData != null )
				{
					// Get money from loan
					int money = consumableData.GetLoanMoney ( );

					// Gain money
					GameManager.Run.Money += money;
					moneyHUD.AddMoney ( money );

					// Get debt from loan
					int debt = consumableData.GetLoanDebt ( );

					// Gain debt
					GameManager.Run.Debt += debt;
					moneyHUD.AddDebt ( debt );

					// Update stats
					GameManager.Run.Stats.OnAddConsumable ( consumable.ID, 1 );
					GameManager.Run.Stats.OnConsumeConsumable ( consumable.ID, 1 );
				}
			}
			else
			{
				// Store new consumable
				GameManager.Run.AddConsumable ( consumable.ID, 1 );

				// Get price
				price = consumables [ index ].Price;

				// Update display of owned consumables
				consumablesHUD.RefreshConsumables ( );
			}

			// Hide consumable from shop
			shopConsumableDisplays [ index ].HideConsumable ( );

			// Trigger that a purchase has been made
			OnPurchase ( price );
		}

		/// <summary>
		/// Rerolls the items in the shop.
		/// </summary>
		public void Reroll ( )
		{
			// Store price of reroll
			int price = rerollPrice;

			// Increase reroll price
			rerollPrice += model.RerollPriceIncrement;

			// Trigger any on reroll effects for items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnReroll ( ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}

			// Reroll the items in the shop
			RollItems ( );

			// Reroll the consumables in the shop
			RollConsumables ( );

			// Trigger that a purchase has been made
			OnPurchase ( price );
		}

		/// <summary>
		/// Closes the rewards screens and opens the shop screen.
		/// </summary>
		public void CloseRewards ( )
		{
			// End rewards
			isRewardsScreen = false;

			// Get shop data
			GetShopModel ( );

			// Display confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Display consumables
			consumablesHUD.SetConsumables ( model.Prices, OnSellConsumable, OnSellAllConsumable );

			// Display timer
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Display owned items
			itemsHUD.SetItems ( model.Prices, OnSellItem );

			// Display shop
			shopContainer.SetActive ( true );
			rewardsContainer.SetActive ( false );

			// Display items for sale
			RollItems ( );

			// Display consumables for sale
			RollConsumables ( );

			// Set reroll
			rerollPrice = model.RerollStartPrice;
			SetReroll ( );
		}
		
		/// <summary>
		/// Exits the shop for the next performance.
		/// </summary>
		public void NextPerformance ( )
		{
			// Load performance scene
			SceneManager.LoadScene ( GameManager.SETLIST_SCENE );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Initializes the shop.
		/// </summary>
		private void Init ( )
		{
			// Save game
			GameManager.Run.Checkpoint = GameManager.SHOP_SCENE;
			Memory.MemoryManager.Save ( );

			// Get shop data
			GetShopModel ( );

			// Display confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Display money
			moneyHUD.SetMoney ( GameManager.Run.Money, GameManager.Run.Debt );

			// Display consumables
			consumablesHUD.SetConsumables ( model.Prices, OnSellConsumable, OnSellAllConsumable );

			// Display status effects
			statusEffectHUD.SetStatusEffects ( );

			// Setup poet
			poetHUD.SetPoet ( Poets.PoetUtility.GetPoet ( GameManager.Run.PoetID ) );

			// Display timer
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Display owned items
			itemsHUD.SetItems ( model.Prices, OnSellItem );

			// Check for rewards
			if ( GameManager.Run.Performance == 0 )
			{
				// Display rewards
				isRewardsScreen = true;
				shopContainer.SetActive ( false );
				rewardsContainer.SetActive ( true );
				rewardsManager.Init ( );
			}
			else
			{
				// Display shop
				shopContainer.SetActive ( true );
				rewardsContainer.SetActive ( false );

				// Display items for sale
				RollItems ( );

				// Display consumables for sale
				RollConsumables ( );

				// Set reroll
				rerollPrice = model.RerollStartPrice;
				SetReroll ( );
			}
		}

		/// <summary>
		/// Initializes the shop data.
		/// </summary>
		private void GetShopModel ( )
		{
			// Get default shop data
			model = new ShopModel ( );

			// Set prices for shop
			SetPrices ( );

			// Trigger any upgrade effects
			for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( GameManager.Run.IsValidUpgrade ( i ) )
				{
					// Trigger upgrade
					GameManager.Run.UpgradeData [ i ].Upgrade.OnInitShop ( model );
				}
			}

			// Trigger any item effects
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Trigger item
					GameManager.Run.ItemData [ i ].Item.OnInitShop ( model );
				}
			}
		}

		/// <summary>
		/// Sets the prices for the items and consumables in the shop.
		/// </summary>
		private void SetPrices ( )
		{
			// Reset prices
			model.Prices = new PriceModel ( );

			// Trigger price modifying effect from perk
			float perkModifier = GameManager.Run.Perk.OnModifyPrices ( );
			if ( perkModifier != 1f )
			{
				model.Prices.ModifyPrices ( perkModifier );
			}

			// Trigger price modifying effects from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Get price modifier
					float itemModifier = GameManager.Run.ItemData [ i ].Item.OnModifyPrices ( );

					// Check for value other than 100%
					if ( itemModifier != 1f )
					{
						// Update prices
						model.Prices.ModifyPrices ( itemModifier );
					}
				}
			}
		}

		/// <summary>
		/// Generates the items for sale in the shop.
		/// </summary>
		/// <returns> The list of items for sale. </returns>
		private void RollItems ( )
		{
			// Store items
			items = new ShopItemModel [ model.ItemCount ];

			// Store items to exclude
			List<int> excludeIDs = new List<int> ( );

			// Check if duplicate items can appear
			bool canDuplicate = false;
			for ( int i = 0; i < GameManager.Run.UpgradeData.Length; i++ )
			{
				// Check for upgrade
				if ( GameManager.Run.IsValidUpgrade ( i ) )
				{
					// Trigger upgrade
					if ( GameManager.Run.UpgradeData [ i ].Upgrade.OnDuplicateItems ( ) )
					{
						// Store that duplicates can appear
						canDuplicate = true;
						break;
					}
				}
			}

			// Check if excludes need to be added
			if ( !canDuplicate )
			{
				// Exclude owned items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.ItemData [ i ] != null )
					{
						excludeIDs.Add ( GameManager.Run.ItemData [ i ].ID );
					}
				}
			}

			// Check if there the player has room to purchase more items
			bool hasRoomForItems = GameManager.Run.CanAddItem ( );

			// Get rarity data
			RarityModel rarity = GameManager.Run.RarityData;

			// Get and display each item on sale
			for ( int i = 0; i < shopItemDisplays.Length; i++ )
			{
				// Display item
				shopItemDisplays [ i ].gameObject.SetActive ( i < items.Length );

				// Check item count
				if ( i < items.Length )
				{
					// HACK: Force item to appear in shop
					if ( i == 0 && forceItem != null && !GameManager.Run.HasItem ( forceItem.ID ) )
					{
						// Store item
						items [ i ] = new ShopItemModel
						{
							Item = forceItem,
							Price = GetItemPrice ( forceItem.Rarity, true )
						};

						// Add item to list of excludes
						if ( !canDuplicate )
						{
							excludeIDs.Add ( forceItem.ID );
						}

						// Display item
						shopItemDisplays [ i ].SetItem ( items [ i ], hasRoomForItems && GameManager.Run.CanPlayerAffordPrice ( items [ i ].Price ) );

						// Skip to next item
						continue;
					}

					// Get a random item
					Items.ItemScriptableObject item = rarity.GenerateItem ( excludeIDs.ToArray ( ) );

					// Store item
					items [ i ] = new ShopItemModel
					{
						Item = item,
						Price = GetItemPrice ( item.Rarity, true )
					};

					// Check for item
					if ( item != null )
					{
						// Add item to list of excludes
						if ( !canDuplicate )
						{
							excludeIDs.Add ( item.ID );
						}

						// Display item
						shopItemDisplays [ i ].SetItem ( items [ i ], hasRoomForItems && GameManager.Run.CanPlayerAffordPrice ( items [ i ].Price ) );
					}
					else
					{
						// Hide item slot
						shopItemDisplays [ i ].HideItem ( );
					}
				}
			}

			// Trigger any on generate item effects
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnGenerateItems ( ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}
		}

		/// <summary>
		/// Gets the price for a generated item.
		/// </summary>
		/// <param name="rarity"> The rarity of the item. </param>
		/// <param name="canBeFree"> Whether or not the item can be free. </param>
		/// <returns> The price of the item. </returns>
		private int GetItemPrice ( Enums.Rarity rarity, bool canBeFree )
		{
			// Track if item should be free
			bool isFree = false;

			// Check if the item can be free
			if ( canBeFree )
			{
				// Check for item price effects from items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) )
					{
						// Check if item should be free
						if ( GameManager.Run.ItemData [ i ].Item.OnItemBuyPrice ( ) )
						{
							// Mark item as free
							isFree = true;

							// Highlight item
							itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );

							// End search
							break;
						}
					}
				}
			}

			// Return price
			return isFree ? 0 : model.Prices.GetItemPrice ( rarity );
		}

		/// <summary>
		/// Generates the consumables for sale in the shop.
		/// </summary>
		/// <returns> The list of consumables for sale. </returns>
		private void RollConsumables ( )
		{
			// Store consumables
			consumables = new ShopConsumableModel [ model.ConsumableCount ];

			// Get rarity data
			RarityModel rarity = GameManager.Run.RarityData;

			// Get and display each consumable on sale
			for ( int i = 0; i < shopConsumableDisplays.Length; i++ )
			{
				// Display consumable
				shopConsumableDisplays [ i ].gameObject.SetActive ( i < consumables.Length );

				// Check consumable count
				if ( i < consumables.Length )
				{
					// HACK: Force consumable to appear in shop
					if ( i == 0 && forceConsumable != null )
					{
						// Store consumable
						consumables [ i ] = new ShopConsumableModel
						{
							Consumable = forceConsumable,
							Price = forceConsumable.Type == Enums.ConsumableType.LOAN ? 0 : GetConsumablePrice ( forceConsumable.Rarity, true )
						};

						// Check if an instance of this consumable can be added
						bool canAddConsumable = GameManager.Run.CanAddConsumable ( ) || GameManager.Run.HasConsumable ( forceConsumable.ID );

						// Display consumable
						shopConsumableDisplays [ i ].SetConsumable ( consumables [ i ], canAddConsumable && GameManager.Run.CanPlayerAffordPrice ( consumables [ i ].Price ) );

						// Skip to next consumable
						continue;
					}

					// Get loan chance
					float loanChance = 0f;

					// Trigger any on loan chance item effects
					for ( int j = 0; j < GameManager.Run.ItemData.Length; j++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( j ) )
						{
							// Apply additional loan chance
							loanChance += GameManager.Run.ItemData [ j ].Item.OnLoanChance ( );
						}
					}

					// Get a random consumable
					Consumables.ConsumableScriptableObject consumable = rarity.GenerateConsumable ( loanChance );

					// Store consumable
					consumables [ i ] = new ShopConsumableModel
					{
						Consumable = consumable,
						Price = consumable.Type == Enums.ConsumableType.LOAN ? 0 : GetConsumablePrice ( consumable.Rarity, true )
					};

					// Check for consumable
					if ( consumable != null )
					{
						// Check if an instance of this consumable can be added
						bool canAddConsumable = GameManager.Run.CanAddConsumable ( ) || GameManager.Run.HasConsumable ( consumable.ID );

						// Display consumable
						shopConsumableDisplays [ i ].SetConsumable ( consumables [ i ], canAddConsumable && GameManager.Run.CanPlayerAffordPrice ( consumables [ i ].Price ) );
					}
					else
					{
						// Hide consumable slot
						shopConsumableDisplays [ i ].HideConsumable ( );
					}
				}
			}

			// Trigger any on generate consumable effects
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnGenerateConsumables ( consumables, model.ConsumableCount ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}
		}

		/// <summary>
		/// Gets the price for a generated consumable.
		/// </summary>
		/// <param name="rarity"> The rarity of the consumable. </param>
		/// <param name="canBeFree"> Whether or not the consumable can be free. </param>
		/// <returns> The price of the consumable. </returns>
		private int GetConsumablePrice ( Enums.Rarity rarity, bool canBeFree )
		{
			// Track if consumable should be free
			bool isFree = false;

			// Check if the consumable can be free
			if ( canBeFree )
			{
				// Check for consumable price effects from items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) )
					{
						// Check if consumable should be free
						if ( GameManager.Run.ItemData [ i ].Item.OnConsumableBuyPrice ( ) )
						{
							// Mark consumable as free
							isFree = true;

							// Highlight item
							itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );

							// End search
							break;
						}
					}
				}
			}

			// Return price
			return isFree ? 0 : model.Prices.GetConsumablePrice ( rarity );
		}

		/// <summary>
		/// Sets the display of the reroll button
		/// </summary>
		private void SetReroll ( )
		{
			// Display reroll price
			rerollPriceText.text = $"${rerollPrice:N0}";

			// Check if the player can afford to reroll
			if ( GameManager.Run.CanPlayerAffordPrice ( rerollPrice ) )
			{
				// Display price color
				rerollPriceText.color = canAffordColor;

				// Enable reroll button
				rerollButton.interactable = true;
			}
			else
			{
				// Display price color
				rerollPriceText.color = cannotAffordColor;

				// Disable reroll button
				rerollButton.interactable = false;
			}
		}

		/// <summary>
		/// The callback for when a purchase is made in the shop.
		/// </summary>
		/// <param name="price"> The price of the purchase. </param>
		private void OnPurchase ( int price )
		{
			// Store money
			int money = GameManager.Run.Money;

			// Update money
			moneyHUD.ApplyMoney ( price * -1, GameManager.Run.Money, GameManager.Run.Debt );
			GameManager.Run.ApplyMoney ( price * -1 );

			// Check for any item triggers
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for on purchase trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnPurchase ( money, price ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, GameManager.Run.ItemData [ i ].Item.IsPurchaseEffectPositive ( ) );
					}
				}
			}

			// Update display
			Refresh ( );
		}

		/// <summary>
		/// The callback for when an item is sold.
		/// </summary>
		/// <param name="index"> The index of the item in the data array. </param>
		private void OnSellItem ( int index )
		{
			// Get item being sold
			Items.ItemScriptableObject item = Items.ItemUtility.GetItem ( GameManager.Run.ItemData [ index ].ID );

			// Check for item
			if ( item != null )
			{
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

				// Get sell price
				int price = model.Prices.GetItemSellPrice ( item.Rarity, isFullPrice );

				// Trigger item being removed
				if ( GameManager.Run.IsValidItem ( index ) )
				{
					price += GameManager.Run.ItemData [ index ].Item.OnRemove ( model );
				}
				
				// Remove item
				GameManager.Run.RemoveItemAtIndex ( index );

				// Update prices
				SetPrices ( );

				// Update sell price displays for items
				itemsHUD.UpdatePrices ( model.Prices );

				// Update sell price displays for consumables
				consumablesHUD.SetConsumables ( model.Prices, OnSellConsumable, OnSellAllConsumable );

				// Trigger that sell as been made
				OnSell ( price, 1 );
			}
		}

		/// <summary>
		/// The callback for when a consumable is sold.
		/// </summary>
		/// <param name="index"> The index of the consumable in the data array. </param>
		private void OnSellConsumable ( int index )
		{
			// Get consumable being sold
			Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID );

			// Check for consumable
			if ( consumable != null )
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

				// Get sell price
				int price = model.Prices.GetConsumableSellPrice ( consumable.Rarity, isFullPrice );

				// Remove item
				GameManager.Run.RemoveConsumableAtIndex ( index, 1 );

				// Trigger that sell as been made
				OnSell ( price, 1 );
			}
		}

		/// <summary>
		/// The callback for when all instances of a consumable are sold.
		/// </summary>
		/// <param name="index"> The index of the consumable in the data array. </param>
		private void OnSellAllConsumable ( int index )
		{
			// Get consumable being sold
			Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID );

			// Check for consumable
			if ( consumable != null )
			{
				// Get the number of instances
				int count = GameManager.Run.ConsumableData [ index ].Count;

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

				// Get sell price
				int price = model.Prices.GetConsumableSellPrice ( consumable.Rarity, isFullPrice ) * count;

				// Remove item
				GameManager.Run.RemoveConsumableAtIndex ( index, count );

				// Trigger that sell as been made
				OnSell ( price, count );
			}
		}

		/// <summary>
		/// The callback for when anything owned is sold in the shop.
		/// </summary>
		/// <param name="price"> The price of the sell. </param>
		/// <param name="instances"> The number of instances of items or consumables sold. </param>
		private void OnSell ( int price, int instances )
		{
			// Trigger any on sell effects from items
			for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
			{
				// Check for item
				if ( GameManager.Run.IsValidItem ( i ) )
				{
					// Check for item trigger
					if ( GameManager.Run.ItemData [ i ].Item.OnSell ( instances ) )
					{
						// Highlight item
						itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
					}
				}
			}

			// Update money
			moneyHUD.ApplyMoney ( price, GameManager.Run.Money, GameManager.Run.Debt );
			GameManager.Run.ApplyMoney ( price );

			// Update sell price displays for consumables
			consumablesHUD.UpdatePrices ( model.Prices );

			// Update display
			Refresh ( );
		}

		/// <summary>
		/// Refreshes the HUD and UI of the shop.
		/// </summary>
		private void Refresh ( )
		{
			// Check for shop
			if ( !isRewardsScreen )
			{
				// Check if there the player has room to purchase more items
				bool hasRoomForItems = GameManager.Run.CanAddItem ( );

				// Refresh available items
				for ( int i = 0; i < items.Length; i++ )
				{
					// Check for item
					if ( items [ i ] != null )
					{
						// Update item price
						if ( items [ i ].Price > 0 )
						{
							items [ i ].Price = GetItemPrice ( items [ i ].Item.Rarity, false );
						}

						// Get whether or not the player can afford the item
						bool canAfford = GameManager.Run.CanPlayerAffordPrice ( items [ i ].Price );

						// Update display
						shopItemDisplays [ i ].UpdatePrice ( items [ i ].Price, hasRoomForItems && canAfford );
					}
				}

				// Refresh available consumables
				for ( int i = 0; i < consumables.Length; i++ )
				{
					// Check for consumables
					if ( consumables [ i ] != null )
					{
						// Update consumable price
						if ( consumables [ i ].Price > 0 )
						{
							consumables [ i ].Price = GetConsumablePrice ( consumables [ i ].Consumable.Rarity, false );
						}

						// Check if an instance of this consumable can be added
						bool canAddConsumable = GameManager.Run.CanAddConsumable ( ) || GameManager.Run.HasConsumable ( consumables [ i ].Consumable.ID );

						// Update display
						shopConsumableDisplays [ i ].UpdatePrice ( consumables [ i ].Price, canAddConsumable && GameManager.Run.CanPlayerAffordPrice ( consumables [ i ].Price ) );
					}
				}

				// Update reroll button
				SetReroll ( );
			}

			// Update confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Update time
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Update status effects
			statusEffectHUD.SetStatusEffects ( );
		}

		#endregion // Private Functions
	}
}