using UnityEngine.Localization;

namespace FlightPaper
{
	/// <summary>
	/// This class contains extension functions for Unity classes.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Gets whether or not this localization data contains valid data.
		/// </summary>
		/// <param name="localizedString"> The localization data. </param>
		/// <returns> Whether or not the localization data is valid. </returns>
		public static bool IsValid ( this LocalizedString localizedString )
		{
			return localizedString != null &&
				   localizedString.TableReference.ReferenceType != UnityEngine.Localization.Tables.TableReference.Type.Empty &&
				   localizedString.TableEntryReference.ReferenceType != UnityEngine.Localization.Tables.TableEntryReference.Type.Empty;
		}
	}
}