using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying a toggle.
	/// </summary>
	[System.Serializable]
	public class ToggleData
	{
		#region Toggle Data

		[Tooltip ( "The image data for the background element." )]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip ( "The image data for the checkbox element." )]
		[SerializeField]
		private ImageData checkboxData;

		[Tooltip ( "The image data for the checkmark element." )]
		[SerializeField]
		private ImageData checkmarkData;

		[Tooltip ( "The image data for the icon element." )]
		[SerializeField]
		private ImageData iconData;

		[Tooltip ( "The text data for the text element." )]
		[SerializeField]
		private TextData textData;

		#endregion // Toggle Data

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
		/// The image data for the checkbox element.
		/// </summary>
		public ImageData CheckboxData
		{
			get
			{
				return checkboxData;
			}
			set
			{
				checkboxData = value;
			}
		}

		/// <summary>
		/// The image data for the checkmark element.
		/// </summary>
		public ImageData CheckmarkData
		{
			get
			{
				return checkmarkData;
			}
			set
			{
				checkmarkData = value;
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