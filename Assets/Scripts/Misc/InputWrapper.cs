using UnityEngine;

namespace WeappyTest
{
    public class InputWrapper
    {
        public static bool Enabled { get; set; } = true;

        public static bool Left => Enabled ? Input.GetKey(KeyCode.LeftArrow) : false;
        public static bool Right => Enabled ? Input.GetKey(KeyCode.RightArrow) : false;
        public static bool Up => Enabled ? Input.GetKey(KeyCode.UpArrow) : false;
        public static bool Down => Enabled ? Input.GetKey(KeyCode.DownArrow) : false;
        public static bool Jump => Enabled ? Input.GetKey(KeyCode.Z) : false;
        public static bool BeginJump => Enabled ? Input.GetKeyDown(KeyCode.Z) : false;
        public static bool BeginGrab => Enabled ? Input.GetKeyDown(KeyCode.X) : false;

        public static bool BeginLeft => Enabled ? Input.GetKeyDown(KeyCode.LeftArrow) : false;
        public static bool BeginRight => Enabled ? Input.GetKeyDown(KeyCode.RightArrow) : false;

        public static bool BeginEnter => Enabled ? Input.GetKeyDown(KeyCode.Return) : false;
    }
}
