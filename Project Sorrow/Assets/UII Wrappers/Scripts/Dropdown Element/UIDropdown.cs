using FlightPaper.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a dropdown.
	/// </summary>
	public class UIDropdown : UIElement
	{
		#region UI Elements

		[SerializeField]
		private TMP_Dropdown dropdown;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIText labelText;

		[SerializeField]
		private UIImage labelIcon;

		[SerializeField]
		private UIImage arrow;

		[SerializeField]
		private UIScrollView scrollViewTemplate;

		[SerializeField]
		private UIToggle itemTemplate;

		#endregion // UI Elements

		#region Dropdown Data

		[SerializeField]
		[NonReorderable] // Fixes render issue with the first element not expanding
		private List<DropdownItemData> items = new List<DropdownItemData> ( );

		#endregion // Dropdown Data

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public TMP_Dropdown Dropdown
		{
			get
			{
				return dropdown;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the background.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the label text for the dropdown.
		/// </summary>
		public UIText LabelText
		{
			get
			{
				return labelText;
			}
		}

		/// <summary>
		/// The UI element for displaying the label icon for the dropdown.
		/// </summary>
		public UIImage LabelIcon
		{
			get
			{
				return labelIcon;
			}
		}

		/// <summary>
		/// The UI element for displaying the arrow for the dropdown.
		/// </summary>
		public UIImage Arrow
		{
			get
			{
				return arrow;
			}
		}

		/// <summary>
		/// The UI element for displaying the scroll view for the dropdown.
		/// </summary>
		public UIScrollView ScrollViewTemplate
		{
			get
			{
				return scrollViewTemplate;
			}
		}

		/// <summary>
		/// The UI element for displaying the item template for the dropdown.
		/// </summary>
		public UIToggle ItemTemplate
		{
			get
			{
				return itemTemplate;
			}
		}

		/// <summary>
		/// Whether or not the dropdown element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return dropdown.interactable;
			}
			set
			{
				dropdown.interactable = value;
			}
		}

		/// <summary>
		/// The index of the currently selected item.
		/// </summary>
		public int Value
		{
			get
			{
				return dropdown.value;
			}
			set
			{
				dropdown.value = value;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this scroll view is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && dropdown != null;
		}

		#endregion // UIElement Override Functions

		#region MonoBehaviour Functions

		private void Awake ( )
		{
			// Update values
			ApplyItems ( );
		}

		private void OnEnable ( )
		{
			// Add callbacks
			UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocaleChanged += OnLocalizationChanged;
		}

		private void OnDisable ( )
		{
			// Remove callbacks
			UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocaleChanged -= OnLocalizationChanged;
		}

		#endregion // MonoBehaviour Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Dropdown.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Dropdown instance. </returns>
		public static UIDropdown CreateUIDropdown ( string gameObjectName = "Dropdown" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI dropdown
			UIDropdown uiDropdown = obj.AddComponent<UIDropdown> ( );
			uiDropdown.RectTransform.sizeDelta = new Vector2 ( 150, 30 );

			// Add dropdown
			uiDropdown.dropdown = obj.AddComponent<TMP_Dropdown> ( );

			// Add background
			uiDropdown.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiDropdown.background.GameObject, obj );
			uiDropdown.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiDropdown.background.RectTransform.anchorMax = Vector2.one;
			uiDropdown.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiDropdown.background.RectTransform.offsetMax = Vector2.zero;
			uiDropdown.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiDropdown.background.Image.type = Image.Type.Sliced;

			// Add label text
			uiDropdown.labelText = UIText.CreateUIText ( "Label Text" );

			// Align text as a child element
			GameObjectUtility.SetParentAndAlign ( uiDropdown.labelText.GameObject, obj );
			uiDropdown.labelText.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiDropdown.labelText.RectTransform.anchorMax = Vector2.one;
			uiDropdown.labelText.RectTransform.offsetMin = new Vector2 ( 10, 5 ); // Set size and position to fill with some padding from the checkbox
			uiDropdown.labelText.RectTransform.offsetMax = new Vector2 ( -30, -5 );

			// Set default text values
			uiDropdown.LabelText.SetColor ( Color.black );
			uiDropdown.LabelText.Text.fontSize = 20;
			uiDropdown.LabelText.Text.alignment = TextAlignmentOptions.Left;

			// Add arrow
			uiDropdown.arrow = UIImage.CreateUIImage ( "Arrow" );

			// Align arrow as a child element
			GameObjectUtility.SetParentAndAlign ( uiDropdown.arrow.GameObject, obj );
			uiDropdown.arrow.RectTransform.anchorMin = Vector2.right; // Set anchors to stretch right
			uiDropdown.arrow.RectTransform.anchorMax = Vector2.one;
			uiDropdown.arrow.RectTransform.offsetMin = new Vector2 ( -10, 5 ); // Set size to 20x20
			uiDropdown.arrow.RectTransform.offsetMax = new Vector2 ( 10, -5 );
			uiDropdown.arrow.RectTransform.anchoredPosition = Vector2.left * 15; // Set position to align right
			uiDropdown.arrow.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/DropdownArrow.psd" ); // Set default arrow sprite

			// Add scroll view template
			uiDropdown.scrollViewTemplate = UIScrollView.CreateUIScrollView ( "Template" );

			// Align scroll view template as a child element
			GameObjectUtility.SetParentAndAlign ( uiDropdown.scrollViewTemplate.GameObject, obj );
			uiDropdown.scrollViewTemplate.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch bottom
			uiDropdown.scrollViewTemplate.RectTransform.anchorMax = Vector2.right;
			uiDropdown.scrollViewTemplate.RectTransform.offsetMin = Vector2.down * 75; // Set size to 
			uiDropdown.scrollViewTemplate.RectTransform.offsetMax = Vector2.up * 75;
			uiDropdown.scrollViewTemplate.RectTransform.pivot = new Vector2 ( 0.5f, 1 ); // Set pivot to top center
			uiDropdown.scrollViewTemplate.RectTransform.anchoredPosition = Vector2.up * 2; // Set position to align bottom

			// Resize content
			uiDropdown.scrollViewTemplate.Content.offsetMin = Vector2.down * 15;
			uiDropdown.scrollViewTemplate.Content.offsetMax = Vector2.up * 15;

			// Remove horizontal scroll bar
			DestroyImmediate ( uiDropdown.scrollViewTemplate.HorizontalScrollbar.GameObject );
			uiDropdown.scrollViewTemplate.IsHorizontalEnabled = false;

			// Set default scroll view values
			uiDropdown.scrollViewTemplate.ScrollView.movementType = ScrollRect.MovementType.Clamped;

			// Add item template
			uiDropdown.itemTemplate = UIToggle.CreateUIToggle ( "Item" );

			// Align item template
			GameObjectUtility.SetParentAndAlign ( uiDropdown.itemTemplate.GameObject, uiDropdown.scrollViewTemplate.Content.gameObject );
			uiDropdown.itemTemplate.RectTransform.anchorMin = Vector2.up * 0.5f; // Set anchors to stretch middle
			uiDropdown.itemTemplate.RectTransform.anchorMax = new Vector2 ( 1, 0.5f );
			uiDropdown.itemTemplate.RectTransform.offsetMin = Vector2.down * 10; // Set size to 
			uiDropdown.itemTemplate.RectTransform.offsetMax = Vector2.up * 10;

			// Set target for item
			uiDropdown.itemTemplate.Toggle.targetGraphic = uiDropdown.itemTemplate.Background.Image;

			// Align checkmark for item template
			GameObjectUtility.SetParentAndAlign ( uiDropdown.itemTemplate.Checkmark.GameObject, uiDropdown.itemTemplate.GameObject );
			uiDropdown.itemTemplate.Checkmark.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch left
			uiDropdown.itemTemplate.Checkmark.RectTransform.anchorMax = Vector2.up;
			uiDropdown.itemTemplate.Checkmark.RectTransform.offsetMin = new Vector2 ( -8, 2 ); // Set size to 16x16
			uiDropdown.itemTemplate.Checkmark.RectTransform.offsetMax = new Vector2 ( 8, -2 );
			uiDropdown.itemTemplate.Checkmark.RectTransform.anchoredPosition = Vector2.right * 12; // Set position to align left

			// Remove checkbox for item template
			DestroyImmediate ( uiDropdown.itemTemplate.Checkbox.GameObject );

			// Align text for item template
			uiDropdown.itemTemplate.Text.RectTransform.offsetMin = new Vector2 ( 22, 2 );
			uiDropdown.itemTemplate.Text.RectTransform.offsetMax = new Vector2 ( -5, -2 );

			// Set text values for item template
			uiDropdown.itemTemplate.Text.Text.fontSize = 14;
			uiDropdown.itemTemplate.Text.SetColor ( Color.black );

			// Hide template
			uiDropdown.scrollViewTemplate.SetActive ( false );

			// Set targets for dropdown
			uiDropdown.dropdown.targetGraphic = uiDropdown.background.Image;
			uiDropdown.dropdown.template = uiDropdown.scrollViewTemplate.RectTransform;
			uiDropdown.dropdown.captionText = uiDropdown.labelText.Text;
			uiDropdown.dropdown.itemText = uiDropdown.itemTemplate.Text.Text;

			// Set default items
			uiDropdown.items = new List<DropdownItemData>
			{
				new DropdownItemData
				{
					Text = new TextData { Unlocalized = "Option A" }
				},
				new DropdownItemData
				{
					Text = new TextData { Unlocalized = "Option B" }
				},
				new DropdownItemData
				{
					Text = new TextData { Unlocalized = "Option C" }
				}
			};
			uiDropdown.ApplyItems ( );

			// Return the created UI scrollbar
			return uiDropdown;
		}

		/// <summary>
		/// Creates an instance of a UI Dropdown in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Dropdown" )]
		private static void CreateUIDropdown ( MenuCommand command )
		{
			// Create UI dropdown
			GameObject obj = CreateUIDropdown ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Dropdown" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this dropdown element with items.
		/// </summary>
		/// <param name="itemData"> The list of data for items. </param>
		public void Initialize ( DropdownItemData [ ] itemData )
		{
			Initialize ( new DropdownData
			{
				ItemData = itemData
			} );
		}

		/// <summary>
		/// Sets this dropdown element.
		/// </summary>
		/// <param name="data"> The dropdown data to display in the UI element. </param>
		public async void Initialize ( DropdownData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this dropdown element asynchronously.
		/// </summary>
		/// <param name="data"> The dropdown data to display in the UI element. </param>
		public async Task InitializeAsync ( DropdownData data )
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

			// Check for label text
			if ( labelText != null && labelText.IsValid ( ) && data.LabelTextData != null && data.LabelTextData.IsValid ( ) )
			{
				// Update the label text
				labelText.Initialize ( data.LabelTextData );
			}

			// Check for label icon
			if ( labelIcon != null && labelIcon.IsValid ( ) && data.LabelIconData != null && data.LabelTextData.IsValid ( ) )
			{
				// Update the label icon
				await labelIcon.InitializeAsync ( data.LabelIconData );
			}

			// Check for arrow
			if ( arrow != null && arrow.IsValid ( ) && data.ArrowData != null && data.ArrowData.IsValid ( ) )
			{
				// Update the label icon
				await arrow.InitializeAsync ( data.ArrowData );
			}

			// Check for scroll view
			if ( scrollViewTemplate != null && scrollViewTemplate.IsValid ( ) && data.ScrollViewTemplateData != null )
			{
				// Update the scroll view
				await scrollViewTemplate.InitializeAsync ( data.ScrollViewTemplateData );
			}

			// Check for item template
			if ( itemTemplate != null && itemTemplate.IsValid ( ) && data.ItemTemplateData != null )
			{
				// Update the item template
				await itemTemplate.InitializeAsync ( data.ItemTemplateData );
			}

			// Check for item data
			if ( data.ItemData.Length > 0 )
			{
				// Update items
				items = new List<DropdownItemData> ( data.ItemData );
				ApplyItems ( );
			}
		}

		/// <summary>
		/// Adds a callback for when this dropdown changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the dropdown changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<int> listener )
		{
			// Check for dropdown
			if ( dropdown != null )
			{
				// Add listener
				dropdown.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this dropdown changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the dropdown changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<int> listener )
		{
			// Check for dropdown
			if ( dropdown != null )
			{
				// Remove listener
				dropdown.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this dropdown changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the dropdown changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<int> listener )
		{
			// Check for dropdown
			if ( dropdown != null )
			{
				// Clear any existing listeners
				dropdown.onValueChanged.RemoveAllListeners ( );

				// Add listener
				dropdown.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this dropdown changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for dropdown
			if ( dropdown != null )
			{
				// Clear any existing listeners
				dropdown.onValueChanged.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Adds a new item to the dropdown.
		/// </summary>
		/// <param name="item"> The data for the new item. </param>
		public void AddItem ( DropdownItemData item )
		{
			// Add new item
			items.Add ( item );

			// Update items
			ApplyItems ( );
		}

		/// <summary>
		/// Removes an existing item from the dropdown.
		/// </summary>
		/// <param name="item"> The data for the existing item. </param>
		public void RemoveItem ( DropdownItemData item )
		{
			// Check for item
			if ( items.Contains ( item ) )
			{
				// Remove item
				items.Remove ( item );

				// Update items
				ApplyItems ( );
			}
		}

		/// <summary>
		/// Sets the value of the slider without triggering any listeners.
		/// </summary>
		/// <param name="value"> The index for the dropdown. </param>
		public void SetValueWithoutNotification ( int value )
		{
			// Check for dropdown
			if ( dropdown != null )
			{
				// Set value
				dropdown.SetValueWithoutNotify ( value );
			}
		}

		/// <summary>
		/// Sets the colors of the dropdown interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the dropdown. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for dropdown
			if ( IsValid ( ) )
			{
				// Update dropdown colors
				dropdown.colors = colorBlock;
			}
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Converts the item data into the dropdown.
		/// </summary>
		private void ApplyItems ( )
		{
			// Store item data
			List<TMP_Dropdown.OptionData> appliedItems = new List<TMP_Dropdown.OptionData> ( );

			// Convert each item
			for ( int i = 0; i < items.Count; i++ )
			{
				// Get text
				string text = string.Empty;
				if ( items [ i ].Text != null )
				{
					text = items [ i ].Text.ToString ( );
				}

				// Get image
				Sprite image = null;
				if ( items [ i ].Image != null )
				{
					image = items [ i ].Image.LoadSprite ( );
				}

				// Create new item
				appliedItems.Add ( new TMP_Dropdown.OptionData ( text, image, Color.white ) );
			}

			// Apply the items
			dropdown.options = appliedItems;
		}

		/// <summary>
		/// Updates the dropdown items when the loclization changes.
		/// </summary>
		/// <param name="locale"> The data for the selected localization. </param>
		private void OnLocalizationChanged ( Locale locale )
		{
			// Apply newly localized items
			ApplyItems ( );
		}

		#endregion // Private Functions
	}
}