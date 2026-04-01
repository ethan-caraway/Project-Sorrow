using UnityEngine;

namespace FlightPaper.ProjectSorrow.Tags
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a tag.
	/// </summary>
	[CreateAssetMenu ( fileName = "Tag", menuName = "Scriptable Objects/Tag" )]
	public class TagScriptableObject : ScriptableObject
	{
		#region Tag Data

		[Tooltip ( "The ID for the tag." )]
		[SerializeField]
		private string id;

		[Tooltip ( "The title of the tag." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the tag." )]
		[TextArea]
		[SerializeField]
		private string description;

		#endregion // Tag Data

		#region Public Properties

		/// <summary>
		/// The ID for the tag.
		/// </summary>
		public string ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The title of the tag.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the tag.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		#endregion // Public Properties
	}
}