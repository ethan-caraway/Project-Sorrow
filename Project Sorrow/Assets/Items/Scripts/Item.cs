namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the base functionality of an item.
	/// </summary>
	public class Item
	{
		#region Class Constructors

		public Item ( int itemID, string instanceID )
		{
			id = itemID;
			instanceId = instanceID;
		}

		#endregion // Class Constructors

		#region Item Data

		/// <summary>
		/// Whether or not the item is enabled for its effects.
		/// </summary>
		public bool IsEnabled = true;

		private int id;
		private string instanceId;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// The ID of this item.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The ID of this instance of the item.
		/// </summary>
		public string InstanceID
		{
			get
			{
				return instanceId;
			}
		}

		#endregion // Public Properties

		#region Public Stats Functions

		/// <summary>
		/// The callback for when the max confidence is calculated.
		/// </summary>
		/// <param name="current"> The current max confidence. </param>
		/// <returns> The additional max confidence gained from this item. </returns>
		public virtual int OnMaxConfidence ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max arrogance is calculated.
		/// </summary>
		/// <param name="current"> The current max arrogance. </param>
		/// <returns> The additional max arrogance gained from this item. </returns>
		public virtual int OnMaxArrogance ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the time allowance is calculated.
		/// </summary>
		/// <param name="current"> The current time allowance in seconds. </param>
		/// <returns> The additional time allowance gained from this item. </returns>
		public virtual float OnTimeAllowance ( float current )
		{
			return 0f;
		}

		/// <summary>
		/// The callback for when reputation is calculated.
		/// </summary>
		/// <param name="current"> The current reputation. </param>
		/// <returns> The additional reputation gained from this item. </returns>
		public virtual int OnReputation ( int current )
		{
			return 0;
		}

		/// <summary>
		/// Gets the description of the item with the variable value(s) inserted.
		/// </summary>
		/// <param name="description"> The base description of the item. </param>
		/// <returns> The description with the variable value(s). </returns>
		public virtual string GetVariableDescription ( string description )
		{
			return description;
		}

		/// <summary>
		/// Gets the description of the item with the variable value(s) inserted before it is owned.
		/// </summary>
		/// <param name="description"> The base description of the item. </param>
		/// <returns> The description with the variable value(s). </returns>
		public virtual string GetWouldBeVariableDescription ( string description )
		{
			return description;
		}

		/// <summary>
		/// Gets the scale value of an item.
		/// </summary>
		/// <param name="current"> The current scale value of the item. </param>
		/// <returns> The new scale value of the item. </returns>
		public virtual int GetIntScaleValue ( int current )
		{
			return current;
		}

		/// <summary>
		/// Gets the integer scale value of the item before it is owned.
		/// </summary>
		/// <returns> The would be integer scale value of the item. </returns>
		public virtual int GetWouldBeIntScaleValue ( )
		{
			return 0;
		}

		/// <summary>
		/// Gets the float scale value of the item before it is owned.
		/// </summary>
		/// <returns> The would be float scale value of the item. </returns>
		public virtual float GetWouldBeFloatScaleValue ( )
		{
			return 0f;
		}

		/// <summary>
		/// Gets the string scale value of the item before it is owned.
		/// </summary>
		/// <returns> The would be string scale value of the item. </returns>
		public virtual string GetWouldBeStringScaleValue ( )
		{
			return string.Empty;
		}

		#endregion // Public Stats Functions

		#region Public Performance Functions

		/// <summary>
		/// The callback for when a performance is initialized.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		public virtual void OnInitPerformance ( Performance.PerformanceModel model )
		{

		}

		/// <summary>
		/// The callback for applying enhancments to a poem at the start of a performance.
		/// </summary>
		/// <returns> The enhancement data to apply. </returns>
		public virtual Poems.PoemModel OnEnhancePoem ( )
		{
			return null;
		}

		/// <summary>
		/// Gets whether or not this item adds word modifiers at the start of a performance.
		/// </summary>
		/// <returns> Whether or not modifiers are being added. </returns>
		public virtual bool IsModifyingWords ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for getting the word modifiers for the performance.
		/// </summary>
		/// <returns> The list of word modifiers. </returns>
		public virtual Enums.WordModifierType [ ] OnModifyWords ( )
		{
			return null;
		}

		/// <summary>
		/// The callback for when the player's potentially incorrect input is compared to the line.
		/// </summary>
		/// <param name="line"> The character of the line. </param>
		/// <param name="input"> The input character. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnCompareCharacters ( char line, char input )
		{
			return false;
		}

		/// <summary>
		/// The callback for when a character is flubbed.
		/// </summary>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnFlub ( )
		{
			return false;
		}

		/// <summary>
		/// Gets whether or not the flub effect of this item is positive.
		/// </summary>
		/// <returns> Whether or not the effect is positive. </returns>
		public virtual bool IsFlubEffectPositive ( )
		{
			return true;
		}

		/// <summary>
		/// The callback for when confidence is lost from a flub.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnLoseConfidence ( Performance.PerformanceModel model )
		{
			return false;
		}

		/// <summary>
		/// The callback for applying snaps earned for performing a character accurately.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <param name="total"> The current total number of snaps earned for the character. </param>
		/// <param name="modifier"> The modifier applied to the word. </param>
		/// <param name="model"> The data for the performance. </param>
		/// <returns> The number of additional snaps earned for the character. </returns>
		public virtual int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			return 0;
		}

		/// <summary>
		/// The callback for completing a character of a poem.
		/// </summary>
		/// <param name="modifier"> The modifier applied to the character. </param>
		/// <param name="model"> The data for the performance. </param>
		/// <returns> The data for the item being triggered. </returns>
		public virtual ItemTriggerModel OnCharacterComplete ( Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			return null;
		}

		/// <summary>
		/// The callback for completing a word of a poem.
		/// </summary>
		/// <param name="total"> The current total number of snaps earned for the word. </param>
		/// <param name="length"> The length of the word. </param>
		/// <param name="modifier"> The modifier applied to the word. </param>
		/// <param name="model"> The data for the performance. </param>
		/// <returns> The data for the item being triggered. </returns>
		public virtual ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			return null;
		}

		/// <summary>
		/// The callback for completing a line of a poem.
		/// </summary>
		/// <param name="total"> The current total number of snaps earned for the line. </param>
		/// <returns> The data for the item being triggered. </returns>
		public virtual ItemTriggerModel OnLineComplete ( int total )
		{
			return null;
		}

		/// <summary>
		/// The callback for applying snaps earned for completing a stanza of a poem.
		/// </summary>
		/// <param name="model"> The data for the performacne. </param>
		/// <returns> The data for the item being triggered. </returns>
		public virtual ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			return null;
		}

		/// <summary>
		/// The callback for determining if a status effect should expire at the end of a line.
		/// </summary>
		/// <returns> Whether or not the status effect should be prevented from expiring. </returns>
		public virtual bool IsStatusEffectPreventExpire ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when status effects expire at the end of a line.
		/// </summary>
		/// <param name="total"> The total number of status effect stacks that expired. </param>
		/// <returns> Whether or not the item was triggered. </returns>
		public virtual bool OnStatusEffectExpire ( int total )
		{
			return false;
		}

		/// <summary>
		/// The callback for when the Bold modifier triggers to earn $1.
		/// </summary>
		/// <returns> Whether or not the item was triggered. </returns>
		public virtual bool OnBoldTrigger ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for applying applause at the end of a performance.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		/// <param name="total"> The current total number of applause earned. </param>
		/// <returns> The data for a bonus. </returns>
		public virtual Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel ( );
		}

		/// <summary>
		/// The callback for when a performance is completed.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		/// <param name="stats"> The data for displaying the summary. </param>
		public virtual void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{

		}

		/// <summary>
		/// The callback for earning commission at the end of a performance.
		/// </summary>
		/// <param name="commission"> The amount of commission earned. </param>
		/// <returns> Whether or not the item was triggered. </returns>
		public virtual bool OnCommission ( int commission )
		{
			return false;
		}

		/// <summary>
		/// The callback for earning interest at the end of a performance.
		/// </summary>
		/// <param name="interest"> The amount of interest generated. </param>
		/// <returns> Whether or not the item was triggered. </returns>
		public virtual bool OnInterest ( int interest )
		{
			return false;
		}

		/// <summary>
		/// The callback for when a round is completed.
		/// </summary>
		/// <returns> Whether or not the item was triggered. </returns>
		public virtual bool OnCompleteRound ( )
		{
			return false;
		}

		/// <summary>
		/// Gets whether or not the complete round effect of this item is positive.
		/// </summary>
		/// <returns> Whether or not the effect is positive. </returns>
		public virtual bool IsCompleteRoundEffectPositive ( )
		{
			return true;
		}

		#endregion // Public Performance Functions

		#region Public Shop Functions

		/// <summary>
		/// The callback for when the shop is initialized.
		/// </summary>
		/// <param name="model"> The data for the shop. </param>
		public virtual void OnInitShop ( Shop.ShopModel model )
		{

		}

		/// <summary>
		/// The callback for when modifying the prices of items and consumables in the shop.
		/// </summary>
		/// <returns> The percentage to modify prices by. </returns>
		public virtual float OnModifyPrices ( )
		{
			return 1f;
		}

		/// <summary>
		/// The callback for when calculating the chance to generate a loan specificially.
		/// </summary>
		/// <returns> The additional loan chance. </returns>
		public virtual float OnLoanChance ( )
		{
			return 0f;
		}

		/// <summary>
		/// The callback for when items are generated in the shop.
		/// </summary>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnGenerateItems ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when calculating if an item is free in the shop.
		/// </summary>
		/// <returns> Whether or not the item should be free. </returns>
		public virtual bool OnItemBuyPrice ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when calculating if an item should sell for full price.
		/// </summary>
		/// <returns> Whether or not the item should sell for full price. </returns>
		public virtual bool OnItemSellPrice ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when consumables are generated in the shop.
		/// </summary>
		/// <param name="consumables"> The data for the consumables generated. </param>
		/// <param name="totalShopConsumables"> The total number of consumables available in the shop. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnGenerateConsumables ( Shop.ShopConsumableModel [ ] consumables, int totalShopConsumables )
		{
			return false;
		}

		/// <summary>
		/// The callback for when calculating if a consumable is free in the shop.
		/// </summary>
		/// <returns> Whether or not the consumable should be free. </returns>
		public virtual bool OnConsumableBuyPrice ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when calculating if a consumable should sell for full price.
		/// </summary>
		/// <returns> Whether or not the consumable should sell for full price. </returns>
		public virtual bool OnConsumableSellPrice ( )
		{
			return false;
		}

		/// <summary>
		/// The callback for when calculating the minimum budget for spending.
		/// </summary>
		/// <param name="current"> The current minimum budget. </param>
		/// <returns> The addition to the minimum budget. </returns>
		public virtual int OnMinBudget ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when any item is purchased.
		/// </summary>
		/// <param name="item"> The data for the item being purchased. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnPurchaseItem ( ItemScriptableObject item )
		{
			return false;
		}

		/// <summary>
		/// The callback for when any consumable is purchased.
		/// </summary>
		/// <param name="consumable"> The data for the consumable being purchased. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnPurchaseConsumable ( Consumables.ConsumableScriptableObject consumable )
		{
			return false;
		}

		/// <summary>
		/// The callback for when any purchase is made in the shop.
		/// </summary>
		/// <param name="money"> The amount of money by the player at time of purchase. </param>
		/// <param name="price"> The price of the purchase. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnPurchase ( int money, int price )
		{
			return false;
		}

		/// <summary>
		/// Gets whether or not the purchase effect of this item is positive.
		/// </summary>
		/// <returns> Whether or not the effect is positive. </returns>
		public virtual bool IsPurchaseEffectPositive ( )
		{
			return true;
		}

		/// <summary>
		/// The callback for when this item is added.
		/// </summary>
		/// <param name="model"> The data for the shop. </param>
		public virtual void OnAdd ( Shop.ShopModel model )
		{

		}

		/// <summary>
		/// The callback for when any item is added.
		/// </summary>
		public virtual void OnAddAnyItem ( )
		{

		}

		/// <summary>
		/// The callback for when any consumable is added.
		/// </summary>
		/// <param name="count"> The number of instances of the consumable being added. </param>
		public virtual void OnAddAnyConsumable ( int count )
		{

		}

		/// <summary>
		/// The callback for when anything is sold.
		/// </summary>
		/// <param name="instances"> The number of instances of items or consumables sold. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnSell ( int instances )
		{
			return false;
		}

		/// <summary>
		/// The callback for when this item is removed.
		/// </summary>
		/// <param name="model"> The data for the shop. </param>
		/// <returns> The amount of additional money earned when removing this item. </returns>
		public virtual int OnRemove ( Shop.ShopModel model )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when any item is removed.
		/// </summary>
		public virtual void OnRemoveAnyItem ( )
		{

		}

		/// <summary>
		/// The callback for when any consumable is removed.
		/// </summary>
		public virtual void OnRemoveAnyConsumable ( )
		{

		}

		/// <summary>
		/// The callback for when the shop is rerolled.
		/// </summary>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual bool OnReroll ( )
		{
			return false;
		}

		#endregion // Public Shop Functions
	}
}