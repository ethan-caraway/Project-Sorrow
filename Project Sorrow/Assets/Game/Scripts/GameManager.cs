namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class controls the setup and progression of the game and storing game-wide data.
	/// </summary>
	public static class GameManager
	{
		#region Game Data Constants

		/// <summary>
		/// The name of the main menu scene.
		/// </summary>
		public const string MAIN_MENU_SCENE = "MainMenu";

		/// <summary>
		/// The name of the character select scene.
		/// </summary>
		public const string CHARACTER_SELECT_SCENE = "CharacterSelect";

		/// <summary>
		/// The name of the collection scene.
		/// </summary>
		public const string COLLECTION_SCENE = "Collection";

		/// <summary>
		/// The name of the setlist scene.
		/// </summary>
		public const string SETLIST_SCENE = "Setlist";

		/// <summary>
		/// The name of the performance scene.
		/// </summary>
		public const string PERFORMANCE_SCENE = "Performance";

		/// <summary>
		/// The name of the encounter scene.
		/// </summary>
		public const string ENCOUNTER_SCENE = "Encounter";
		
		/// <summary>
		/// The name of the shop scene.
		/// </summary>
		public const string SHOP_SCENE = "Shop";

		#endregion // Game Data Constants

		#region Game Data

		/// <summary>
		/// Whether or not the tutotrial is currently active.
		/// </summary>
		public static bool IsTutorial;

		/// <summary>
		/// Whether or not a run is currently active.
		/// </summary>
		public static bool IsRunActive;

		/// <summary>
		/// The data for the current run.
		/// </summary>
		public static RunModel Run;

		/// <summary>
		/// The difficulty data for the current run.
		/// </summary>
		public static Difficulty.DifficultyScriptableObject Difficulty;

		#endregion // Game Data
	}
}