using UnityEngine;
using UnityEngine.Networking;


namespace Adverty.PlatformSpecific
{
    public class WebRequest : IPlatformWebRequest
    {
        #if UNITY_5 || UNITY_2017_1
        public AsyncOperation Send(UnityWebRequest request)
        {
            return request.Send();
        } 
        #else
        public AsyncOperation Send(UnityWebRequest request)
        {
            return request.SendWebRequest();
        }
        #endif

        #if UNITY_5 || UNITY_2017_1
        public bool IsNetworkError(UnityWebRequest request)
        {
            return request.isError && !IsHttpError(request);
        }
        #else
        public bool IsNetworkError(UnityWebRequest request)
        {
            return request.isNetworkError;
        }
        #endif

        #if UNITY_5
        public bool IsHttpError(UnityWebRequest request)
        {
            return request.responseCode >= 400;
        }
        #else
        public bool IsHttpError(UnityWebRequest request)
        {
            return request.isHttpError;
        }
        #endif


        #if UNITY_5
        public UnityWebRequest GetTexture(string uri, bool nonReadable)
        {
            return UnityWebRequest.GetTexture(uri, nonReadable);
        }
        #else
        public UnityWebRequest GetTexture(string uri, bool nonReadable)
        {
            return UnityWebRequestTexture.GetTexture(uri, nonReadable);
        }
        #endif
    }
}