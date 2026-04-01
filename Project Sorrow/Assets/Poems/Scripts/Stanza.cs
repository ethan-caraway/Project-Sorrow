using UnityEngine;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class stores the data for a stanza in a poem.
	/// </summary>
	[System.Serializable]
	public class Stanza
	{
		public Stanza ( string [ ] texts )
		{
			lines = texts;
		}


		[Tooltip ( "The lines that make up this stanza." )]
		[SerializeField]
		private string [ ] lines;

		/// <summary>
		/// The lines that make up this stanza.
		/// </summary>
		public string [ ] Lines
		{
			get
			{
				return lines;
			}
		}

		/// <summary>
		/// The total number of characters in this stanza.
		/// </summary>
		public int CharacterCount
		{
			get
			{
				// Count characters
				int count = 0;
				for ( int i = 0; i < lines.Length; i++ )
				{
					count += lines [ i ].Length;
				}

				// Return total
				return count;
			}
		}
	}
}
