using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Menus
{
	/// <summary>
	/// This class controls the display of a bar in the stats chart in the stats collection menu.
	/// </summary>
	public class StatChartBar : MonoBehaviour
	{
		#region Chart Data Constants

		private const float FILL_DURATION = 0.75f;

		#endregion // Chart Data Constants

		#region UI Elements

		[SerializeField]
		private Image highlightImage;

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private Slider barSlider;

		[SerializeField]
		private Image fillImage;

		[SerializeField]
		private HUD.IntegerCounter counter;

		[SerializeField]
		private GameObject infoContainer;

		[SerializeField]
		private GameObject standardInfoContainer;

		[SerializeField]
		private TMP_Text standardTitleText;

		[SerializeField]
		private TMP_Text standardDescriptionText;

		[SerializeField]
		private GameObject poetInfoContainer;

		[SerializeField]
		private TMP_Text poetTitleText;

		[SerializeField]
		private Image perkIconImage;

		[SerializeField]
		private TMP_Text perkTitleText;

		[SerializeField]
		private TMP_Text perkDescriptionText;

		#endregion // UI Elements

		#region Chart Data

		[SerializeField]
		private Color32 highlightDefaultColor;

		[SerializeField]
		private Color32 fillDefaultColor;

		#endregion // Chart Data

		#region Public Functions

		/// <summary>
		/// Sets the stat to be displayed for a given item.
		/// </summary>
		/// <param name="item"> The data for the item. </param>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		public void SetItemStat ( Items.ItemScriptableObject item, float percentage, int stat )
		{
			// Get rarity color
			Color32 rarityColor = Utils.GetRarityColorsOld ( item.Rarity ).normalColor;

			// Display item
			highlightImage.color = rarityColor;
			iconImage.sprite = item.Icon;

			// Set item info
			infoContainer.SetActive ( false );
			standardInfoContainer.SetActive ( true );
			poetInfoContainer.SetActive ( false );
			standardTitleText.text = item.Title;
			standardDescriptionText.text = item.Description;

			// Set bar color
			fillImage.color = rarityColor;

			// Set and animate stat
			AnimateBar ( percentage, stat );
		}

		/// <summary>
		/// Sets the stat to be displayed for a given consumable.
		/// </summary>
		/// <param name="consumable"> The data for the consumable. </param>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		public void SetConsumableStat ( Consumables.ConsumableScriptableObject consumable, float percentage, int stat )
		{
			// Get rarity color
			Color32 rarityColor = Utils.GetRarityColorsOld ( consumable.Rarity ).normalColor;

			// Display consumable
			highlightImage.color = rarityColor;
			iconImage.sprite = consumable.Icon;

			// Set consumable info
			infoContainer.SetActive ( false );
			standardInfoContainer.SetActive ( true );
			poetInfoContainer.SetActive ( false );
			standardTitleText.text = consumable.Title;
			standardDescriptionText.text = consumable.Description;

			// Set bar color
			fillImage.color = rarityColor;

			// Set and animate stat
			AnimateBar ( percentage, stat );
		}

		/// <summary>
		/// Sets the stat to be displayed for a given poet.
		/// </summary>
		/// <param name="poet"> The data for the poet. </param>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		public void SetPoetStat ( Poets.PoetScriptableObject poet, float percentage, int stat )
		{
			// Display poet
			highlightImage.color = highlightDefaultColor;
			iconImage.sprite = poet.Icon;

			// Set poet info
			infoContainer.SetActive ( false );
			standardInfoContainer.SetActive ( false );
			poetInfoContainer.SetActive ( true );
			poetTitleText.text = poet.Title;
			perkIconImage.sprite = poet.Perk.Icon;
			perkTitleText.text = poet.Perk.Title;
			perkDescriptionText.text = poet.Perk.Description;

			// Set bar color
			fillImage.color = fillDefaultColor;

			// Set and animate stat
			AnimateBar ( percentage, stat );
		}

		/// <summary>
		/// Sets the stat to be displayed for a given judge.
		/// </summary>
		/// <param name="judge"> The data for the judge. </param>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		public void SetJudgeStat ( Judges.JudgeScriptableObject judge, float percentage, int stat )
		{
			// Display judge
			highlightImage.color = highlightDefaultColor;
			iconImage.sprite = judge.Icon;

			// Set judge info
			infoContainer.SetActive ( false );
			standardInfoContainer.SetActive ( true );
			poetInfoContainer.SetActive ( false );
			standardTitleText.text = judge.Title;
			standardDescriptionText.text = judge.Description;

			// Set bar color
			fillImage.color = fillDefaultColor;

			// Set and animate stat
			AnimateBar ( percentage, stat );
		}

		/// <summary>
		/// Sets the stat to be displayed for a given upgrade.
		/// </summary>
		/// <param name="upgrade"> The data for the upgrade. </param>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		public void SetUpgradeStat ( Upgrades.UpgradeScriptableObject upgrade, float percentage, int stat )
		{
			// Display upgrade
			highlightImage.color = highlightDefaultColor;
			iconImage.sprite = upgrade.Icon;

			// Set upgrade info
			infoContainer.SetActive ( false );
			standardInfoContainer.SetActive ( true );
			poetInfoContainer.SetActive ( false );
			standardTitleText.text = upgrade.Title;
			standardDescriptionText.text = upgrade.Description;

			// Set bar color
			fillImage.color = fillDefaultColor;

			// Set and animate stat
			AnimateBar ( percentage, stat );
		}

		/// <summary>
		/// Displays the information for the object of the stat.
		/// </summary>
		public void PreviewInfo ( )
		{
			// Display info
			infoContainer.SetActive ( true );
		}

		/// <summary>
		/// Hides the information for the object of the stat.
		/// </summary>
		public void EndPreview ( )
		{
			// Hide info
			infoContainer.SetActive ( false );
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Animates the stat bar filling.
		/// </summary>
		/// <param name="percentage"> The percentage to fill the bar. </param>
		/// <param name="stat"> The value of the stat. </param>
		private void AnimateBar ( float percentage, int stat )
		{
			// Reset slider
			barSlider.value = 0;

			// Animate bar filling
			barSlider.DOValue ( percentage, FILL_DURATION );

			// Reset stat
			counter.SetNumber ( 0, true );

			// Animate stat counting
			counter.AddNumber ( stat, false );
		}

		#endregion // Private Functions
	}
}