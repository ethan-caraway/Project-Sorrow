using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying an input field.
	/// </summary>
	[System.Serializable]
	public class InputFieldData
	{
		#region Input Field Data

		[Tooltip ( "The image data for the background element." )]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip ( "The text data for the placeholder element." )]
		[SerializeField]
		private TextData placeholderData;

		[Tooltip ( "The text data for the text element." )]
		[SerializeField]
		private TextData textData;

		#endregion // Input Field Data

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
		/// The text data for the placeholder element.
		/// </summary>
		public TextData PlaceholderData
		{
			get
			{
				return placeholderData;
			}
			set
			{
				placeholderData = value;
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