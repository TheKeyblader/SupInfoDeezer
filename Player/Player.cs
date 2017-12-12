using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Player
{

    public static class DZPlayerIndex32
    {
        public static readonly Int32 INVALID = Int32.MaxValue;
        public static readonly Int32 PREVIOUS = Int32.MaxValue - 1;
        public static readonly Int32 CURRENT = Int32.MaxValue - 2;
        public static readonly Int32 NEXT = Int32.MaxValue - 3;
    };

    public static class DZPlayerIndex64
    {
        public static readonly Int64 INVALID = Int64.MaxValue;
        public static readonly Int64 PREVIOUS = Int64.MaxValue - 1;
        public static readonly Int64 CURRENT = Int64.MaxValue - 2;
        public static readonly Int64 NEXT = Int64.MaxValue - 3;
    };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void dz_player_onevent_cb(IntPtr playerHandle, IntPtr eventHandle, IntPtr data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void dz_player_onindexprogress_cb(IntPtr playerHandle, uint progress, IntPtr data);

    public class Player
    {
        internal IntPtr ThisHandle;
        public delegate void EventPlayerArgs(DZPlayerEvent dZPlayerEvent);
        public event EventPlayerArgs Event;
        public Player(Connect connect)
        {
            ThisHandle = dz_player_new(connect.ThisHandle);
            dz_player_set_event_cb(ThisHandle, EventPlayer);
            Activate();            
            SetVolume(100);
        }
        private void Activate()
        {
            if(dz_player_activate(ThisHandle,new IntPtr(this.GetHashCode()))!= 0)
            {
                throw new Exception();
            }
        }

        public void SetVolume(int volume)
        {
            if(dz_player_set_output_volume(ThisHandle,null,IntPtr.Zero,volume)!= 0)
            {
                throw new Exception();
            }
        }
        public void Load(string url)
        {
            if (dz_player_load(ThisHandle, null, IntPtr.Zero, url) != 0) {
                throw new Exception();
            }
        }
        public void Play(DZPlayerCommand command,uint index)
        {
            if(dz_player_play(ThisHandle, null, IntPtr.Zero, (int)command,index)!= 0)
            { throw new Exception(); }
        }
        public void PlayAds()
        {
            if(dz_player_play_audioads(ThisHandle,null,IntPtr.Zero)!= 0)
            {
                throw new Exception();
            }
        }
        private void EventPlayer(IntPtr playerHandle, IntPtr eventHandle,IntPtr data)
        {
            Event?.Invoke((DZPlayerEvent)dz_player_event_get_type(eventHandle));
        }

        [DllImport("libdeezer.x64.dll",CallingConvention = CallingConvention.Cdecl)] private static extern IntPtr dz_player_new(IntPtr self);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_activate(IntPtr player, IntPtr supervisor);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_deactivate(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_cache_next(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string trackUrl);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_load(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string tracklistData);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_pause(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_play(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, int command, uint idx);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_set_output_volume(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, int volume);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_play_audioads(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_stop(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_resume(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_seek(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, uint pos);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_set_index_progress_cb(IntPtr player, dz_player_onindexprogress_cb cb, uint refreshTime);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_set_event_cb(IntPtr player, dz_player_onevent_cb cb);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_set_repeat_mode(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, int mode);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_enable_shuffle_mode(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, bool shuffle_mode);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_event_get_type(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern int dz_player_seek(IntPtr playerHandle, dz_activity_operation_callback cb, IntPtr data, int microseconds);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern IntPtr dz_player_event_track_selected_dzapiinfo(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern IntPtr dz_player_event_track_selected_next_track_dzapiinfo(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern bool dz_player_event_track_selected_is_preview(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private static extern bool dz_player_event_get_queuelist_context(IntPtr playerEventHandle, ref int out_streaming_mode, ref int index);
    }
}
