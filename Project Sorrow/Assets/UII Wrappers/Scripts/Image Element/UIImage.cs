using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls an image as a UI element.
	/// </summary>
	public class UIImage : UIElement
	{
		#region UI Elements

		[SerializeField]
		private Image image;

		[SerializeField]
		private RawImage rawImage;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The UI element for displaying a sprite.
		/// </summary>
		public Image Image
		{
			get
			{
				return image;
			}
		}

		/// <summary>
		/// The UI element for displaying a texture.
		/// </summary>
		public RawImage RawImage
		{
			get
			{
				return rawImage;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this image is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && ( image != null || rawImage != null );
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Image.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Image instance. </returns>
		public static UIImage CreateUIImage ( string gameObjectName = "Image" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName );

			// Add UI image
			UIImage uiImage = obj.AddComponent<UIImage> ( );

			// Add image element
			uiImage.image = obj.AddComponent<Image> ( );

			// Return the created UI image
			return uiImage;
		}

		/// <summary>
		/// Creates an instance of a UI Raw Image.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Image instance. </returns>
		public static UIImage CreateUIRawImage ( string gameObjectName = "Raw Image" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName );

			// Add UI image
			UIImage uiImage = obj.AddComponent<UIImage> ( );

			// Add raw image element
			uiImage.rawImage = obj.AddComponent<RawImage> ( );

			// Return the created UI image
			return uiImage;
		}

		/// <summary>
		/// Creates an instance of a UI Image in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Image" )]
		private static void CreateUIImage ( MenuCommand command )
		{
			// Create UI image
			GameObject obj = CreateUIImage ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Image" );
			Selection.activeObject = obj;
		}

		/// <summary>
		/// Creates an instance of a UI Raw Image in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Raw Image" )]
		private static void CreateUIRawImage ( MenuCommand command )
		{
			// Create UI raw image
			GameObject obj = CreateUIRawImage ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Raw Image" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this image element.
		/// </summary>
		/// <param name="sprite"> The sprite to display in the UI element. </param>
		public void Initialize ( Sprite sprite )
		{
			// Display element
			SetActive ( true );

			// Check for image
			if ( !IsValid ( ) || sprite == null )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Check for image
			if ( image != null )
			{
				image.sprite = sprite;
				return;
			}

			// Check for raw image
			if ( rawImage != null )
			{
				rawImage.texture = sprite.texture;
			}
		}

		/// <summary>
		/// Sets this image element.
		/// </summary>
		/// <param name="sprite"> The image data to display in the UI element. </param>
		public async void Initialize ( ImageData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this image element asynchronously.
		/// </summary>
		/// <param name="sprite"> The image data to display in the UI element. </param>
		public async Task InitializeAsync ( ImageData data )
		{
			// Display element
			SetActive ( true );

			// Check for image
			if ( !IsValid ( ) || data == null || !data.IsValid ( ) )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Get the sprite
			Sprite sprite = await data.LoadSpriteAsync ( );

			// Check for sprite
			if ( sprite == null )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Check for image
			if ( image != null )
			{
				image.sprite = sprite;
				return;
			}

			// Check for raw image
			if ( rawImage != null )
			{
				rawImage.texture = sprite.texture;
			}
		}

		/// <summary>
		/// Sets the color of the image.
		/// </summary>
		/// <param name="color"> The color overlay for the image. </param>
		public void SetColor ( Color color )
		{
			// Check for image
			if ( image != null )
			{
				image.color = color;
				return;
			}

			// Check for raw image
			if ( rawImage != null )
			{
				rawImage.color = color;
			}
		}

		#endregion // Public Functions
	}
}