using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{

    public GameObject board;

    private int clicks = 0;
    public void onClick(){
        BoardSetupHandler boardSetupHandler = board.GetComponent<BoardSetupHandler>();

        switch(clicks){
            case 0:
                boardSetupHandler.highlightSquare((1,'A'), false);
                break;
            case 1:
                boardSetupHandler.highlightSquare((4,'C'),true);
                break;
            case 2:
                boardSetupHandler.highlightSquare((10,'J'),true);
                break;
        }

        clicks++;
    }
}
