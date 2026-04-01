using UnityEngine;

namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class is a scriptable object that stores the data for an encounter.
	/// </summary>
	[CreateAssetMenu ( fileName = "Encounter", menuName = "Scriptable Objects/Encounter" )]
	public class EncounterScriptableObject : ScriptableObject
	{
		#region Encounter Data

		[Tooltip ( "The ID of the encounter." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The title of the encounter." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the encounter." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The options data of the encounter." )]
		[SerializeField]
		private OptionModel [ ] options;

		[Tooltip ( "The minimum round that the encounter can appear." )]
		[SerializeField]
		private int minRound;

		#endregion // Encounter Data

		#region Public Properties

		/// <summary>
		/// The ID of the encounter.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The title of the encounter.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the encounter.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The options data for the encounter.
		/// </summary>
		public OptionModel [ ] Options
		{
			get
			{
				return options;
			}
		}

		/// <summary>
		/// The minimum round that the encounter can appear.
		/// </summary>
		public int MinRound
		{
			get
			{
				return minRound;
			}
		}

		#endregion // Public Properties
	}
}