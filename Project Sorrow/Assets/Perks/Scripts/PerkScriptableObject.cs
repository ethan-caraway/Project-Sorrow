using UnityEngine;

namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class is a scriptable object that stores the data for a perk.
	/// </summary>
	[CreateAssetMenu ( fileName = "Perk", menuName = "Scriptable Objects/Perk" )]
	public class PerkScriptableObject : ScriptableObject
	{
		#region Perk Data

		[Tooltip ( "The ID for the perk." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the perk." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the perk." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The icon for the consumable." )]
		[SerializeField]
		private Sprite icon;

		#endregion // Perk Data

		#region Public Properties

		/// <summary>
		/// The ID for the perk.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the perk.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the perk.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
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

		#endregion // Public Properties
	}
}