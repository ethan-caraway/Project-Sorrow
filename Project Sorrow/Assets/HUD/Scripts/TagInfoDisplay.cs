using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of a tag's information.
	/// </summary>
	public class TagInfoDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Sets the tag information to display.
		/// </summary>
		/// <param name="tag"> The data of the tag on display. </param>
		public void SetTag ( Tags.TagScriptableObject tag )
		{
			// Check for tag
			if ( tag == null )
			{
				return;
			}

			// Display item name
			titleText.text = tag.Title;

			// Display item description
			descriptionText.text = tag.Description;
		}

		#endregion // Public Functions
	}
}