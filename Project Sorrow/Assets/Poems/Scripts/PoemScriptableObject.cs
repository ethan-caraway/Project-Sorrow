using UnityEngine;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a poem.
	/// </summary>
	[CreateAssetMenu ( fileName = "Poem", menuName = "Scriptable Objects/Poem" )]
	public class PoemScriptableObject : ScriptableObject
	{
		#region Poem Data

		[Tooltip ( "The ID of the poem." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The title of this poem." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The author of this poem." )]
		[SerializeField]
		private string author;

		[Tooltip ( "Whether or not the author is a woman." )]
		[SerializeField]
		private bool isFemaleAuthor;

		[Tooltip ( "The stanzas that make up this poem." )]
		[SerializeField]
		private Stanza [ ] stanzas;

		[Tooltip ( "The difficulty rating of this poem (0 - 5)." )]
		[Range ( -1, 5 )]
		[SerializeField]
		private int rating;

		#endregion // Poem Data

		#region Public Properties

		/// <summary>
		/// The ID of the poem.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The title of this poem.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The author of this poem.
		/// </summary>
		public string Author
		{
			get
			{
				return author;
			}
		}

		/// <summary>
		/// Whether or not the author is a woman.
		/// </summary>
		public bool IsFemaleAuthor
		{
			get
			{
				return isFemaleAuthor;
			}
		}

		/// <summary>
		/// The stanzas that make up this poem.
		/// </summary>
		public Stanza [ ] Stanzas
		{
			get
			{
				return stanzas;
			}
		}

		/// <summary>
		/// The difficulty rating of this poem (1 - 5).
		/// </summary>
		public int Rating
		{
			get
			{
				return rating;
			}
		}

		/// <summary>
		/// The total number of lines in this poem.
		/// </summary>
		public int LineCount
		{
			get
			{
				// Count lines
				int count = 0;
				for ( int i = 0; i < stanzas.Length; i++ )
				{
					count += stanzas [ i ].Lines.Length;
				}

				// Return total
				return count;
			}
		}

		/// <summary>
		/// The total number of characters in this poem.
		/// </summary>
		public int CharacterCount
		{
			get
			{
				// Count characters
				int count = 0;
				for ( int i = 0; i < stanzas.Length; i++ )
				{
					// Count eact line
					for ( int j = 0; j < stanzas [ i ].Lines.Length; j++ )
					{
						count += stanzas [ i ].Lines [ j ].Length;
					}
				}

				// Return total
				return count;
			}
		}

		#endregion // Public Properties
	}
}