using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Performance
{
	/// <summary>
	/// This class controls the summary screen after a performance.
	/// </summary>
	public class SummaryHUD : MonoBehaviour
	{
		#region Summary Data Constants

		private const float SUMMARY_DELAY = 1f;
		private const float SUMMARY_FADE = 2f;

		private const float FADE_INTERVAL = 0.25f;
		private const float FADE_DURATION = 0.5f;

		private const float COUNTER_INTERVAL = 1f;

		#endregion // Summary Data Constants

		#region UI Elements

		[SerializeField]
		private HUD.MoneyHUD moneyHUD;

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private HUD.ItemsHUD itemsHUD;

		[SerializeField]
		private HUD.SnapsHUD snapsHUD;

		[SerializeField]
		private Image background;

		[SerializeField]
		private GameObject completeContainer;

		[SerializeField]
		private CanvasGroup completeTitleGroup;

		[SerializeField]
		private TMP_Text completeTitleText;

		[SerializeField]
		private TMP_Text completeDescriptionText;

		[SerializeField]
		private CanvasGroup completeApplauseGroup;

		[SerializeField]
		private TMP_Text completeReputationText;

		[SerializeField]
		private HUD.IntegerCounter completeConfidenceCounter;

		[SerializeField]
		private HUD.IntegerCounter completeApplauseCounter;

		[SerializeField]
		private CanvasGroup completeMoneyGroup;

		[SerializeField]
		private HUD.IntegerCounter completeCommissionCounter;

		[SerializeField]
		private HUD.IntegerCounter completeTimeCounter;

		[SerializeField]
		private TMP_Text completeInterestCapText;

		[SerializeField]
		private HUD.IntegerCounter completeInterestCounter;

		[SerializeField]
		private CanvasGroup completeControlsGroup;

		[SerializeField]
		private GameObject completeContinueButton;

		[SerializeField]
		private GameObject completeNewRunButton;

		[SerializeField]
		private GameObject completeMainMenuButton;

		[SerializeField]
		private GameObject failContainer;

		[SerializeField]
		private CanvasGroup failTitleGroup;

		[SerializeField]
		private TMP_Text failReasonText;

		[SerializeField]
		private CanvasGroup failControlsGroup;

		[SerializeField]
		private GameObject failureNewRunButton;

		#endregion // UI Elements

		#region Summary Data

		[SerializeField]
		private Color32 successColor;

		[SerializeField]
		private Color32 failColor;

		private int debt;

		#endregion // Summary Data

		#region Public Functions

		/// <summary>
		/// Displays the summary screen for completing a performance.
		/// </summary>
		/// <param name="model"> The data to display in the summary. </param>
		public void ShowSummary ( SummaryStatsModel model )
		{
			// Display screen
			gameObject.SetActive ( true );
			completeContainer.SetActive ( true );
			failContainer.SetActive ( false );

			// Hide all elements
			background.color = new Color ( background.color.r, background.color.g, background.color.b, 0 );
			completeTitleGroup.alpha = 0;
			completeApplauseGroup.alpha = 0;
			completeMoneyGroup.alpha = 0;
			completeControlsGroup.alpha = 0;

			// Set time remaining
			completeTimeCounter.SetNumber ( 0, true );

			// Set bonus
			completeApplauseCounter.SetNumber ( 0, true );

			// Set commission
			completeCommissionCounter.SetNumber ( 0, true );

			// Set flubs
			completeReputationText.text = $"(Reputation: {model.Reputation})";
			completeConfidenceCounter.SetNumber ( 0, true );

			// Set interest
			completeInterestCapText.text = $"(Interest Cap: ${model.InterestCap})";
			completeInterestCounter.SetNumber ( 0, true );

			// Store debt
			debt = GameManager.Run.Debt;

			// Check for success
			if ( model.IsSuccess )
			{
				// Play success animation
				AnimateCompleteSuccess ( model );
			}
			else
			{
				// Play failure animation
				AnimateCompleteFail ( model );
			}
		}

		/// <summary>
		/// Displays the failure screen for running out of time.
		/// </summary>
		public void ShowOutOfTime ( )
		{
			// Set prompt
			failReasonText.text = "(Ran out of time)";

			// Play animation
			AnimateFail ( );
		}

		/// <summary>
		/// Displays the failure screen for running out of confidence.
		/// </summary>
		public void ShowOutOfConfidence ( )
		{
			// Set prompt
			failReasonText.text = "(Flubbed too many times)";

			// Play animation
			AnimateFail ( );
		}

		/// <summary>
		/// Continues the run to the shop.
		/// </summary>
		public void ContinueRun ( )
		{
			// Check for judge round
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Check for Clown judge
				if ( GameManager.Run.CurrentRound.JudgeID == Judges.JudgeHelper.GetClownId ( ) )
				{
					// Enable items
					Judges.JudgeHelper.SetClownItemsEnabled ( true );
				}
			}

			// Check for end of round
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) )
			{
				// Trigger end of round effects for items
				for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
				{
					// Check for item
					if ( GameManager.Run.IsValidItem ( i ) )
					{
						// Check for item trigger
						if ( GameManager.Run.ItemData [ i ].Item.OnCompleteRound ( ) )
						{
							// Highlight item
							itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, GameManager.Run.ItemData [ i ].Item.IsCompleteRoundEffectPositive ( ) );
						}
					}
				}
			}

			// Increment to new performance
			GameManager.Run.NextPerformance ( );

			// Check for intermission
			string nextScene = GameManager.Run.Performance == 2 ? GameManager.ENCOUNTER_SCENE : GameManager.SHOP_SCENE;

			// Load the shop
			SceneManager.LoadScene ( nextScene );
		}

		/// <summary>
		/// Begins a new run.
		/// </summary>
		public void StartNewRun ( )
		{
			// End run
			GameManager.IsRunActive = false;

			// End tutorial if active
			GameManager.IsTutorial = false;

			// Clear run data
			GameManager.Run = null;
			GameManager.Difficulty = null;

			// Save erasing run data
			Memory.MemoryManager.Save ( );

			// Load setlist
			SceneManager.LoadScene ( GameManager.CHARACTER_SELECT_SCENE );
		}

		/// <summary>
		/// Returns to the main menu.
		/// </summary>
		public void MainMenu ( )
		{
			// End run
			GameManager.IsRunActive = false;

			// End tutorial if active
			GameManager.IsTutorial = false;

			// Clear run data
			GameManager.Run = null;
			GameManager.Difficulty = null;

			// Save game
			if ( !GameManager.IsTutorial )
			{
				Memory.MemoryManager.Save ( );
			}

			// Load main menu
			SceneManager.LoadScene ( GameManager.MAIN_MENU_SCENE );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Plays the animation for the success summary screen.
		/// </summary>
		/// <param name="model"> The data to display in the summary. </param>
		private void AnimateCompleteSuccess ( SummaryStatsModel model )
		{
			// Set title
			completeTitleText.text = GameManager.Run.IsLastPerformanceOfRun ( ) ? "VICTORY" : "SUCCESS";
			completeTitleText.color = successColor;

			// Set description
			completeDescriptionText.text = GameManager.Run.IsLastPerformanceOfRun ( ) ? "You won!" : "Great job";

			// Display correct controls
			completeContinueButton.SetActive ( !GameManager.IsTutorial && !GameManager.Run.IsLastPerformanceOfRun ( ) );
			completeNewRunButton.SetActive ( !GameManager.IsTutorial && GameManager.Run.IsLastPerformanceOfRun ( ) );
			completeMainMenuButton.SetActive ( GameManager.IsTutorial || GameManager.Run.IsLastPerformanceOfRun ( ) );

			// Create animation
			Sequence sequence = DOTween.Sequence ( );

			// Fade out from performance
			sequence.AppendInterval ( SUMMARY_DELAY );
			sequence.Append ( background.DOFade ( 1f, SUMMARY_FADE ).SetEase ( Ease.InQuad ) );

			// Fade in snaps
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeApplauseGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Count snaps from confidence
			AnimateConfidence ( sequence, model.Confidence );

			// Count snaps from bonuses
			AnimateApplause ( sequence, model.Applause );

			// Fade in money
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeMoneyGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Count money from commmision
			AnimateCommission ( sequence, model );

			// Count money from time remaining
			AnimateTime ( sequence, model );

			// Count money from interest
			AnimateInterest ( sequence, model );

			// Fade in title
			sequence.Append ( completeTitleGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Fade in controls
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeControlsGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );
		}

		/// <summary>
		/// Plays the animation for the failure summary screen.
		/// </summary>
		/// <param name="model"> The data to display in the summary. </param>
		private void AnimateCompleteFail ( SummaryStatsModel model )
		{
			// Set title
			completeTitleText.text = "FAILURE";
			completeTitleText.color = failColor;

			// Set description
			completeDescriptionText.text = "Tough crowd";

			// Display correct controls
			completeContinueButton.SetActive ( false );
			completeNewRunButton.SetActive ( true );
			completeMainMenuButton.SetActive ( true );

			// Create animation
			Sequence sequence = DOTween.Sequence ( );

			// Fade out from performance
			sequence.AppendInterval ( SUMMARY_DELAY );
			sequence.Append ( background.DOFade ( 1f, SUMMARY_FADE ).SetEase ( Ease.InQuad ) );

			// Fade in snaps
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeApplauseGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Count snaps from confidence
			AnimateConfidence ( sequence, model.Confidence );

			// Count snaps from bonuses
			AnimateApplause ( sequence, model.Applause );

			// Fade in title
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeTitleGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Fade in controls
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( completeControlsGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );
		}

		/// <summary>
		/// Creates the animation for showing the snaps earned from confidence remaining.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="snaps"> The amount of snaps earned from time remaining. </param>
		private void AnimateConfidence ( Sequence sequence, int snaps )
		{
			// Count snaps from time
			if ( snaps > 0 )
			{
				sequence.AppendCallback ( ( ) =>
				{
					completeConfidenceCounter.AddNumber ( snaps );
					snapsHUD.AddSnaps ( snaps );
				} );
				sequence.AppendInterval ( COUNTER_INTERVAL );
			}
			else
			{
				sequence.AppendInterval ( FADE_INTERVAL );
			}
		}

		/// <summary>
		/// Creates the animation for showing the applause earned.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="models"> The data for the applause earned. </param>
		private void AnimateApplause ( Sequence sequence, ApplauseModel [ ] models )
		{
			// Count snaps from time
			if ( models.Length > 0 )
			{
				// Animate applause
				for ( int i = 0; i < models.Length; i++ )
				{
					AnimateApplauseInstance ( sequence, models [ i ] );
				}
			}
			else
			{
				sequence.AppendInterval ( FADE_INTERVAL );
			}
		}

		/// <summary>
		/// Creates the animation for showing the applause earned.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="model"> The data for the applause. </param>
		private void AnimateApplauseInstance ( Sequence sequence, ApplauseModel model )
		{
			sequence.AppendCallback ( ( ) =>
			{
				completeApplauseCounter.AddNumber ( model.Applause );
				snapsHUD.AddSnaps ( model.Applause );
				if ( model.ItemID != Items.ItemModel.NO_ITEM_ID )
				{
					itemsHUD.HighlightItem ( model.ItemID, model.ItemInstanceID, true );
				}
			} );
			sequence.AppendInterval ( COUNTER_INTERVAL );
		}

		/// <summary>
		/// Creates the animation for showing the money earned from commision.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="model"> The data for the summary. </param>
		private void AnimateCommission ( Sequence sequence, SummaryStatsModel model )
		{
			// Count money from commission
			if ( model.Commission > 0 )
			{
				sequence.AppendCallback ( ( ) =>
				{
					completeCommissionCounter.AddNumber ( model.Commission );

					// Update money
					if ( debt < 0 )
					{
						// Check if commission will pay off debt
						if ( debt + model.Commission > 0 )
						{
							moneyHUD.AddDebt ( debt * -1 );
							moneyHUD.AddMoney ( debt + model.Commission );
						}
						else
						{
							moneyHUD.AddDebt ( model.Commission );
						}
					}
					else
					{
						moneyHUD.AddMoney ( model.Commission );
					}

					// Check for any items triggered
					for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
						{
							// Check for item trigger
							if ( GameManager.Run.ItemData [ i ].Item.OnCommission ( model.Commission ) )
							{
								// Highlight item
								itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
							}
						}
					}
				} );
				sequence.AppendInterval ( COUNTER_INTERVAL );
			}
			else
			{
				sequence.AppendInterval ( FADE_INTERVAL );
			}
		}

		/// <summary>
		/// Creates the animation for showing the money earned from time remaining.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="model"> The data for the summary. </param>
		private void AnimateTime ( Sequence sequence, SummaryStatsModel model )
		{
			// Count money from time remaining
			if ( model.Confidence > 0 )
			{
				sequence.AppendCallback ( ( ) =>
				{
					completeTimeCounter.AddNumber ( model.Time );

					// Update money
					int newDebt = debt + model.Commission;
					if ( newDebt < 0 )
					{
						// Check if money will pay off debt
						if ( newDebt + model.Time > 0 )
						{
							moneyHUD.AddDebt ( newDebt * -1 );
							moneyHUD.AddMoney ( newDebt + model.Time );
						}
						else
						{
							moneyHUD.AddDebt ( model.Time );
						}
					}
					else
					{
						moneyHUD.AddMoney ( model.Time );
					}
				} );
				sequence.AppendInterval ( COUNTER_INTERVAL );
			}
			else
			{
				sequence.AppendInterval ( FADE_INTERVAL );
			}
		}

		/// <summary>
		/// Creates the animation for showing the money earned from interest.
		/// </summary>
		/// <param name="sequence"> The animation data. </param>
		/// <param name="model"> The data for the summary. </param>
		private void AnimateInterest ( Sequence sequence, SummaryStatsModel model )
		{
			// Count money from interest
			if ( model.Interest > 0 )
			{
				sequence.AppendCallback ( ( ) =>
				{
					completeInterestCounter.AddNumber ( model.Interest );

					// Update money
					int newDebt = debt + model.Commission + model.Time;
					if ( newDebt < 0 )
					{
						// Check if interest will pay off debt
						if ( newDebt + model.Interest > 0 )
						{
							moneyHUD.AddDebt ( newDebt * -1 );
							moneyHUD.AddMoney ( newDebt + model.Interest );
						}
						else
						{
							moneyHUD.AddDebt ( model.Interest );
						}
					}
					else
					{
						moneyHUD.AddMoney ( model.Interest );
					}

					// Check for any items triggered
					for ( int i = 0; i < GameManager.Run.ItemData.Length; i++ )
					{
						// Check for item
						if ( GameManager.Run.IsValidItem ( i ) && GameManager.Run.ItemData [ i ].Item.IsEnabled )
						{
							// Check for item trigger
							if ( GameManager.Run.ItemData [ i ].Item.OnInterest ( model.Interest ) )
							{
								// Highlight item
								itemsHUD.HighlightItem ( GameManager.Run.ItemData [ i ].ID, GameManager.Run.ItemData [ i ].InstanceID, true );
							}
						}
					}
				} );
				sequence.AppendInterval ( COUNTER_INTERVAL );
			}
			else
			{
				sequence.AppendInterval ( FADE_INTERVAL );
			}
		}

		/// <summary>
		/// Plays the animation for the failure screen.
		/// </summary>
		private void AnimateFail ( )
		{
			// Display screen
			gameObject.SetActive ( true );
			completeContainer.SetActive ( false );
			failContainer.SetActive ( true );
			failureNewRunButton.SetActive ( !GameManager.IsTutorial );

			// Hide all elements
			background.color = new Color ( background.color.r, background.color.g, background.color.b, 0 );
			failTitleGroup.alpha = 0;
			failReasonText.alpha = 0;
			failControlsGroup.alpha = 0;

			// Create animation
			Sequence sequence = DOTween.Sequence ( );

			// Fade out from performance
			sequence.AppendInterval ( SUMMARY_DELAY );
			sequence.Append ( background.DOFade ( 1f, SUMMARY_FADE ).SetEase ( Ease.InQuad ) );

			// Fade in title
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( failTitleGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Fade in reason
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( failReasonText.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );

			// Fade in controls
			sequence.AppendInterval ( FADE_INTERVAL );
			sequence.Append ( failControlsGroup.DOFade ( 1f, FADE_DURATION ).SetEase ( Ease.InQuad ) );
		}

		#endregion // Private Functions
	}
}