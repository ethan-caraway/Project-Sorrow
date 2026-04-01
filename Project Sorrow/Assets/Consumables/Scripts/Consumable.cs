namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the base functionality of a consumable.
	/// </summary>
	public class Consumable
	{
		#region Public Performance Functions

		/// <summary>
		/// The callback for getting the status effects to apply to the player.
		/// </summary>
		/// <param name="instances"> The total number of instance of this consumable. </param>
		/// <returns> The data for the status effect(s) to add. </returns>
		public virtual StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			return null;
		}

		/// <summary>
		/// The callback for getting the enhancements to add to a poem.
		/// </summary>
		/// <param name="instances"> The total number of instances of this consumable. </param>
		/// <returns> The poem data for the enhancements to add. </returns>
		public virtual Poems.PoemModel OnEnhancePoem ( int instances )
		{
			return null;
		}

		/// <summary>
		/// The callback for getting the word modifiers to add to a poem.
		/// </summary>
		/// <param name="instances"> The total number of instances of this consumable. </param>
		/// <returns> The list of word modifiers. </returns>
		public virtual Enums.WordModifierType [ ] OnModifyWords ( int instances )
		{
			return null;
		}

		#endregion // Public Performance Functions

		#region Public Shop Functions

		/// <summary>
		/// The callback for the amount of money gained from a loan.
		/// </summary>
		/// <returns> The amount of money from the loan. </returns>
		public virtual int GetLoanMoney ( )
		{
			return 0;
		}

		/// <summary>
		/// The callback for the amount of debt gained from a loan.
		/// </summary>
		/// <returns> The amount of debt from the loan. </returns>
		public virtual int GetLoanDebt ( )
		{
			return 0;
		}

		#endregion // Public Shop Functions
	}
}