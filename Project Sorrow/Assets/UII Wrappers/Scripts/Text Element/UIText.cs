using FlightPaper.Data;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls text as a UI element.
	/// </summary>
	public class UIText : UIElement
	{
		#region UI Elements

		[SerializeField]
		private TextMeshProUGUI text;

		[SerializeField]
		private TextLocalizer localizer;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The text UI element.
		/// </summary>
		public TextMeshProUGUI Text
		{
			get
			{
				return text;
			}
		}

		/// <summary>
		/// The localizer for the text UI element.
		/// </summary>
		public TextLocalizer Localizer
		{
			get
			{
				return localizer;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this text is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && text != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Text.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Text instance. </returns>
		public static UIText CreateUIText ( string gameObjectName = "Text" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName );

			// Add UI text
			UIText uiText = obj.AddComponent<UIText> ( );

			// Add TMP text element
			uiText.text = obj.AddComponent<TextMeshProUGUI> ( );

			// Add localizer
			uiText.localizer = obj.AddComponent<TextLocalizer> ( );

			// Return the created UI text
			return uiText;
		}

		/// <summary>
		/// Creates an instance of a UI Text in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem("GameObject/UI/UI Text")]
		private static void CreateUIText ( MenuCommand command )
		{
			// Create UI text
			GameObject obj = CreateUIText ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Text" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this text element.
		/// </summary>
		/// <param name="unlocalizedText"> The unlocalized text to display in the UI element. </param>
		public void Initialize ( string unlocalizedText )
		{
			// Display element
			SetActive ( true );

			// Check for text
			if ( !IsValid ( ) || string.IsNullOrEmpty ( unlocalizedText ) )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Disable localization
			if ( localizer != null )
			{
				localizer.enabled = false;
			}

			// Update text
			text.text = unlocalizedText;
		}

		/// <summary>
		/// Sets this text element.
		/// </summary>
		/// <param name="data"> The text data to display in the UI element. </param>
		public void Initialize ( TextData data )
		{
			// Display element
			SetActive ( true );

			// Check for text
			if ( !IsValid ( ) || data == null || !data.IsValid ( ) )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Check for localized text
			if ( localizer != null && data.Localized != null && data.Localized.IsValid ( ) )
			{
				// Ensure the localizer is enabled
				localizer.enabled = true;

				// Update localized text
				localizer.SetReference ( data.Localized, data.Arguments );
				return;
			}

			// Update unlocalized text
			// NOTE: Even if the text data is localized, the text element will be considered unlocalized if a localizer is not present to update the text on localization changes
			text.text = data.ToString ( );
		}

		/// <summary>
		/// Sets the color of the text element.
		/// </summary>
		/// <param name="color"> The color to update the text. </param>
		public void SetColor ( Color color )
		{
			// Check for text
			if ( IsValid ( ) )
			{
				// Update text color
				text.color = color;
			}
		}

		#endregion // Public Functions
	}
}