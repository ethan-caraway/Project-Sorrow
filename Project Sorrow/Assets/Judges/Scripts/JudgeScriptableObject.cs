using UnityEngine;

namespace FlightPaper.ProjectSorrow.Judges
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a judge.
	/// </summary>
	[CreateAssetMenu ( fileName = "Judge", menuName = "Scriptable Objects/Judge" )]
	public class JudgeScriptableObject : ScriptableObject
	{
		#region Judge Data Constants

		/// <summary>
		/// The minimum round value for boss judges.
		/// </summary>
		public const int BOSS_MIN_ROUND = 1000;

		#endregion // Judge Data Constants

		#region Judge Data

		[Tooltip ( "The ID of the judge." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the judge." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the judge." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The tag associated with the judge.")]
		[SerializeField]
		private string tag;

		[Tooltip ( "The icon of the judge." )]
		[SerializeField]
		private Sprite icon;

		[Tooltip ( "The quotes from the judge." )]
		[SerializeField]
		private string [ ] quotes;

		[Tooltip ( "The minimum round that the judge can appear in. Boss judges will have 1000." )]
		[SerializeField]
		private int minRound;

		#endregion // Judge Data

		#region Public Properties

		/// <summary>
		/// The ID of the judge.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the judge.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the judge.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// Whether or not there is a tag associated with the judge.
		/// </summary>
		public bool HasTag
		{
			get
			{
				return !string.IsNullOrEmpty ( tag );
			}
		}

		/// <summary>
		/// The tag associated with the judge.
		/// </summary>
		public string Tag
		{
			get
			{
				return tag;
			}
		}

		/// <summary>
		/// The icon of the judge.
		/// </summary>
		public Sprite Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The quotes from the judge.
		/// </summary>
		public string [ ] Quotes
		{
			get
			{
				return quotes;
			}
		}

		/// <summary>
		/// The minimum round that the judge can appear in.
		/// Boss judges will have 1000.
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