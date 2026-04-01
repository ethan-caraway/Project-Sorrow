using UnityEngine;

namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class controls the camera's orthographic size to fit the aspect ratio of the environment.
	/// </summary>
	public class ForceCameraSize : MonoBehaviour
	{
		#region Camera Data Constants

		private const float DEFAULT_SIZE = 5.4f;

		#endregion // Camera Data Constants

		#region Camera Data

		[SerializeField]
		private Camera cam;

		private float width;
		private float height;

		#endregion // Camera Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Check for camera
			if ( cam == null )
			{
				cam = GetComponent<Camera> ( );
			}

			// Set camera size
			SetSize ( );
		}

		private void Update ( )
		{
			// Check for change is resolution
			if ( width != Screen.width || height != Screen.height )
			{
				// Update camera size
				SetSize ( );
			}
		}

		#endregion // MonoBehaviour Functions

		#region Private Functions

		/// <summary>
		/// Forces the orthographic size of the camera.
		/// </summary>
		private void SetSize ( )
		{
			// Store screen size
			width = Screen.width;
			height = Screen.height;;

			// Get aspect ratio
			float aspectRatio = width / height;
			
			// Check aspect ratio
			if ( aspectRatio < 16f / 9f )
			{
				// Store scaler
				float scaler = DEFAULT_SIZE * ( 16f / 9f );

				// Force camera size
				cam.orthographicSize = scaler / aspectRatio;
			}
			else
			{
				// Force camera size
				cam.orthographicSize = DEFAULT_SIZE;
			}
		}

		#endregion // Private Functions
	}
}