using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the display of an option.
	/// </summary>
	public class OptionDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private Button optionButton;

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private HUD.ItemInfoDisplay itemDisplay;

		[SerializeField]
		private HUD.ConsumableInfoDisplay consumableDisplay;

		[SerializeField]
		private HUD.TagInfoDisplay tagDisplay;

		#endregion // UI Elements

		#region Option Data

		[SerializeField]
		private Color32 titleColor;

		[SerializeField]
		private Color32 descriptionColor;

		[SerializeField]
		private Color32 unavailableColor;

		private bool isAvailable;

		#endregion // Option Data

		#region Public Functions

		/// <summary>
		/// Displays the data for an option.
		/// </summary>
		/// <param name="model"> The data of the option. </param>
		/// <param name="available"> Whether or not the option is available. </param>
		public void SetOption ( OptionModel model, bool available )
		{
			// Display the option info
			titleText.text = model.Title;

			// Check if the option is available
			isAvailable = available;
			optionButton.interactable = isAvailable;
			if ( isAvailable )
			{
				// Display option as available
				titleText.color = titleColor;
				descriptionText.text = model.Description;
				descriptionText.color = descriptionColor;

				// Check for item info
				if ( model.Item != null )
				{
					// Set item info
					itemDisplay.gameObject.SetActive ( true );
					itemDisplay.SetItem ( model.Item, string.Empty );

					// Hide consumable info
					consumableDisplay.gameObject.SetActive ( false );

					// Set tag info if available
					tagDisplay.gameObject.SetActive ( model.Item.HasTag );
					if ( model.Item.HasTag )
					{
						tagDisplay.SetTag ( Tags.TagUtility.GetTag ( model.Item.Tag ) );
					}
				}
				// Check for consumable info
				else if ( model.Consumable != null )
				{
					// Hide item info
					itemDisplay.gameObject.SetActive ( false );

					// Set consumable
					consumableDisplay.gameObject.SetActive ( true );
					consumableDisplay.SetConsumable ( model.Consumable );

					// Set tag info if available
					tagDisplay.gameObject.SetActive ( model.Consumable.HasTag );
					if ( model.Consumable.HasTag )
					{
						tagDisplay.SetTag ( Tags.TagUtility.GetTag ( model.Consumable.Tag ) );
					}
				}
				// Check for tag info
				else if ( !string.IsNullOrEmpty ( model.Tag ) )
				{
					// Hide item info
					itemDisplay.gameObject.SetActive ( false );

					// Hide consumable info
					consumableDisplay.gameObject.SetActive ( false );

					// Get tag info
					Tags.TagScriptableObject tag = Tags.TagUtility.GetTag ( model.Tag );

					// Set tag info
					tagDisplay.gameObject.SetActive ( tag != null );
					if ( tag != null )
					{
						tagDisplay.SetTag ( tag );
					}
				}
				else
				{
					// Hide displays
					itemDisplay.gameObject.SetActive ( false );
					consumableDisplay.gameObject.SetActive ( false );
					tagDisplay.gameObject.SetActive ( false );
				}
			}
			else
			{
				// Display option as unavailable
				titleText.color = unavailableColor;
				descriptionText.text = model.Requirement;
				descriptionText.color = unavailableColor;

				// Hide displays
				itemDisplay.gameObject.SetActive ( false );
				consumableDisplay.gameObject.SetActive ( false );
				tagDisplay.gameObject.SetActive ( false );
			}

			// Hide info
			infoContainer.SetActive ( false );
		}

		/// <summary>
		/// Display the info for the option.
		/// </summary>
		public void ShowInfo ( )
		{
			// Display info if available
			infoContainer.SetActive ( isAvailable );
		}

		/// <summary>
		/// Hides the info for the option.
		/// </summary>
		public void HideInfo ( )
		{
			// Hide info
			infoContainer.SetActive ( false );
		}

		#endregion // Public Functions
	}
}