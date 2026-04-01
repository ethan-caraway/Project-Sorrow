namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the base functionality of a perk.
	/// </summary>
	public class Perk
	{
		#region Class Constructors

		public Perk ( int perkID )
		{
			id = perkID;
		}

		#endregion // Class Constructors

		#region Perk Data Constants

		/// <summary>
		/// The ID used for no perk.
		/// </summary>
		public const int NO_PERK_ID = 0;

		#endregion // Perk Data Constants

		#region Perk Data

		private int id;

		#endregion // Perk Data

		#region Public Properties

		/// <summary>
		/// The ID of this perk.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		#endregion // Public Properties

		#region Public Stats Functions

		/// <summary>
		/// The callback for when the run starts.
		/// </summary>
		public virtual void OnStartRun ( )
		{

		}

		/// <summary>
		/// The callback for when the max confidence is calculated.
		/// </summary>
		/// <param name="current"> The current max confidence. </param>
		/// <returns> The additional max confidence gained from this perk. </returns>
		public virtual int OnMaxConfidence ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max arrogance is calculated.
		/// </summary>
		/// <param name="current"> The current max arrogance. </param>
		/// <returns> The additional max arrogance gained from this perk. </returns>
		public virtual int OnMaxArrogance ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the time allowance is calculated.
		/// </summary>
		/// <param name="current"> The current time allowance in seconds. </param>
		/// <returns> The additional time allowance gained from this perk. </returns>
		public virtual float OnTimeAllowance ( float current )
		{
			return 0f;
		}

		/// <summary>
		/// The callback for when the reputation is calculated.
		/// </summary>
		/// <param name="current"> The current reputation. </param>
		/// <returns> The additional reputation gained from this perk. </returns>
		public virtual int OnReputation ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max item slots are calculated.
		/// </summary>
		/// <returns> The additional max item slots gained from this perk. </returns>
		public virtual int OnMaxItems ( )
		{
			return 0;
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
		/// The callback for when a character is flubbed.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		/// <returns> Whether or not this item was triggered. </returns>
		public virtual void OnFlub ( Performance.PerformanceModel model )
		{
			
		}

		/// <summary>
		/// The callback for applying snaps earned for performing a character accurately.
		/// </summary>
		/// <param name="text"> The text being scored. </param>
		/// <param name="total"> The current total number of snaps earned for the character. </param>
		/// <param name="modifier"> The modifier applied to the word. </param>
		/// <returns> The number of additional snaps earned for the character. </returns>
		public virtual int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier )
		{
			return 0;
		}

		/// <summary>
		/// The callback for applying snaps earned for completing a line of a poem.
		/// </summary>
		/// <returns> The data of the status effect to apply. </returns>
		public virtual StatusEffects.StatusEffectModel OnLineComplete ( )
		{
			return null;
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

		#endregion // Public Performance Functions

		#region Public Shop Functions

		/// <summary>
		/// The callback for when modifying the prices of items and consumables in the shop.
		/// </summary>
		/// <returns> The percentage to modify prices by. </returns>
		public virtual float OnModifyPrices ( )
		{
			return 1f;
		}

		#endregion // Public Shop Functions
	}
}