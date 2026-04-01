using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a toggle button.
	/// </summary>
	public class UIToggle : UIElement
	{
		#region UI Elements

		[SerializeField]
		private Toggle toggle;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIImage checkbox;

		[SerializeField]
		private UIImage checkmark;

		[SerializeField]
		private UIImage icon;

		[SerializeField]
		private UIText text;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public Toggle Toggle
		{
			get
			{
				return toggle;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the toggle.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the checkbox container for the toggle.
		/// </summary>
		public UIImage Checkbox
		{
			get
			{
				return checkbox;
			}
		}

		/// <summary>
		/// The UI element for displaying the checkmark for the toggle.
		/// </summary>
		public UIImage Checkmark
		{
			get
			{
				return checkmark;
			}
		}

		/// <summary>
		/// The UI element for displaying the descriptive icon for the toggle.
		/// </summary>
		public UIImage Icon
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		/// The UI element for displaying the descriptive text for the toggle.
		/// </summary>
		public UIText Text
		{
			get
			{
				return text;
			}
		}

		/// <summary>
		/// Whether or not the toggle element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return toggle.interactable;
			}
			set
			{
				toggle.interactable = value;
			}
		}

		/// <summary>
		/// Whether or not the toggle value is on.
		/// </summary>
		public bool IsOn
		{
			get
			{
				return toggle.isOn;
			}
			set
			{
				toggle.isOn = value;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this toggle is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && toggle != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Toggle.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Toggle instance. </returns>
		public static UIToggle CreateUIToggle ( string gameObjectName = "Toggle" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI toggle
			UIToggle uiToggle = obj.AddComponent<UIToggle> ( );
			uiToggle.RectTransform.sizeDelta = new Vector2 ( 150, 30 );

			// Add toggle
			uiToggle.toggle = obj.AddComponent<Toggle> ( );

			// Add background
			uiToggle.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiToggle.background.GameObject, obj );
			uiToggle.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiToggle.background.RectTransform.anchorMax = Vector2.one;
			uiToggle.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiToggle.background.RectTransform.offsetMax = Vector2.zero;
			uiToggle.background.SetColor ( Color.clear ); // Set transparent

			// Add checkbox
			uiToggle.checkbox = UIImage.CreateUIImage ( "Checkbox" );

			// Align checkbox as a child element
			GameObjectUtility.SetParentAndAlign ( uiToggle.checkbox.GameObject, obj );
			uiToggle.checkbox.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch left
			uiToggle.checkbox.RectTransform.anchorMax = Vector2.up;
			uiToggle.checkbox.RectTransform.offsetMin = Vector2.left * 15; // Set size to 30x30
			uiToggle.checkbox.RectTransform.offsetMax = Vector2.right * 15;
			uiToggle.checkbox.RectTransform.anchoredPosition = Vector2.right * 15; // Set position to align left
			uiToggle.checkbox.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiToggle.checkbox.Image.type = Image.Type.Sliced;

			// Add checkmark
			uiToggle.checkmark = UIImage.CreateUIImage ( "Checkmark" );

			// Align checkmark as a child element of the checkbox
			GameObjectUtility.SetParentAndAlign ( uiToggle.checkmark.GameObject, uiToggle.checkbox.GameObject );
			uiToggle.checkmark.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiToggle.checkmark.RectTransform.anchorMax = Vector2.one;
			uiToggle.checkmark.RectTransform.offsetMin = Vector2.one * 5; // Set size and position to fill with padding
			uiToggle.checkmark.RectTransform.offsetMax = Vector2.one * -5;
			uiToggle.checkmark.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/Checkmark.psd" ); // Set default checkmark sprite

			// Set targets for toggle
			uiToggle.toggle.targetGraphic = uiToggle.checkbox.Image;
			uiToggle.toggle.graphic = uiToggle.checkmark.Image;

			// Add text
			uiToggle.text = UIText.CreateUIText ( );

			// Align text as a child element
			GameObjectUtility.SetParentAndAlign ( uiToggle.text.GameObject, obj );
			uiToggle.text.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiToggle.text.RectTransform.anchorMax = Vector2.one;
			uiToggle.text.RectTransform.offsetMin = new Vector2 ( 35, 5 ); // Set size and position to fill with some padding from the checkbox
			uiToggle.text.RectTransform.offsetMax = Vector2.one * -5;

			// Set default text value
			uiToggle.text.Text.text = "Toggle";
			uiToggle.text.Text.alignment = TMPro.TextAlignmentOptions.Left;
			uiToggle.text.Text.fontSize = 24;

			// Set toggle on
			uiToggle.toggle.isOn = true;

			// Return the created UI toggle
			return uiToggle;
		}

		/// <summary>
		/// Creates an instance of a UI Toggle in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Toggle" )]
		private static void CreateUIToggle ( MenuCommand command )
		{
			// Create UI toggle
			GameObject obj = CreateUIToggle ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Toggle" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this toggle element.
		/// </summary>
		/// <param name="data"> The toggle data to display in the UI element. </param>
		public async void Initialize ( ToggleData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this toggle element asynchronously.
		/// </summary>
		/// <param name="data"> The toggle data to display in the UI element. </param>
		public async Task InitializeAsync ( ToggleData data )
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

			// Check for checkbox
			if ( checkbox != null && checkbox.IsValid ( ) && data.CheckboxData != null && data.CheckboxData.IsValid ( ) )
			{
				// Update the checkbox
				await checkbox.InitializeAsync ( data.CheckboxData );
			}

			// Check for checkmark
			if ( checkmark != null && checkmark.IsValid ( ) && data.CheckmarkData != null && data.CheckmarkData.IsValid ( ) )
			{
				// Update the checkmark
				await checkmark.InitializeAsync ( data.CheckmarkData );
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
		/// Adds a callback for when this toggle changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the toggle changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<bool> listener )
		{
			// Check for toggle
			if ( toggle != null )
			{
				// Add listener
				toggle.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this toggle changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the toggle changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<bool> listener )
		{
			// Check for toggle
			if ( toggle != null )
			{
				// Remove listener
				toggle.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this toggle changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the toggle changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<bool> listener )
		{
			// Check for toggle
			if ( toggle != null )
			{
				// Clear any existing listeners
				toggle.onValueChanged.RemoveAllListeners ( );

				// Add listener
				toggle.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this toggle changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for toggle
			if ( toggle != null )
			{
				// Clear any existing listeners
				toggle.onValueChanged.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Sets the value of the toggle without triggering any listeners.
		/// </summary>
		/// <param name="isOn"> Whether or not the toggle is on. </param>
		public void SetIsOnWithoutNotification ( bool isOn )
		{
			// Check for toggle
			if ( toggle != null )
			{
				// Set value
				toggle.SetIsOnWithoutNotify ( isOn );
			}
		}

		/// <summary>
		/// Sets the colors of the toggle interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the toggle. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for toggle
			if ( IsValid ( ) )
			{
				// Update toggle colors
				toggle.colors = colorBlock;
			}
		}

		#endregion // Public Functions
	}
}