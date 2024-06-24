using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace Oculus.Interaction.Demo
{
    public class PolaroidCamera : MonoBehaviour, IHandGrabUseDelegate
    {
        [Header("Polaroid Camera")]
        [SerializeField]

        public GameObject chatGPTObject; // Reference to the ChatGPT (Jason) GameObject
        public GameObject playerObject; // Reference to the player GameObject (assuming it's set elsewhere)

        public GameObject photoPrefab = null;
        public GameObject secretPhotoPrefab = null;
        [SerializeField]
        private MeshRenderer screenRenderer = null;
        [SerializeField]
        private Transform spawnLocation = null;

        private Camera renderCamera = null;

        public AudioClip audioClip;
        private AudioSource audioSource;

        private void Awake()
        {
            renderCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            CreateRenderTexture();
            TurnOff();
            audioSource = GetComponent<AudioSource>();
        }

        private void CreateRenderTexture()
        {
            RenderTexture newTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
            newTexture.antiAliasing = 4;

            renderCamera.targetTexture = newTexture;
            screenRenderer.material.mainTexture = newTexture;
        }

        public void TakePhoto()
        {
            //Check if last child is Tag "CameraPhoto"
            if (transform.childCount > 0)
            {
                if (transform.GetChild(transform.childCount - 1).tag == "CameraPhoto")
                {
                    return;
                }
            }
            Photo newPhoto = CreatePhoto();
            SetPhotoImage(newPhoto);
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        // private bool IsPlayerAndChatGPTInView()
        // {
        //     // Example: Check if both player and ChatGPT (Jason) are within camera's view
        //     if (playerObject != null && chatGPTObject != null)
        //     {
        //         Vector3 playerScreenPoint = renderCamera.WorldToViewportPoint(playerObject.transform.position);
        //         Vector3 chatGPTScreenPoint = renderCamera.WorldToViewportPoint(chatGPTObject.transform.position);

        //         // Check if both are in front of the camera and within the viewport
        //         if (playerScreenPoint.z > 0 && chatGPTScreenPoint.z > 0 &&
        //             playerScreenPoint.x > 0 && playerScreenPoint.x < 1 &&
        //             playerScreenPoint.y > 0 && playerScreenPoint.y < 1 &&
        //             chatGPTScreenPoint.x > 0 && chatGPTScreenPoint.x < 1 &&
        //             chatGPTScreenPoint.y > 0 && chatGPTScreenPoint.y < 1)
        //         {
        //             return true;
        //         }
        //     }

        //     return false;
        // }
        private bool IsPlayerAndChatGPTInView()
        {
            // Example: Check if both player and ChatGPT (Jason) are within camera's view
            if (playerObject != null && chatGPTObject != null)
            {
                Vector3 playerScreenPoint = renderCamera.WorldToViewportPoint(playerObject.transform.position);
                Vector3 chatGPTScreenPoint = renderCamera.WorldToViewportPoint(chatGPTObject.transform.position);

                // Check if player is in view
                if (playerScreenPoint.z > 0 && playerScreenPoint.x > 0 && playerScreenPoint.x < 1 &&
                    playerScreenPoint.y > 0 && playerScreenPoint.y < 1)
                {
                    Debug.Log("Player is in view.");
                }
                else
                {
                    Debug.Log("Player is not in view.");
                }

                // Check if ChatGPT (Jason) is in view
                if (chatGPTScreenPoint.z > 0 && chatGPTScreenPoint.x > 0 && chatGPTScreenPoint.x < 1 &&
                    chatGPTScreenPoint.y > 0 && chatGPTScreenPoint.y < 1)
                {
                    Debug.Log("ChatGPT (Jason) is in view.");
                }
                else
                {
                    Debug.Log("ChatGPT (Jason) is not in view.");
                }

                // Return true if both are in view
                return playerScreenPoint.z > 0 && chatGPTScreenPoint.z > 0 &&
                    playerScreenPoint.x > 0 && playerScreenPoint.x < 1 &&
                    playerScreenPoint.y > 0 && playerScreenPoint.y < 1 &&
                    chatGPTScreenPoint.x > 0 && chatGPTScreenPoint.x < 1 &&
                    chatGPTScreenPoint.y > 0 && chatGPTScreenPoint.y < 1;
            }

            return false;
        }


        private Photo CreatePhoto()
        {
            if (IsPlayerAndChatGPTInView())
            {
                GameObject secretPhotoObject = Instantiate(secretPhotoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
                return secretPhotoObject.GetComponent<Photo>();
            }
            else
            {
                GameObject photoObject = Instantiate(photoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
                return photoObject.GetComponent<Photo>();
            }
        }

        private void SetPhotoImage(Photo photo)
        {
            Texture2D newTexture = RenderCameraToTexture(renderCamera);
            photo.SetImage(newTexture);
        }

        private Texture2D RenderCameraToTexture(Camera camera)
        {
            camera.Render();
            RenderTexture.active = camera.targetTexture;

            Texture2D photo = new Texture2D(256, 256, TextureFormat.RGB24, false);
            photo.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            photo.Apply();

            return photo;
        }

        public void TurnOn()
        {
            renderCamera.enabled = true;
            screenRenderer.material.color = Color.white;
        }

        public void TurnOff()
        {
            renderCamera.enabled = false;
            screenRenderer.material.color = Color.black;
        }

        #region input

        public void BeginUse()
        {
            TakePhoto();
        }

        public void EndUse()
        {
            // Do nothing for now
        }

        public float ComputeUseStrength(float strength)
        {
            // No strength computation needed, always return 1
            return 1f;
        }

        #endregion
    }
}
