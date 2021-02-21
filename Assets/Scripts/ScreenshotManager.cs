using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenshotManager : MonoBehaviour
{
    private Camera screenshotCamera;
    private int resWidth = 1920;
    private int resHeight = 1080;

    private void Awake()
    {
        screenshotCamera = GetComponent<Camera>();

        if (screenshotCamera.targetTexture == null)
        {
            screenshotCamera.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = screenshotCamera.targetTexture.width;
            resHeight = screenshotCamera.targetTexture.height;
        }

        screenshotCamera.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (screenshotCamera.gameObject.activeInHierarchy)
        {
            Texture2D screenshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            screenshotCamera.Render();
            RenderTexture.active = screenshotCamera.targetTexture;

            screenshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] byteArray = screenshot.EncodeToPNG();

            int screenshotIndex = SceneManagement.index;

            string path = string.Format("{0}/Screenshots/SaveScreenshot {1}.png", Application.dataPath, screenshotIndex);
            System.IO.File.WriteAllBytes(path, byteArray);

            screenshotCamera.gameObject.SetActive(false);
        }
    }

    public void TakeScreenshot()
    {
        screenshotCamera.gameObject.SetActive(true);
    }
}
