namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class contains functions for creating consumables.
	/// </summary>
	public static class ConsumableHelper
	{
		#region Consumable Data Constants

		private const int PREDATORY_LOAN_ID = 1;
		private const int BANK_LOAN_ID = 2;
		private const int PERSONAL_LOAN_ID = 3;
		private const int MINERAL_WATER_ID = 4;
		private const int SPARKLING_WATER_ID = 5;
		private const int TONIC_WATER_ID = 6;
		private const int NA_BEER_ID = 7;
		private const int LIGHT_BEER_ID = 8;
		private const int CRAFT_BEER_ID = 9;
		private const int DECAF_COFFEE_ID = 10;
		private const int MILK_COFFEE_ID = 11;
		private const int BLACK_COFFEE_ID = 12;
		private const int BLENDED_WHISKEY_ID = 13;
		private const int SINGLE_MALT_WHISKEY_ID = 14;
		private const int SINGLE_CASK_WHISKEY_ID = 15;
		private const int KETTLE_CHIPS_ID = 16;
		private const int SALT_AND_VINEGAR_CHIPS_ID = 17;
		private const int BBQ_CHIPS_ID = 18;
		private const int RED_WINE_ID = 19;
		private const int WHITE_WINE_ID = 20;
		private const int SPARKLING_WINE_ID = 21;
		private const int CHEESEBURGER_ID = 22;
		private const int SMASH_BURGER_ID = 23;
		private const int VEGGIE_BURGER_ID = 24;
		private const int BLACK_TEA_ID = 25;
		private const int OOLONG_TEA = 26;
		private const int GREEN_TEA_ID = 27;
		private const int GARDEN_SALAD_ID = 28;
		private const int CAESAR_SALAD_ID = 29;
		private const int COBB_SALAD_ID = 30;
		private const int BREAKFAST_TACOS_ID = 31;
		private const int FISH_TACOS_ID = 32;
		private const int BARBACOA_TACOS_ID = 33;
		private const int DIET_SODA_ID = 34;
		private const int CRAFT_SODA_ID = 35;
		private const int HARD_SODA_ID = 36;
		private const int GRAPE_JUICE_ID = 37;
		private const int APPLE_JUICE_ID = 38;
		private const int ORANGE_JUICE_ID = 39;
		private const int SKIM_MILK_ID = 40;
		private const int LOW_FAT_MILK_ID = 41;
		private const int WHOLE_MILK_ID = 42;
		private const int SIRLOIN_STEAK_ID = 43;
		private const int RIBEYE_STEAK_ID = 44;
		private const int TENDERLOIN_STEAK_ID = 45;
		private const int DRY_MARTINI_ID = 46;
		private const int DIRTY_MARTINI_ID = 47;
		private const int VODKA_MARTINI_ID = 48;
		private const int PINK_LEMONADE_ID = 49;
		private const int MINT_LEMONADE_ID = 50;
		private const int HARD_LEMONADE_ID = 51;
		private const int CHOCOLATE_CANDY_ID = 52;
		private const int HARD_CANDY_ID = 53;
		private const int SOUR_CANDY_ID = 54;
		private const int WHITE_RICE_ID = 55;
		private const int BROWN_RICE_ID = 56;
		private const int BLACK_RICE_ID = 57;
		private const int COMBO_MEAL_ID = 58;
		private const int SAMPLE_PLATTER_ID = 59;
		private const int DRINK_FLIGHT_ID = 60;

		#endregion // Consumable Data Constants

		#region Public Functions

		/// <summary>
		/// Gets an instance of a consumable for a given ID.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <returns> The instance of the consumable. </returns>
		public static Consumable GetConsumable ( int id )
		{
			// Check ID
			switch ( id )
			{
				case PREDATORY_LOAN_ID:
					return new PredatoryLoan ( );

				case BANK_LOAN_ID:
					return new BankLoan ( );

				case PERSONAL_LOAN_ID:
					return new PersonalLoan ( );

				case MINERAL_WATER_ID:
					return new MineralWater ( );

				case SPARKLING_WATER_ID:
					return new SparklingWater ( );

				case TONIC_WATER_ID:
					return new TonicWater ( );

				case NA_BEER_ID:
					return new NABeer ( );

				case LIGHT_BEER_ID:
					return new LightBeer ( );

				case CRAFT_BEER_ID:
					return new CraftBeer ( );

				case DECAF_COFFEE_ID:
					return new DecafCoffee ( );

				case MILK_COFFEE_ID:
					return new MilkCoffee ( );

				case BLACK_COFFEE_ID:
					return new BlackCoffee ( );

				case BLENDED_WHISKEY_ID:
					return new BlendedWhiskey ( );

				case SINGLE_MALT_WHISKEY_ID:
					return new SingleMaltWhiskey ( );

				case SINGLE_CASK_WHISKEY_ID:
					return new SingleCaskWhiskey ( );

				case KETTLE_CHIPS_ID:
					return new KettleChips ( );

				case SALT_AND_VINEGAR_CHIPS_ID:
					return new SaltAndVinegarChips ( );

				case BBQ_CHIPS_ID:
					return new BBQChips ( );

				case RED_WINE_ID:
					return new RedWine ( );

				case WHITE_WINE_ID:
					return new WhiteWine ( );

				case SPARKLING_WINE_ID:
					return new SparklingWine ( );

				case CHEESEBURGER_ID:
					return new Cheeseburger ( );

				case SMASH_BURGER_ID:
					return new SmashBurger ( );

				case VEGGIE_BURGER_ID:
					return new VeggieBurger ( );

				case BLACK_TEA_ID:
					return new BlackTea ( );

				case OOLONG_TEA:
					return new OolongTea ( );

				case GREEN_TEA_ID:
					return new GreenTea ( );

				case GARDEN_SALAD_ID:
					return new GardenSalad ( );

				case CAESAR_SALAD_ID:
					return new CaesarSalad ( );

				case COBB_SALAD_ID:
					return new CobbSalad ( );

				case BREAKFAST_TACOS_ID:
					return new BreakfastTacos ( );

				case FISH_TACOS_ID:
					return new FishTacos ( );

				case BARBACOA_TACOS_ID:
					return new BarbacoaTacos ( );

				case DIET_SODA_ID:
					return new DietSoda ( );

				case CRAFT_SODA_ID:
					return new CraftSoda ( );

				case HARD_SODA_ID:
					return new HardSoda ( );

				case GRAPE_JUICE_ID:
					return new GrapeJuice ( );

				case APPLE_JUICE_ID:
					return new AppleJuice ( );

				case ORANGE_JUICE_ID:
					return new OrangeJuice ( );

				case SKIM_MILK_ID:
					return new SkimMilk ( );

				case LOW_FAT_MILK_ID:
					return new LowFatMilk ( );

				case WHOLE_MILK_ID:
					return new WholeMilk ( );

				case SIRLOIN_STEAK_ID:
					return new SirloinSteak ( );

				case RIBEYE_STEAK_ID:
					return new RibeyeSteak ( );

				case TENDERLOIN_STEAK_ID:
					return new TenderloinSteak ( );

				case DRY_MARTINI_ID:
					return new DryMartini ( );

				case DIRTY_MARTINI_ID:
					return new DirtyMartini ( );

				case VODKA_MARTINI_ID:
					return new VodkaMartini ( );

				case PINK_LEMONADE_ID:
					return new PinkLemonade ( );

				case MINT_LEMONADE_ID:
					return new MintLemonade ( );

				case HARD_LEMONADE_ID:
					return new HardLemonade ( );

				case CHOCOLATE_CANDY_ID:
					return new ChocolateCandy ( );

				case HARD_CANDY_ID:
					return new HardCandy ( );

				case SOUR_CANDY_ID:
					return new SourCandy ( );

				case WHITE_RICE_ID:
					return new WhiteRice ( );

				case BROWN_RICE_ID:
					return new BrownRice ( );

				case BLACK_RICE_ID:
					return new BlackRice ( );

				case COMBO_MEAL_ID:
					return new ComboMeal ( );

				case SAMPLE_PLATTER_ID:
					return new SamplePlatter ( );

				case DRINK_FLIGHT_ID:
					return new DrinkFlight ( );
			}

			// Return that no consumable was found
			return null;
		}

		#endregion // Public Functions
	}
}