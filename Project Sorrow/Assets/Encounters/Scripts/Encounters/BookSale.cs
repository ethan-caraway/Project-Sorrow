namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Book Sale encounter.
	/// </summary>
	public class BookSale : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					return GameManager.Run.CanAddItem ( ) && GameManager.Run.CanPlayerAffordPrice ( 5 );

				case 1:
					return GameManager.Run.CanAddItem ( ) && GameManager.Run.CanPlayerAffordPrice ( 15 );
			}

			// Return true by default
			return base.IsOptionAvailable ( index, option );
		}

		public override ResultModel GetResults ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					// Add Chapbook
					GameManager.Run.AddItem ( option.Item.ID );

					// Deduct money
					return new ResultModel
					{
						Money = -5
					};

				case 1:
					// Add Signed First Edition
					GameManager.Run.AddItem ( option.Item.ID );

					// Deduct money
					return new ResultModel
					{
						Money = -15
					};

				case 2:
					// Deduct time and skip shop
					return new ResultModel
					{
						IsShopSkipped = true,
						Bonus = new EncounterBonusModel
						{
							TimeAllowance = -45f
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}