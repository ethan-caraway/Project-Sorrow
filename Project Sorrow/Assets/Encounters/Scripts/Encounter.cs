namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the base functionality of an encounter.
	/// </summary>
	public class Encounter
	{
		#region Public Functions

		/// <summary>
		/// The callback for determining if an option is available based on its requirements.
		/// </summary>
		/// <param name="index"> The index of the option. </param>
		/// <param name="option"> The data for the option. </param>
		/// <returns> Whether or not the option is available. </returns>
		public virtual bool IsOptionAvailable ( int index, OptionModel option )
		{
			return true;
		}

		/// <summary>
		/// The callback for getting the results of an option for the encounter.
		/// </summary>
		/// <param name="index"> The index of the option. </param>
		/// <param name="option"> The data for the option. </param>
		/// <returns> The data for the results. </returns>
		public virtual ResultModel GetResults ( int index, OptionModel option )
		{
			return new ResultModel ( );
		}

		#endregion // Public Functions
	}
}