using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Player
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Connect_Config
    {
        public string AppId;

        public string ProductId;
        public string ProductBuildId;

        public string UserProfilePath;

        public ConnectOnEventCb ConnectEventCb;

        public string AnonymousBlob;

        public AppCrashDelegate AppCrashDelegate;
    }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void dz_activity_operation_callback(IntPtr d, IntPtr data, int status, int result);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ConnectOnEventCb(IntPtr libcConnect, IntPtr libcConnectEvent, IntPtr userdata);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool AppCrashDelegate();

    public class Connect
    {
        internal IntPtr ThisHandle;

        public delegate void ConnectEventArgs(DZConnectionEvent dZConnectionEvent);
        public event ConnectEventArgs OnConnection;
        public Connect(Connect_Config config,string accesToken,bool offlineMode,ConnectEventArgs @event = null )
        {
            config.ConnectEventCb = OnEvent;
            if (@event != null)
            {
                OnConnection += @event;
            }
            ThisHandle = dz_connect_new(ref config);
            Activate();
            SetAccesToken(accesToken);
            SetOfflineMode(offlineMode);
            SetCachePath(config.UserProfilePath);
        }
        private void Activate()
        {
            if(dz_connect_activate(ThisHandle,new IntPtr(this.GetHashCode()))!= 0)
            {
                throw new Exception();
            }
        }
        public void SetAccesToken(string accesToken)
        {
            if(dz_connect_set_access_token(ThisHandle,null,IntPtr.Zero,accesToken)!= 0)
            {
                throw new Exception();
            }
        }
        public void SetOfflineMode(bool offlineMode)
        {
            if (dz_connect_offline_mode(ThisHandle, null, IntPtr.Zero, offlineMode) != 0)
            {
                throw new Exception();
            }
        }
        public void SetCachePath(string Path)
        {
            if (dz_connect_cache_path_set(ThisHandle, null, IntPtr.Zero, Path)!=0)
            {
                throw new Exception();
            }
        }

        private void OnEvent(IntPtr Connect, IntPtr ConnectEvent, IntPtr userdata)
        {
            OnConnection?.Invoke((DZConnectionEvent)dz_connect_event_get_type(ConnectEvent));
        }
        public int GetDeviceId()
        {
            return dz_connect_get_device_id(ThisHandle).ToInt32();
        }

        [DllImport("libdeezer.x64.dll",CallingConvention = CallingConvention.Cdecl)] static extern void dz_object_release(IntPtr objectHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern IntPtr dz_connect_new(ref Connect_Config config);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern IntPtr dz_connect_get_device_id(IntPtr self);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_activate(IntPtr self, IntPtr userdata);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_cache_path_set(IntPtr self, dz_activity_operation_callback cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string local_path);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_set_access_token(IntPtr self, dz_activity_operation_callback cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string token);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_offline_mode(IntPtr self, dz_activity_operation_callback cb, IntPtr data, bool offline_mode_forced);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_event_get_type(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] static extern int dz_connect_deactivate(IntPtr connectHandle, dz_activity_operation_callback cb, IntPtr data);
    }
}
