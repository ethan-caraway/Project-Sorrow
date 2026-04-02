using FlightPaper.ProjectSorrow.Consumables;
using FlightPaper.ProjectSorrow.Shop;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Coaster item.
	/// </summary>
	public class Coaster : Item
	{
		#region Class Constructors

		public Coaster ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID ).ToString ( "0.#" ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "1" );
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get scale value
			float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Apply additional snaps earned
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = Enums.SplashColorType.SNAPS_GOLD,
					SplashText = $"<b>x{scale}</b>"
				},
				Snaps = (int)( total * ( scale - 1f ) )
			};
		}

		public override bool OnCompleteRound ( )
		{
			// Reset scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 1f );

			// Trigger item
			return true;
		}

		public override bool IsCompleteRoundEffectPositive ( )
		{
			// Highlight negative effect
			return false;
		}

		public override bool OnPurchaseConsumable ( ConsumableScriptableObject consumable )
		{
			// Check for loan
			if ( consumable.Type == Enums.ConsumableType.LOAN )
			{
				return false;
			}

			// Scale value
			GameManager.Run.AddItemFloatScaleValue ( ID, InstanceID, 0.2f );

			// Trigger item
			return true;
		}

		public override void OnAdd ( ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 1f );
		}

		#endregion // Item Override Functions
	}
}