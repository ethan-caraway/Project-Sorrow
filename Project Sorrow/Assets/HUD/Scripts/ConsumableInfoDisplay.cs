using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of an consumable's information.
	/// </summary>
	public class ConsumableInfoDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Sets the consumable information to display.
		/// </summary>
		/// <param name="consumable"> The data of the consumable on display. </param>
		public void SetConsumable ( Consumables.ConsumableScriptableObject consumable )
		{
			// Check for consumable
			if ( consumable == null )
			{
				return;
			}

			// Display consumable name
			titleText.text = consumable.Title;

			// Display consumable description
			descriptionText.text = consumable.Description;
		}

		#endregion // Public Functions
	}
}