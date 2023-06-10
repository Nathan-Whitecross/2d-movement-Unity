using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveJumpHoldInput(GameObject gameObject)
    {
        return Input.GetButton("Jump");
    }

    public override bool RetrieveDashInput(GameObject gameObject)
    {
        return Input.GetKey("left shift");
    }

    public override float RetrieveClimbInput(GameObject gameObject)
    {
        return Input.GetAxisRaw("Vertical");
    }
}
