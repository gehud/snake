using UnityEngine;

namespace Snake.UI {
	public class ControlsDetector : MonoBehaviour {
		[SerializeField]
		private GameObject mobileControls;
		[SerializeField]
		private GameObject normalCameras;
		[SerializeField]
		private GameObject mobileCameras;

		private void Awake() {
			//#if !UNITY_EDITOR && UNITY_WEBGL
			//			if (Web.IsMobile()) {
			//				mobileControls.SetActive(true);
			//				normalCameras.SetActive(false);
			//				mobileCameras.SetActive(true);
			//			}
			//#endif
			if (Application.isMobilePlatform) {
				mobileControls.SetActive(true);
				normalCameras.SetActive(false);
				mobileCameras.SetActive(true);
			}
		}
	}
}