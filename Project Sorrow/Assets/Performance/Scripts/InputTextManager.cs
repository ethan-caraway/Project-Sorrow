using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls reading input from the input field.
	/// </summary>
	public class InputTextManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_InputField inputField;

		[SerializeField]
		private PoemManager manager;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Reads and submits the text being entered into the input field. Only one character is allowed at a time. 
		/// </summary>
		/// <param name="text"> The new text from the input field. </param>
		public void InputText ( string text )
		{
			// Check for empty text
			if ( string.IsNullOrEmpty ( text ) )
			{
				return;
			}

			// Submit the entered text
			manager.OnInputText ( text );

			// Clear the text to ensure only one character is submitted at a time.
			inputField.text = string.Empty;
		}

		#endregion // Public Functions
	}
}