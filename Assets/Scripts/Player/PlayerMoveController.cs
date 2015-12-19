using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveController : NetworkBehaviour
{
    [SerializeField]
    float Speed = 30;

    [SerializeField]
    Transform LookTransform = null;
    [SerializeField]
    Rigidbody Rigidbody = null;

    public enum PlayerCommand
    {
        Vertical, Horizontal, Fire, Reload
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            CmdInput(PlayerCommand.Horizontal, horizontal);
        }


        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            CmdInput(PlayerCommand.Vertical, vertical);
        }

        float fire = Input.GetAxis("Fire1");
        if (fire != 0)
        {
            CmdInput(PlayerCommand.Fire, fire);
        }
    }

    [Command]
    public void CmdInput(PlayerCommand cmd, float value)
    {
        switch(cmd)
        {
            case PlayerCommand.Fire:
                //Debug.Log("Fire! " + value);
                break;

            case PlayerCommand.Horizontal:
                //Debug.Log("hor " + value);
                Rigidbody.AddForce(new Vector3(Speed * value, 0, 0));
                break;

            case PlayerCommand.Vertical:
                //Debug.Log("ver " + value);
                Rigidbody.AddForce(new Vector3(0, 0, Speed * value));
                break;
        }
    }
}
