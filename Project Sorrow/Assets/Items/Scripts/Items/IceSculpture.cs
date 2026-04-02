using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Ice Sculpture item.
	/// </summary>
	public class IceSculpture : Item
	{
		#region Class Constructors

		public IceSculpture ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private float prevTime;
		private float decayTime;
		private float startScale;

		#endregion // Item Data

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID ).ToString ( "0.##" ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "2" );
		}

		public override void OnInitPerformance ( PerformanceModel model )
		{
			// Set starting time
			prevTime = model.TimeRemaining;
			decayTime = 0;
		}

		public override ItemTriggerModel OnCharacterComplete ( Enums.WordModifierType modifier, PerformanceModel model )
		{
			// Check time progress
			if ( prevTime > model.TimeRemaining )
			{
				// Add the delta of time passed
				decayTime += prevTime - model.TimeRemaining;
			}
			else
			{
				// Add the delta of time passed in case time was added
				decayTime += model.TimeRemaining - prevTime;
			}

			// Store current time
			prevTime = model.TimeRemaining;
			
			// Check decay
			if ( decayTime > 30f )
			{
				// Reset time
				decayTime -= 30f;

				// Get scale value
				float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );
				if ( scale > 1f )
				{
					// Decrease scale value
					scale -= 0.05f;

					// Check for underflow
					if ( scale < 1f )
					{
						// Reset scale
						scale = 1f;
					}

					// Stoe scale
					GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, scale );

					// Trigger item
					return new ItemTriggerModel
					{
						ID = ID,
						InstanceID = InstanceID,
						Highlight = new HUD.ItemHighlightModel
						{
							IsPositive = false,
							SplashColor = Enums.SplashColorType.PENALTY_RED,
							SplashText = "Downgrade"
						}
					};
				}
			}

			// Return that the item was not triggered
			return base.OnCharacterComplete ( modifier, model );
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get scale
			float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Trigger item
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

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 2f );
		}

		#endregion // Item Override Functions
	}
}