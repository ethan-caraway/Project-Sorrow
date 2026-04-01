using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightPaper.ProjectSorrow.Poems
{
	/// <summary>
	/// This class controls the preview of a poem.
	/// </summary>
	public class PoemPreview : MonoBehaviour
	{
		#region UI Elements

		[SerializeField]
		private HUD.ConsumablesHUD consumablesHUD;

		[SerializeField]
		private Scrollbar scrollbar;

		[SerializeField]
		private Transform container;

		[SerializeField]
		private LinePreview linePrefab;

		[SerializeField]
		private GameObject stanzaPrefab;

		[SerializeField]
		private GameObject enhancementsContainer;

		[SerializeField]
		private GameObject draftText;

		[SerializeField]
		private TMP_Text snapsText;

		[SerializeField]
		private TMP_Text confidenceText;

		[SerializeField]
		private TMP_Text arroganceText;

		[SerializeField]
		private TMP_Text timeText;

		[SerializeField]
		private TMP_Text reputationText;

		[SerializeField]
		private TMP_Text applauseText;

		[SerializeField]
		private TMP_Text commissionText;

		#endregion // UI Elements

		#region Poem Data

		private PoemModel model;
		private System.Action<PoemModel> onApplyToPoem;

		private Dictionary<LineKey, List<WordModel>> availableWords = new Dictionary<LineKey, List<WordModel>> ( );
		private List<GameObject> poemObjects = new List<GameObject> ( );
		private Dictionary<LineKey, LinePreview> lines = new Dictionary<LineKey, LinePreview> ( );

		#endregion // Poem Data

		#region Public Functions

		/// <summary>
		/// Displays a poem to preview.
		/// </summary>
		/// <param name="poem"> The data for the poem. </param>
		/// <param name="onApply"> The callback for when word modifiers or enhancments are applied to a poem. </param>
		public void SetPoem ( PoemModel poem, System.Action<PoemModel> onApply )
		{
			// Store callback
			onApplyToPoem = onApply;

			// Check for new poem
			if ( model == null || model.ID != poem.ID )
			{
				// Store poem data
				model = poem;
				Stanza [ ] stanzas = poem.ToStanzas ( );

				// Get the available words
				availableWords.Clear ( );
				availableWords = PoemHelper.GetWords ( stanzas, poem.Modifiers );

				// Display the poem
				DisplayPoem ( stanzas, poem.Modifiers );

				// Display the enhancements for the poem
				DisplayEnhancements ( );
			}

			// Enable consumable application
			consumablesHUD.EnableApplyToPoem ( OnApplyConsumable, OnApplyAllConsumable );

			// Scroll to the top of the poem
			scrollbar.value = 1;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Displays the poem with its modified words.
		/// </summary>
		/// <param name="stanzas"> The data for each stanza of the poem. </param>
		/// <param name="wordModifiers"> The word modifiers applied to the poem. </param>
		private void DisplayPoem ( Stanza [ ] stanzas, WordModel [ ] wordModifiers )
		{
			// Clear previous poem
			for ( int i = 0; i < poemObjects.Count; i++ )
			{
				Destroy ( poemObjects [ i ] );
			}
			poemObjects.Clear ( );
			lines.Clear ( );

			// Get each modifier
			Dictionary<LineKey, List<WordModel>> modifiers = PoemHelper.GetModifiersByLine ( wordModifiers );

			// Create poem
			for ( int s = 0; s < stanzas.Length; s++ )
			{
				// Create each line
				for ( int l = 0; l < stanzas [ s ].Lines.Length; l++ )
				{
					// Create line
					LinePreview line = Instantiate ( linePrefab, container );

					// Create key
					LineKey key = new LineKey
					{
						Stanza = s,
						Line = l
					};

					// Get line modifiers
					WordModel [ ] mods = null;
					if ( modifiers != null && modifiers.ContainsKey ( key ) && modifiers [ key ].Count > 0 )
					{
						mods = modifiers [ key ].ToArray ( );
					}

					// Display the line
					line.SetLine ( stanzas [ s ].Lines [ l ], mods );

					// Store line
					poemObjects.Add ( line.gameObject );
					lines.Add ( key, line );
				}

				// Check for another stanza
				if ( s + 1 < stanzas.Length )
				{
					// Create stanza break
					GameObject stanzaBreak = Instantiate ( stanzaPrefab, container );

					// Store stanza break
					poemObjects.Add ( stanzaBreak );
				}
			}
		}

		/// <summary>
		/// Displays all enhancements applied to the previewed poem.
		/// </summary>
		private void DisplayEnhancements ( )
		{
			// Check for enhancements
			if ( model.Level > 0 ||
				 model.Confidence > 0 ||
				 model.Arrogance > 0 ||
				 model.TimeAllowance > 0f ||
				 model.Reputation > 0 ||
				 model.Applause > 0 ||
				 model.Commission > 0 )
			{
				// Display enhancements
				enhancementsContainer.SetActive ( true );

				// Display if in permanent draft
				draftText.SetActive ( model.Level > 0 );

				// Display additional snaps earned
				snapsText.gameObject.SetActive ( model.Level > 1 );
				snapsText.text = $"\u2022<indent=1em>Characters earn <color=#A1740E>+{model.BaseSnaps - 1}</color> snaps";

				// Display additional confidence
				confidenceText.gameObject.SetActive ( model.Confidence > 0 );
				confidenceText.text = $"\u2022<indent=1em><color=blue>+{model.Confidence}</color> confidence";

				// Display additional arrogance
				arroganceText.gameObject.SetActive ( model.Arrogance > 0 );
				arroganceText.text = $"\u2022<indent=1em><color=purple>+{model.Arrogance}</color> arrogance";

				// Display additional time
				timeText.gameObject.SetActive ( model.TimeAllowance > 0f );
				timeText.text = $"\u2022<indent=1em><color=#FFE100>+{Utils.FormatTime ( model.TimeAllowance )}</color>";

				// Display additional reputation
				reputationText.gameObject.SetActive ( model.Reputation > 0 );
				reputationText.text = $"\u2022<indent=1em><color=#A1740E>+{model.Reputation}</color> reputation";

				// Display additional applause
				applauseText.gameObject.SetActive ( model.Applause > 0 );
				applauseText.text = $"\u2022<indent=1em><color=#A1740E>+{model.Applause}</color> applause";

				// Display additional commission
				commissionText.gameObject.SetActive ( model.Commission > 0 );
				commissionText.text = $"\u2022<indent=1em><color=green>+${model.Commission}</color> commission";
			}
			else
			{
				// Hide enhancements
				enhancementsContainer.SetActive ( false );
			}
		}

		/// <summary>
		/// Applies a consumable to the poem.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		private void OnApplyConsumable ( int index )
		{
			// Get consumable data
			Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID );

			// Check for consumable
			if ( consumable != null )
			{
				// Check consumable type
				if ( consumable.Type == Enums.ConsumableType.MODIFIER )
				{
					// Apply modifiers
					ApplyModifiers ( index, 1 );
				}
				else if ( consumable.Type == Enums.ConsumableType.ENHANCEMENT )
				{
					// Apply enhancements
					ApplyEnhancements ( index, 1 );
				}
			}
		}

		/// <summary>
		/// Applies all instances of a consumable to the poem.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		private void OnApplyAllConsumable ( int index )
		{
			// Get total instances
			int count = GameManager.Run.ConsumableData [ index ].Count;

			// Get consumable data
			Consumables.ConsumableScriptableObject consumable = Consumables.ConsumableUtility.GetConsumable ( GameManager.Run.ConsumableData [ index ].ID );

			// Check for consumable
			if ( consumable != null )
			{
				// Check consumable type
				if ( consumable.Type == Enums.ConsumableType.MODIFIER )
				{
					// Apply modifiers
					ApplyModifiers ( index, count );
				}
				else if ( consumable.Type == Enums.ConsumableType.ENHANCEMENT )
				{
					// Apply enhancements
					ApplyEnhancements ( index, count );
				}
			}
		}

		/// <summary>
		/// Applies performance enhancements from a consumable to the poem.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		/// <param name="instances"> The total number of instances of the consumable. </param>
		private void ApplyEnhancements ( int index, int instances )
		{
			// Get the enhancements from the consumable
			PoemModel addModel = GameManager.Run.ConsumableData [ index ].Consumable.OnEnhancePoem ( instances );

			// Update stats
			GameManager.Run.Stats.OnConsumeConsumable ( GameManager.Run.ConsumableData [ index ].ID, instances );

			// Remove instance of the consumable
			GameManager.Run.RemoveConsumableAtIndex ( index, instances );

			// Apply enhancements
			onApplyToPoem ( addModel );

			// Display updated enhancements
			DisplayEnhancements ( );
		}

		/// <summary>
		/// Applies word modifiers from a consumable to the poem.
		/// </summary>
		/// <param name="index"> The index of the consumable. </param>
		/// <param name="instances"> The total number of instances of the consumable. </param>
		private void ApplyModifiers ( int index, int instances )
		{
			// Get the word modifiers from the consumable
			Enums.WordModifierType [ ] newModifiers = GameManager.Run.ConsumableData [ index ].Consumable.OnModifyWords ( instances );

			// Update stats
			GameManager.Run.Stats.OnConsumeConsumable ( GameManager.Run.ConsumableData [ index ].ID, instances );

			// Remove instance of the consumable
			GameManager.Run.RemoveConsumableAtIndex ( index, instances );

			// Update consumables HUD
			//consumablesHUD.RefreshConsumables ( );

			// Apply modifiers
			AddModifiers ( model.ToStanzas ( ), model.Modifiers, newModifiers );
		}

		/// <summary>
		/// Applies new word modifiers to the poem.
		/// </summary>
		/// <param name="stanzas"> The data for each stanza of the poem. </param>
		/// <param name="oldModifiers"> The word modifiers already applied to the poem. </param>
		/// <param name="newModifiers"> The modifiers to be applied. </param>
		private void AddModifiers ( Stanza [ ] stanzas, WordModel [ ] oldModifiers, Enums.WordModifierType [ ] newModifiers )
		{
			// Get each existing modifier
			Dictionary<LineKey, List<WordModel>> wordModifiers = PoemHelper.GetModifiersByLine ( oldModifiers );
			if ( wordModifiers == null )
			{
				wordModifiers = new Dictionary<LineKey, List<WordModel>> ( );
			}

			// Store word modifiers
			List<WordModel> newWordModifiers = new List<WordModel> ( );

			// Get the keys for the available words from each line
			List<LineKey> keys = availableWords.Keys.ToList ( );

			// Apply each modifier to a random word
			for ( int i = 0; i < newModifiers.Length; i++ )
			{
				// Check for keys
				if ( keys.Count < 1 )
				{
					break;
				}

				// Get random line
				int keyIndex = 0;
				if ( keys.Count > 1 )
				{
					keyIndex = Random.Range ( 0, keys.Count );
				}
				LineKey key = keys [ keyIndex ];

				// Get a random word for the line
				WordModel word = availableWords [ key ] [ Random.Range ( 0, availableWords [ key ].Count ) ];

				// Remove word from availability
				availableWords [ key ].Remove ( word );
				if ( availableWords [ key ].Count < 1 )
				{
					availableWords.Remove ( key );
					keys.Remove ( key );
				}

				// Store word modifier
				word.Modifier = newModifiers [ i ];
				newWordModifiers.Add ( word );
				if ( !wordModifiers.ContainsKey ( key ) )
				{
					wordModifiers.Add ( key, new List<WordModel> ( ) );
				}
				wordModifiers [ key ].Add ( word );

				// Update stats
				GameManager.Run.Stats.OnApplyModifier ( word.Modifier );
			}

			// Get the keys for word modifiers from each line
			keys = wordModifiers.Keys.ToList ( );

			// Update each line
			for ( int i = 0; i < keys.Count; i++ )
			{
				// Get line
				LineKey key = keys [ i ];

				// Update the line
				lines [ key ].SetLine ( stanzas [ key.Stanza ].Lines [ key.Line ], wordModifiers [ key ].OrderBy ( x => x.StartIndex ).ToArray ( ) );
			}

			// Apply word modifiers to the poem
			PoemModel newModel = new PoemModel
			{
				Modifiers = newWordModifiers.ToArray ( )
			};
			onApplyToPoem ( newModel );
		}

		#endregion // Private Functions
	}
}