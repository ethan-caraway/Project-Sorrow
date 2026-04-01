using TMPro;
using UnityEngine;

namespace FlightPaper.ProjectSorrow.HUD
{
	/// <summary>
	/// This class controls the display of an item's information.
	/// </summary>
	public class ItemInfoDisplay : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private TMP_Text titleText;

		[SerializeField]
		private TMP_Text descriptionText;

		[SerializeField]
		private TMP_Text scaleText;

		#endregion // UI Elements

		#region Public Functions

		/// <summary>
		/// Sets the item information to display.
		/// </summary>
		/// <param name="item"> The data of the item on display. </param>
		/// <param name="instanceID"> The ID of the instance of the item. </param>
		public void SetItem ( Items.ItemScriptableObject item, string instanceID )
		{
			// Check for item
			if ( item == null )
			{
				return;
			}

			// Display item name
			titleText.text = item.Title;

			// Display item description
			if ( item.IsVariableDescription )
			{
				// Check if item is owned
				if ( GameManager.IsRunActive && GameManager.Run.HasItem ( item.ID ) && !string.IsNullOrEmpty ( instanceID ) )
				{
					// Display variable description
					descriptionText.text = GameManager.Run.GetItemVariableDescription ( item.ID, instanceID, item.Description );
				}
				else
				{
					// Display would be variable description
					descriptionText.text = GameManager.Run.GetWouldBeItemVariableDescription ( item.ID, item.Description );
				}
			}
			else
			{
				// Display base description
				descriptionText.text = item.Description;
			}

			// Display scale value
			scaleText.gameObject.SetActive ( item.IsScalable );
			if ( item.IsScalable )
			{
				// Check scale type
				if ( item.ScaleType == Enums.ScaleType.SNAPS )
				{
					// Store scale value
					int scale = 0;

					// Check if item is owned
					if ( GameManager.IsRunActive && GameManager.Run.HasItem ( item.ID ) )
					{
						// Get current scale value
						scale = GameManager.Run.GetItemIntScaleValue ( item.ID, instanceID );
					}
					else
					{
						// Get would be scale value
						scale = GameManager.Run.GetWouldBeItemIntScaleValue ( item.ID );
					}

					// Format scale value
					scaleText.text = $"(Currently: <color=#A1740E>+{scale:N0}</color>)";
				}
				else if ( item.ScaleType == Enums.ScaleType.MULTIPLIER )
				{
					// Store scale value
					float scale = 0;

					// Check if item is owned
					if ( GameManager.IsRunActive && GameManager.Run.HasItem ( item.ID ) )
					{
						// Get current scale value
						scale = GameManager.Run.GetItemFloatScaleValue ( item.ID, instanceID );
					}
					else
					{
						// Get would be scale value
						scale = GameManager.Run.GetWouldBeItemFloatScaleValue ( item.ID );
					}

					// Format scale value
					scaleText.text = $"(Currently: <color=#A1740E><b>x{scale:0.#}</b></color>)";
				}
				else if ( item.ScaleType == Enums.ScaleType.TIME )
				{
					// Store scale value
					float scale = 0;

					// Check if item is owned
					if ( GameManager.IsRunActive && GameManager.Run.HasItem ( item.ID ) )
					{
						// Get current scale value
						scale = GameManager.Run.GetItemFloatScaleValue ( item.ID, instanceID );
					}
					else
					{
						// Get would be scale value
						scale = GameManager.Run.GetWouldBeItemFloatScaleValue ( item.ID );
					}

					// Format scale value
					scaleText.text = $"(Currently: <color=#FFE100>+{Utils.FormatTime ( scale )}</color>)";
				}
				else if ( item.ScaleType == Enums.ScaleType.COUNT )
				{
					// Store scale value
					int scale = 0;

					// Check if item is owned
					if ( GameManager.IsRunActive && GameManager.Run.HasItem ( item.ID ) )
					{
						// Get current scale value
						scale = GameManager.Run.GetItemIntScaleValue ( item.ID, instanceID );
					}
					else
					{
						// Get would be scale value
						scale = GameManager.Run.GetWouldBeItemIntScaleValue ( item.ID );
					}

					// Format scale value
					scaleText.text = $"(Currently: <b>{scale}</b> Remaining)";
				}
			}
		}

		#endregion // Public Functions
	}
}