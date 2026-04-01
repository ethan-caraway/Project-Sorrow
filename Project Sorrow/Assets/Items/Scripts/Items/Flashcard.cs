using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Flashcard item.
	/// </summary>
	public class Flashcard : Item
	{
		#region Class Constructors

		public Flashcard ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemStringScaleValue ( ID, InstanceID ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", GetStartingLetter ( ) );
		}

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for matching character
			if ( text.ToUpper ( ) == GameManager.Run.GetItemStringScaleValue ( ID, InstanceID ) )
			{
				// Return double the snaps
				return total;
			}

			// Return no additional snaps
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Set random letter
			GameManager.Run.SetItemStringScaleValue ( ID, InstanceID, GetRandomLetter ( ) );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemStringScaleValue ( ID, InstanceID, GetStartingLetter ( ) );
		}

		#endregion // Item Override Functions

		#region Private Functions

		/// <summary>
		/// Gets a random letter from the alphabet.
		/// </summary>
		/// <returns> The random letter. </returns>
		private string GetRandomLetter ( )
		{
			// Store the alphabet
			string [ ] letters =
			{
				"A",
				"B",
				"C",
				"D",
				"E",
				"F",
				"G",
				"H",
				"I",
				"J",
				"K",
				"L",
				"M",
				"N",
				"O",
				"P",
				"Q",
				"R",
				"S",
				"T",
				"U",
				"V",
				"W",
				"X",
				"Y",
				"Z"
			};

			// Get random letter
			return letters [ Random.Range ( 0, letters.Length ) ];
		}

		/// <summary>
		/// Gets a random staring letter based on the current round and performance.
		/// </summary>
		/// <returns> The random letter. </returns>
		private string GetStartingLetter ( )
		{
			// Store the most frequent letters
			string [ ] letters =
			{
				"A",
				"E",
				"H",
				"I",
				"N",
				"O",
				"R",
				"S",
				"T"
			};

			// Get random index based on round and performance
			int index = ( ( GameManager.Run.Round * Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND ) + GameManager.Run.Performance ) % letters.Length;

			// Return random letter
			return letters [ index ];
		}

		#endregion // Private Functions
	}
}