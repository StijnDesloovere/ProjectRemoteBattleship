using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHandler : MonoBehaviour
{
    public GameObject boardSetup;
    public GameObject textOutput;
    public int shipSize;
    private int framesPassed = 0;

    public (int,char) boardPosition = (0, 'A');

    public Vector3 getPosition()
    {
        Vector3 position = transform.position;
        if (shipSize == 2 || shipSize == 4)
        {
            float shipRotation = transform.localRotation.eulerAngles.y;
            switch (shipRotation)
            {
                case float n when (shipRotation >= 315 || shipRotation < 45):
                    position = position + new Vector3(0, 0, -0.001f);
                    break;
                case float n when (shipRotation >= 45 && shipRotation < 135):
                    position = position + new Vector3(-0.001f, 0, 0);
                    break;
                case float n when (shipRotation >= 135 && shipRotation < 225):
                    position = position + new Vector3(0, 0, 0.001f);
                    break;
                case float n when (shipRotation >= 255 && shipRotation < 315):
                    position = position + new Vector3(0.001f, 0, 0);
                    break;
            }
        }
        return position;
    }

}
