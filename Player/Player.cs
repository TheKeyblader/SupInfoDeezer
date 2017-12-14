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

    public unsafe class Player
    {
        internal IntPtr ThisHandle;
        public delegate void EventPlayerArgs(DZPlayerEvent dZPlayerEvent);
        public event EventPlayerArgs Event;
        private bool playing;
        public bool Playing => playing;

        private int volume;
        public int Volume => volume;
        public Player(Connect connect)
        {
            ThisHandle = dz_player_new(connect.ThisHandle);
            Activate();
            dz_player_set_event_cb(ThisHandle, EventPlayer);
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
            if(dz_player_set_output_volume(ThisHandle,IntPtr.Zero,IntPtr.Zero,volume)!= 0)
            {
                throw new Exception();
            }
            this.volume = volume;
        }
        public void Load(string url)
        {
            if (dz_player_load(ThisHandle, IntPtr.Zero, IntPtr.Zero, url) != 0) {
                throw new Exception();
            }
        }
        public void Play(DZPlayerCommand command,uint index)
        {
            if(dz_player_play(ThisHandle, IntPtr.Zero, IntPtr.Zero, (int)command,index)!= 0)
            { throw new Exception(); }
            playing = true;
            var lastState = NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_AWAYMODE_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }
        public void Pause()
        {
            if(dz_player_pause(ThisHandle,IntPtr.Zero,IntPtr.Zero)!= 0)
            {
                throw new Exception();
            }
            playing = false;

            var lastState = NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
        public void Resume()
        {
            if(dz_player_resume(ThisHandle,IntPtr.Zero,IntPtr.Zero)!= 0)
            {
                throw new Exception();
            }
            playing = true;
            var lastState = NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_AWAYMODE_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }
        public void Stop()
        {
            if(dz_player_stop(ThisHandle,IntPtr.Zero,IntPtr.Zero)!= 0)
            {
                throw new Exception();
            }

            var lastState = NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
        public void PlayAds()
        {
            if(dz_player_play_audioads(ThisHandle,IntPtr.Zero,IntPtr.Zero)!= 0)
            {
                throw new Exception();
            }
        }
        private void EventPlayer(IntPtr playerHandle, IntPtr eventHandle,IntPtr data)
        {
            Event?.Invoke((DZPlayerEvent)dz_player_event_get_type(eventHandle));
        }

        public void callback(IntPtr d, IntPtr data, int status, int result)
        {

        }

        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern IntPtr dz_player_new(IntPtr self);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_activate(IntPtr player, IntPtr supervisor);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_deactivate(IntPtr playerHandle, IntPtr cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_cache_next(IntPtr playerHandle, IntPtr cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string trackUrl);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_load(IntPtr playerHandle, IntPtr cb, IntPtr data, [MarshalAs(UnmanagedType.LPStr)]string tracklistData);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_pause(IntPtr playerHandle, IntPtr cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_play(IntPtr playerHandle, IntPtr cb, IntPtr data, int command, uint idx);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_set_output_volume(IntPtr playerHandle, IntPtr cb, IntPtr data, int volume);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_play_audioads(IntPtr playerHandle, IntPtr cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_stop(IntPtr playerHandle, IntPtr cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_resume(IntPtr playerHandle, IntPtr cb, IntPtr data);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_seek(IntPtr playerHandle, IntPtr cb, IntPtr data, uint pos);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_set_index_progress_cb(IntPtr player, dz_player_onindexprogress_cb cb, uint refreshTime);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_set_event_cb(IntPtr player, dz_player_onevent_cb cb);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_set_repeat_mode(IntPtr playerHandle, IntPtr cb, IntPtr data, int mode);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_enable_shuffle_mode(IntPtr playerHandle, IntPtr cb, IntPtr data, bool shuffle_mode);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_event_get_type(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern int dz_player_seek(IntPtr playerHandle, IntPtr cb, IntPtr data, int microseconds);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern IntPtr dz_player_event_track_selected_dzapiinfo(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern IntPtr dz_player_event_track_selected_next_track_dzapiinfo(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern bool dz_player_event_track_selected_is_preview(IntPtr eventHandle);
        [DllImport("libdeezer.x64.dll", CallingConvention = CallingConvention.Cdecl)] private unsafe static extern bool dz_player_event_get_queuelist_context(IntPtr playerEventHandle, ref int out_streaming_mode, ref int index);
    }
}
