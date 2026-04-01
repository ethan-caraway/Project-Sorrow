using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a button.
	/// </summary>
	public class UIButton : UIElement
	{
		#region UI Elements

		[SerializeField]
		private Button button;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIImage icon;

		[SerializeField]
		private UIText text;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public Button Button
		{
			get
			{
				return button;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the button.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the descriptive icon for the button.
		/// </summary>
		public UIImage Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The UI element for displaying the descriptive text for the button.
		/// </summary>
		public UIText Text
		{
			get
			{
				return text;
			}
		}

		/// <summary>
		/// Whether or not the button element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return button.interactable;
			}
			set
			{
				button.interactable = value;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this button is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && button != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Button.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Button instance. </returns>
		public static UIButton CreateUIButton ( string gameObjectName = "Button" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI button
			UIButton uiButton = obj.AddComponent<UIButton> ( );
			uiButton.RectTransform.sizeDelta = new Vector2 ( 150, 30 );

			// Add button
			uiButton.button = obj.AddComponent<Button> ( );

			// Add background
			uiButton.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiButton.background.GameObject, obj );
			uiButton.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiButton.background.RectTransform.anchorMax = Vector2.one;
			uiButton.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiButton.background.RectTransform.offsetMax = Vector2.zero;
			uiButton.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiButton.background.Image.type = Image.Type.Sliced;

			// Set target for button
			uiButton.button.targetGraphic = uiButton.background.Image;

			// Add text
			uiButton.text = UIText.CreateUIText ( );

			// Align text as a child element
			GameObjectUtility.SetParentAndAlign ( uiButton.text.GameObject, obj );
			uiButton.text.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiButton.text.RectTransform.anchorMax = Vector2.one;
			uiButton.text.RectTransform.offsetMin = Vector2.one * 5; // Set size and position to fill with some padding
			uiButton.text.RectTransform.offsetMax = Vector2.one * -5;

			// Set default text value
			uiButton.text.Text.text = "Button";
			uiButton.text.Text.alignment = TMPro.TextAlignmentOptions.Center;
			uiButton.text.Text.fontSize = 24;
			uiButton.text.SetColor ( Color.black );

			// Return the created UI button
			return uiButton;
		}

		/// <summary>
		/// Creates an instance of a UI Button in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Button" )]
		private static void CreateUIButton ( MenuCommand command )
		{
			// Create UI button
			GameObject obj = CreateUIButton ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Button" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this button element.
		/// </summary>
		/// <param name="data"> The button data to display in the UI element. </param>
		public async void Initialize ( ButtonData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this button element asynchronously.
		/// </summary>
		/// <param name="data"> The button data to display in the UI element. </param>
		public async Task InitializeAsync ( ButtonData data )
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

			// Check for icon
			if ( icon != null && icon.IsValid ( ) && data.IconData != null && data.IconData.IsValid ( ) )
			{
				// Update the icon
				await icon.InitializeAsync ( data.IconData );
			}

			// Check for text
			if ( text != null && text.IsValid ( ) && data.TextData != null && data.TextData.IsValid ( ) )
			{
				// Update the text
				text.Initialize ( data.TextData );
			}
		}

		/// <summary>
		/// Adds a callback for when this button is clicked.
		/// </summary>
		/// <param name="listener"> The callback for when the button is clicked. </param>
		public void AddOnClickListener ( UnityAction listener )
		{
			// Check for button
			if ( button != null )
			{
				// Add listener
				button.onClick.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this button is clicked.
		/// </summary>
		/// <param name="listener"> The callback for when the button is clicked. </param>
		public void RemoveOnClickListener ( UnityAction listener )
		{
			// Check for button
			if ( button != null )
			{
				// Remove listener
				button.onClick.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this button is clicked.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the button is clicked. </param>
		public void SetOnClickListener ( UnityAction listener )
		{
			// Check for button
			if ( button != null )
			{
				// Clear any existing listeners
				button.onClick.RemoveAllListeners ( );

				// Add listener
				button.onClick.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this button is clicked.
		/// </summary>
		public void ClearOnClickListeners ( )
		{
			// Check for button
			if ( button != null )
			{
				// Clear any existing listeners
				button.onClick.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Sets the colors of the button interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the button. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for button
			if ( IsValid ( ) )
			{
				// Update button colors
				button.colors = colorBlock;
			}
		}

		#endregion // Public Functions
	}
}