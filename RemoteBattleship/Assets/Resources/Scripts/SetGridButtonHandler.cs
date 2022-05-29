using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGridButtonHandler : MonoBehaviour
{
    public GameObject cornerMarker;
    public GameObject textOutput;
    public GameObject boardSetup;
    public GameObject addShipButton;
    public Sprite gridSprite;
    public Sprite indicatorsSprite;


    // private int clicks = 0;

    public void buttonClicked(){
        // Create grid sprite
        var board = new GameObject();
        var spriteRenderer = board.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = gridSprite;

        // Position grid sprite
        board.transform.position = cornerMarker.transform.position;
        board.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        board.transform.Rotate(90, 0, 0);

        // Position grid indication
        var indicators = new GameObject();
        var indicatorsSpriteRenderer = indicators.AddComponent<SpriteRenderer>();
        indicatorsSpriteRenderer.sprite = indicatorsSprite;

        indicators.transform.position = cornerMarker.transform.position;
        indicators.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        indicators.transform.Rotate(90, 0, 0);

        // Configure the grid corners
        boardSetup.GetComponent<BoardSetupHandler>().topLeftCornerPosition = new Vector3(board.transform.position.x - spriteRenderer.bounds.extents.x, cornerMarker.transform.position.y, board.transform.position.z + spriteRenderer.bounds.extents.z);
        boardSetup.GetComponent<BoardSetupHandler>().bottomRightCornerPosition = new Vector3(board.transform.position.x + spriteRenderer.bounds.extents.x, cornerMarker.transform.position.y, board.transform.position.z - spriteRenderer.bounds.extents.z);

        // Disable this button and activate the add ship button
        addShipButton.SetActive(true);
        transform.gameObject.SetActive(false);

        textOutput.GetComponent<UnityEngine.UI.Text>().text = "Place the 1x2 Ship on:";

        // switch (clicks){
        //     case 0:
        //         // Instantiate North Buoy
        //         // Vector3 northMarkerPosition = cornerMarker.transform.position;
        //         // GameObject northBuoy = Instantiate(buoy, northMarkerPosition, Quaternion.identity);
        //         // northBuoy.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        //         // northBuoy.transform.Rotate(-90,0,-44.93f);

        //         // // Set the position
        //         // boardSetup.GetComponent<BoardSetupHandler>().northCornerPosition = northMarkerPosition;

        //         // textOutput.GetComponent<UnityEngine.UI.Text>().text = "Place a buoy in the right lower corner";
        //         var board = new GameObject ();
        //         var spriteRenderer = board.AddComponent<SpriteRenderer> ();
        //         spriteRenderer.sprite = gridSprite;

        //         board.transform.position = cornerMarker.transform.position;
        //         board.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        //         board.transform.Rotate(90,0,0);
        //         boardSetup.GetComponent<BoardSetupHandler>().northCornerPosition = new Vector3(board.transform.position.x - spriteRenderer.bounds.extents.x, 0, board.transform.position.z + spriteRenderer.bounds.extents.z);
        //         boardSetup.GetComponent<BoardSetupHandler>().southCornerPosition = new Vector3(board.transform.position.x + spriteRenderer.bounds.extents.x, 0, board.transform.position.z - spriteRenderer.bounds.extents.z);
                

        //         clicks++;
        //         break;
        //     case 1:
        //         // Instantiate South Buoy
        //         // Vector3 southMarkerPosition = cornerMarker.transform.position;
        //         // GameObject southBuoy = Instantiate(buoy, southMarkerPosition, Quaternion.identity);
        //         // southBuoy.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        //         // southBuoy.transform.Rotate(-90,0,-44.93f);

        //         // // Set the position
        //         // boardSetup.GetComponent<BoardSetupHandler>().southCornerPosition = southMarkerPosition;

        //         // textOutput.GetComponent<UnityEngine.UI.Text>().text = "Place a buoy in the right lower corner";
        //         clicks++;
        //         break;
        // }


    }

}
