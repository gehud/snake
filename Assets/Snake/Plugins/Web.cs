using System.Runtime.InteropServices;
using UnityEngine;

namespace Snake {
	public class Web : MonoBehaviour {
		public static bool IsMobile() {
			//#if !UNITY_EDITOR && UNITY_WEBGL
			//					return IsMobileInternal();
			//#else
			//			return false;
			//#endif
			return false;
		}

		//[DllImport("__Internal")]
		//private static extern bool IsMobileInternal();

		//[DllImport("__Internal")]
		//private static extern void EnterFullscreenInternal();

		//[DllImport("__Internal")]
		//private static extern void LockPortraitInternal();

		private void Awake() {
//#if !UNITY_EDITOR && UNITY_WEBGL
//		if (IsMobile()) {
//			EnterFullscreenInternal();
//			LockPortraitInternal();
//		}
//#endif
			if (Application.isMobilePlatform) {
				Screen.orientation = ScreenOrientation.Portrait;
			}
		}
	}
}