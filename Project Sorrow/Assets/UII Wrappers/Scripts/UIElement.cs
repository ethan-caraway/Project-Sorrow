using UnityEngine;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the base functionality for UI elements.
	/// </summary>
	public class UIElement : MonoBehaviour
	{
		#region Public Properties

		/// <summary>
		/// The game object for this UI element.
		/// </summary>
		public GameObject GameObject
		{
			get
			{
				return gameObject;
			}
		}

		/// <summary>
		/// The rect transform for this UI element.
		/// </summary>
		public RectTransform RectTransform
		{
			get
			{
				return transform as RectTransform;
			}
		}

		/// <summary>
		/// Whether or not this game object is active.
		/// </summary>
		public bool IsActive
		{
			get
			{
				return gameObject.activeSelf;
			}
		}

		#endregion // Public Properties

		#region Public Virtual Functions

		/// <summary>
		/// Gets whether or not this is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public virtual bool IsValid ( )
		{
			return gameObject != null;
		}

		#endregion // Public Virtual Functions

		#region Public Functions

		/// <summary>
		/// Toggles the game object for this UI element.
		/// </summary>
		/// <param name="isActive"> Whether or not the game object should be active. </param>
		public void SetActive ( bool isActive )
		{
			// Check for game object
			if ( gameObject != null )
			{
				// Toggle game objects
				gameObject.SetActive ( isActive );
			}
		}

		#endregion // Public Functions
	}
}