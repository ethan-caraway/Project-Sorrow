using FlightPaper.Data;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FlightPaper.UI
{
	/// <summary>
	/// This class controls the UI elements that comprise a scroll view.
	/// </summary>
	public class UIScrollView : UIElement
	{
		#region UI Elements

		[SerializeField]
		private ScrollRect scrollView;

		[SerializeField]
		private UIImage background;

		[SerializeField]
		private UIImage viewport;

		[SerializeField]
		private UIScrollbar horizontalScrollbar;

		[SerializeField]
		private UIScrollbar verticalScrollbar;

		#endregion // UI Elements

		#region Public Properties

		/// <summary>
		/// The interactive UI element.
		/// </summary>
		public ScrollRect ScrollView
		{
			get
			{
				return scrollView;
			}
		}

		/// <summary>
		/// The UI element for displaying the background container for the scroll view.
		/// </summary>
		public UIImage Background
		{
			get
			{
				return background;
			}
		}

		/// <summary>
		/// The UI element for displaying the viewport container for the scroll view.
		/// </summary>
		public UIImage Viewport
		{
			get
			{
				return viewport;
			}
		}

		/// <summary>
		/// The UI element for displaying the horizontal scrollbar for the scroll view.
		/// </summary>
		public UIScrollbar HorizontalScrollbar
		{
			get
			{
				return horizontalScrollbar;
			}
		}

		/// <summary>
		/// The UI element for displaying the vertical scrollbar for the scroll view.
		/// </summary>
		public UIScrollbar VerticalScrollbar
		{
			get
			{
				return verticalScrollbar;
			}
		}

		/// <summary>
		/// The container for the content of this scroll view.
		/// </summary>
		public RectTransform Content
		{
			get
			{
				return scrollView.content;
			}
		}

		/// <summary>
		/// Whether or not horizontal scrolling is enabled in this scroll view.
		/// </summary>
		public bool IsHorizontalEnabled
		{
			get
			{
				return scrollView.horizontal;
			}
			set
			{
				scrollView.horizontal = value;
			}
		}

		/// <summary>
		/// Whether or not vertical scrolling is enabled in this scroll view.
		/// </summary>
		public bool IsVerticalEnabled
		{
			get
			{
				return scrollView.vertical;
			}
			set
			{
				scrollView.vertical = value;
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
			return base.IsValid ( ) && scrollView != null;
		}

		#endregion // UIElement Override Functions

		#region Editor Functions

		#if UNITY_EDITOR

		/// <summary>
		/// Creates an instance of a UI Scroll View.
		/// </summary>
		/// <param name="gameObjectName"> The name for the game object. </param>
		/// <returns> The created UI Scroll View instance. </returns>
		public static UIScrollView CreateUIScrollView ( string gameObjectName = "Scroll View" )
		{
			// Create base game object
			GameObject obj = new GameObject ( gameObjectName, typeof ( RectTransform ) );

			// Add UI scroll view
			UIScrollView uiScrollView = obj.AddComponent<UIScrollView> ( );
			uiScrollView.RectTransform.sizeDelta = new Vector2 ( 200, 200 );

			// Add scroll view
			uiScrollView.scrollView = obj.AddComponent<ScrollRect> ( );

			// Add background
			uiScrollView.background = UIImage.CreateUIImage ( "Background" );

			// Align background as a child element
			GameObjectUtility.SetParentAndAlign ( uiScrollView.background.GameObject, obj );
			uiScrollView.background.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiScrollView.background.RectTransform.anchorMax = Vector2.one;
			uiScrollView.background.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiScrollView.background.RectTransform.offsetMax = Vector2.zero;
			uiScrollView.background.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiScrollView.background.Image.type = Image.Type.Sliced;
			uiScrollView.background.SetColor ( new Color ( 1f, 1f, 1f, 0.5f ) ); // Set color to transparent white

			// Add viewport
			uiScrollView.viewport = UIImage.CreateUIImage ( "Viewport" );

			// Align viewport as a child element
			GameObjectUtility.SetParentAndAlign ( uiScrollView.viewport.GameObject, obj );
			uiScrollView.viewport.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch
			uiScrollView.viewport.RectTransform.anchorMax = Vector2.one;
			uiScrollView.viewport.RectTransform.pivot = Vector2.up; // Set pivot to top left
			uiScrollView.viewport.RectTransform.offsetMin = Vector2.zero; // Set size and position to fill
			uiScrollView.viewport.RectTransform.offsetMax = Vector2.zero;
			uiScrollView.viewport.Image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite> ( "UI/Skin/UISprite.psd" ); // Set default sprite
			uiScrollView.viewport.Image.type = Image.Type.Sliced;

			// Add mask to viewport
			Mask mask = uiScrollView.viewport.GameObject.AddComponent<Mask> ( );
			mask.showMaskGraphic = false;

			// Add content
			RectTransform content = new GameObject ( "Content", typeof ( RectTransform ) ).GetComponent<RectTransform> ( );

			// Align content as a child element of the viewport
			GameObjectUtility.SetParentAndAlign ( content.gameObject, uiScrollView.viewport.GameObject );
			content.anchorMin = Vector2.up; // Set anchors to stretch top
			content.anchorMax = Vector2.one;
			content.pivot = Vector2.up; // Set pivot to top left
			content.offsetMin = Vector2.down * 300; // Set size and position to fill with 300 height 
			content.offsetMax = Vector2.zero;

			// Add horizontal scrollbar
			uiScrollView.horizontalScrollbar = UIScrollbar.CreateUIScrollbar ( "Scrollbar Horizontal" );

			// Align horizontal scrollbar as a child element
			GameObjectUtility.SetParentAndAlign ( uiScrollView.horizontalScrollbar.GameObject, obj );
			uiScrollView.horizontalScrollbar.RectTransform.anchorMin = Vector2.zero; // Set anchors to stretch bottom
			uiScrollView.horizontalScrollbar.RectTransform.anchorMax = Vector2.right;
			uiScrollView.horizontalScrollbar.RectTransform.pivot = Vector2.zero; // Set pivot to bottom left

			// Add vertical scrollbar
			uiScrollView.verticalScrollbar = UIScrollbar.CreateUIScrollbar ( "Scrollbar Vertical" );
			uiScrollView.verticalScrollbar.Scrollbar.direction = Scrollbar.Direction.BottomToTop;

			// Align vertical scrollbar as a child element
			GameObjectUtility.SetParentAndAlign ( uiScrollView.verticalScrollbar.GameObject, obj );
			uiScrollView.verticalScrollbar.RectTransform.anchorMin = Vector2.right; // Set anchors to stretch right
			uiScrollView.verticalScrollbar.RectTransform.anchorMax = Vector2.one;
			uiScrollView.verticalScrollbar.RectTransform.pivot = Vector2.one; // Set pivot to top right
			uiScrollView.verticalScrollbar.RectTransform.offsetMin = Vector2.left * 20; // Set size and position to fill with 20 width
			uiScrollView.verticalScrollbar.RectTransform.offsetMax = Vector2.zero;

			// Set targets for scroll view
			uiScrollView.scrollView.viewport = uiScrollView.viewport.RectTransform;
			uiScrollView.scrollView.content = content;
			uiScrollView.scrollView.horizontalScrollbar = uiScrollView.horizontalScrollbar.Scrollbar;
			uiScrollView.scrollView.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			uiScrollView.scrollView.verticalScrollbar = uiScrollView.verticalScrollbar.Scrollbar;
			uiScrollView.scrollView.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			uiScrollView.verticalScrollbar.Value = 1f;

			// Return the created UI scrollbar
			return uiScrollView;
		}

		/// <summary>
		/// Creates an instance of a UI Scroll View in the editor.
		/// </summary>
		/// <param name="command"> The data for the menu command from the editor. </param>
		[MenuItem ( "GameObject/UI/UI Scroll View" )]
		private static void CreateUIScrollView ( MenuCommand command )
		{
			// Create UI scroll view
			GameObject obj = CreateUIScrollView ( ).GameObject;

			// Ensure the game object is parented to the selected game object if this was context click
			GameObjectUtility.SetParentAndAlign ( obj, command.context as GameObject );

			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo ( obj, "Create UI Scroll View" );
			Selection.activeObject = obj;
		}

		#endif // UNITY_EDITOR

		#endregion // Editor Functions

		#region Public Functions

		/// <summary>
		/// Sets this scroll view element.
		/// </summary>
		/// <param name="data"> The scroll view data to display in the UI element. </param>
		public async void Initialize ( ScrollViewData data )
		{
			await InitializeAsync ( data );
		}

		/// <summary>
		/// Sets this scroll view element asynchronously.
		/// </summary>
		/// <param name="data"> The scroll view data to display in the UI element. </param>
		public async Task InitializeAsync ( ScrollViewData data )
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

			// Check for viewport
			if ( viewport != null && viewport.IsValid ( ) && data.ViewportData != null && data.ViewportData.IsValid ( ) )
			{
				// Update the viewport
				await viewport.InitializeAsync ( data.ViewportData );
			}

			// Check for horizontal scrollbar
			if ( horizontalScrollbar != null && horizontalScrollbar.IsValid ( ) && data.HorizontalScrollbarData != null )
			{
				// Update the horizontal scrollbar
				await horizontalScrollbar.InitializeAsync ( data.HorizontalScrollbarData );
			}

			// Check for vertical scrollbar
			if ( verticalScrollbar != null && verticalScrollbar.IsValid ( ) && data.VerticalScrollbarData != null )
			{
				// Update the vertical scrollbar
				await verticalScrollbar.InitializeAsync ( data.VerticalScrollbarData );
			}
		}

		/// <summary>
		/// Adds a callback for when this scroll view changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the scroll view changes value. </param>
		public void AddOnValueChangedListener ( UnityAction<Vector2> listener )
		{
			// Check for scrollbar
			if ( scrollView != null )
			{
				// Add listener
				scrollView.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes a callback for when this scroll view changes value.
		/// </summary>
		/// <param name="listener"> The callback for when the scroll view changes value. </param>
		public void RemoveOnValueChangedListener ( UnityAction<Vector2> listener )
		{
			// Check for scroll view
			if ( scrollView != null )
			{
				// Remove listener
				scrollView.onValueChanged.RemoveListener ( listener );
			}
		}

		/// <summary>
		/// Sets the only callback for when this scroll view changes value.
		/// Any existing callbacks will be removed.
		/// </summary>
		/// <param name="listener"> The callback for when the scroll view changes value. </param>
		public void SetOnValueChangedListener ( UnityAction<Vector2> listener )
		{
			// Check for scroll view
			if ( scrollView != null )
			{
				// Clear any existing listeners
				scrollView.onValueChanged.RemoveAllListeners ( );

				// Add listener
				scrollView.onValueChanged.AddListener ( listener );
			}
		}

		/// <summary>
		/// Removes all callbacks for when this scroll view changes value.
		/// </summary>
		public void ClearOnValueChangedListeners ( )
		{
			// Check for scroll view
			if ( scrollView != null )
			{
				// Clear any existing listeners
				scrollView.onValueChanged.RemoveAllListeners ( );
			}
		}

		#endregion // Public Functions
	}
}