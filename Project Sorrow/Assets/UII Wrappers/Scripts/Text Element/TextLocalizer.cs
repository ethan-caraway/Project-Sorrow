using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the localization of a UI Text elements.
	/// </summary>
	[RequireComponent ( typeof ( TextMeshProUGUI ) )]
	public class TextLocalizer : UnityEngine.Localization.Components.LocalizeStringEvent
	{
		#region UI Elements

		private TextMeshProUGUI text;

		#endregion // UI Elements

		#region LocalizeStringEvent Override Functions

		protected override void UpdateString ( string value )
		{
			// Get text element
			if ( text == null )
			{
				text = GetComponent<TextMeshProUGUI> ( );
			}

			// Update text element
			if ( text != null )
			{
				text.text = value;
			}
		}

		#endregion // LocalizeStringEvent Override Functions

		#region Public Functions

		/// <summary>
		/// Sets the localization data for the localizer.
		/// </summary>
		/// <param name="data"> The localization data. </param>
		/// <param name="arguments"> The persistent arguments for replacement at runtime. </param>
		public void SetReference ( LocalizedString data, IList<object> arguments = null )
		{
			// Check for arguments
			if ( arguments != null )
			{
				// Set argument parameters
				StringReference.Arguments = arguments;
			}

			// Set localization reference
			StringReference.SetReference ( data.TableReference, data.TableEntryReference );
		}

		/// <summary>
		/// Sets the persistent arguments for replacement at runtime.
		/// </summary>
		/// <param name="arguments"> The arguments for replacement. </param>
		public void SetArguments ( IList<object> arguments = null )
		{
			// Set argument parameters
			StringReference.Arguments = arguments;

			// Update localiation
			StringReference.RefreshString ( );
		}

		#endregion // Public Functions
	}
}