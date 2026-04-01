using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Marker item.
	/// </summary>
	public class Marker : Item
	{
		#region Class Constructors

		public Marker ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool IsModifyingWords ( )
		{
			// Return that this item modifies words
			return true;
		}

		public override Enums.WordModifierType [ ] OnModifyWords ( )
		{
			// Get modifiers
			Enums.WordModifierType [ ] modifiers = new Enums.WordModifierType [ 3 ];
			for ( int i = 0; i < modifiers.Length; i++ )
			{
				// Get random modifier
				modifiers [ i ] = (Enums.WordModifierType)Random.Range ( 1, 7 );
			}

			// Return the random modifiers
			return modifiers;
		}

		#endregion // Item Override Functions
	}
}