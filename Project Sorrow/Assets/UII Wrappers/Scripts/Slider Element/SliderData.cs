using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying a slider.
	/// </summary>
	[System.Serializable]
	public class SliderData
	{
		#region Slider Data

		[Tooltip ( "The image data for the background element." )]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip ( "The image data for the fill element." )]
		[SerializeField]
		private ImageData fillData;

		[Tooltip ( "The image data for the handle element." )]
		[SerializeField]
		private ImageData handleData;

		#endregion // Slider Data

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
		/// The image data for the fill element.
		/// </summary>
		public ImageData FillData
		{
			get
			{
				return fillData;
			}
			set
			{
				fillData = value;
			}
		}

		/// <summary>
		/// The image data for the handle element.
		/// </summary>
		public ImageData HandleData
		{
			get
			{
				return handleData;
			}
			set
			{
				handleData = value;
			}
		}

		#endregion // Public Properties
	}
}