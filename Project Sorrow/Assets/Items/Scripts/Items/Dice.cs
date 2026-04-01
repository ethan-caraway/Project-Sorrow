using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Dice item.
	/// </summary>
	public class Dice : Item
	{
		#region Class Constructors

		public Dice ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Return 2-12 snaps
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Snaps = Random.Range ( 1, 7 ) + Random.Range ( 1, 7 )
			};
		}

		#endregion // Item Override Functions
	}
}