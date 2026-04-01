using System.Collections.Generic;
using System.Linq;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class stores the data for a poem.
	/// </summary>
	[System.Serializable]
	public class PoemModel
	{
		#region Poem Data

		/// <summary>
		/// The ID of the poem.
		/// </summary>
		public int ID;

		/// <summary>
		/// The current level of the poem.
		/// </summary>
		public int Level;

		/// <summary>
		/// The confidence enhancements applied to this poem.
		/// </summary>
		public int Confidence = 0;

		/// <summary>
		/// The arrogance enhancements applied to this poem.
		/// </summary>
		public int Arrogance = 0;

		/// <summary>
		/// The time allowance enhancements applied to this poem.
		/// </summary>
		public float TimeAllowance = 0f;

		/// <summary>
		/// The interest cap enhancements applied to this poem.
		/// </summary>
		public int Reputation = 0;

		/// <summary>
		/// The applause enhancements applied to this poem.
		/// </summary>
		public int Applause = 0;

		/// <summary>
		/// The commission enhancements applied to this poem.
		/// </summary>
		public int Commission = 0;

		/// <summary>
		/// The word modifiers applied to this poem.
		/// </summary>
		public WordModel [ ] Modifiers;

		#endregion // Poem Data

		#region Public Properties

		/// <summary>
		/// The amount of base snaps earned for each character.
		/// </summary>
		public int BaseSnaps
		{
			get
			{
				return PoemHelper.GetBaseSnaps ( Level );
			}
		}

		/// <summary>
		/// Whether or not this poem has any enhancements applied to it.
		/// </summary>
		public bool HasEnhancements
		{
			get
			{
				// Return any enhancements
				return Confidence > 0 ||
					Arrogance > 0 ||
					TimeAllowance > 0f ||
					Reputation > 0 ||
					Applause > 0 ||
					Commission > 0;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Constructs the stanzas for the poem.
		/// </summary>
		/// <returns> The stanzas for the poem. </returns>
		public Stanza [ ] ToStanzas ( )
		{
			// Get poem
			PoemScriptableObject poem = PoemUtility.GetPoem ( ID );

			// Check for poem
			if ( poem == null )
			{
				return null;
			}

			// Check for tutorial
			if ( ID == 0 )
			{
				return poem.Stanzas;
			}

			// Create stanzas
			Stanza [ ] stanzas = new Stanza [ poem.Stanzas.Length + 1 ];

			// Create title stanza
			string [ ] lines = { poem.Title };
			stanzas [ 0 ] = new Stanza ( lines );

			// Add all stanzas
			for ( int i = 1; i < stanzas.Length; i++ )
			{
				stanzas [ i ] = poem.Stanzas [ i - 1 ];
			}

			// Return the stanzas for the poem
			return stanzas;
		}

		/// <summary>
		/// Adds modifiers and enhancements to this poem.
		/// </summary>
		/// <param name="addModel"> The poem data to add to this model. </param>
		public void Add ( PoemModel addModel )
		{
			// Check for data
			if ( addModel == null )
			{
				return;
			}

			// Add enhancements
			Confidence += addModel.Confidence;
			Arrogance += addModel.Arrogance;
			TimeAllowance += addModel.TimeAllowance;
			Reputation += addModel.Reputation;
			Applause += addModel.Applause;
			Commission += addModel.Commission;

			// Add modifiers
			AddModifiers ( addModel.Modifiers );
		}

		/// <summary>
		/// Adds new modifiers to this poem.
		/// </summary>
		/// <param name="newModifiers"> The data for the word modifiers. </param>
		public void AddModifiers ( WordModel [ ] newModifiers )
		{
			// Check for modifiers
			if ( newModifiers == null || newModifiers.Length == 0 || newModifiers [ 0 ] == null )
			{
				return;
			}	

			// Get existing modifiers
			List<WordModel> modifiers = new List<WordModel> ( );
			if ( Modifiers != null && Modifiers.Length > 0 )
			{
				modifiers.AddRange ( Modifiers );
			}

			// Add new modifiers
			modifiers.AddRange ( newModifiers );

			// Store all modifiers
			Modifiers = modifiers.OrderBy ( x => x.StartIndex ).ToArray ( );
		}

		#endregion // Public Functions
	}
}