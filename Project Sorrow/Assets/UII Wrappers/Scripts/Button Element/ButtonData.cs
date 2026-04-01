using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying a button.
	/// </summary>
	[System.Serializable]
	public class ButtonData
	{
		#region Button Data

		[Tooltip("The image data for the background element.")]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip("The image data for the icon element.")]
		[SerializeField]
		private ImageData iconData;

		[Tooltip("The text data for the text element.")]
		[SerializeField]
		private TextData textData; 

		#endregion // Button Data

		#region Public Properties

		/// <summary>
		/// The image data for the background element.
		/// </summary>
		public ImageData BackgroundData
		{
			get
			{
				return backgroundData;
			}
			set
			{
				backgroundData = value;
			}
		}

		/// <summary>
		/// The image data for the icon element.
		/// </summary>
		public ImageData IconData
		{
			get
			{
				return iconData;
			}
			set
			{
				iconData = value;
			}
		}

		/// <summary>
		/// The text data for the text element.
		/// </summary>
		public TextData TextData
		{
			get
			{
				return textData;
			}
			set
			{
				textData = value;
			}
		}

		#endregion // Public Properties
	}
}