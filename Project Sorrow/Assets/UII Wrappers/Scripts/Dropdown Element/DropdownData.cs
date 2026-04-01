using UnityEngine;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying a dropdown.
	/// </summary>
	[System.Serializable]
	public class DropdownData
	{
		#region Dropdown Data

		[Tooltip ( "The image data for the background element." )]
		[SerializeField]
		private ImageData backgroundData;

		[Tooltip ( "The text data for the label text element." )]
		[SerializeField]
		private TextData labelTextData;

		[Tooltip ( "The image data for the label icon element." )]
		[SerializeField]
		private ImageData labelIconData;

		[Tooltip ( "The image data for the arrow element." )]
		[SerializeField]
		private ImageData arrowData;

		[Tooltip ( "The scroll view data for the scroll view template element." )]
		[SerializeField]
		private ScrollViewData scrollViewTemplateData;

		[Tooltip ( "The toggle data for the item template element." )]
		[SerializeField]
		private ToggleData itemTemplateData;

		[Tooltip ( "The data for the item elements in this dropdown." )]
		[SerializeField]
		private DropdownItemData [ ] itemData;

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
		/// The text data for the label text element.
		/// </summary>
		public TextData LabelTextData
		{
			get
			{
				return labelTextData;
			}
			set
			{
				labelTextData = value;
			}
		}

		/// <summary>
		/// The image data for the label icon element.
		/// </summary>
		public ImageData LabelIconData
		{
			get
			{
				return labelIconData;
			}
			set
			{
				labelIconData = value;
			}
		}

		/// <summary>
		/// The image data for the arrow element.
		/// </summary>
		public ImageData ArrowData
		{
			get
			{
				return arrowData;
			}
			set
			{
				arrowData = value;
			}
		}

		/// <summary>
		/// The scroll view data for the scroll view template element.
		/// </summary>
		public ScrollViewData ScrollViewTemplateData
		{
			get
			{
				return scrollViewTemplateData;
			}
			set
			{
				scrollViewTemplateData = value;
			}
		}

		/// <summary>
		/// The toggle data for the item template element.
		/// </summary>
		public ToggleData ItemTemplateData
		{
			get
			{
				return itemTemplateData;
			}
			set
			{
				itemTemplateData = value;
			}
		}

		/// <summary>
		/// The data for the item elements in the dropdown.
		/// </summary>
		public DropdownItemData [ ] ItemData
		{
			get
			{
				return itemData;
			}
			set
			{
				itemData = value;
			}
		}

		#endregion // Public Properties
	}
}