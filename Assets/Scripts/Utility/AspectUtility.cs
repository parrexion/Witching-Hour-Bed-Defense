/// <summary>
/// Forces the screen to a desired aspect ratio, using letterboxing/pillarboxing as needed. 
/// Includes functions that return corrected screen values for Screen.width/height and Input.mousePosition.
/// 
/// http://wiki.unity3d.com/index.php?title=AspectRatioEnforcer
/// Author: Eric Haines (Eric5h5)
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;

public class AspectUtility : MonoBehaviour {

	private static float wantedAspectRatio;
	private static Camera cam;
	private static Camera backgroundCam;
	private static int width;
	private static int height;

	public static System.Action<float> onAspectUpdated;

	public const float TARGET_ASPECT_RATIO = 1.7777778f;
	private static float currentAspectRatio = 1f;


	private void Awake() {
		cam = GetComponent<Camera>();
		if (!cam) {
			cam = Camera.main;
		}
		if (!cam) {
			Debug.LogError("No camera available");
			return;
		}
		wantedAspectRatio = TARGET_ASPECT_RATIO;
		SetCamera();
	}

	private void LateUpdate() {
		if (Screen.width != width || Screen.height != height) {
			SetCamera();
		}
	}

	public static void SetCamera() {
		width = Screen.width;
		height = Screen.height;
		currentAspectRatio = (float)width / height;
		// If the current aspect ratio is already approximately equal to the desired aspect ratio,
		// use a full-screen Rect (in case it was set to something else previously)
		if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f) {
			cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
			if (backgroundCam) {
				Destroy(backgroundCam.gameObject);
			}
			return;
		}
		// Pillarbox
		if (currentAspectRatio > wantedAspectRatio) {
			float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
			cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
		}
		// Letterbox
		else {
			float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
			cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
		}
		if (!backgroundCam) {
			// Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
			backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
			backgroundCam.depth = int.MinValue;
			backgroundCam.clearFlags = CameraClearFlags.SolidColor;
			backgroundCam.backgroundColor = Color.black;
			backgroundCam.cullingMask = 0;
		}
		onAspectUpdated?.Invoke(currentAspectRatio);
	}

	public static int ScreenHeight {
		get {
			return (int)(Screen.height * cam.rect.height);
		}
	}

	public static int ScreenWidth {
		get {
			return (int)(Screen.width * cam.rect.width);
		}
	}

	public static int XOffset {
		get {
			return (int)(Screen.width * cam.rect.x);
		}
	}

	public static int YOffset {
		get {
			return (int)(Screen.height * cam.rect.y);
		}
	}

	public static Rect ScreenRect {
		get {
			return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
		}
	}

	public static Vector2 WorldToScreenSpace(Vector3 worldPosition) {
		return ToScreenSpace(cam.WorldToScreenPoint(worldPosition));
	}

	public static Vector2 ToScreenSpace(Vector2 normalScreenSpace) {
		normalScreenSpace.y -= (int)(cam.rect.y * Screen.height);
		normalScreenSpace.x -= (int)(cam.rect.x * Screen.width);
		return normalScreenSpace;
	}

	public static Vector3 MousePosition {
		get {
			Vector3 mousePos = Input.mousePosition;
			mousePos.y -= (int)(cam.rect.y * Screen.height);
			mousePos.x -= (int)(cam.rect.x * Screen.width);
			return mousePos;
		}
	}

	public static Vector2 GuiMousePosition {
		get {
			Vector2 mousePos = Event.current.mousePosition;
			mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
			mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
			return mousePos;
		}
	}
}