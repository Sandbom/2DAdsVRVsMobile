using System;
using Adverty.Native;
using System.Runtime.InteropServices;

namespace Adverty.PlatformSpecific
{
    public class AndroidWebViewPlugin : IAndroidWebViewPlugin
    {
        #if !UNITY_EDITOR && UNITY_ANDROID
        [DllImport("glbridge")]
        private static extern IntPtr CreateWebView(IntPtr jniEnv, int listenerId, int width, int height, IntPtr surface, IntPtr surfaceTexture);

        [DllImport("glbridge")]
        private static extern void WebViewLoadUrl(IntPtr webView, string url);

        [DllImport("glbridge")]
        private static extern void WebViewLoadHtml(IntPtr webView, string html);

        [DllImport("glbridge")]
        private static extern void WebViewDestroy(IntPtr webView);

        [DllImport("glbridge")]
        private static extern void WebViewDraw(IntPtr webView);

        [DllImport("glbridge")]
        private static extern void WebViewSetOnPageLoadedMethod(IntPtr onPageLoadedMethod);

        [DllImport("glbridge")]
        private static extern void WebViewSetOnFrameAvailableMethod(IntPtr onFrameAvailableMethod);

        public IntPtr NativeCreate(IntPtr jniEnv, int listenerId, int width, int height, IntPtr surface, IntPtr surfaceTexture)
        {
            return CreateWebView(jniEnv, listenerId, width, height, surface, surfaceTexture);
        }

        public void NativeLoadUrl(IntPtr webView, string url)
        {
            WebViewLoadUrl(webView, url);
        }

        public void NativeLoadHtml(IntPtr webView, string html)
        {
            WebViewLoadHtml(webView, html);
        }

        public void NativeDestroy(IntPtr webView)
        {
            WebViewDestroy(webView);
        }

        public void NativeDraw(IntPtr webView)
        {
            WebViewDraw(webView);
        }

        public void NativeSetOnPageLoadedMethod(IntPtr onPageLoadedMethod)
        {
            WebViewSetOnPageLoadedMethod(onPageLoadedMethod);
        }

        public void NativeSetOnFrameAvailableMethod(IntPtr onFrameAvailableMethod)
        {
            WebViewSetOnFrameAvailableMethod(onFrameAvailableMethod);
        }

        #else
        public IntPtr NativeCreate(IntPtr jniEnv, int listenerId, int width, int height, IntPtr surface, IntPtr surfaceTexture)
        {
            throw new NotImplementedException();
        }

        public void NativeLoadUrl(IntPtr webView, string url)
        {
            throw new NotImplementedException();
        }

        public void NativeLoadHtml(IntPtr webView, string html)
        {
            throw new NotImplementedException();
        }

        public void NativeDestroy(IntPtr webView)
        {
            throw new NotImplementedException();
        }

        public void NativeDraw(IntPtr webView)
        {
            throw new NotImplementedException();
        }

        public void NativeSetOnPageLoadedMethod(IntPtr onPageLoadedMethod)
        {
            throw new NotImplementedException();
        }

        public void NativeSetOnFrameAvailableMethod(IntPtr onFrameAvailableMethod)
        {
            throw new NotImplementedException();
        }
        #endif
    }
}

