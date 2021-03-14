using UnityEngine;

namespace WeappyTest
{
    public class InputWrapper
    {
        public static bool Left => Input.GetKey(KeyCode.LeftArrow);
        public static bool Right => Input.GetKey(KeyCode.RightArrow);
        public static bool Up => Input.GetKey(KeyCode.UpArrow);
        public static bool Down => Input.GetKey(KeyCode.DownArrow);
        public static bool Jump => Input.GetKey(KeyCode.Z);
        public static bool BeginJump => Input.GetKeyDown(KeyCode.Z);
        public static bool BeginGrab => Input.GetKeyDown(KeyCode.X);
    }
}
