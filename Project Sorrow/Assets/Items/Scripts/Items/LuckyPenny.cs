using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Lucky Penny item.
	/// </summary>
	public class LuckyPenny : Item
	{
		#region Class Constructors

		public LuckyPenny ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool OnItemBuyPrice ( )
		{
			return IsFree ( );
		}

		public override bool OnConsumableBuyPrice ( )
		{
			return IsFree ( );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets whether or not a price should be free.
		/// </summary>
		/// <returns> Whether or not the price is free. </returns>
		private bool IsFree ( )
		{
			// 10% chance to make the item free
			return Random.Range ( 0f, 1f ) < 0.1f;
		}

		#endregion // Private Functions
	}
}