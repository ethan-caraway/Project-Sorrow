using UnityEngine;

namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a consumable.
	/// </summary>
	[CreateAssetMenu ( fileName = "Consumable", menuName = "Scriptable Objects/Consumable" )]
	public class ConsumableScriptableObject : ScriptableObject
	{
		#region Consumable Data

		[Tooltip ( "The ID for the consumable." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the consumable." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the consumable." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The tag associated with the consumable." )]
		[SerializeField]
		private string tag;

		[Tooltip ( "The 128x128 icon for the consumable." )]
		[SerializeField]
		private Sprite icon;

		[Tooltip ( "The 64x64 icon for the consumable." )]
		[SerializeField]
		private Sprite smallIcon;

		[Tooltip ( "The rarity of the consumable." )]
		[SerializeField]
		private Enums.Rarity rarity;

		[Tooltip ( "The type of the consumable." )]
		[SerializeField]
		private Enums.ConsumableType type;

		#endregion // Consumable Data

		#region Public Properties

		/// <summary>
		/// The ID for the consumable.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the consumable.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the consumable.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// Whether or not there is a tag associated with the consumable
		/// </summary>
		public bool HasTag
		{
			get
			{
				return !string.IsNullOrEmpty ( tag );
			}
		}

		/// <summary>
		/// The tag associate with the consumable.
		/// </summary>
		public string Tag
		{
			get
			{
				return tag;
			}
		}

		/// <summary>
		/// The 128x128 icon for the consumable.
		/// </summary>
		public Sprite Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The 64x64 icon for the consumable.
		/// </summary>
		public Sprite SmallIcon
		{
			get
			{
				return smallIcon;
			}
		}

		/// <summary>
		/// The rarity of the consumable.
		/// </summary>
		public Enums.Rarity Rarity
		{
			get
			{
				return rarity;
			}
		}

		/// <summary>
		/// The type of the consumable.
		/// </summary>
		public Enums.ConsumableType Type
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// Whether or not this consumable must be applied to a poem.
		/// </summary>
		public bool IsApplyToPoem
		{
			get
			{
				return type == Enums.ConsumableType.MODIFIER || type == Enums.ConsumableType.ENHANCEMENT;
			}
		}

		#endregion // Public Properties
	}
}