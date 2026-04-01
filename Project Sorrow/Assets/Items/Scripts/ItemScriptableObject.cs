using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class is a scriptable object that stores the data for an item.
	/// </summary>
	[CreateAssetMenu ( fileName = "Item", menuName = "Scriptable Objects/Item" )]
	public class ItemScriptableObject : ScriptableObject
	{
		#region Item Data

		[Tooltip ( "The ID for the item." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the item." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the item." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The tag associated with the item.")]
		[SerializeField]
		private string tag;

		[Tooltip ( "The 128x128 icon for the item." )]
		[SerializeField]
		private Sprite icon;

		[Tooltip ( "The 64x64 icon for the item." )]
		[SerializeField]
		private Sprite smallIcon;

		[Tooltip ( "The rarity of the item." )]
		[SerializeField]
		private Enums.Rarity rarity;

		[Tooltip ( "If and how the value of the item scales." )]
		[SerializeField]
		private Enums.ScaleType scaleType;

		[Tooltip ( "Whether or not the description has a variable replacement." )]
		[SerializeField]
		private bool isVariableDescription;

		[Tooltip ( "Whether or not the item provides utility rather than earning snaps." )]
		[SerializeField]
		private bool isUtility;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// The ID for the item.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the item.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the item.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// Whether or not there is a tag associated with the item.
		/// </summary>
		public bool HasTag
		{
			get
			{
				return !string.IsNullOrEmpty ( tag );
			}
		}

		/// <summary>
		/// The tag associated with the item.
		/// </summary>
		public string Tag
		{
			get
			{
				return tag;
			}
		}

		/// <summary>
		/// The 128x128 icon for the item.
		/// </summary>
		public Sprite Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The 64x64 icon for the item.
		/// </summary>
		public Sprite SmallIcon
		{
			get
			{
				return smallIcon;
			}
		}

		/// <summary>
		/// The rarity of the item.
		/// </summary>
		public Enums.Rarity Rarity
		{
			get
			{
				return rarity;
			}
		}

		/// <summary>
		/// Whether or not the value of the item scales.
		/// </summary>
		public bool IsScalable
		{
			get
			{
				return scaleType != Enums.ScaleType.NONE;
			}
		}

		/// <summary>
		/// If and how the value of the item scales.
		/// </summary>
		public Enums.ScaleType ScaleType
		{
			get
			{
				return scaleType;
			}
		}

		/// <summary>
		/// Whether or not the description has a variable replacement.
		/// </summary>
		public bool IsVariableDescription
		{
			get
			{
				return isVariableDescription;
			}
		}

		/// <summary>
		/// Whether or not the item provides utility rather than earning snaps.
		/// </summary>
		public bool IsUtility
		{
			get
			{
				return isUtility;
			}
		}

		#endregion // Public Properties
	}
}