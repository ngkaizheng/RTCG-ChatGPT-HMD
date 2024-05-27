using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace Oculus.Interaction.Demo
{
    public class PolaroidCamera : MonoBehaviour, IHandGrabUseDelegate
    {
        [Header("Polaroid Camera")]
        [SerializeField]
        private GameObject photoPrefab = null;
        [SerializeField]
        private MeshRenderer screenRenderer = null;
        [SerializeField]
        private Transform spawnLocation = null;

        private Camera renderCamera = null;

        private void Awake()
        {
            renderCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            CreateRenderTexture();
            TurnOff();
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
        }

        private Photo CreatePhoto()
        {
            GameObject photoObject = Instantiate(photoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
            return photoObject.GetComponent<Photo>();
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
