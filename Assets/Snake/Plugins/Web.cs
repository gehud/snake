using System.Runtime.InteropServices;
using UnityEngine;

namespace Snake {
	public class Web : MonoBehaviour {
		[DllImport("__Internal")]
		private static extern void EnterFullscreenInternal();

		[DllImport("__Internal")]
		private static extern void LockPortraitOrientationInternal();

		private void Awake() {
#if !UNITY_EDITOR && UNITY_WEBGL
			if (Application.isMobilePlatform) {
				EnterFullscreenInternal();
				LockPortraitOrientationInternal();
			}
#endif
		}
	}
}