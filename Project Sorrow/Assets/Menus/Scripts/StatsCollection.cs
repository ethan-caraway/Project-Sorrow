using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the stats tab in the collections menu.
	/// </summary>
	public class StatsCollection : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text runStatText;

		[SerializeField]
		private TMP_Text winStatText;

		[SerializeField]
		private TMP_Text winRateStatText;

		[SerializeField]
		private TMP_Text difficultyStatText;

		[SerializeField]
		private TMP_Text performanceStatText;

		[SerializeField]
		private TMP_Text mostPerformanceStatText;

		[SerializeField]
		private TMP_Text mostRoundStatText;

		[SerializeField]
		private TMP_Text scoreStatText;

		[SerializeField]
		private TMP_Text confidenceStatText;

		[SerializeField]
		private TMP_Text moneyStatText;

		[SerializeField]
		private TMP_Text debtStatText;

		[SerializeField]
		private TMP_Text timeStatText;

		[SerializeField]
		private TMP_Dropdown categoryDropdown;

		[SerializeField]
		private TMP_Dropdown statDropdown;

		[SerializeField]
		private StatChartBar [ ] chartBars;

		[SerializeField]
		private TMP_Text chartText;

		#endregion // UI Elements

		#region Stat Data

		private int categoryIndex;

		#endregion // Stat Data

		#region Public Functions

		/// <summary>
		/// Initializes the stats collection panel.
		/// </summary>
		public void Init ( )
		{
			// Display global stats
			DisplayStats ( );

			// Display first category
			SetCategory ( 0 );
		}

		/// <summary>
		/// Displays the chart for the selected category of stats.
		/// </summary>
		/// <param name="index"> The index of the category. </param>
		public void SetCategory ( int index )
		{
			// Store category
			categoryIndex = index;
			categoryDropdown.value = index;

			// Store new stats
			List<string> stats = new List<string> ( );

			// Check category
			switch ( categoryIndex )
			{
				// Check for items
				case 0:
					// Set stats
					stats.Add ( "Most Acquired" );
					stats.Add ( "Most Wins" );
					break;

				// Check for consumables
				case 1:
					// Set stats
					stats.Add ( "Most Acquired" );
					stats.Add ( "Most Consumed" );
					break;

				// Check for poets
				case 2:
					// Set stats
					stats.Add ( "Most Runs" );
					stats.Add ( "Most Wins" );
					stats.Add ( "Best Performance" );
					break;

				// Check for judges
				case 3:
					// Set stats
					stats.Add ( "Most Performances" );
					stats.Add ( "Most Successes" );
					break;

				// Check for upgrades
				case 4:
					// Set stats
					stats.Add ( "Most Acquired" );
					stats.Add ( "Most Wins" );
					break;
			}

			// Update stats
			statDropdown.ClearOptions ( );
			statDropdown.AddOptions ( stats );

			// Display first stat for the category
			SetStat ( 0 );
		}

		/// <summary>
		/// Displays the chart for the selected stat.
		/// </summary>
		/// <param name="index"> The index of the stat. </param>
		public void SetStat ( int index )
		{
			// Display stat
			statDropdown.value = index;

			// Display chart
			SetChart ( categoryIndex, index );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the stats in the global stats panel.
		/// </summary>
		private void DisplayStats ( )
		{
			// Track stats
			int runs = 0;
			int wins = 0;
			int difficulty = 0;
			int performances = 0;
			int rounds = 0;
			int score = 0;

			// Derive stats
			for ( int i = 0; i < Progression.ProgressManager.Progress.PoetStats.Length; i++ )
			{
				// Increment runs
				runs += Progression.ProgressManager.Progress.PoetStats [ i ].Runs;

				// Increment wins
				wins += Progression.ProgressManager.Progress.PoetStats [ i ].Wins;

				// Check difficulty
				if ( difficulty < Progression.ProgressManager.Progress.PoetStats [ i ] .HighestDifficultyWin )
				{
					// Store new highest difficulty
					difficulty = Progression.ProgressManager.Progress.PoetStats [ i ].HighestDifficultyWin;
				}

				// Check most performances
				if ( performances < Progression.ProgressManager.Progress.PoetStats [ i ].HighestPerformance )
				{
					// Store new highest performance
					performances = Progression.ProgressManager.Progress.PoetStats [ i ].HighestPerformance;
				}

				// Check most rounds
				if ( rounds < Progression.ProgressManager.Progress.PoetStats [ i ].HighestRound )
				{
					// Store new highest round
					rounds = Progression.ProgressManager.Progress.PoetStats [ i ].HighestRound;
				}

				// Check high score
				if ( score < Progression.ProgressManager.Progress.PoetStats [ i ].HighScore )
				{
					// Store new high score
					score = Progression.ProgressManager.Progress.PoetStats [ i ].HighScore;
				}
			}

			// Calculate win rate
			float winRate = runs > 0 ? wins / runs : 0f;

			// Display stats
			runStatText.text = runs.ToString ( );
			winStatText.text = wins.ToString ( );
			winRateStatText.text = winRate.ToString ( "P1" );
			difficultyStatText.text = difficulty > 0 ? $"Prestige {Utils.ToRomanNumeral ( difficulty )}" : "N/A";
			performanceStatText.text = Progression.ProgressManager.Progress.TotalSuccessfulPerformances.ToString ( );
			mostPerformanceStatText.text = performances.ToString ( );
			mostRoundStatText.text = rounds.ToString ( );
			scoreStatText.text = score.ToString ( );
			confidenceStatText.text = Progression.ProgressManager.Progress.ChallengeStats.MostStartConfidence.ToString ( );
			moneyStatText.text = $"${Progression.ProgressManager.Progress.ChallengeStats.MostMoney}";
			debtStatText.text = Progression.ProgressManager.Progress.ChallengeStats.MostDebt < 0 ? $"-${Progression.ProgressManager.Progress.ChallengeStats.MostDebt * -1}" : "$0";
			timeStatText.text = Utils.FormatTime ( Progression.ProgressManager.Progress.ChallengeStats.MostStartTime );
		}

		/// <summary>
		/// Sets the stat chart based on the selected stat and category.
		/// </summary>
		/// <param name="category"> The index of the category. </param>
		/// <param name="stat"> The index of the stat. </param>
		private void SetChart ( int category, int stat )
		{
			// Check category
			switch ( category )
			{
				// Check for items
				case 0:
					// Check for most acquired
					if ( stat == 0 )
					{
						SetItemMostAcquired ( );
					}
					// Check for most wins
					else if ( stat == 1 )
					{
						SetItemMostWins ( );
					}
					break;

				// Check for consumables
				case 1:
					// Check for most acquired
					if ( stat == 0 )
					{
						SetConsumableMostAcquired ( );
					}
					// Check for most consumed
					else if ( stat == 1 )
					{
						SetConsumableMostConsumed ( );
					}
					break;

				// Check for poets
				case 2:
					// Check for most runs
					if ( stat == 0 )
					{
						SetPoetMostRuns ( );
					}
					// Check for most wins
					else if ( stat == 1 )
					{
						SetPoetMostWins ( );
					}
					// Check for high score
					else if ( stat == 2 )
					{
						SetPoetHighScore ( );
					}
					break;

				// Check for judges
				case 3:
					// Check for most performances
					if ( stat == 0 )
					{
						SetJudgeMostPerformed ( );
					}
					// Check for most successes
					else if ( stat == 1 )
					{
						SetJudgeMostSuccess ( );
					}
					break;

				// Check for upgrades
				case 4:
					// Check for most acquired
					if ( stat == 0 )
					{
						SetUpgradeMostAcquired ( );
					}
					// Check for most wins
					else if ( stat == 1 )
					{
						SetUpgradeMostWins ( );
					}
					break;
			}
		}

		/// <summary>
		/// Sets the stats chart for the most acquired items.
		/// </summary>
		private void SetItemMostAcquired ( )
		{
			// Get item stats
			List<Progression.ItemStatsModel> stats = new List<Progression.ItemStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.ItemStats );

			// Sort by most acquired
			stats = stats.OrderByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most acquired
			int max = stats [ 0 ].Owns;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get item
				Items.ItemScriptableObject item = Items.ItemUtility.GetItem ( stats [ i ].ID );

				// Check for discovered item
				if ( item != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Owns / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetItemStat ( item, percentage, stats [ i ].Owns );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more items during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most win items.
		/// </summary>
		private void SetItemMostWins ( )
		{
			// Get item stats
			List<Progression.ItemStatsModel> stats = new List<Progression.ItemStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.ItemStats );

			// Sort by most wins
			stats = stats.OrderByDescending ( x => x.Wins ).ThenByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most wins
			int max = stats [ 0 ].Wins;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get item
				Items.ItemScriptableObject item = Items.ItemUtility.GetItem ( stats [ i ].ID );

				// Check for discovered item
				if ( item != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Wins / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetItemStat ( item, percentage, stats [ i ].Wins );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more items during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most acquired consumables.
		/// </summary>
		private void SetConsumableMostAcquired ( )
		{
			// Get consumable stats
			List<Progression.ConsumableStatsModel> stats = new List<Progression.ConsumableStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.ConsumableStats );

			// Sort by most acquired
			stats = stats.OrderByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most acquired
			int max = stats [ 0 ].Owns;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get consumable
				Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( stats [ i ].ID );

				// Check for discovered consumable
				if ( consumable != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Owns / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetConsumableStat ( consumable, percentage, stats [ i ].Owns );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more consumables during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most consumed consumables.
		/// </summary>
		private void SetConsumableMostConsumed ( )
		{
			// Get consumable stats
			List<Progression.ConsumableStatsModel> stats = new List<Progression.ConsumableStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.ConsumableStats );

			// Sort by most consumed
			stats = stats.OrderByDescending ( x => x.Consumed ).ThenByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most consumed
			int max = stats [ 0 ].Consumed;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get consumable
				Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( stats [ i ].ID );

				// Check for discovered consumable
				if ( consumable != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Consumed / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetConsumableStat ( consumable, percentage, stats [ i ].Consumed );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more consumables during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most runs poets.
		/// </summary>
		private void SetPoetMostRuns ( )
		{
			// Get poet stats
			List<Progression.PoetStatsModel> stats = new List<Progression.PoetStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.PoetStats );

			// Sort by most runs
			stats = stats.OrderByDescending ( x => x.Runs ).ToList ( );

			// Get relative max for most runs
			int max = stats [ 0 ].Runs;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get poet
				Poets.PoetScriptableObject poet = Poets.PoetUtility.GetPoet ( stats [ i ].ID );

				// Check for unlocked poet
				if ( poet != null && poet.IsUnlocked )
				{
					// Calculate percentage
					float percentage = stats [ i ].Runs / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetPoetStat ( poet, percentage, stats [ i ].Runs );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Unlocked more poets to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most wins poets.
		/// </summary>
		private void SetPoetMostWins ( )
		{
			// Get poet stats
			List<Progression.PoetStatsModel> stats = new List<Progression.PoetStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.PoetStats );

			// Sort by most wins
			stats = stats.OrderByDescending ( x => x.Wins ).ThenByDescending ( x => x.Runs ).ToList ( );

			// Get relative max for most wins
			int max = stats [ 0 ].Wins;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get poet
				Poets.PoetScriptableObject poet = Poets.PoetUtility.GetPoet ( stats [ i ].ID );

				// Check for unlocked poet
				if ( poet != null && poet.IsUnlocked )
				{
					// Calculate percentage
					float percentage = stats [ i ].Wins / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetPoetStat ( poet, percentage, stats [ i ].Wins );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Unlocked more poets to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the high score poets.
		/// </summary>
		private void SetPoetHighScore ( )
		{
			// Get poet stats
			List<Progression.PoetStatsModel> stats = new List<Progression.PoetStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.PoetStats );

			// Sort by high score
			stats = stats.OrderByDescending ( x => x.HighScore ).ToList ( );

			// Get relative max for high score
			int max = stats [ 0 ].HighScore;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get poet
				Poets.PoetScriptableObject poet = Poets.PoetUtility.GetPoet ( stats [ i ].ID );

				// Check for unlocked poet
				if ( poet != null && poet.IsUnlocked )
				{
					// Calculate percentage
					float percentage = stats [ i ].HighScore / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetPoetStat ( poet, percentage, stats [ i ].HighScore );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Unlocked more poets to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most performed judges.
		/// </summary>
		private void SetJudgeMostPerformed ( )
		{
			// Get judge stats
			List<Progression.JudgeStatsModel> stats = new List<Progression.JudgeStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.JudgeStats );

			// Sort by most performed
			stats = stats.OrderByDescending ( x => x.Performances ).ToList ( );

			// Get relative max for most performed
			int max = stats [ 0 ].Performances;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get judge
				Judges.JudgeScriptableObject judge = Judges.JudgeUtility.GetJudge ( stats [ i ].ID );

				// Check for discovered judge
				if ( judge != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Performances / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetJudgeStat ( judge, percentage, stats [ i ].Performances );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Perform against more judges during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most success judges.
		/// </summary>
		private void SetJudgeMostSuccess ( )
		{
			// Get judge stats
			List<Progression.JudgeStatsModel> stats = new List<Progression.JudgeStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.JudgeStats );

			// Sort by most success
			stats = stats.OrderByDescending ( x => x.Successes ).ThenByDescending ( x => x.Performances ).ToList ( );

			// Get relative max for most success
			int max = stats [ 0 ].Successes;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get judge
				Judges.JudgeScriptableObject judge = Judges.JudgeUtility.GetJudge ( stats [ i ].ID );

				// Check for discovered judge
				if ( judge != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Successes / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetJudgeStat ( judge, percentage, stats [ i ].Successes );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Perform against more judges during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most acquired upgrades.
		/// </summary>
		private void SetUpgradeMostAcquired ( )
		{
			// Get upgrade stats
			List<Progression.UpgradeStatsModel> stats = new List<Progression.UpgradeStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.UpgradeStats );

			// Sort by most acquired
			stats = stats.OrderByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most acquired
			int max = stats [ 0 ].Owns;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get upgrade
				Upgrades.UpgradeScriptableObject upgrade = Upgrades.UpgradeUtility.GetUpgrade ( stats [ i ].ID );

				// Check for discovered upgrade
				if ( upgrade != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Owns / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetUpgradeStat ( upgrade, percentage, stats [ i ].Owns );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more upgrades during a run to discover their stats";
			}
		}

		/// <summary>
		/// Sets the stats chart for the most wins upgrades.
		/// </summary>
		private void SetUpgradeMostWins ( )
		{
			// Get upgrade stats
			List<Progression.UpgradeStatsModel> stats = new List<Progression.UpgradeStatsModel> ( );
			stats.AddRange ( Progression.ProgressManager.Progress.UpgradeStats );

			// Sort by most wins
			stats = stats.OrderByDescending ( x => x.Wins ).ThenByDescending ( x => x.Owns ).ToList ( );

			// Get relative max for most wins
			int max = stats [ 0 ].Wins;
			if ( max == 0 )
			{
				max = 1;
			}

			// Track if the chart is complete
			bool isComplete = true;

			// Set chart
			for ( int i = 0; i < chartBars.Length; i++ )
			{
				// Get upgrade
				Upgrades.UpgradeScriptableObject upgrade = Upgrades.UpgradeUtility.GetUpgrade ( stats [ i ].ID );

				// Check for discovered upgrade
				if ( upgrade != null && stats [ i ].IsDiscovered )
				{
					// Calculate percentage
					float percentage = stats [ i ].Wins / (float)max;

					// Display bar
					chartBars [ i ].gameObject.SetActive ( true );
					chartBars [ i ].SetUpgradeStat ( upgrade, percentage, stats [ i ].Wins );
				}
				else
				{
					// Mark chart as incomplete
					isComplete = false;

					// Hide bar
					chartBars [ i ].gameObject.SetActive ( false );
				}
			}

			// Check for complete chart
			chartText.gameObject.SetActive ( !isComplete );
			if ( !isComplete )
			{
				chartText.text = "Acquire more upgrades during a run to discover their stats";
			}
		}

		#endregion // Private Functions
	}
}