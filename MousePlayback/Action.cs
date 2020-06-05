using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace MousePlayback
{
    /// <summary>
    /// Represents an action, a mouse move, a mouse click, pressing a key on your keyboard, etc.
    /// A playable MousePlayback Script will have many of these.
    /// </summary>
    [Serializable]
    public class Action
    {
        /// <summary>
        /// Creates a new action. This can be a mouse movement, click, keyboard press, etc. etc.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x">X Coördinate of the mouse cursor</param>
        /// <param name="y">Y Coördinate of the mouse cursor</param>
        /// <param name="scrollAmount">Amount of scroll actions to scroll. 0 if ActionType is not WHEEL</param>
        /// <param name="timestamp">The timestamp of this action</param>
        /// <param name="type">The type of action. See ActionType enumeration</param>
        /// <param name="key">The key to press for this action</param>
        /// <param name="modifiers">The modifiers (ctrl/alt/shift) to hold down during this action</param>
        public Action(int id, int? x, int? y, int scrollAmount, DateTime timestamp, ActionType type, VirtualKeyCode key = VirtualKeyCode.NONAME, KeyModifiers modifiers = new KeyModifiers())
        {
            ID = id;
            X = x;
            Y = y;
            TimeStamp = timestamp;            
            Type = type;
            Key = key;
            Modifiers = modifiers;
            ScrollAmount = scrollAmount;
        }       


        public int ID { get; }
        /// <summary>
        /// X Coördinate of the mouse cursor
        /// </summary>
        public int? X { get; }
        /// <summary>
        /// Y Coördinate of the mouse cursor
        /// </summary>
        public int? Y { get; }
        /// <summary>
        /// Amount of scroll actions to scroll. 0 if ActionType is not WHEEL
        /// </summary>
        public int ScrollAmount { get; }
        /// <summary>
        /// The timestamp of this action
        /// </summary>
        public DateTime TimeStamp { get; }
        /// <summary>
        /// The type of action. See ActionType enumeration
        /// </summary>
        public ActionType Type { get; }
        /// <summary>
        /// The key to press for this action
        /// </summary>
        public VirtualKeyCode Key { get; }
        /// <summary>
        /// The modifiers (ctrl/alt/shift) to hold down during this action
        /// </summary>
        public KeyModifiers Modifiers { get; set; }

        /// <summary>
        /// Determines wether this action is a left, right or middle mouse button click.
        /// </summary>
        public bool IsMouseClick
        {
            get { return Type == ActionType.MOUSE_LEFT_DOWN || Type == ActionType.MOUSE_LEFT_UP || Type == ActionType.MOUSE_RIGHT_DOWN || Type == ActionType.MOUSE_RIGHT_UP || Type == ActionType.MOUSE_MIDDLE_DOWN || Type == ActionType.MOUSE_MIDDLE_UP; }
        }

        /// <summary>
        /// Determines wether this action is pressing a key on the keyboard
        /// </summary>
        public bool IsKey
        {
            get { return Key != VirtualKeyCode.NONAME; }
        }


        /// <summary>
        /// What kind of action was this? right/left mouse up/down? key? wheel? etc
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// A mouse movement
            /// </summary>
            MOUSE_MOVE,
            /// <summary>
            /// A left mouse-click down
            /// </summary>
            MOUSE_LEFT_DOWN,
            /// <summary>
            /// A left mouse-click up
            /// </summary>
            MOUSE_LEFT_UP,
            /// <summary>
            /// A left right-click down
            /// </summary>
            MOUSE_RIGHT_DOWN,
            /// <summary>
            /// A left right-click up
            /// </summary>
            MOUSE_RIGHT_UP,
            /// <summary>
            /// A mouse wheel press down
            /// </summary>
            MOUSE_MIDDLE_DOWN,
            /// <summary>
            /// A mouse wheel press up
            /// </summary>
            MOUSE_MIDDLE_UP,
            /// <summary>
            /// An keyboard key_up event
            /// </summary>
            KEY_UP,
            /// <summary>
            /// An keyboard key_down event 
            /// </summary>
            KEY_DOWN,
            /// <summary>
            /// A mouse wheel scrolling action
            /// </summary>
            WHEEL
        }

        [Serializable]
        public struct KeyModifiers
        {            
            /// <summary>
            /// Determines if the Ctrl key should be hold down for this action
            /// </summary>
            public bool CtrlKeyDown { get; set; }
            /// <summary>
            /// Determines if the Alt key should be hold down for this action
            /// </summary>
            public bool AltKeyDown { get; set; }
            /// <summary>
            /// Determines if the Shift key should be hold down for this action
            /// </summary>
            public bool ShiftKeyDown { get; set; }
        }
    }
}
