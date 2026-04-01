namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Arts Degree item.
	/// </summary>
	public class ArtsDegree : Item
	{
		#region Class Constructors

		public ArtsDegree ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override float OnLoanChance ( )
		{
			return 0.2f;
		}

		public override bool OnGenerateConsumables ( Shop.ShopConsumableModel [ ] consumables, int totalShopConsumables )
		{
			// Check for loan
			for ( int i = 0; i < consumables.Length; i++ )
			{
				if ( i < totalShopConsumables && consumables [ i ].Consumable.Type == Enums.ConsumableType.LOAN )
				{
					return true;
				}
			}

			// Return that no loan was generated
			return base.OnGenerateConsumables ( consumables, totalShopConsumables );
		}

		#endregion // Item Override Functions
	}
}