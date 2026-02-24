using System.Collections.Generic;

using ctr_wp7.ctr_commons;

namespace ctr_wp7.iframework.visual
{
    internal interface TouchDelegate
    {
        bool touchesBeganwithEvent(List<CTRTouchState> touches);

        bool touchesEndedwithEvent(List<CTRTouchState> touches);

        bool touchesMovedwithEvent(List<CTRTouchState> touches);

        bool touchesCancelledwithEvent(List<CTRTouchState> touches);

        bool backButtonPressed();

        bool menuButtonPressed();
    }
}
