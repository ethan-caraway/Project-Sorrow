namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Ox Horn item.
	/// </summary>
	public class OxHorn : Item
	{
		#region Class Constructors

		public OxHorn ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			// Apply Stubborn
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				StatusEffect = new StatusEffects.StatusEffectModel
				{
					Type = Enums.StatusEffectType.STUBBORN,
					Count = 3
				}
			};
		}

		#endregion // Item Override Functions
	}
}