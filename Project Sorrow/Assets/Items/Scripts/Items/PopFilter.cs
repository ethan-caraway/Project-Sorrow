namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Pop Filter item.
	/// </summary>
	public class PopFilter : Item
	{
		#region Class Constructors

		public PopFilter ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool OnCompareCharacters ( char line, char input )
		{
			return System.Char.ToLower ( line ) == input;
		}

		#endregion // Item Override Functions
	}
}