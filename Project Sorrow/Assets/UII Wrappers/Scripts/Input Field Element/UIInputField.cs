using FlightPaper.Data;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise an input field.
	/// </summary>
	public class UIInputField : UIElement
	{
		#region UI Elements

		[SerializeField]
		private TMP_InputField inputField;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIText placeholder;

		[SerializeField]
		private UIText text;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public TMP_InputField InputField
		{
			get
			{
				return inputField;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the input field.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the placeholder text for the input field.
		/// </summary>
		public UIText Placeholder
		{
			get
			{
				return placeholder;
			}
		}

		/// <summary>
		/// The UI element for displaying the text for the input field.
		/// </summary>
		public UIText Text
		{
			get
			{
				return text;
			}
		}

		/// <summary>
		/// Whether or not the input field element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return inputField.interactable;
			}
			set
			{
				inputField.interactable = value;
			}
		}

		/// <summary>
		/// The text value for the input field.
		/// </summary>
		public string Value
		{
			get
			{
				return inputField.text;
			}
			set
			{
				inputField.text = value;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this slider is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && inputField != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Input Field.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Input Field instance. </returns>
		public static UIInputField CreateUIInputField ( string gameObjectName = "Input Field" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI input field
			UIInputField uiInputField = obj.AddComponent<UIInputField> ( );
			uiInputField.RectTransform.sizeDelta = new Vector2 ( 150, 30 );

			// Add input field
			uiInputField.inputField = obj.AddComponent<TMP_InputField> ( );

			// Add background
			uiInputField.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiInputField.background.GameObject, obj );
			uiInputField.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiInputField.background.RectTransform.anchorMax = Vector2.one;
			uiInputField.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiInputField.background.RectTransform.offsetMax = Vector2.zero;
			uiInputField.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiInputField.background.Image.type = Image.Type.Sliced;

			// Add text area
			RectTransform textArea = new GameObject ( "Text Area", typeof ( RectTransform ) ).GetComponent<RectTransform> ( );

			// Align text area as a child element
			GameObjectUtility.SetParentAndAlign ( textArea.gameObject, obj );
			textArea.anchorMin = Vector2.zero; // Set anchors to stretch
			textArea.anchorMax = Vector2.one;
			textArea.offsetMin = new Vector2 ( 10, 5 ); // Set size and position to fill with a buffer
			textArea.offsetMax = new Vector2 ( -10, -5 );

			// Add rect mask
			RectMask2D mask = textArea.gameObject.AddComponent<RectMask2D> ( );
			mask.padding = Vector4.one * -5;

			// Add placeholder
			uiInputField.placeholder = UIText.CreateUIText ( "Placeholder" );

			// Align placeholder as a child element of the text area
			GameObjectUtility.SetParentAndAlign ( uiInputField.placeholder.GameObject, textArea.gameObject );
			uiInputField.placeholder.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiInputField.placeholder.RectTransform.anchorMax = Vector2.one;
			uiInputField.placeholder.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiInputField.placeholder.RectTransform.offsetMax = Vector2.zero;

			// Set default placeholder value
			uiInputField.placeholder.Text.text = "Enter text...";
			uiInputField.placeholder.Text.alignment = TextAlignmentOptions.Left;
			uiInputField.placeholder.Text.fontStyle = FontStyles.Italic;
			uiInputField.placeholder.Text.fontSize = 14;
			uiInputField.placeholder.SetColor ( new Color ( 0f, 0f, 0f, 0.5f ) );

			// Add text
			uiInputField.text = UIText.CreateUIText ( );

			// Aligh text as a child element of the text area
			GameObjectUtility.SetParentAndAlign ( uiInputField.text.GameObject, textArea.gameObject );
			uiInputField.text.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiInputField.text.RectTransform.anchorMax = Vector2.one;
			uiInputField.text.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiInputField.text.RectTransform.offsetMax = Vector2.zero;

			// Set default text value
			uiInputField.text.Text.alignment = TextAlignmentOptions.Left;
			uiInputField.text.Text.fontSize = 14;
			uiInputField.text.SetColor ( Color.black );

			// Set targets for input field
			uiInputField.inputField.targetGraphic = uiInputField.background.Image;
			uiInputField.inputField.textViewport = textArea;
			uiInputField.inputField.textComponent = uiInputField.text.Text;
			uiInputField.inputField.placeholder = uiInputField.placeholder.Text;

			// Set default input field value
			uiInputField.inputField.fontAsset = uiInputField.text.Text.font;
			uiInputField.inputField.pointSize = 14;

			// Return the created UI input field
			return uiInputField;
		}

		/// <summary>
		/// Creates an instance of a UI Input Field in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Input Field" )]
		private static void CreateUIInputField ( MenuCommand command )
		{
			// Create UI input field
			GameObject obj = CreateUIInputField ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Input Field" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this input field element.
		/// </summary>
		/// <param name="data"> The input field data to display in the UI element. </param>
		public async void Initialize ( InputFieldData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this input field element asynchronously.
		/// </summary>
		/// <param name="data"> The input field data to display in the UI element. </param>
		public async Task InitializeAsync ( InputFieldData data )
		{
			// Display element
			SetActive ( true );

			// Check for button
			if ( !IsValid ( ) || data == null )
			{
				// Hide element
				SetActive ( false );
				return;
			}

			// Check for background
			if ( background != null && background.IsValid ( ) && data.BackgroundData != null && data.BackgroundData.IsValid ( ) )
			{
				// Update the background
				await background.InitializeAsync ( data.BackgroundData );
			}

			// Check for placeholder
			if ( placeholder != null && placeholder.IsValid ( ) && data.PlaceholderData != null && data.PlaceholderData.IsValid ( ) )
			{
				// Update the placeholder
				placeholder.Initialize ( data.PlaceholderData );
			}

			// Check for text
			if ( text != null && text.IsValid ( ) && data.TextData != null && data.TextData.IsValid ( ) )
			{
				// Update the text
				text.Initialize ( data.TextData );
			}
		}

		/// <summary>
		/// Adds a callback for when this input field changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the input field changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Add listener
				inputField.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this input field changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the input field changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Remove listener
				inputField.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this input field changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the input field changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Clear any existing listeners
				inputField.onValueChanged.RemoveAllListeners ( );

				// Add listener
				inputField.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this input field changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Clear any existing listeners
				inputField.onValueChanged.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Adds a callback for when this input field finishes changing value.
		/// </summary>
		/// <param name="listener"> The callback for when the input field finishes changing value. </param>
		public void AddOnEndEditListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Add listener
				inputField.onEndEdit.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this input field finishes changing value.
		/// </summary>
		/// <param name="listener"> The callback for when the input field finishes changing value. </param>
		public void RemoveOnEndEditListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Remove listener
				inputField.onEndEdit.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this input field finishes changing value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the input field finishes changing value. </param>
		public void SetOnEndEditListener ( UnityAction<string> listener )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Clear any existing listeners
				inputField.onEndEdit.RemoveAllListeners ( );

				// Add listener
				inputField.onEndEdit.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this input field finishes changing value.
		/// </summary>
		public void ClearOnEndEditListeners ( )
		{
			// Check for input field
			if ( inputField != null )
			{
				// Clear any existing listeners
				inputField.onEndEdit.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Sets the value of the input field without triggering any listeners.
		/// </summary>
		/// <param name="value"> The value for the input field. </param>
		public void SetValueWithoutNotification ( string value )
		{
			// Check for slider
			if ( inputField != null )
			{
				// Set value
				inputField.SetTextWithoutNotify ( value );
			}
		}

		/// <summary>
		/// Sets the colors of the input field interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the input field. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for input field
			if ( IsValid ( ) )
			{
				// Update input field colors
				inputField.colors = colorBlock;
			}
		}

		#endregion // Public Functions
	}
}