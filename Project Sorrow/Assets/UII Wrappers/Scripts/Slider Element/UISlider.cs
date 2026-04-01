using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a slider.
	/// </summary>
	public class UISlider : UIElement
	{
		#region UI Elements

		[SerializeField]
		private Slider slider;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIImage fill;

		[SerializeField]
		private UIImage handle;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public Slider Slider
		{
			get
			{
				return slider;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the slider.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the fill area for the slider.
		/// </summary>
		public UIImage Fill
		{
			get
			{
				return fill;
			}
		}

		/// <summary>
		/// The UI element for displaying the handle for the slider.
		/// </summary>
		public UIImage Handle
		{
			get
			{
				return handle;
			}
		}

		/// <summary>
		/// Whether or not the slider element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return slider.interactable;
			}
			set
			{
				slider.interactable = value;
			}
		}

		/// <summary>
		/// The value for the slider.
		/// </summary>
		public float Value
		{
			get
			{
				return slider.value;
			}
			set
			{
				slider.value = value;
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
			return base.IsValid ( ) && slider != null; 
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Slider.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Slider instance. </returns>
		public static UISlider CreateUISlider ( string gameObjectName = "Slider" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI slider
			UISlider uiSlider = obj.AddComponent<UISlider> ( );
			uiSlider.RectTransform.sizeDelta = new Vector2 ( 150, 20 );

			// Add slider
			uiSlider.slider = obj.AddComponent<Slider> ( );

			// Add background
			uiSlider.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiSlider.background.GameObject, obj );
			uiSlider.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiSlider.background.RectTransform.anchorMax = Vector2.one;
			uiSlider.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiSlider.background.RectTransform.offsetMax = Vector2.zero;
			uiSlider.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiSlider.background.Image.type = Image.Type.Sliced;
			uiSlider.background.SetColor ( Color.grey ); // Set color to grey

			// Add fill area
			RectTransform fillArea = new GameObject ( "Fill Area", typeof ( RectTransform ) ).GetComponent<RectTransform> ( );

			// Align fill area as a child element
			GameObjectUtility.SetParentAndAlign ( fillArea.gameObject, obj );
			fillArea.anchorMin = Vector2.zero; // Set anchors to stretch
			fillArea.anchorMax = Vector2.one;
			fillArea.offsetMin = Vector2.zero; // Set size and position to fill
			fillArea.offsetMax = Vector2.zero;

			// Add fill
			uiSlider.fill = UIImage.CreateUIImage ( "Fill" );

			// Align fill as a child element of the fill area
			GameObjectUtility.SetParentAndAlign ( uiSlider.fill.GameObject, fillArea.gameObject );
			uiSlider.fill.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiSlider.fill.RectTransform.anchorMax = Vector2.one;
			uiSlider.fill.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiSlider.fill.RectTransform.offsetMax = Vector2.zero;
			uiSlider.fill.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiSlider.fill.Image.type = Image.Type.Sliced;

			// Add handle area
			RectTransform handleArea = new GameObject ( "Handle Area", typeof ( RectTransform ) ).GetComponent<RectTransform> ( );

			// Align handle area as a child element
			GameObjectUtility.SetParentAndAlign ( handleArea.gameObject, obj );
			handleArea.anchorMin = Vector2.zero; // Set anchors to stretch
			handleArea.anchorMax = Vector2.one;
			handleArea.offsetMin = Vector2.right * 15; // Set size and position to fill with a left and right buffer
			handleArea.offsetMax = Vector2.left * 15;

			// Add handle
			uiSlider.handle = UIImage.CreateUIImage ( "Handle" );

			// Align handle as a child element of the handle area
			GameObjectUtility.SetParentAndAlign ( uiSlider.handle.GameObject, handleArea.gameObject );
			uiSlider.handle.RectTransform.anchorMin = Vector2.right; // Set anchors to stretch right
			uiSlider.handle.RectTransform.anchorMax = Vector2.one;
			uiSlider.handle.RectTransform.offsetMin = new Vector2 ( -15, -5 ); // Set size and position to fill with padding
			uiSlider.handle.RectTransform.offsetMax = new Vector2 ( 15, 5 );
			uiSlider.handle.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiSlider.handle.Image.type = Image.Type.Sliced;

			// Set targets for slider
			uiSlider.slider.targetGraphic = uiSlider.handle.Image;
			uiSlider.slider.fillRect = uiSlider.fill.RectTransform;
			uiSlider.slider.handleRect = uiSlider.handle.RectTransform;

			// Set slider value
			uiSlider.slider.value = 1f;

			// Return the created UI slider
			return uiSlider;
		}

		/// <summary>
		/// Creates an instance of a UI Slider in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Slider" )]
		private static void CreateUISlider ( MenuCommand command )
		{
			// Create UI slider
			GameObject obj = CreateUISlider ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Slider" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this slider element.
		/// </summary>
		/// <param name="data"> The slider data to display in the UI element. </param>
		public async void Initialize ( SliderData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this slider element asynchronously.
		/// </summary>
		/// <param name="data"> The slider data to display in the UI element. </param>
		public async Task InitializeAsync ( SliderData data )
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

			// Check for fill
			if ( fill != null && fill.IsValid ( ) && data.FillData != null && data.FillData.IsValid ( ) )
			{
				// Update the fill
				await fill.InitializeAsync ( data.FillData );
			}

			// Check for handle
			if ( handle != null && handle.IsValid ( ) && data.HandleData != null && data.HandleData.IsValid ( ) )
			{
				// Update the handle
				await handle.InitializeAsync ( data.HandleData );
			}
		}

		/// <summary>
		/// Adds a callback for when this slider changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the slider changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for slider
			if ( slider != null )
			{
				// Add listener
				slider.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this slider changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the slider changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for slider
			if ( slider != null )
			{
				// Remove listener
				slider.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this slider changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the slider changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for slider
			if ( slider != null )
			{
				// Clear any existing listeners
				slider.onValueChanged.RemoveAllListeners ( );

				// Add listener
				slider.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this slider changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for slider
			if ( slider != null )
			{
				// Clear any existing listeners
				slider.onValueChanged.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Sets the value of the slider without triggering any listeners.
		/// </summary>
		/// <param name="value"> The value for the slider. </param>
		public void SetValueWithoutNotification ( float value )
		{
			// Check for slider
			if ( slider != null )
			{
				// Set value
				slider.SetValueWithoutNotify ( value );
			}
		}

		/// <summary>
		/// Sets the colors of the slider interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the slider. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for slider
			if ( IsValid ( ) )
			{
				// Update slider colors
				slider.colors = colorBlock;
			}
		}

		#endregion // Public Functions
	}
}