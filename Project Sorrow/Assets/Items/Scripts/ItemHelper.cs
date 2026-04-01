namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class contains functions for creating items.
	/// </summary>
	public static class ItemHelper
	{
		#region Item Data Constants

		private const int CREDIT_CARD_ID = 1;
		private const int STOCK_PORTFOLIO_ID = 2;
		private const int COUPON_ID = 3;
		private const int DONATION_BOX_ID = 4;
		private const int STOPWATCH_ID = 5;
		private const int BAR_TAB_ID = 6;
		private const int DESIGNER_PURSE_ID = 7;
		private const int LUCKY_PENNY_ID = 8;
		private const int MEGAPHONE_ID = 9;
		private const int SECRET_MENU_ID = 10;
		private const int RUNNING_SHOES_ID = 11;
		private const int PENCIL_ID = 12;
		private const int PEN_ID = 13;
		private const int STYLE_GUIDE_ID = 14;
		private const int BLANK_PAGE_ID = 15;
		private const int SUNGLASSES_ID = 16;
		private const int SMARTWATCH_ID = 17;
		private const int SPORTS_JERSEY_ID = 18;
		private const int MICROPHONE_ID = 19;
		private const int GOLD_MEDAL_ID = 20;
		private const int SNOWBALL_ID = 21;
		private const int KEYBOARD_ID = 22;
		private const int TYPEWRITER_ID = 23;
		private const int POCKET_WATCH_ID = 24;
		private const int INVENTORY_LEDGER_ID = 25;
		private const int ICE_CHEST_ID = 26;
		private const int SCALES_ID = 27;
		private const int AMP_ID = 28;
		private const int POP_FILTER_ID = 29;
		private const int COASTER_ID = 30;
		private const int PRICE_TAG_ID = 31;
		private const int CHAPBOOK_ID = 32;
		private const int HOURGLASS_ID = 33;
		private const int WORD_PUZZLE_ID = 34;
		private const int LUXURY_WATCH_ID = 35;
		private const int DICTIONARY_ID = 36;
		private const int THESAURUS_ID = 37;
		private const int MARKER_ID = 38;
		private const int ARTS_DEGREE_ID = 39;
		private const int CANDLE_ID = 40;
		private const int FIRST_DRAFT_ID = 41;
		private const int FLASHCARD_ID = 42;
		private const int SIGNED_FIRST_EDITION_ID = 43;
		private const int TOOLBOX_ID = 44;
		private const int EIGHT_BALL_ID = 45;
		private const int FOUR_OF_DIAMONDS_ID = 46;
		private const int GIFT_CARD_ID = 47;
		private const int PIGGY_BANK_ID = 48;
		private const int LOYALTY_CARD_ID = 49;
		private const int LEATHER_JACKET_ID = 50;
		private const int GRILL_ID = 51;
		private const int TEA_KETTLE_ID = 52;
		private const int SALAD_TONGS_ID = 53;
		private const int SOMBRERO_ID = 54;
		private const int BOTTLE_OPENER_ID = 55;
		private const int JUICE_PITCHER_ID = 56;
		private const int ERASER_ID = 57;
		private const int TIP_JAR_ID = 58;
		private const int MEAL_TICKET_ID = 59;
		private const int GIANT_CHECK_ID = 60;
		private const int THREED_PRINTER_ID = 61;
		private const int IOU_ID = 62;
		private const int LUNCHBOX_ID = 63;
		private const int RECEIPT_ID = 64;
		private const int DEFIBRILLATOR_ID = 65;
		private const int GOLD_BAR_ID = 66;
		private const int WHEEL_OF_FORTUNE_ID = 67;
		private const int DICE_ID = 68;
		private const int ADRENALINE_SHOT_ID = 69;
		private const int ANTIQUE_ID = 70;
		private const int ICE_SCULPTURE_ID = 71;
		private const int CIGARETTES_ID = 72;
		private const int DISGUISE_ID = 73;
		private const int READING_GLASSES_ID = 74;
		private const int YOYO_ID = 75;
		private const int RESUME_ID = 76;
		private const int WANTED_POSTER_ID = 77;
		private const int TROPHY_ID = 78;
		private const int TEXTBOOK_ID = 79;
		private const int RED_FLAG_ID = 80;
		private const int PAINTING_ID = 81;
		private const int MIRROR_ID = 82;
		private const int SPOTLIGHT_ID = 83;
		private const int OX_HORN_ID = 84;
		private const int MONOCLE_ID = 85;
		private const int SKULL_ID = 86;
		private const int CAT_EARS_ID = 87;
		private const int BALLOONS_ID = 88;
		private const int SOAPBOX_ID = 89;
		private const int DRAMA_MASKS_ID = 90;
		private const int VHS_ID = 91;
		private const int LIFE_JACKET_ID = 92;
		private const int BOUQUET_ID = 93;

		#endregion // Item Data Constants

		#region Public Functions

		/// <summary>
		/// Gets an instance of an item for a given ID.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		/// <param name="instance"> The ID of the instance of the item. </param>
		/// <returns> The instance of the item. </returns>
		public static Item GetItem ( int id, string instance )
		{
			// Check ID
			switch ( id )
			{
				case CREDIT_CARD_ID:
					return new CreditCard ( id, instance );

				case STOCK_PORTFOLIO_ID:
					return new StockPortfolio ( id, instance );

				case COUPON_ID:
					return new Coupon ( id, instance );

				case DONATION_BOX_ID:
					return new DonationBox ( id, instance );

				case STOPWATCH_ID:
					return new Stopwatch ( id, instance );

				case BAR_TAB_ID:
					return new BarTab ( id, instance );

				case DESIGNER_PURSE_ID:
					return new DesignerPurse ( id, instance );

				case LUCKY_PENNY_ID:
					return new LuckyPenny ( id, instance );

				case MEGAPHONE_ID:
					return new Megaphone ( id, instance );

				case SECRET_MENU_ID:
					return new SecretMenu ( id, instance );

				case RUNNING_SHOES_ID:
					return new RunningShoes ( id, instance );

				case PENCIL_ID:
					return new Pencil ( id, instance );

				case PEN_ID:
					return new Pen ( id, instance );

				case STYLE_GUIDE_ID:
					return new StyleGuide ( id, instance );

				case BLANK_PAGE_ID:
					return new BlankPage ( id, instance );

				case SUNGLASSES_ID:
					return new Sunglasses ( id, instance );

				case SMARTWATCH_ID:
					return new Smartwatch ( id, instance );

				case SPORTS_JERSEY_ID:
					return new SportsJersey ( id, instance );

				case MICROPHONE_ID:
					return new Microphone ( id, instance );

				case GOLD_MEDAL_ID:
					return new GoldMedal ( id, instance );

				case SNOWBALL_ID:
					return new Snowball ( id, instance );

				case KEYBOARD_ID:
					return new Keyboard ( id, instance );

				case TYPEWRITER_ID:
					return new Typewriter ( id, instance );

				case POCKET_WATCH_ID:
					return new PocketWatch ( id, instance );

				case INVENTORY_LEDGER_ID:
					return new InventoryLedger ( id, instance );

				case ICE_CHEST_ID:
					return new IceChest ( id, instance );

				case SCALES_ID:
					return new Scales ( id, instance );

				case AMP_ID:
					return new Amp ( id, instance );

				case POP_FILTER_ID:
					return new PopFilter ( id, instance );

				case COASTER_ID:
					return new Coaster ( id, instance );

				case PRICE_TAG_ID:
					return new PriceTag ( id, instance );

				case CHAPBOOK_ID:
					return new Chapbook ( id, instance );

				case HOURGLASS_ID:
					return new Houglass ( id, instance );

				case WORD_PUZZLE_ID:
					return new WordPuzzle ( id, instance );

				case LUXURY_WATCH_ID:
					return new LuxuryWatch ( id, instance );

				case DICTIONARY_ID:
					return new Dictionary ( id, instance );

				case THESAURUS_ID:
					return new Thesaurus ( id, instance );

				case MARKER_ID:
					return new Marker ( id, instance );

				case ARTS_DEGREE_ID:
					return new ArtsDegree ( id, instance );

				case CANDLE_ID:
					return new Candle ( id, instance );

				case FIRST_DRAFT_ID:
					return new FirstDraft ( id, instance );

				case FLASHCARD_ID:
					return new Flashcard ( id, instance );

				case SIGNED_FIRST_EDITION_ID:
					return new SignedFirstEdition ( id, instance );

				case TOOLBOX_ID:
					return new Toolbox ( id, instance );

				case EIGHT_BALL_ID:
					return new EightBall ( id, instance );

				case FOUR_OF_DIAMONDS_ID:
					return new FourOfDiamonds ( id, instance );

				case GIFT_CARD_ID:
					return new GiftCard ( id, instance );

				case PIGGY_BANK_ID:
					return new PiggyBank ( id, instance );

				case LOYALTY_CARD_ID:
					return new LoyaltyCard ( id, instance );

				case LEATHER_JACKET_ID:
					return new LeatherJacket ( id, instance );

				case GRILL_ID:
					return new Grill ( id, instance );

				case TEA_KETTLE_ID:
					return new TeaKettle ( id, instance );

				case SALAD_TONGS_ID:
					return new SaladTongs ( id, instance );

				case SOMBRERO_ID:
					return new Sombrero ( id, instance );

				case BOTTLE_OPENER_ID:
					return new BottleOpener ( id, instance );

				case JUICE_PITCHER_ID:
					return new JuicePitcher ( id, instance );

				case ERASER_ID:
					return new Eraser ( id, instance );

				case TIP_JAR_ID:
					return new TipJar ( id, instance );

				case MEAL_TICKET_ID:
					return new MealTicket ( id, instance );

				case GIANT_CHECK_ID:
					return new GiantCheck ( id, instance );

				case THREED_PRINTER_ID:
					return new ThreeDPrinter ( id, instance );

				case IOU_ID:
					return new IOU ( id, instance );

				case LUNCHBOX_ID:
					return new Lunchbox ( id, instance );

				case RECEIPT_ID:
					return new Receipt ( id, instance );

				case DEFIBRILLATOR_ID:
					return new Defibrillator ( id, instance );

				case GOLD_BAR_ID:
					return new GoldBar ( id, instance );

				case WHEEL_OF_FORTUNE_ID:
					return new WheelOfFortune ( id, instance );

				case DICE_ID:
					return new Dice ( id, instance );

				case ADRENALINE_SHOT_ID:
					return new AdrenalineShot ( id, instance );

				case ANTIQUE_ID:
					return new Antique ( id, instance );

				case ICE_SCULPTURE_ID:
					return new IceSculpture ( id, instance );

				case CIGARETTES_ID:
					return new Cigarettes ( id, instance );

				case DISGUISE_ID:
					return new Disguise ( id, instance );

				case READING_GLASSES_ID:
					return new ReadingGlasses ( id, instance );

				case YOYO_ID:
					return new Yoyo ( id, instance );

				case RESUME_ID:
					return new Resume ( id, instance );

				case WANTED_POSTER_ID:
					return new WantedPoster ( id, instance );

				case TROPHY_ID:
					return new Trophy ( id, instance );

				case TEXTBOOK_ID:
					return new Textbook ( id, instance );

				case RED_FLAG_ID:
					return new RedFlag ( id, instance );

				case PAINTING_ID:
					return new Painting ( id, instance );

				case MIRROR_ID:
					return new Mirror ( id, instance );

				case SPOTLIGHT_ID:
					return new Spotlight ( id, instance );

				case OX_HORN_ID:
					return new OxHorn ( id, instance );

				case MONOCLE_ID:
					return new Monocle ( id, instance );

				case SKULL_ID:
					return new Skull ( id, instance );

				case CAT_EARS_ID:
					return new CatEars ( id, instance );

				case BALLOONS_ID:
					return new Balloons ( id, instance );

				case SOAPBOX_ID:
					return new Soapbox ( id, instance );

				case DRAMA_MASKS_ID:
					return new DramaMasks ( id, instance );

				case VHS_ID:
					return new VHS ( id, instance );

				case LIFE_JACKET_ID:
					return new LifeJacket ( id, instance );

				case BOUQUET_ID:
					return new Bouquet ( id, instance );
			}

			// Return that no item was found
			return null;
		}

		#endregion // Public Functions
	}
}