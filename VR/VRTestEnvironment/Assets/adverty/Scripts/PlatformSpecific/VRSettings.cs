namespace Adverty.PlatformSpecific
{
    public class VRSettings : IPlatformVRSettings
    {
        public string GetModel()
        {
#if (UNITY_5 || UNITY_2017_1) && ADVERTY_XR_SUPPORT
            return UnityEngine.VR.VRDevice.model;
#elif ADVERTY_XR_SUPPORT
            return UnityEngine.XR.XRDevice.model;
#else
            return string.Empty;
#endif
        }
    }
}