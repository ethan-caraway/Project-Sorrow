using UnityEngine;

namespace FlightPaper.ProjectSorrow.Poets
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a poet.
	/// </summary>
	[CreateAssetMenu ( fileName = "Poet", menuName = "Scriptable Objects/Poet" )]
	public class PoetScriptableObject : ScriptableObject
	{
		#region Perk Data

		[Tooltip ( "The ID for the poet." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the poet." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the poet." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The full body portrait for the poet." )]
		[SerializeField]
		private Sprite portrait;

		[Tooltip ( "The icon for the poet." )]
		[SerializeField]
		private Sprite icon;

		[Tooltip ( "The unlock criteria for the poet." )]
		[TextArea]
		[SerializeField]
		private string unlockCriteria;

		[Tooltip ( "The data for the poet's perk" )]
		[SerializeField]
		Perks.PerkScriptableObject perk;

		#endregion // Poet Data

		#region Public Properties

		/// <summary>
		/// The ID for the poet.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the poet.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the poet.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The full body portrait for the poet.
		/// </summary>
		public Sprite Portrait
		{
			get
			{
				return portrait;
			}
		}

		/// <summary>
		/// The icon for the perk.
		/// </summary>
		public Sprite Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The unlock criteria of the poet.
		/// </summary>
		public string UnlockCriteria
		{
			get
			{
				return unlockCriteria;
			}
		}

		/// <summary>
		/// The data for the poet's perk.
		/// </summary>
		public Perks.PerkScriptableObject Perk
		{
			get
			{
				return perk;
			}
		}

		/// <summary>
		/// Whether or not this poet is currently unlocked.
		/// </summary>
		public bool IsUnlocked
		{
			get
			{
				return PoetHelper.IsUnlocked ( id );
			}
		}

		#endregion // Public Properties
	}
}