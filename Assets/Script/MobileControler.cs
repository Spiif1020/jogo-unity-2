using UnityEngine;

public class MobileControler : MonoBehaviour
{
 public static bool leftHeld;
    public static bool rightHeld;
    public static bool jumpPressed;

    public void LeftDown()  { leftHeld = true; }
    public void LeftUp()    { leftHeld = false; }

    public void RightDown() { rightHeld = true; }
    public void RightUp()   { rightHeld = false; }

    public void JumpDown()  { jumpPressed = true; }
    public void JumpUp()    { jumpPressed = false; }
}
