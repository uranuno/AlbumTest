using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AlbumControl : MonoBehaviour {

	[SerializeField] Transform content;
	[SerializeField] GameObject photoPrefab;

	[SerializeField] StudioControl studio;

	void OnEnable () {

		if (!Directory.Exists(studio.dataPath)) return;

		DirectoryInfo dirInfo = new DirectoryInfo(studio.dataPath);

		int sw = Screen.width;
		int sh = Screen.height;
		float ar = (float)sw/(float)sh;

		foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
			if (fileInfo.Extension == ".png") {
				Texture2D tex = LoadPNG (fileInfo.FullName, sw, sh);
				GameObject photo = (GameObject)Instantiate(photoPrefab);
				photo.transform.SetParent (content, false);
				photo.GetComponentInChildren<RawImage>().texture = tex;
				photo.GetComponentInChildren<AspectRatioFitter>().aspectRatio = ar;
			}
		}
	}

	void OnDisable () {

		foreach (Transform photo in content) {
			RawImage photoImage = photo.GetComponentInChildren<RawImage>();
			if (photoImage != null) Destroy (photoImage.texture);
			Destroy (photo.gameObject);
		}
	}

	static Texture2D LoadPNG(string filePath, int width, int height) {
		
		Texture2D tex = null;
		byte[] fileData;
		
		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}
}
