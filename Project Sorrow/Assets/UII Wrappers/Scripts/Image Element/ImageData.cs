using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FlightPaper.Data
{
	/// <summary>
	/// This class stores the data for displaying an image.
	/// </summary>
	[System.Serializable]
	public class ImageData
	{
		#region Image Data

		[Tooltip("The directly loaded sprite for the image.")]
		[SerializeField]
		private Sprite sprite;

		[Tooltip("The loaded addressable for the image")]
		[SerializeField]
		private AssetReference addressable;

		private Sprite loadedAddressable;

		#endregion // Image Data

		#region Public Properties

		/// <summary>
		/// The directly loaded sprite for the image.
		/// </summary>
		public Sprite Sprite
		{
			get
			{
				return sprite;
			}
			set
			{
				sprite = value;
			}
		}

		/// <summary>
		/// The loaded addressable for the image.
		/// </summary>
		public AssetReference Addressable
		{
			get
			{
				return addressable;
			}
			set
			{
				addressable = value;
			}
		}

		#endregion // Public Properties

		#region Public Functions

		/// <summary>
		/// Gets whether or not this image data contains valid data.
		/// </summary>
		/// <returns> Whether or not this image data is valid. </returns>
		public bool IsValid ( )
		{
			return sprite != null || ( addressable != null && addressable.RuntimeKeyIsValid ( ) );
		}

		/// <summary>
		/// Gets the sprite value from this image data.
		/// </summary>
		/// <returns> The sprite from this image data. </returns>
		public Sprite LoadSprite ( )
		{
			// Check for valid data
			if ( !IsValid ( ) )
			{
				return null;
			}

			// Check for directly loaded sprite
			if ( sprite != null )
			{
				return sprite;
			}

			// Check if the sprite addressable is already loaded
			if ( loadedAddressable != null )
			{
				return loadedAddressable;
			}

			// Return that the sprite has not loaded yet
			return null;
		}

		/// <summary>
		/// Gets the sprite value from this image data asynchronously.
		/// </summary>
		/// <returns> The sprite from this image data. </returns>
		public async Task<Sprite> LoadSpriteAsync ( )
		{
			// Check for valid data
			if ( !IsValid ( ) )
			{
				return null;
			}

			// Check for directly loaded sprite
			if ( sprite != null )
			{
				return sprite;
			}

			// Check if the sprite addressable is already loaded
			if ( loadedAddressable != null )
			{
				return loadedAddressable;
			}

			// Check for addressable
			AssetReference spriteAddressable = addressable;
			if ( spriteAddressable != null && spriteAddressable.RuntimeKeyIsValid ( ) )
			{
				// Check if addressable is loaded internally
				if ( spriteAddressable.IsValid ( ) )
				{
					// Wait for the sprite to load
					AsyncOperationHandle<Sprite> handler = spriteAddressable.OperationHandle.Convert<Sprite> ( );
					await handler.Task;

					// Store the loaded sprite
					loadedAddressable = handler.Result;
				}
				else
				{
					// Wait for the sprite to load
					AsyncOperationHandle<Sprite> handler = spriteAddressable.LoadAssetAsync<Sprite> ( );
					await handler.Task;

					// Check for successful load
					if ( handler.Status == AsyncOperationStatus.Succeeded )
					{
						// Store the loaded sprite
						loadedAddressable = handler.Result;
					}
				}
			}

			// Return the loaded sprite
			return loadedAddressable;
		}

		#endregion // Public Functions
	}
}