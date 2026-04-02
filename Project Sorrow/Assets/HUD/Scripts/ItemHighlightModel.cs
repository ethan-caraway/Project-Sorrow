namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This struct stores the data for how an item trigger should be displayed in the HUD.
	/// </summary>
	public struct ItemHighlightModel
	{
		#region Item Data

		/// <summary>
		/// Whether or not the trigger effect is positive.
		/// </summary>
		public bool IsPositive;

		/// <summary>
		/// The color of the splash background.
		/// </summary>
		public Enums.SplashColorType SplashColor;

		/// <summary>
		/// The prompt to display in the splash.
		/// </summary>
		public string SplashText;

		#endregion // Item Data

		#region Public Functions

		/// <summary>
		/// Whether or not this highlight data is valid.
		/// </summary>
		/// <returns> Whether or not the data is valid. </returns>
		public bool IsValid ( )
		{
			return SplashColor != Enums.SplashColorType.NONE && !string.IsNullOrEmpty ( SplashText );
		}

		#endregion // Public Functions
	}
}