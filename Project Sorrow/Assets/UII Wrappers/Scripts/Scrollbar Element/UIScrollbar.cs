using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a scrollbar.
	/// </summary>
	public class UIScrollbar : UIElement
	{
		#region UI Elements

		[SerializeField]
		private Scrollbar scrollbar;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIImage handle;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public Scrollbar Scrollbar
		{
			get
			{
				return scrollbar;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the scrollbar.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the handle for the scrollbar.
		/// </summary>
		public UIImage Handle
		{
			get
			{
				return handle;
			}
		}

		/// <summary>
		/// Whether or not the scrollbar element can be interacted with.
		/// </summary>
		public bool IsInteractive
		{
			get
			{
				return scrollbar.interactable;
			}
			set
			{
				scrollbar.interactable = value;
			}
		}

		/// <summary>
		/// The value for the scrollbar.
		/// </summary>
		public float Value
		{
			get
			{
				return scrollbar.value;
			}
			set
			{
				scrollbar.value = value;
			}
		}

		#endregion // Public Properties

		#region UIElement Override Functions

		/// <summary>
		/// Gets whether or not this scrollbar is a valid UI element.
		/// </summary>
		/// <returns> Whether or not the UI element is valid. </returns>
		public override bool IsValid ( )
		{
			return base.IsValid ( ) && scrollbar != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Scrollbar.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Scrollbar instance. </returns>
		public static UIScrollbar CreateUIScrollbar ( string gameObjectName = "Scrollbar" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI scrollbar
			UIScrollbar uiScrollbar = obj.AddComponent<UIScrollbar> ( );
			uiScrollbar.RectTransform.sizeDelta = new Vector2 ( 150, 20 );

			// Add scrollbar
			uiScrollbar.scrollbar = obj.AddComponent<Scrollbar> ( );

			// Add background
			uiScrollbar.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiScrollbar.background.GameObject, obj );
			uiScrollbar.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiScrollbar.background.RectTransform.anchorMax = Vector2.one;
			uiScrollbar.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiScrollbar.background.RectTransform.offsetMax = Vector2.zero;
			uiScrollbar.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiScrollbar.background.Image.type = Image.Type.Sliced;
			uiScrollbar.background.SetColor ( Color.grey ); // Set color to grey

			// Add sliding area
			RectTransform slidingArea = new GameObject ( "Sliding Area", typeof ( RectTransform ) ).GetComponent<RectTransform> ( );

			// Align sliding area as a child element
			GameObjectUtility.SetParentAndAlign ( slidingArea.gameObject, obj );
			slidingArea.anchorMin = Vector2.zero; // Set anchors to stretch
			slidingArea.anchorMax = Vector2.one;
			slidingArea.offsetMin = Vector2.one * 5; // Set size and position to fill with padding
			slidingArea.offsetMax = Vector2.one * -5;

			// Add handle
			uiScrollbar.handle = UIImage.CreateUIImage ( "Handle" );

			// Align handle as a child element of the sliding area
			GameObjectUtility.SetParentAndAlign ( uiScrollbar.handle.GameObject, slidingArea.gameObject );
			uiScrollbar.handle.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch 
			uiScrollbar.handle.RectTransform.anchorMax = Vector2.one;
			uiScrollbar.handle.RectTransform.offsetMin = Vector2.one * -5; // Set size and position to fill with padding
			uiScrollbar.handle.RectTransform.offsetMax = Vector2.one * 5;
			uiScrollbar.handle.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiScrollbar.handle.Image.type = Image.Type.Sliced;

			// Set targets for scrollbar
			uiScrollbar.scrollbar.targetGraphic = uiScrollbar.handle.Image;
			uiScrollbar.scrollbar.handleRect = uiScrollbar.handle.RectTransform;

			// Set scrollbar values
			uiScrollbar.scrollbar.size = 0.2f;

			// Return the created UI scrollbar
			return uiScrollbar;
		}

		/// <summary>
		/// Creates an instance of a UI Scrollbar in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Scrollbar" )]
		private static void CreateUIScrollbar ( MenuCommand command )
		{
			// Create UI scrollbar
			GameObject obj = CreateUIScrollbar ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Scrollbar" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this scrollbar element.
		/// </summary>
		/// <param name="data"> The scrollbar data to display in the UI element. </param>
		public async void Initialize ( ScrollbarData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this scrollbar element asynchronously.
		/// </summary>
		/// <param name="data"> The scrollbar data to display in the UI element. </param>
		public async Task InitializeAsync ( ScrollbarData data )
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

			// Check for handle
			if ( handle != null && handle.IsValid ( ) && data.HandleData != null && data.HandleData.IsValid ( ) )
			{
				// Update the handle
				await handle.InitializeAsync ( data.HandleData );
			}
		}

		/// <summary>
		/// Adds a callback for when this scrollbar changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the scrollbar changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for scrollbar
			if ( scrollbar != null )
			{
				// Add listener
				scrollbar.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this scrollbar changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the scrollbar changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for scrollbar
			if ( scrollbar != null )
			{
				// Remove listener
				scrollbar.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this scrollbar changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the scrollbar changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<float> listener )
		{
			// Check for scrollbar
			if ( scrollbar != null )
			{
				// Clear any existing listeners
				scrollbar.onValueChanged.RemoveAllListeners ( );

				// Add listener
				scrollbar.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this scrollbar changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for scrollbar
			if ( scrollbar != null )
			{
				// Clear any existing listeners
				scrollbar.onValueChanged.RemoveAllListeners ( );
			}
		}

		/// <summary>
		/// Sets the value of the scrollbar without triggering any listeners.
		/// </summary>
		/// <param name="value"> The value for the scrollbar. </param>
		public void SetValueWithoutNotification ( float value )
		{
			// Check for scrollbar
			if ( scrollbar != null )
			{
				// Set value
				scrollbar.SetValueWithoutNotify ( value );
			}
		}

		/// <summary>
		/// Sets the colors of the scrollbar interaction.
		/// </summary>
		/// <param name="colorBlock"> The colors to update the scrollbar. </param>
		public void OverrideColors ( ColorBlock colorBlock )
		{
			// Check for slider
			if ( IsValid ( ) )
			{
				// Update slider colors
				scrollbar.colors = colorBlock;
			}
		}

		#endregion // Public Functions
	}
}