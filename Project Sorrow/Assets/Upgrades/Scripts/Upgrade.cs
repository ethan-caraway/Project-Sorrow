namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the base functionality of a upgrade.
	/// </summary>
	public class Upgrade
	{
		#region Class Constructors

		public Upgrade ( int upgradeID )
		{
			id = upgradeID;
		}

		#endregion // Class Constructors

		#region Upgrade Data

		private int id;

		#endregion // Upgrade Data

		#region Public Properties

		/// <summary>
		/// The ID of this upgrade.
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
		/// <returns> The additional max confidence gained from this upgrade. </returns>
		public virtual int OnMaxConfidence ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max arrogance is calculated.
		/// </summary>
		/// <param name="current"> The current max arrogance. </param>
		/// <returns> The additional max arrogance gained from this upgrade. </returns>
		public virtual int OnMaxArrogance ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the time allowance is calculated.
		/// </summary>
		/// <param name="current"> The current time allowance in seconds. </param>
		/// <returns> The additional time allowance gained from this upgrade. </returns>
		public virtual float OnTimeAllowance ( float current )
		{
			return 0f;
		}

		/// <summary>
		/// The callback for when reputation is calculated.
		/// </summary>
		/// <param name="current"> The current reputation. </param>
		/// <returns> The additional reputation gained from this upgrade. </returns>
		public virtual int OnReputation ( int current )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max item slots are calculated.
		/// </summary>
		/// <returns> The additional max item slots gained from this upgrade. </returns>
		public virtual int OnMaxItems ( )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the max consumable slots are calculated.
		/// </summary>
		/// <returns> The additional max consumable slots gained from this upgrade. </returns>
		public virtual int OnMaxConsumables ( )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the rarity data is calculated.
		/// </summary>
		/// <param name="model"> The data for the rarity. </param>
		public virtual Shop.RarityModel OnRarity ( Shop.RarityModel model )
		{
			return model;
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
		/// The callback for applying applause at the end of a performance.
		/// </summary>
		/// <param name="model"> The data for the performance. </param>
		/// <param name="total"> The current total number of bonus snaps earned. </param>
		/// <returns> The data for a bonus. </returns>
		public virtual Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel ( );
		}

		#endregion // Public Performance Functions

		#region Public Shop Functions

		/// <summary>
		/// The callback for when this upgrade is added.
		/// </summary>
		/// <returns> The amount of money earned from the upgrade. </returns>
		public virtual int OnAdd ( )
		{
			return 0;
		}

		/// <summary>
		/// The callback for when the shop is initialized.
		/// </summary>
		/// <param name="model"> The data for the shop. </param>
		public virtual void OnInitShop ( Shop.ShopModel model )
		{

		}

		/// <summary>
		/// The callback for when checking if duplicate items can appear in the shop.
		/// </summary>
		/// <returns> Whether or not duplicate items can appear in the shop. </returns>
		public virtual bool OnDuplicateItems ( )
		{
			return false;
		}

		#endregion // Public Shop Functions
	}
}