namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Snowball item.
	/// </summary>
	public class Snowball : Item
	{
		#region Class Constructors

		public Snowball ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ).ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "0" );
		}

		public override bool OnFlub ( )
		{
			// Reset scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 0 );

			// Return that this item was triggered
			return true;
		}

		public override bool IsFlubEffectPositive ( )
		{
			return false;
		}

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Get scale value
			int snaps = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Increment scale value
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 1 );

			// Apply additional snaps
			return snaps;
		}

		#endregion // Item Override Functions
	}
}