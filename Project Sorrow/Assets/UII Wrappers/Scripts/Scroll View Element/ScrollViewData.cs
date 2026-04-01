using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying a scroll view.
	/// </summary>
	[System.Serializable]
	public class ScrollViewData
	{
		#region Scrollbar Data

		[Tooltip ( "The image data for the background element." )]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip ( "The image data for the viewport element." )]
		[SerializeField]
		private ImageData viewportData;

		[Tooltip ( "The scrollbar data for the horizontal scrollbar element." )]
		[SerializeField]
		private ScrollbarData horizontalScrollbarData;

		[Tooltip ( "The scrollbar data for the vertical scrollbar element." )]
		[SerializeField]
		private ScrollbarData verticalScrollbarData;

		#endregion // Scrollbar Data

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
		/// The image data for the viewport element.
		/// </summary>
		public ImageData ViewportData
		{
			get
			{
				return viewportData;
			}
			set
			{
				viewportData = value;
			}
		}

		/// <summary>
		/// The scrollbar data for the horizontal scrollbar element.
		/// </summary>
		public ScrollbarData HorizontalScrollbarData
		{
			get
			{
				return horizontalScrollbarData;
			}
			set
			{
				horizontalScrollbarData = value;
			}
		}

		/// <summary>
		/// The scrollbar data for the vertical scrollbar element.
		/// </summary>
		public ScrollbarData VerticalScrollbarData
		{
			get
			{
				return verticalScrollbarData;
			}
			set
			{
				verticalScrollbarData = value;
			}
		}

		#endregion // Public Properties
	}
}