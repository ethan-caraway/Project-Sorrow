using UnityEngine;
using UnityEngine.InputSystem;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls rotating a UI element toward the pointer when hovering.
	/// </summary>
	public class PointerBillboard : MonoBehaviour
	{
		#region Billboard Data Constants

		private const float MAX_ROTATION = 30f;

		#endregion // Billboard Data Constants

		#region UI Elements

		[SerializeField]
		private RectTransform targetTransform;

		[SerializeField]
		private Transform rotationTransform;

		#endregion // UI Elements

		#region Billboard Data

		private bool isHovering = false;

		private Camera cam;

		#endregion // Billboard Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Store camera
			cam = Camera.main;
		}

		private void Update ( )
		{
			// Check if hovering
			if ( isHovering )
			{
				// Get mouse position
				Vector2 mousePos = GetPointerHoverPosition ( );

				// Track rotation
				Vector3 rotation = Vector3.zero;

				// Get the x rotation
				if ( mousePos.x < 0 )
				{
					rotation.y = Mathf.Lerp ( 0f, MAX_ROTATION, mousePos.x / ( targetTransform.rect.width / -2f ) );
				}
				else
				{
					rotation.y = Mathf.Lerp ( 0f, -MAX_ROTATION, mousePos.x / ( targetTransform.rect.width / 2f ) );
				}

				// Get the y rotation
				if ( mousePos.y < 0 )
				{
					rotation.x = Mathf.Lerp ( 0f, -MAX_ROTATION, mousePos.y / ( targetTransform.rect.height / -2f ) );
				}
				else
				{
					rotation.x = Mathf.Lerp ( 0f, MAX_ROTATION, mousePos.y / ( targetTransform.rect.height / 2f ) );
				}
				
				// Rotate billboard
				rotationTransform.rotation = Quaternion.Euler ( rotation );
			}
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// The callback for when the mouse pointer begins hovering over the target.
		/// </summary>
		public void OnHoverEnter ( )
		{
			// Mark that the item is being hovered
			isHovering = true;
		}

		/// <summary>
		/// The callback for when the mouse pointer ends hovering over the target.
		/// </summary>
		public void OnHoverExit ( )
		{
			// Mark that the item is no longer being hovered
			isHovering = false;

			// Reset rotation
			ResetBillboard ( );
		}

		/// <summary>
		/// Resets the rotation of the billboard
		/// </summary>
		public void ResetBillboard ( )
		{
			// Prevent addition further hovering
			isHovering = false;

			// Reset rotation
			rotationTransform.rotation = Quaternion.identity;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Gets the coordinates of the mouse relative to the anchor position of the target.
		/// </summary>
		/// <returns></returns>
		private Vector2 GetPointerHoverPosition ( )
		{
			// Track position
			Vector2 pos;

			// Get position in the target
			RectTransformUtility.ScreenPointToLocalPointInRectangle ( targetTransform, Mouse.current.position.value, cam, out pos );

			// Return position relative to the anchor
			return pos;
		}

		#endregion // Private Functions
	}
}