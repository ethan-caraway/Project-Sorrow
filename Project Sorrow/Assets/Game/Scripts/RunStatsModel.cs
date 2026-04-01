namespace FlightPaper.ProjectSorrow
{
	/// <summary>
	/// This class stores the stats for the current run.
	/// </summary>
	[System.Serializable]
	public class RunStatsModel
	{
		#region Stats Data

		/// <summary>
		/// The total number of successful performances completed this run.
		/// </summary>
		public int TotalSuccessfulPerformances = 0;

		/// <summary>
		/// The current total number of lines completed in a row without flubbing.
		/// </summary>
		public int CurrentLinesWithoutFlubbing = 0;

		#endregion // Stats Data

		#region Public Functions

		/// <summary>
		/// Updates the stats upon starting a run.
		/// </summary>
		public void OnStartRun ( )
		{
			// Update run stats
			Progression.ProgressManager.Progress.OnStartRun ( GameManager.Run.PoetID, GameManager.Difficulty.ID );
		}

		/// <summary>
		/// Updates the stats upon earning or spending money.
		/// </summary>
		public void OnApplyMoney ( )
		{
			// Check for record money
			if ( GameManager.Run.Money > Progression.ProgressManager.Progress.ChallengeStats.MostMoney )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostMoney = GameManager.Run.Money;
			}

			// Check for record debt
			if ( GameManager.Run.Debt < Progression.ProgressManager.Progress.ChallengeStats.MostDebt )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostDebt = GameManager.Run.Debt;
			}
		}

		/// <summary>
		/// Updates the stats upon consuming a consumable.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <param name="instances"> The number of instances of the consumable. </param>
		public void OnConsumeConsumable ( int id, int instances )
		{
			// Update stats for consumables
			Progression.ProgressManager.Progress.OnConsumeConsumable ( id, instances );
		}

		/// <summary>
		/// Updates the stats upon applying a modifier to a word.
		/// </summary>
		/// <param name="modifier"> The modifier being applied. </param>
		public void OnApplyModifier ( Enums.WordModifierType modifier )
		{
			// Update stats for modifiers
			Progression.ProgressManager.Progress.OnApplyModifier ( modifier );
		}

		/// <summary>
		/// Updates the stats upon applying a status effect.
		/// </summary>
		/// <param name="statusEffect"> The status effect being applied. </param>
		/// <param name="count"> The number of stacks of the status effect being applied. </param>
		/// <param name="total"> The current number of stacks owned of the status effect. </param>
		public void OnApplyStatusEffect ( Enums.StatusEffectType statusEffect, int count, int total )
		{
			// Update stats for status effects
			Progression.ProgressManager.Progress.OnApplyStatusEffect ( statusEffect, count, total );
		}

		/// <summary>
		/// Updates the stats upon start a performance.
		/// </summary>
		public void OnInitPerformance ( )
		{
			// Get max confidence
			int maxConfidence = GameManager.Run.MaxConfidence;
			if ( maxConfidence > Progression.ProgressManager.Progress.ChallengeStats.MostStartConfidence )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostStartConfidence = maxConfidence;
			}

			// Get max time
			float maxTime = GameManager.Run.TimeAllowance;
			if ( maxTime > Progression.ProgressManager.Progress.ChallengeStats.MostStartTime )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostStartTime = maxTime;
			}

			// Update stats for performances
			Progression.ProgressManager.Progress.OnInitPerformance ( GameManager.Run.Performance, GameManager.Run.Round, GameManager.Run.PoetID, GameManager.Run.IsLastPerformanceOfRound ( ) ? GameManager.Run.CurrentRound.JudgeID : 0 );
		}

		/// <summary>
		/// The callback for when a word modifier triggers.
		/// </summary>
		/// <param name="modifier"> The modifier being triggered. </param>
		/// <param name="value"> The value of the stat to add. </param>
		public void OnModifierTrigger ( Enums.WordModifierType modifier, int value )
		{
			// Update stats for modifiers
			Progression.ProgressManager.Progress.OnModifierTrigger ( modifier, value );
		}

		/// <summary>
		/// The callback for when a status effect triggers.
		/// </summary>
		/// <param name="statusEffect"> The status effect being triggered. </param>
		/// <param name="value"> The value of the stat to add. </param>
		public void OnStatusEffectTrigger ( Enums.StatusEffectType statusEffect, int value )
		{
			// Update stats for status effects
			Progression.ProgressManager.Progress.OnStatusEffectTrigger ( statusEffect, value );
		}

		/// <summary>
		/// The callback for when a flub is made.
		/// </summary>
		public void OnFlub ( )
		{
			// Reset line count
			// Set to -1 as completing this current line will increment to 0
			CurrentLinesWithoutFlubbing = -1;
		}

		/// <summary>
		/// The callback for applying snaps earned for completing a word of a poem.
		/// </summary>
		/// <param name="total"> The current total number of snaps earned for the word. </param>
		/// <param name="length"> The length of the word. </param>
		/// <param name="modifier"> The modifier applied to the word. </param>
		/// <param name="model"> The data for the performance. </param>
		public void OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Update stats for modifiers
			Progression.ProgressManager.Progress.OnWordComplete ( modifier );
		}

		/// <summary>
		/// The callback for when a line is completed.
		/// </summary>
		public void OnLineComplete ( )
		{
			// Increment lines completed
			CurrentLinesWithoutFlubbing++;
			if ( CurrentLinesWithoutFlubbing > Progression.ProgressManager.Progress.ChallengeStats.MostLinesWithoutFlubbing )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostLinesWithoutFlubbing = CurrentLinesWithoutFlubbing;
			}
		}

		/// <summary>
		/// Updates the stats upon successfully completing a performance.
		/// </summary>
		/// <param name="poemID"> The ID of the poem for the performance. </param>
		/// <param name="model"> The data for the performance. </param>
		/// <param name="stats"> The data for the summary. </param>
		public void OnCompletePerformance ( int poemID, Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increment total performances
			TotalSuccessfulPerformances++;
			Progression.ProgressManager.Progress.TotalSuccessfulPerformances++;

			// Check for complete a poem by a woman
			Poems.PoemScriptableObject poem = Poems.PoemUtility.GetPoem ( poemID );
			if ( poem != null && poem.IsFemaleAuthor )
			{
				Progression.ProgressManager.Progress.ChallengeStats.SuccessfulPerformancesByWomen++;
			}

			// Check for new record for confidence remaining
			if ( model.ConfidenceRemaining > Progression.ProgressManager.Progress.ChallengeStats.MostConfidenceRemaining )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostConfidenceRemaining = model.ConfidenceRemaining;
			}

			// Check for new record for snaps from confidence remaining
			if ( stats.Confidence > Progression.ProgressManager.Progress.ChallengeStats.MostSnapsFromConfidenceRemaining )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostSnapsFromConfidenceRemaining = stats.Confidence;
			}

			// Check for new record for interest earned
			if ( stats.Interest > Progression.ProgressManager.Progress.ChallengeStats.MostInterest )
			{
				Progression.ProgressManager.Progress.ChallengeStats.MostInterest = stats.Interest;
			}

			// Update stats for performance completions
			Progression.ProgressManager.Progress.OnCompletePerformance ( model.SnapsCount >= model.SnapsGoal, model.SnapsCount, GameManager.Run.PoetID, GameManager.Run.IsLastPerformanceOfRound ( ) ? GameManager.Run.CurrentRound.JudgeID : 0, GameManager.Difficulty.ID );

			// Check for end of run
			if ( GameManager.Run.IsLastPerformanceOfRun ( ) )
			{
				// Update stats for win
				Progression.ProgressManager.Progress.OnWin ( GameManager.Run.PoetID, GameManager.Difficulty.ID, GameManager.Run.ItemData, GameManager.Run.UpgradeData );
			}
		}

		/// <summary>
		/// The callback for when an item is added.
		/// </summary>
		/// <param name="id"> The ID of the item. </param>
		public void OnAddItem ( int id )
		{
			// Register item being added
			Progression.ProgressManager.Progress.OnAddItem ( id );
		}

		/// <summary>
		/// The callback for when a consumable is added.
		/// </summary>
		/// <param name="id"> The ID of the consumable. </param>
		/// <param name="count"> The number of instances of the consumable being added. </param>
		public void OnAddConsumable ( int id, int count )
		{
			// Register consumable being added
			Progression.ProgressManager.Progress.OnAddConsumable ( id, count );
		}

		/// <summary>
		/// The callback for when an upgrade is added.
		/// </summary>
		/// <param name="id"> The ID of the upgrade. </param>
		public void OnAddUpgrade ( int id )
		{
			// Register upgrade being added
			Progression.ProgressManager.Progress.OnAddUpgrade ( id );
		}

		#endregion // Public Functions
	}
}