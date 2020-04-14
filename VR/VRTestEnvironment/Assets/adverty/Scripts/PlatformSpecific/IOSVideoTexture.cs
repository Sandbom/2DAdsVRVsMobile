using System;
using System.Runtime.InteropServices;

namespace Adverty.PlatformSpecific
{
    public class IosVideoTexture : IIosVideoTexture
    {
#if !UNITY_EDITOR && UNITY_IOS

        [DllImport("__Internal")]
        private static extern IntPtr AdvertyVideoPlayerCreateTexture(IntPtr videoPlayer);

        [DllImport("__Internal")]
        private static extern IntPtr AdvertyVideoPlayerCreateVideoPlayer(int listenerId, string url);

        [DllImport("__Internal")]
        private static extern void AdvertyVideoPlayerPause(IntPtr videoPlayer);

        [DllImport("__Internal")]
        private static extern void AdvertyVideoPlayerPlay(IntPtr videoPlayer);

        [DllImport("__Internal")]
        private static extern int AdvertyVideoPlayerGetTextureWidth(IntPtr videoPlayer);

        [DllImport("__Internal")]
        private static extern int AdvertyVideoPlayerGetTextureHeight(IntPtr videoPlayer);

        [DllImport("__Internal")]
        private static extern void AdvertyVideoPlayerDestroy(IntPtr videoPlayer);

        public IntPtr NativeAdvertyVideoPlayerCreateTexture(IntPtr videoPlayer)
        {
            return AdvertyVideoPlayerCreateTexture(videoPlayer);
        }

        public IntPtr NativeAdvertyVideoPlayerCreateVideoPlayer(int listenerId, string url)
        {
            return AdvertyVideoPlayerCreateVideoPlayer(listenerId, url);
        }

        public void NativeAdvertyVideoPlayerPause(IntPtr videoPlayer)
        {
            AdvertyVideoPlayerPause(videoPlayer);
        }

        public void NativeAdvertyVideoPlayerPlay(IntPtr videoPlayer)
        {
            AdvertyVideoPlayerPlay(videoPlayer);
        }

        public int NativeAdvertyVideoPlayerGetTextureWidth(IntPtr videoPlayer)
        {
            return AdvertyVideoPlayerGetTextureWidth(videoPlayer);
        }

        public int NativeAdvertyVideoPlayerGetTextureHeight(IntPtr videoPlayer)
        {
            return AdvertyVideoPlayerGetTextureHeight(videoPlayer);
        }

        public void NativeAdvertyVideoPlayerDestroy(IntPtr videoPlayer)
        {
            AdvertyVideoPlayerDestroy(videoPlayer);
        }
#else
        public IntPtr NativeAdvertyVideoPlayerCreateTexture(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }

        public IntPtr NativeAdvertyVideoPlayerCreateVideoPlayer(int listenerId, string url)
        {
            throw new NotImplementedException();
        }

        public int CVideoPlayerPlugin_GetTextureId(IntPtr texture)
        {
            throw new NotImplementedException();
        }

        public void NativeAdvertyVideoPlayerPause(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }

        public void NativeAdvertyVideoPlayerPlay(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }

        public int NativeAdvertyVideoPlayerGetTextureWidth(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }

        public int NativeAdvertyVideoPlayerGetTextureHeight(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }

        public void NativeAdvertyVideoPlayerDestroy(IntPtr videoPlayer)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
