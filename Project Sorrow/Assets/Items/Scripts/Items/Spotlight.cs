using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Spotlight item.
	/// </summary>
	public class Spotlight : Item
	{
		#region Class Constructors

		public Spotlight ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Get status effects
			Enums.StatusEffectType [ ] statusEffects = new Enums.StatusEffectType [ ]
			{
				Enums.StatusEffectType.STUBBORN,
				Enums.StatusEffectType.GREEDY,
				Enums.StatusEffectType.DRAMATIC,
				Enums.StatusEffectType.POPULAR,
				Enums.StatusEffectType.EXCITED,
				Enums.StatusEffectType.SERIOUS
			};

			// Apply random status effect
			GameManager.Run.AddStatusEffect ( statusEffects [ Random.Range ( 0, statusEffects.Length ) ], 3 );
		}

		#endregion // Item Override Functions
	}
}