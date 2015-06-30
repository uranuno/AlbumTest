using UnityEngine;
using System.Collections;
using System.IO;

public class StudioControl : MonoBehaviour {

	public string folderName = "Capture";

	[SerializeField] GameObject buttons;
	[SerializeField] GameObject album;

	public string dataPath {
		get { return Application.persistentDataPath + "/" + folderName; }
	}

	public void Capture () {

		StartCoroutine (CaptureRoutine ());
	}

	public IEnumerator CaptureRoutine () {

		buttons.SetActive (false);

		Debug.Log (string.Format(
			"dataPath:\n{0}\nexists:{1}",
			dataPath,
			Directory.Exists(dataPath)
			));
		
		if (!Directory.Exists(dataPath)) {
			Debug.Log ("Directory Created!");
			Directory.CreateDirectory(dataPath);
		}
		
		string path = folderName;
		#if UNITY_EDITOR
		path = dataPath;
		#endif
		Application.CaptureScreenshot(path + "/" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss-fff") + ".png");

		yield return new WaitForSeconds (1f);

		string files = "Captured!\n";
		DirectoryInfo dirInfo = new DirectoryInfo(dataPath);
		foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
			files += fileInfo.Name + "\n";
		}
		Debug.Log (files);

		buttons.SetActive (true);
	}

	public void ShowAlbum (bool state) {

		album.SetActive (state);
	}
}
