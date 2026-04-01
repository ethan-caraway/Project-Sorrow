using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class forces the input field for typing to always be focused.
	/// </summary>
	public class FocusEnforcer : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_InputField inputField;

		#endregion // UI Elements

		#region MonoBehaviour Functions

		private void Awake ( )
		{
			// Check for input field
			if ( inputField == null )
			{
				inputField = GetComponent<TMP_InputField> ( );
			}

			// Force focus
			EventSystem.current.SetSelectedGameObject ( inputField.gameObject );
			IgnoreSubmit ( );
		}

		private void Update ( )
		{
			// Check if focus is removed
			if ( EventSystem.current.currentSelectedGameObject != inputField.gameObject )
			{
				// Force focus
				EventSystem.current.SetSelectedGameObject ( inputField.gameObject );
			}

			// Ensure input while enabled
			IgnoreSubmit ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Ignores submitting the input field text when pressing Enter. The input field remains focused and editable.
		/// </summary>
		public void IgnoreSubmit ( )
		{
			inputField.ActivateInputField ( );
			inputField.Select ( );
		}

		#endregion // Public Functions
	}
}