using System;
using System.Collections.Generic;
using System.Text;

namespace Player
{
    public enum DZConnectionEvent
    {
        UNKNOWN,
        USER_OFFLINE_AVAILABLE,
        USER_ACCESS_TOKEN_OK,
        USER_ACCESS_TOKEN_FAILED,
        USER_LOGIN_OK,
        USER_LOGIN_FAIL_NETWORK_ERROR,
        USER_LOGIN_FAIL_BAD_CREDENTIALS,
        USER_LOGIN_FAIL_USER_INFO,
        USER_LOGIN_FAIL_OFFLINE_MODE,
        USER_NEW_OPTIONS,
        ADVERTISEMENT_START,
        ADVERTISEMENT_STOP
    };

    public enum DZConnectStreamingMode
    {
        UNKNOWN,
        ONEDEMAND,
        RADIO
    };

    public enum DZPlayerEvent
    {
        UNKNOWN,
        LIMITATION_FORCED_PAUSE,
        QUEUELIST_LOADED,
        QUEUELIST_NO_RIGHT,
        QUEUELIST_TRACK_NOT_AVAILABLE_OFFLINE,
        QUEUELIST_TRACK_RIGHTS_AFTER_AUDIOADS,
        QUEUELIST_SKIP_NO_RIGHT,
        QUEUELIST_TRACK_SELECTED,
        QUEUELIST_NEED_NATURAL_NEXT,
        MEDIASTREAM_DATA_READY,
        MEDIASTREAM_DATA_READY_AFTER_SEEK,
        RENDER_TRACK_START_FAILURE,
        RENDER_TRACK_START,
        RENDER_TRACK_END,
        RENDER_TRACK_PAUSED,
        RENDER_TRACK_SEEKING,
        RENDER_TRACK_UNDERFLOW,
        RENDER_TRACK_RESUMED,
        RENDER_TRACK_REMOVED
    };

    public enum DZPlayerCommand
    {
        UNKNOWN,
        START_TRACKLIST,
        JUMP_IN_TRACKLIST,
        NEXT,
        PREV,
        DISLIKE,
        NATURAL_END,
        RESUMED_AFTER_ADS,
    };

    public enum DZPlayerRepeatMode
    {
        OFF,
        ONE,
        ALL
    };
}
