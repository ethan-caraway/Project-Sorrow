using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Setlist
{
	/// <summary>
	/// This class controls the setlist screen before a performance.
	/// </summary>
	public class SetlistManager : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ConfidenceHUD confidenceHUD;

		[SerializeField]
		private HUD.MoneyHUD moneyHUD;

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private HUD.StatusEffectHUD statusEffectHUD;

		[SerializeField]
		private HUD.PoetHUD poetHUD;

		[SerializeField]
		private HUD.TimerHUD timerHUD;

		[SerializeField]
		private HUD.ItemsHUD itemsHUD;

		[SerializeField]
		private HUD.RunInfoHUD runInfoHUD;

		[SerializeField]
		private GameObject setlistContainer;

		[SerializeField]
		private PerformanceDisplay [ ] performanceDisplays;

		[SerializeField]
		private Image judgeIcon;

		[SerializeField]
		private TMP_Text judgeText;

		[SerializeField]
		private HUD.TagInfoDisplay judgeTagDisplay;

		[SerializeField]
		private GameObject draftContainer;

		[SerializeField]
		private RectTransform draftAnimationRect;

		[SerializeField]
		private DraftPoemDisplay [ ] draftDisplays;

		[SerializeField]
		private DraftPoemDisplay dragDraftDisplay;

		[SerializeField]
		private Button confirmDraftButton;

		[SerializeField]
		private GameObject draftInputBlocker;

		[SerializeField]
		private Poems.PoemPreview poemPreview;

		#endregion // UI Elements

		#region Setlist Data

		private Poems.PoemModel [ ] draftPoems;
		private bool isDrafting;
		private bool isDraggingDraft;
		private int dragDraftIndex;
		private bool isDraggingSetlist;
		private int dragSetlistIndex;

		#endregion // Setlist Data

		#region MonoBehaviour Functions

		private void Start ( )
		{
			// Setup the setlist
			Init ( );
		}

		#endregion // MonoBehaviour Functions

		#region Public Functions

		/// <summary>
		/// Opens the preview of a setlist poem.
		/// </summary>
		/// <param name="index"> The index of the poem in the setlist. </param>
		public void InspectSetlistPoem ( int index )
		{
			// Hide setlist
			setlistContainer.SetActive ( false );

			// Hide draft
			draftContainer.SetActive ( false );

			// Display preview
			poemPreview.gameObject.SetActive ( true );
			poemPreview.SetPoem ( GameManager.Run.CurrentRound.Poems [ index ], ( addModel ) =>
			{
				// Add modifiers and enhancements
				GameManager.Run.CurrentRound.Poems [ index ].Add ( addModel );

				// Check for permanent draft poem
				if ( GameManager.Run.CurrentRound.Poems [ index ].Level > 0 )
				{
					// Enhancement permanent draft poem
					GameManager.Run.EnhancePermanentDraftPoem ( GameManager.Run.CurrentRound.Poems [ index ].ID, addModel );
				}
			} );
		}

		/// <summary>
		/// Opens the preview of a draft poem.
		/// </summary>
		/// <param name="index"> The index of the poem in the draft. </param>
		public void InspectDraftPoem ( int index )
		{
			// Hide setlist
			setlistContainer.SetActive ( false );

			// Hide draft
			draftContainer.SetActive ( false );

			// Display preview
			poemPreview.gameObject.SetActive ( true );
			poemPreview.SetPoem ( draftPoems [ index ], ( addModel ) =>
			{
				// Add modifiers and enhancements to draft poem
				draftPoems [ index ].Add ( addModel );

				// Check for permanent draft poem
				if ( draftPoems [ index ].Level > 0 )
				{
					// Enhancement permanent draft poem
					GameManager.Run.EnhancePermanentDraftPoem ( draftPoems [ index ].ID, addModel );
				}
			} );
		}

		/// <summary>
		/// Exits the preview of a poem.
		/// </summary>
		public void ExitPreview ( )
		{
			// Display setlist
			setlistContainer.SetActive ( true );

			// Display draft if currently drafting
			draftContainer.SetActive ( isDrafting );

			// Hide preview
			poemPreview.gameObject.SetActive ( false );

			// Disable consumable application
			consumablesHUD.DisableApplyToPoem ( );
		}

		/// <summary>
		/// Begins the next performance.
		/// </summary>
		public void BeginPerformance ( )
		{
			// Load performance
			SceneManager.LoadScene ( GameManager.PERFORMANCE_SCENE );
		}

		/// <summary>
		/// Begins dragging a draft poem to the setlist.
		/// </summary>
		/// <param name="index"> The index of the draft poem. </param>
		public void BeginDragDraft ( int index )
		{
			// Check for drag
			if ( isDrafting && !isDraggingDraft && !isDraggingSetlist && !IsDraftInSetlist ( draftPoems [ index ].ID ) )
			{
				// Mark drag
				isDraggingDraft = true;
				dragDraftIndex = index;

				// Hide poem in draft
				draftDisplays [ index ].ToggleDisplay ( false );

				// Display poem dragging
				dragDraftDisplay.gameObject.SetActive ( true );
				dragDraftDisplay.SetPoem ( draftPoems [ index ] );
			}
		}

		/// <summary>
		/// Ends dragging a draft poem to the setlist.
		/// </summary>
		public void EndDragDraft ( )
		{
			// Check for drag
			if ( isDrafting && isDraggingDraft )
			{
				// Stop drag
				isDraggingDraft = false;

				// Hide drag poem
				dragDraftDisplay.gameObject.SetActive ( false );

				// Reset draft poem if not applied to setlist
				if ( !IsDraftInSetlist ( draftPoems [ dragDraftIndex ].ID ) )
				{
					// Display poem still in draft
					draftDisplays [ dragDraftIndex ].ToggleDisplay ( true );
				}
			}
		}

		/// <summary>
		/// Drops a draft poem into a slot in the setlist.
		/// </summary>
		/// <param name="index"> The index of the slot in the setlist. </param>
		public void DropDraftToSetlist ( int index )
		{
			// Check for drag
			if ( isDrafting && isDraggingDraft )
			{
				// Assign draft poem
				SetDraftToSetlist ( dragDraftIndex, index );
			}
		}

		/// <summary>
		/// Automatically assigns a draft poem to the first available slot in the setlist.
		/// </summary>
		/// <param name="index"> The index of the draft poem. </param>
		public void ClickDraft ( int index )
		{
			// Check for drag
			if ( isDrafting && !isDraggingDraft && !isDraggingSetlist && !IsDraftInSetlist ( draftPoems [ index ].ID ) )
			{
				// Check for first available slot
				int slotIndex = -1;
				for ( int i = 0; i < GameManager.Run.CurrentRound.Poems.Length; i++ )
				{
					// Check for available slot
					if ( GameManager.Run.CurrentRound.Poems [ i ] == null || GameManager.Run.CurrentRound.Poems [ i ].ID == 0 )
					{
						slotIndex = i;
						break;
					}
				}

				// Check for slot
				if ( slotIndex == -1 )
				{
					// Prevent any poem from being assigned
					return;
				}

				// Assign draft poem
				if ( SetDraftToSetlist ( index, slotIndex ) )
				{
					// Hide draft poem
					draftDisplays [ index ].ToggleDisplay ( false );
				}
			}
		}

		/// <summary>
		/// Begins dragging a poem from the setlist.
		/// </summary>
		/// <param name="index"> The index of the setlist poem. </param>
		public void BeginDragSetlist ( int index )
		{
			// Check for drag and poem
			if ( isDrafting && !isDraggingDraft && !isDraggingSetlist && GameManager.Run.CurrentRound.Poems [ index ] != null && GameManager.Run.CurrentRound.Poems [ index ].ID != 0 )
			{
				// Check for The Minimalist judge
				if ( index == Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Prevent editing judge poem
					return;
				}

				// Mark drag
				isDraggingSetlist = true;
				dragSetlistIndex = index;

				// Hide poem in setlist
				performanceDisplays [ index ].SetPoem ( null );

				// Display poem dragging
				dragDraftDisplay.gameObject.SetActive ( true );
				dragDraftDisplay.SetPoem ( GameManager.Run.CurrentRound.Poems [ index ] );
			}
		}

		/// <summary>
		/// Ends dragging a poem from the setlist.
		/// </summary>
		public void EndDragSetlist ( )
		{
			// Check for drag
			if ( isDrafting && isDraggingSetlist )
			{
				// Stop drag
				isDraggingSetlist = false;

				// Hide drag poem
				dragDraftDisplay.gameObject.SetActive ( false );

				// Reset poem if still in setlist
				if ( GameManager.Run.CurrentRound.Poems [ dragSetlistIndex ] != null )
				{
					// Display poem still in setlist
					performanceDisplays [ dragSetlistIndex ].SetPoem ( GameManager.Run.CurrentRound.Poems [ dragSetlistIndex ] );
				}
			}
		}

		/// <summary>
		/// Removes a poem from the setlist
		/// </summary>
		public void DropSetlistToDraft ( )
		{
			// Check for drag
			if ( isDrafting && isDraggingSetlist )
			{
				// Unassign poem
				SetSetlistToDraft ( dragSetlistIndex );
			}
		}

		/// <summary>
		/// Moves a setlist poem to a new slot in the setlist
		/// </summary>
		/// <param name="index"> The index of the new slot in the setlist. </param>
		public void DropSetlistToSetlist ( int index )
		{
			// Check for drag
			if ( isDrafting && isDraggingSetlist )
			{
				// Store poem being moved
				Poems.PoemModel poem = GameManager.Run.CurrentRound.Poems [ dragSetlistIndex ];

				// Check for existing poem in setlist
				if ( GameManager.Run.CurrentRound.Poems [ index ] != null && GameManager.Run.CurrentRound.Poems [ index ].ID != 0 )
				{
					// Check for The Minimalist judge
					if ( index == Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
					{
						// Prevent editing judge poem
						return;
					}

					// Move existing poem to the previous slot
					GameManager.Run.CurrentRound.Poems [ dragSetlistIndex ] = GameManager.Run.CurrentRound.Poems [ index ];
				}
				else
				{
					// Clear previous slot
					GameManager.Run.CurrentRound.Poems [ dragSetlistIndex ] = null;
				}

				// Set poem to new slot
				GameManager.Run.CurrentRound.Poems [ index ] = poem;

				// Display poem in new slot
				performanceDisplays [ index ].SetPoem ( GameManager.Run.CurrentRound.Poems [ index ] );
			}
		}

		/// <summary>
		/// Automatically unassigns a poem from the setlist and returns it to the draft.
		/// </summary>
		/// <param name="index"> The index of setlist poem. </param>
		public void ClickSetlist ( int index )
		{
			// Check for drag and poem
			if ( isDrafting && !isDraggingDraft && !isDraggingSetlist && GameManager.Run.CurrentRound.Poems [ index ] != null && GameManager.Run.CurrentRound.Poems [ index ].ID != 0 )
			{
				// Check for The Minimalist judge
				if ( index == Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Prevent editing judge poem
					return;
				}

				// Unassign poem
				SetSetlistToDraft ( index );
			}
		}

		/// <summary>
		/// Drags a poems in or out of the setlist
		/// </summary>
		/// <param name="eventData"> The event data for the drag. </param>
		public void DragPoem ( BaseEventData eventData )
		{
			// Check if can drag
			if ( isDrafting && ( isDraggingDraft || isDraggingSetlist ) )
			{
				// Get pointer data
				PointerEventData pointerEventData = eventData as PointerEventData;

				// Get rect transform
				RectTransform rectTransform = dragDraftDisplay.transform as RectTransform;

				// Get valid mouse position
				Vector3 mousePosition;
				if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out mousePosition ) )
				{
					// Set position
					rectTransform.position = mousePosition;
				}
			}
		}

		/// <summary>
		/// Confirms the setlist the player has drafted.
		/// </summary>
		public void ConfirmDraft ( )
		{
			// Block input
			draftInputBlocker.SetActive ( true );

			// End draft
			SetupSetlist ( true );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Initializes the setlist.
		/// </summary>
		private void Init ( )
		{
			// Save game
			GameManager.Run.Checkpoint = GameManager.SETLIST_SCENE;
			Memory.MemoryManager.Save ( );

			// Set confidence
			confidenceHUD.SetConfidence ( GameManager.Run.MaxConfidence, GameManager.Run.MaxArrogance );

			// Set money
			moneyHUD.SetMoney ( GameManager.Run.Money, GameManager.Run.Debt );

			// Set consumables
			consumablesHUD.SetConsumables ( null, null, null );

			// Set status effects
			statusEffectHUD.SetStatusEffects ( );

			// Setup poet
			poetHUD.SetPoet ( Poets.PoetUtility.GetPoet ( GameManager.Run.PoetID ) );

			// Set timer
			timerHUD.UpdateTimer ( GameManager.Run.TimeAllowance, GameManager.Run.TimeAllowance );

			// Set items
			itemsHUD.SetItems ( null, null );

			// Get judge data
			Judges.JudgeScriptableObject judge = Judges.JudgeUtility.GetJudge ( GameManager.Run.CurrentRound.JudgeID );

			// Display judge
			judgeIcon.sprite = judge.Icon;
			judgeText.text = $"<b>{judge.Title}</b>: {judge.Description}";

			// Display judge tag
			if ( judge.HasTag )
			{
				judgeTagDisplay.gameObject.SetActive ( true );
				judgeTagDisplay.SetTag ( Tags.TagUtility.GetTag ( judge.Tag ) );
			}
			else
			{
				judgeTagDisplay.gameObject.SetActive ( false );
			}

			// Check for draft
			if ( GameManager.Run.Performance == 0 )
			{
				// Initialize draft
				SetupDraft ( );
			}
			else
			{
				// Initialize setlist
				SetupSetlist ( false );
			}
		}

		/// <summary>
		/// Sets up the draft for the round.
		/// </summary>
		private void SetupDraft ( )
		{
			// Set that a draft is occurring
			isDrafting = true;
			runInfoHUD.IsDrafting = true;

			// Get draft poems
			draftPoems = new Poems.PoemModel [ RoundModel.MAX_DRAFTS ];
			for ( int i = 0; i < draftPoems.Length; i++ )
			{
				// Check for permanent draft poem
				if ( GameManager.Run.PermanentDraftPoems [ i ] != null && GameManager.Run.PermanentDraftPoems [ i ].ID != 0 )
				{
					// Add permanent poem to the draft
					draftPoems [ i ] = GameManager.Run.PermanentDraftPoems [ i ];
				}
				else
				{
					// Add generated poem to the draft
					draftPoems [ i ] = new Poems.PoemModel
					{
						ID = GameManager.Run.CurrentRound.DraftIDs [ i ]
					};
				}
			}

			// Display draft
			draftContainer.SetActive ( true );
			for ( int i = 0; i < draftDisplays.Length; i++ )
			{
				// Set draft poem
				draftDisplays [ i ].SetPoem ( draftPoems [ i ] );
			}

			// Disable confirm button
			confirmDraftButton.interactable = false;

			// Display performances
			for ( int i = 0; i < performanceDisplays.Length; i++ )
			{
				// Get snaps requirement
				int snaps = GameManager.Difficulty.GetSnapsRequirement ( GameManager.Run.Round, i );

				// Check for Literary Critic judge
				if ( i == performanceDisplays.Length - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetLiteraryCriticId ( ) )
				{
					// Increase snaps requirement
					snaps = Judges.JudgeHelper.GetLiteraryCriticSnapsGoal ( GameManager.Difficulty.Rounds [ GameManager.Run.Round ] );
				}
				// Check for The Minimalist judge
				else if ( i == performanceDisplays.Length - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Display performance info
					performanceDisplays [ i ].SetPerformance (
						GameManager.Run.CurrentRound.Poems [ i ],
						snaps,
						GameManager.Run.Performance );

					// Skip setting draft
					continue;
				}

				// Display performance info
				performanceDisplays [ i ].SetDraft ( snaps );
			}
		}

		/// <summary>
		/// Sets up the setlist without drafting.
		/// </summary>
		/// <param name="animateDraft"> Whether or not the draft container should animate closing. </param>
		private void SetupSetlist ( bool animateDraft )
		{
			// Set that a draft has already occurred
			isDrafting = false;
			runInfoHUD.IsDrafting = false;

			// Check if draft container is animating
			if ( animateDraft )
			{
				// Animate draft closing
				draftAnimationRect.DOAnchorPosX ( draftAnimationRect.rect.width, 0.5f ).SetEase ( Ease.OutQuad ).OnComplete ( ( ) =>
				{
					// Hide draft
					draftContainer.SetActive ( false );
				} );
			}
			else
			{
				// Hide draft
				draftContainer.SetActive ( false );
			}

			// Display performances
			for ( int i = 0; i < performanceDisplays.Length; i++ )
			{
				// Get snaps requirement
				int snaps = GameManager.Difficulty.GetSnapsRequirement ( GameManager.Run.Round, i );

				// Check for Literary Critic judge
				if ( i == performanceDisplays.Length - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetLiteraryCriticId ( ) )
				{
					// Increase snaps requirement
					snaps = Judges.JudgeHelper.GetLiteraryCriticSnapsGoal ( GameManager.Difficulty.Rounds [ GameManager.Run.Round ] );
				}

				// Display performance info
				performanceDisplays [ i ].SetPerformance (
					GameManager.Run.CurrentRound.Poems [ i ],
					snaps,
					GameManager.Run.Performance );
			}
		}

		/// <summary>
		/// Checks whether or not a draft poem has been added to the setlist.
		/// </summary>
		/// <param name="id"> The ID of the poem. </param>
		/// <returns> Whether or not the poem is in the setlest. </returns>
		private bool IsDraftInSetlist ( int id )
		{
			// Check each setlist poem
			for ( int i = 0; i < GameManager.Run.CurrentRound.Poems.Length; i++ )
			{
				// Check for poem
				if ( GameManager.Run.CurrentRound.Poems [ i ] != null && GameManager.Run.CurrentRound.Poems [ i ].ID == id )
				{
					// Return that the draft poem is already in the setlist
					return true;
				}
			}

			// Return that the draft poem is not in the setlist
			return false;
		}

		/// <summary>
		/// Assigns a draft poem to the setlist.
		/// </summary>
		/// <param name="draftIndex"> The index of the draft poem. </param>
		/// <param name="setlistIndex"> The index of the slot in the setlist being assigned to. </param>
		/// <returns> Whether or not the draft poem was successfully assigned to the setlist. </returns>
		private bool SetDraftToSetlist ( int draftIndex, int setlistIndex )
		{
			// Check for existing poem in setlist
			if ( GameManager.Run.CurrentRound.Poems [ setlistIndex ] != null && GameManager.Run.CurrentRound.Poems [ setlistIndex ].ID != 0 )
			{
				// Check for The Minimalist judge
				if ( setlistIndex == Difficulty.DifficultyScriptableObject.PERFORMANCES_PER_ROUND - 1 && GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetMinimalistId ( ) )
				{
					// Prevent editing judge poem
					return false;
				}

				// Display existing poem in drafts
				for ( int i = 0; i < draftPoems.Length; i++ )
				{
					// Check for poem
					if ( draftPoems [ i ].ID == GameManager.Run.CurrentRound.Poems [ setlistIndex ].ID )
					{
						draftDisplays [ i ].ToggleDisplay ( true );
						break;
					}
				}
			}

			// Set draft poem to setlist
			GameManager.Run.CurrentRound.Poems [ setlistIndex ] = draftPoems [ draftIndex ];

			// Display poem in setlist
			performanceDisplays [ setlistIndex ].SetPoem ( GameManager.Run.CurrentRound.Poems [ setlistIndex ] );

			// Check if the setlist is ready
			confirmDraftButton.interactable = IsSetlistReady ( );

			// Return that the draft poem was successfully assigned to the setlist
			return true;
		}

		/// <summary>
		/// Unassigns a setlist poem and returns it to the draft.
		/// </summary>
		/// <param name="index"> The index of the slot in the setlist being unassigned. </param>
		private void SetSetlistToDraft ( int index )
		{
			// Display poem in drafts
			for ( int i = 0; i < draftPoems.Length; i++ )
			{
				// Check for poem
				if ( draftPoems [ i ].ID == GameManager.Run.CurrentRound.Poems [ index ].ID )
				{
					draftPoems [ i ] = GameManager.Run.CurrentRound.Poems [ index ];
					draftDisplays [ i ].ToggleDisplay ( true );
					break;
				}
			}

			// Clear poem from setlist
			GameManager.Run.CurrentRound.Poems [ index ] = null;
			performanceDisplays [ index ].SetPoem ( null );

			// Set that setlist is not complete
			confirmDraftButton.interactable = false;
		}

		/// <summary>
		/// Checks whether or not all slots have been filled for the setlist.
		/// </summary>
		/// <returns> Whether or not the setlist is complete. </returns>
		private bool IsSetlistReady ( )
		{
			// Check each setlist poem
			for ( int i = 0; i < GameManager.Run.CurrentRound.Poems.Length; i++ )
			{
				// Check for poem
				if ( GameManager.Run.CurrentRound.Poems [ i ] == null || GameManager.Run.CurrentRound.Poems [ i ].ID == 0 )
				{
					// Return that the setlist is incomplete
					return false;
				}
			}

			// Return that the setlist is complete
			return true;
		}

		#endregion // Private Functions
	}
}