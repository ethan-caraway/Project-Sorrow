namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class stores the data for when an item is triggered.
	/// </summary>
	public class ItemTriggerModel
	{
		#region Item Data

		/// <summary>
		/// The ID of the item being triggered.
		/// </summary>
		public int ID = ItemModel.NO_ITEM_ID;

		/// <summary>
		/// The ID of this instance of the item being triggered.
		/// </summary>
		public string InstanceID = string.Empty;

		/// <summary>
		/// The data for highlighting the item in the HUD when the item is triggered.
		/// </summary>
		public HUD.ItemHighlightModel Highlight;

		/// <summary>
		/// The number of snaps earned from the item being triggered.
		/// </summary>
		public int Snaps = 0;

		/// <summary>
		/// The data for the status effect gained from the item being triggered.
		/// </summary>
		public StatusEffects.StatusEffectModel StatusEffect = null;

		#endregion // Item Data

		#region Public Functions

		/// <summary>
		/// Checks whether or not the data from an item being triggered is valid.
		/// </summary>
		/// <returns> Whether or not the data is valid. </returns>
		public bool IsValid ( )
		{
			// Check for item data
			return ID != ItemModel.NO_ITEM_ID && !string.IsNullOrEmpty ( InstanceID ) && Highlight.IsValid ( );
		}

		#endregion // Public Functions
	}
}