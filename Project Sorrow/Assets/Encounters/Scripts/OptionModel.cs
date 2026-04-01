using UnityEngine;

namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class stores the data for an encounter option.
	/// </summary>
	[System.Serializable]
	public class OptionModel
	{
		#region Encounter Data

		[Tooltip ( "The action title for the option." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The results description for the option." )]
		[SerializeField]
		private string description;

		[Tooltip ( "The requirement description for the option." )]
		[SerializeField]
		private string requirement;

		[Tooltip ( "The encounter conclusion for the option." )]
		[TextArea]
		[SerializeField]
		private string conclusion;

		[Tooltip ( "Whether or not the option is hidden due to a secret requirement." )]
		[SerializeField]
		private bool isHidden;

		[Tooltip ( "The item information to display for the option." )]
		[SerializeField]
		private Items.ItemScriptableObject item;

		[Tooltip ( "The consumable information to display for the option." )]
		[SerializeField]
		private Consumables.ConsumableScriptableObject consumable;

		[Tooltip ( "The tag information to display for the option." )]
		[SerializeField]
		private string tag;

		#endregion // Encounter Data

		#region Public Properties

		/// <summary>
		/// The action title for the option.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The results description for the option.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The requirement description for the option.
		/// </summary>
		public string Requirement
		{
			get
			{
				return requirement;
			}
		}

		/// <summary>
		/// The encounter conclusion for the option.
		/// </summary>
		public string Conclusion
		{
			get
			{
				return conclusion;
			}
		}

		/// <summary>
		/// Whether or not the option is hidden due to a secret requirement.
		/// </summary>
		public bool IsHidden
		{
			get
			{
				return isHidden;
			}
		}

		/// <summary>
		/// The item information to display for the option.
		/// </summary>
		public Items.ItemScriptableObject Item
		{
			get
			{
				return item;
			}
		}

		/// <summary>
		/// The consumable information to display for the option.
		/// </summary>
		public Consumables.ConsumableScriptableObject Consumable
		{
			get
			{
				return consumable;
			}
		}

		/// <summary>
		/// The tag information to display for the option.
		/// </summary>
		public string Tag
		{
			get
			{
				return tag;
			}
		}

		#endregion // Public Properties
	}
}