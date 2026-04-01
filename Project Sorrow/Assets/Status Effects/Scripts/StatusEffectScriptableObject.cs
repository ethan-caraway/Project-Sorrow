using UnityEngine;

namespace FlightPaper.ProjectSorrow.StatusEffects
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a status effect.
	/// </summary>
	[CreateAssetMenu ( fileName = "Status Effect", menuName = "Scriptable Objects/Status Effect" )]
	public class StatusEffectScriptableObject : ScriptableObject
	{
		#region Consumable Data

		[Tooltip ( "The type of the status effect." )]
		[SerializeField]
		private Enums.StatusEffectType type;

		[Tooltip ( "The tag associated with the status effect." )]
		[SerializeField]
		private string tag;

		[Tooltip ( "The 16x16 icon for the status effect." )]
		[SerializeField]
		private Sprite icon;

		#endregion // Consumable Data

		#region Public Properties

		/// <summary>
		/// The type of the status effect.
		/// </summary>
		public Enums.StatusEffectType Type
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// The tag associate with the status effect.
		/// </summary>
		public string Tag
		{
			get
			{
				return tag;
			}
		}

		/// <summary>
		/// The 16x16 icon for the status effect.
		/// </summary>
		public Sprite Icon
		{
			get
			{
				return icon;
			}
		}

		#endregion // Public Properties
	}
}