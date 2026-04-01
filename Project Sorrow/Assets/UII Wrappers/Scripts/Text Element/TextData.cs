using UnityEngine;
using UnityEngine.Localization;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying text.
	/// </summary>
	[System.Serializable]
	public class TextData
	{
		#region Text Data

		[Tooltip("The localized data for the text.")]
		[SerializeField]
		private LocalizedString localized = new LocalizedString ( );

		[Tooltip("The unlocalized data for the text. This should be used if localization is not provided.")]
		[SerializeField]
		private string unlocalized;

		[Tooltip("The dynamic arguments for the text to replace at runtime.")]
		[SerializeField]
		private object [ ] arguments;

		#endregion // Text Data

		#region Public Properties

		/// <summary>
		/// The localized data for the text.
		/// </summary>
		public LocalizedString Localized
		{
			get
			{
				return localized;
			}
			set
			{
				localized = value;
			}
		}

		/// <summary>
		/// The unlocalized data for the text.
		/// This should be used if localization is not provided.
		/// </summary>
		public string Unlocalized
		{
			get
			{
				return unlocalized;
			}
			set
			{
				unlocalized = value;
			}
		}

		/// <summary>
		/// The dynamic arguments for the text to replace at runtime.
		/// </summary>
		public object [ ] Arguments
		{
			get
			{
				return arguments;
			}
			set
			{
				arguments = value;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Gets whether or not this text data contains valid data.
		/// </summary>
		/// <returns> Whether or not this text data is valid. </returns>
		public bool IsValid ( )
		{
			return !string.IsNullOrEmpty ( unlocalized ) || localized.IsValid ( );
		}

		/// <summary>
		/// Gets the string value for this text data.
		/// </summary>
		/// <returns> The string value for this text data. </returns>
		public override string ToString ( )
		{
			// Check for localized text
			if ( localized.IsValid ( ) )
			{
				// Check for arguments
				if ( arguments != null && arguments.Length > 0 )
				{
					// Return the localized text with the arguments replaced
					return localized.GetLocalizedString ( arguments );
				}
				else
				{
					// Return the localized text
					return localized.GetLocalizedString ( );
				}
			}

			// Check for arguments
			if ( arguments != null && arguments.Length > 0 )
			{
				// Return the unlocalized text with the arguments replaced
				return string.Format ( unlocalized, arguments );
			}
			else
			{
				// Return the unlocalized text
				return unlocalized;
			}
		}

		#endregion // Public Functions
	}
}