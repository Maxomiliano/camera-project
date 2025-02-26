using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Photographer : MonoBehaviour
{
    [SerializeField] private float batteryPerShot = 50f;
    public Camera _fotoCamera;
    public RenderTexture _renderTexture;
    public RawImage _photoPreview;
    public string screenshotDirectory = "D:/Max/Screenshots";

    public static Action<float> OnScreenshotTaken;

    private void Start()
    {
        if (!Directory.Exists(screenshotDirectory))
        {
            Directory.CreateDirectory(screenshotDirectory);
        }
    }

    public void TakeSnap()
    {
        StartCoroutine(Screenshot());
        StartCoroutine(HidePhotoPreview());
        OnScreenshotTaken?.Invoke(batteryPerShot);
    }

    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        //Asigna rendertexture a la camara
        _fotoCamera.targetTexture = _renderTexture;
        _fotoCamera.Render();

        //Crea una nueva textura y lee los pixeles desde la RenderTexture
        Texture2D texturePhoto = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = _renderTexture;
        texturePhoto.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
        texturePhoto.Apply();

        //Guardar la imagen en el disco
        byte[] bytes = texturePhoto.EncodeToPNG();
        string filename = screenshotDirectory + "/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        File.WriteAllBytes(filename, bytes);
        Debug.Log("Foto guardada en " + filename);

        //mostrar la imagen en la UI
        _photoPreview.gameObject.SetActive(true);
        _photoPreview.texture = texturePhoto;

        //limpieza
        RenderTexture.active = null;
        _fotoCamera.targetTexture = null;
    }

    private IEnumerator HidePhotoPreview()
    {
        yield return new WaitForSeconds(3f);
        _photoPreview.gameObject.SetActive(false);
    }
}
