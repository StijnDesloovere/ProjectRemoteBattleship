using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShipButtonHandler : MonoBehaviour
{

    public GameObject infoText;
    public BoardSetupHandler boardSetup;
    public Multiplayer multiplayer;

    // Ships
    public GameObject ship1x2;
    public GameObject ship1x3A;
    public GameObject ship1x3B;
    public GameObject ship1x4;
    public GameObject ship1x5;
    
    public GameObject readyButton;
    private int currentShip = 0;

    private (int, (int,char))[] shipPositions = new (int, (int,char))[17];

    // Save the position of the current ship and calculate the tiles is occupies
    public void buttonClicked()
    {
        (int, char) boardPosition = boardSetup.getCurrentShipPosition();
        switch(currentShip)
        {
            case 0: 
                // infoText.GetComponent<UnityEngine.UI.Text>().text = "Place the 1x3 Ship on:";
                shipPositions[0] = (1, boardPosition);
                float ship1x2Rotation = ship1x2.transform.localRotation.eulerAngles.y;
                switch(ship1x2Rotation){
                    case float n when (ship1x2Rotation >= 315 || ship1x2Rotation < 45):
                        shipPositions[1] = (1, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        break;
                    case float n when (ship1x2Rotation >= 45 && ship1x2Rotation < 135):
                        shipPositions[1] = (1, (boardPosition.Item1+1, boardPosition.Item2));
                        break;
                    case float n when (ship1x2Rotation >= 135 && ship1x2Rotation < 225):
                        shipPositions[1] = (1, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        break;
                    case float n when (ship1x2Rotation >= 255 && ship1x2Rotation < 315):
                        shipPositions[1] = (1, (boardPosition.Item1-1, boardPosition.Item2));
                        break;
                }
                boardSetup.nextShip();
                infoText.GetComponent<UnityEngine.UI.Text>().text = shipPositions[0] + ", " + shipPositions[1];
                break;
            case 1: 
                // infoText.GetComponent<UnityEngine.UI.Text>().text =  "Place the second 1x3 Ship on:";
                shipPositions[2] = (2,boardPosition);
                float ship1x3ARotation = ship1x3A.transform.localRotation.eulerAngles.y;
                switch(ship1x3ARotation){
                    case float n when (ship1x3ARotation >= 315 || ship1x3ARotation < 45):
                        shipPositions[3] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        shipPositions[4] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        break;
                    case float n when (ship1x3ARotation >= 45 && ship1x3ARotation < 135):
                        shipPositions[3] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        shipPositions[4] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        break;
                    case float n when (ship1x3ARotation >= 135 && ship1x3ARotation < 225):
                        shipPositions[3] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        shipPositions[4] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        break;
                    case float n when (ship1x3ARotation >= 255 && ship1x3ARotation < 315):
                        shipPositions[3] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        shipPositions[4] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        break;
                }
                infoText.GetComponent<UnityEngine.UI.Text>().text = shipPositions[2] + ", " + shipPositions[3] + ", " + shipPositions[4];
                boardSetup.nextShip();
                break;
            case 2: 
                // infoText.GetComponent<UnityEngine.UI.Text>().text = "Place the 1x4 Ship on:";
                shipPositions[5] = (2,boardPosition);
                float ship1x3BRotation = ship1x3B.transform.localRotation.eulerAngles.y;
                switch(ship1x3BRotation){
                    case float n when (ship1x3BRotation >= 315 || ship1x3BRotation < 45):
                        shipPositions[6] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        shipPositions[7] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        break;
                    case float n when (ship1x3BRotation >= 45 && ship1x3BRotation < 135):
                        shipPositions[6] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        shipPositions[7] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        break;
                    case float n when (ship1x3BRotation >= 135 && ship1x3BRotation < 225):
                        shipPositions[6] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        shipPositions[7] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        break;
                    case float n when (ship1x3BRotation >= 255 && ship1x3BRotation < 315):
                        shipPositions[6] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        shipPositions[7] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        break;
                }
                infoText.GetComponent<UnityEngine.UI.Text>().text = shipPositions[5] + ", " + shipPositions[6] + ", " + shipPositions[7];
                boardSetup.nextShip();
                break;
            case 3:
                // infoText.GetComponent<UnityEngine.UI.Text>().text = "Place the 1x5 Ship on:";
                shipPositions[8] = (2,boardPosition);
                float ship1x4Rotation = ship1x4.transform.localRotation.eulerAngles.y;
                switch(ship1x4Rotation){
                    case float n when (ship1x4Rotation >= 315 || ship1x4Rotation < 45):
                        shipPositions[9] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        shipPositions[10] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-2)));
                        shipPositions[11] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        break;
                    case float n when (ship1x4Rotation >= 45 && ship1x4Rotation < 135):
                        shipPositions[9] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        shipPositions[10]= (2, (boardPosition.Item1+2, boardPosition.Item2));
                        shipPositions[11] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        break;
                    case float n when (ship1x4Rotation >= 135 && ship1x4Rotation < 225):
                        shipPositions[9] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        shipPositions[10] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+2)));
                        shipPositions[11] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        break;
                    case float n when (ship1x4Rotation >= 255 && ship1x4Rotation < 315):
                        shipPositions[9] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        shipPositions[10] = (2, (boardPosition.Item1-2, boardPosition.Item2));
                        shipPositions[11] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        break;
                }
                infoText.GetComponent<UnityEngine.UI.Text>().text = shipPositions[8] + ", " + shipPositions[9] + ", " + shipPositions[10] + ", " + shipPositions[11];
                boardSetup.nextShip();
                break;
            case 4:
                //infoText.GetComponent<UnityEngine.UI.Text>().text = "";
                shipPositions[12] = (2,boardPosition);
                float ship1x5Rotation = ship1x5.transform.localRotation.eulerAngles.y;
                switch(ship1x5Rotation){
                    case float n when (ship1x5Rotation >= 315 || ship1x5Rotation < 45):
                        shipPositions[13] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        shipPositions[14] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-2)));
                        shipPositions[15] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        shipPositions[16] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+2)));
                        break;
                    case float n when (ship1x5Rotation >= 45 && ship1x5Rotation < 135):
                        shipPositions[13] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        shipPositions[14]= (2, (boardPosition.Item1+2, boardPosition.Item2));
                        shipPositions[15] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        shipPositions[16] = (2, (boardPosition.Item1-2, boardPosition.Item2));
                        break;
                    case float n when (ship1x5Rotation >= 135 && ship1x5Rotation < 225):
                        shipPositions[13] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+1)));
                        shipPositions[14] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2+2)));
                        shipPositions[15] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-1)));
                        shipPositions[16] = (2, (boardPosition.Item1, (char)((int)boardPosition.Item2-2)));
                        break;
                    case float n when (ship1x5Rotation >= 255 && ship1x5Rotation < 315):
                        shipPositions[13] = (2, (boardPosition.Item1-1, boardPosition.Item2));
                        shipPositions[14] = (2, (boardPosition.Item1-2, boardPosition.Item2));
                        shipPositions[15] = (2, (boardPosition.Item1+1, boardPosition.Item2));
                        shipPositions[16] = (2, (boardPosition.Item1+2, boardPosition.Item2));
                        break;
                }
                infoText.GetComponent<UnityEngine.UI.Text>().text = shipPositions[12] + ", " + shipPositions[13] + ", " + shipPositions[14] + ", " + shipPositions[15] + ", " + shipPositions[16];
                boardSetup.nextShip();
                multiplayer.initializeOccupiedTiles(shipPositions);

                readyButton.SetActive(true);
                transform.gameObject.SetActive(false);
                break;
        }

        currentShip++;
    }

}
