namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class stores the data for an owned item.
	/// </summary>
	[System.Serializable]
	public class ItemModel
	{
		#region Item Data Constants

		/// <summary>
		/// The ID used for no item.
		/// </summary>
		public const int NO_ITEM_ID = 0;

		#endregion // Item Data Constants

		#region Item Data

		/// <summary>
		/// The ID of the item.
		/// </summary>
		public int ID;

		/// <summary>
		/// The ID of this instance of the item.
		/// </summary>
		public string InstanceID;

		/// <summary>
		/// The current integer scalable value for the item.
		/// </summary>
		public int IntScaleValue;

		/// <summary>
		/// The current float scalable value for the item.
		/// </summary>
		public float FloatScaleValue;

		/// <summary>
		/// The current string scalable value for the item.
		/// </summary>
		public string StringScaleValue;

		/// <summary>
		/// The data for the item.
		/// </summary>
		[System.NonSerialized]
		public Item Item;

		#endregion // Item Data

		#region Public Functions

		/// <summary>
		/// Checks whether or not this model contains valid item data.
		/// </summary>
		/// <returns></returns>
		public bool IsValid ( )
		{
			// Validate data
			return ID != NO_ITEM_ID && Item != null;
		}

		#endregion // Public Functions
	}
}