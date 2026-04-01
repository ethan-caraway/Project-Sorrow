using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Bouquet item.
	/// </summary>
	public class Bouquet : Item
	{
		#region Class Constructors

		public Bouquet ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool IsStatusEffectPreventExpire ( )
		{
			return Random.Range ( 0f, 1f ) < 0.3f;
		}

		#endregion // Item Override Functions
	}
}