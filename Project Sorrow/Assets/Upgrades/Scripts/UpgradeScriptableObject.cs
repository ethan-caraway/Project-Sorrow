using UnityEngine;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class is a scriptable object that stores the data for an upgrade.
	/// </summary>
	[CreateAssetMenu ( fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade" )]
	public class UpgradeScriptableObject : ScriptableObject
	{
		#region Upgrade Data

		[Tooltip ( "The ID for the upgrade." )]
		[SerializeField]
		private int id;

		[Tooltip ( "The name of the upgrade." )]
		[SerializeField]
		private string title;

		[Tooltip ( "The description of the upgrade." )]
		[TextArea]
		[SerializeField]
		private string description;

		[Tooltip ( "The icon for the upgrade." )]
		[SerializeField]
		private Sprite icon;

		#endregion // Upgrade Data

		#region Public Properties

		/// <summary>
		/// The ID for the upgrade.
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The name of the upgrade.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// The description of the upgrade.
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}

		/// <summary>
		/// The icon for the upgrade.
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