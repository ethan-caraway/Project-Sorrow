using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls displaying the player's current money in the HUD.
	/// </summary>
	public class MoneyHUD : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private IntegerCounter moneyCounter;

		[SerializeField]
		private GameObject debtContainer;

		[SerializeField]
		private IntegerCounter debtCounter;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Set the amount of money to display.
		/// </summary>
		/// <param name="money"> The amount of money the player has. </param>
		/// <param name="debt"> The amount of debt the player has. </param>
		public void SetMoney ( int money, int debt )
		{
			// Display money
			moneyCounter.SetNumber ( money, true );

			// Display debt
			debtContainer.SetActive ( debt < 0 );
			debtCounter.SetNumber ( debt, true );
		}

		/// <summary>
		/// Adds an amount of money to display.
		/// </summary>
		/// <param name="money"> The amount of money being added. </param>
		public void AddMoney ( int money )
		{
			// Update money
			moneyCounter.AddNumber ( money );
		}

		/// <summary>
		/// Adds an amount of debt to display.
		/// </summary>
		/// <param name="debt"> The amount of debt being added. </param>
		public void AddDebt ( int debt )
		{
			// Update debt
			debtContainer.SetActive ( true );
			debtCounter.AddNumber ( debt );
		}

		/// <summary>
		/// Applies an amount of money to displayed. 
		/// </summary>
		/// <param name="money"> The amount of money being applied. </param>
		/// <param name="currentMoney"> The amount of money the player currently has. </param>
		/// <param name="currentDebt"> The amount of debt the player currently has. </param>
		public void ApplyMoney ( int money, int currentMoney, int currentDebt )
		{
			// Check for adding money
			if ( money > 0 )
			{
				// Check for debt
				if ( currentDebt < 0 )
				{
					// Check if debt will be cleared
					if ( currentDebt + money > 0 )
					{
						// Remove debt
						AddDebt ( currentDebt * -1 );

						// Add money
						AddMoney ( currentDebt + money );
					}
					else
					{
						// Add debt
						AddDebt ( money );
					}
				}
				else
				{
					// Add money
					AddMoney ( money );
				}
			}
			else
			{
				// Check for new debt
				if ( currentMoney + money < 0 )
				{
					// Check for money
					if ( currentMoney > 0 )
					{
						// Remove money
						AddMoney ( currentMoney * -1 );

						// Add debt
						AddDebt ( currentMoney + money );
					}
					else
					{
						// Add debt
						AddDebt ( money );
					}
				}
				else
				{
					// Subtract money
					AddMoney ( money );
				}
			}

			// Update stats
			if ( !GameManager.IsTutorial )
			{
				GameManager.Run.Stats.OnApplyMoney ( );
			}
		}

		#endregion // Public Functions
	}
}