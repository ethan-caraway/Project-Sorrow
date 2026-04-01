using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for an item in a dropdown.
	/// </summary>
	[System.Serializable]
	public class DropdownItemData
	{
		#region Item Data

		[Tooltip("The text data for the text element of the item.")]
		[SerializeField]
		private TextData text;

		[Tooltip("The image data for the image element of the item.")]
		[SerializeField]
		private ImageData image;

		#endregion // Item Data

		#region Public Properties

		/// <summary>
		/// The text data for the text element of the item.
		/// </summary>
		public TextData Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		/// <summary>
		/// The image data for the image element of the item.
		/// </summary>
		public ImageData Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;
			}
		}

		#endregion // Public Properties
	}
}