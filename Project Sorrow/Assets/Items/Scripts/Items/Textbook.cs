using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Textbook item.
	/// </summary>
	public class Textbook : Item
	{
		#region Class Constructors

		public Textbook ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Poems.PoemModel OnEnhancePoem ( )
		{
			// Create enhancement
			Poems.PoemModel enhancement = new Poems.PoemModel ( );

			// Select a random enhancement
			switch ( Random.Range ( 0, 6 ) )
			{
				// Confidence
				case 0:
					enhancement.Confidence += 1;
					break;

				// Arrogance
				case 1:
					enhancement.Arrogance += 1;
					break;

				// Time
				case 2:
					enhancement.TimeAllowance += 20f;
					break;

				// Reputation
				case 3:
					enhancement.Reputation += 2;
					break;

				// Applause
				case 4:
					enhancement.Applause += 40;
					break;

				// Commission
				case 5:
					enhancement.Commission += 1;
					break;

				
			}

			// Return the new enhancement to apply
			return enhancement;
		}

		#endregion // Item Override Functions
	}
}