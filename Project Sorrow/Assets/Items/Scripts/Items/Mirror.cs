namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Mirror item.
	/// </summary>
	public class Mirror : Item
	{
		#region Class Constructors

		public Mirror ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnMaxConfidence ( int current )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnMaxConfidence ( current );
			}

			// Apply no affect
			return base.OnMaxConfidence ( current );
		}

		public override int OnMaxArrogance ( int current )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnMaxArrogance ( current );
			}

			// Apply no affect
			return base.OnMaxArrogance ( current );
		}

		public override float OnTimeAllowance ( float current )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnTimeAllowance ( current );
			}

			// Apply no affect
			return base.OnTimeAllowance ( current );
		}

		public override int OnReputation ( int current )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnReputation ( current );
			}

			// Apply no affect
			return base.OnReputation ( current );
		}

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnInitPerformance ( model );
			}
		}

		public override Poems.PoemModel OnEnhancePoem ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnEnhancePoem ( );
			}

			// Apply no affect
			return base.OnEnhancePoem ( );
		}

		public override bool IsModifyingWords ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.IsModifyingWords ( );
			}

			// Apply no affect
			return base.IsModifyingWords ( );
		}

		public override Enums.WordModifierType [ ] OnModifyWords ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnModifyWords ( );
			}

			// Apply no affect
			return base.OnModifyWords ( );
		}

		public override bool OnCompareCharacters ( char line, char input )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnCompareCharacters ( line, input );
			}

			// Apply no affect
			return base.OnCompareCharacters ( line, input );
		}

		public override bool OnFlub ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnFlub ( );
			}

			// Apply no affect
			return base.OnFlub ( );
		}

		public override bool IsFlubEffectPositive ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.IsFlubEffectPositive ( );
			}

			// Apply no affect
			return base.IsFlubEffectPositive ( );
		}

		public override bool OnLoseConfidence ( Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnLoseConfidence ( model );
			}

			// Apply no affect
			return base.OnLoseConfidence ( model );
		}

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnScoreCharacter ( text, total, modifier, model );
			}

			// Apply no affect
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		public override ItemTriggerModel OnCharacterComplete ( Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnCharacterComplete ( modifier, model );
			}

			// Apply no affect
			return base.OnCharacterComplete ( modifier, model );
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnWordComplete ( total, length, modifier, model );
			}

			// Apply no affect
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnLineComplete ( total );
			}

			// Apply no affect
			return base.OnLineComplete ( total );
		}

		public override ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnStanzaComplete ( model );
			}

			// Apply no affect
			return base.OnStanzaComplete ( model );
		}

		public override bool OnBoldTrigger ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnBoldTrigger ( );
			}

			// Apply no affect
			return base.OnBoldTrigger ( );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = GameManager.Run.ItemData [ 0 ].Item.OnApplause ( model, total ).Applause
				};
			}

			// Apply no affect
			return base.OnApplause ( model, total );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnCompletePerformance ( model, stats );
			}
		}

		public override bool OnCommission ( int commission )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnCommission ( commission );
			}

			// Apply no affect
			return base.OnCommission ( commission );
		}

		public override bool OnInterest ( int interest )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnInterest ( interest );
			}

			// Apply no affect
			return base.OnInterest ( interest );
		}

		public override bool OnCompleteRound ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnCompleteRound ( );
			}

			// Apply no affect
			return base.OnCompleteRound ( );
		}

		public override bool IsCompleteRoundEffectPositive ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.IsCompleteRoundEffectPositive ( );
			}

			// Apply no affect
			return base.IsCompleteRoundEffectPositive ( );
		}

		public override void OnInitShop ( Shop.ShopModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnInitShop ( model );
			}
		}

		public override float OnModifyPrices ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnModifyPrices ( );
			}

			// Apply no affect
			return base.OnModifyPrices ( );
		}

		public override float OnLoanChance ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnLoanChance ( );
			}

			// Apply no affect
			return base.OnLoanChance ( );
		}

		public override bool OnGenerateItems ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnGenerateItems ( );
			}

			// Apply no affect
			return base.OnGenerateItems ( );
		}

		public override bool OnItemBuyPrice ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnItemBuyPrice ( );
			}

			// Apply no affect
			return base.OnItemBuyPrice ( );
		}

		public override bool OnItemSellPrice ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnItemSellPrice ( );
			}

			// Apply no affect
			return base.OnItemSellPrice ( );
		}

		public override bool OnGenerateConsumables ( Shop.ShopConsumableModel [ ] consumables, int totalShopConsumables )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnGenerateConsumables ( consumables, totalShopConsumables );
			}

			// Apply no affect
			return base.OnGenerateConsumables ( consumables, totalShopConsumables );
		}

		public override bool OnConsumableBuyPrice ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnConsumableBuyPrice ( );
			}

			// Apply no affect
			return base.OnConsumableBuyPrice ( );
		}

		public override bool OnConsumableSellPrice ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnConsumableSellPrice ( );
			}

			// Apply no affect
			return base.OnConsumableSellPrice ( );
		}

		public override int OnMinBudget ( int current )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnMinBudget ( current );
			}

			// Apply no affect
			return base.OnMinBudget ( current );
		}

		public override bool OnPurchaseItem ( ItemScriptableObject item )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnPurchaseItem ( item );
			}

			// Apply no affect
			return base.OnPurchaseItem ( item );
		}

		public override bool OnPurchaseConsumable ( Consumables.ConsumableScriptableObject consumable )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnPurchaseConsumable ( consumable );
			}

			// Apply no affect
			return base.OnPurchaseConsumable ( consumable );
		}

		public override bool OnPurchase ( int money, int price )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnPurchase ( money, price );
			}

			// Apply no affect
			return base.OnPurchase ( money, price );
		}

		public override bool IsPurchaseEffectPositive ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.IsPurchaseEffectPositive ( );
			}

			// Apply no affect
			return base.IsPurchaseEffectPositive ( );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnAdd ( model );
			}
		}

		public override void OnAddAnyItem ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnAddAnyItem ( );
			}
		}

		public override void OnAddAnyConsumable ( int count )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnAddAnyConsumable ( count );
			}
		}

		public override bool OnSell ( int instances )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnSell ( instances );
			}

			// Apply no affect
			return base.OnSell ( instances );
		}

		public override int OnRemove ( Shop.ShopModel model )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnRemove ( model );
			}

			// Apply no affect
			return base.OnRemove ( model );
		}

		public override void OnRemoveAnyItem ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnRemoveAnyItem ( );
			}
		}

		public override void OnRemoveAnyConsumable ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				GameManager.Run.ItemData [ 0 ].Item.OnRemoveAnyConsumable ( );
			}
		}

		public override bool OnReroll ( )
		{
			// Check for leftmost item
			if ( GameManager.Run.IsValidItem ( 0 ) && GameManager.Run.ItemData [ 0 ].ID != ID && GameManager.Run.ItemData [ 0 ].Item.IsEnabled )
			{
				// Retrigger item
				return GameManager.Run.ItemData [ 0 ].Item.OnReroll ( );
			}

			// Apply no affect
			return base.OnReroll ( );
		}

		#endregion // Item Override Functions
	}
}