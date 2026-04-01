using System.Collections.Generic;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Combo Meal consumable.
	/// </summary>
	public class ComboMeal : Consumable
	{
		#region Consumable Override Functions

		public override Poems.PoemModel OnEnhancePoem ( int instances )
		{
			// Store final enhancement
			Poems.PoemModel enhancement = new Poems.PoemModel ( );

			// Apply for each instance
			for ( int i = 0; i < instances; i++ )
			{
				// Get list of potential enhancements
				List<Poems.PoemModel> enhancementOptions = new List<Poems.PoemModel> ( );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					Confidence = 8
				} );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					Arrogance = 8
				} );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					TimeAllowance = 180f
				} );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					Reputation = 15
				} );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					Applause = 400
				} );
				enhancementOptions.Add ( new Poems.PoemModel
				{
					Commission = 8
				} );

				// Add the first random enhancment
				int index = Random.Range ( 0, enhancementOptions.Count );
				enhancement.Add ( enhancementOptions [ index ] );
				enhancementOptions.RemoveAt ( index );

				// Add the second random enhancement
				index = Random.Range ( 0, enhancementOptions.Count );
				enhancement.Add ( enhancementOptions [ index ] );
			}

			// Return new enhancements
			return enhancement;
		}

		#endregion // Consumable Override Functions
	}
}