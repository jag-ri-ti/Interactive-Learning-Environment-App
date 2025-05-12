using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class CameraCapture : MonoBehaviour
{
    public RawImage cameraPreview;
    public Button captureButton;

    private WebCamTexture webCam;
    private AspectRatioFitter aspectFitter;

    void Start()
    {
        webCam = new WebCamTexture();
        cameraPreview.texture = webCam;
        cameraPreview.material.mainTexture = webCam;
        webCam.Play();

        aspectFitter = cameraPreview.GetComponent<AspectRatioFitter>();
        aspectFitter.aspectRatio = (float)webCam.width / webCam.height;

        captureButton.onClick.AddListener(() => StartCoroutine(CaptureAndSend()));
    }

    IEnumerator CaptureAndSend()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCam.width, webCam.height);
        photo.SetPixels(webCam.GetPixels());
        photo.Apply();

        byte[] imageData = photo.EncodeToJPG();

        Debug.Log("Clicked");
        // Prepare API request
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "photo.jpg", "image/jpeg");

        using (UnityWebRequest www = UnityWebRequest.Post("https://duckie-2ccy.onrender.com/upload", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text.Trim().ToLower();
                Debug.Log("Detected part: " + result);

                switch (result)
                {
                    case "head":
                        SceneManager.LoadScene("HeadScene");
                        break;
                    case "hand":
                        SceneManager.LoadScene("HandScene");
                        break;
                    case "leg":
                        SceneManager.LoadScene("LegScene");
                        break;
                    default:
                        Debug.LogWarning("Unknown body part detected.");
                        break;
                }
            }
            else
            {
                Debug.LogError("Upload failed: " + www.error);
            }
        }

    }

    void Update()
    {
        if (webCam != null && webCam.width > 100)
        {
            // Set the aspect ratio
            float ratio = (float)webCam.width / webCam.height;
            aspectFitter.aspectRatio = ratio;

            // Adjust the rotation based on the video rotation angle
            int rotation = -webCam.videoRotationAngle;
            cameraPreview.rectTransform.localEulerAngles = new Vector3(0, 0, rotation);

            // Flip vertically if front-facing
            cameraPreview.rectTransform.localScale = new Vector3(
                1,
                webCam.videoVerticallyMirrored ? -1 : 1,
                1
            );
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }
    }
}
