using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Events;
using TextSpeech;
using System.Text.RegularExpressions;
using UnityEngine.Android;
using Photon.Pun;

public class BoardSetupHandler : MonoBehaviour
{
    public (int, (int, char))[] occupiedTiles;
    public GameObject currentShip;
    public GameObject ship1x3A;
    public GameObject ship1x3B;
    public GameObject ship1x4;
    public GameObject ship1x5;
    public GameObject textOutput;
    public Sprite indicatorSprite;

    public Sprite MissIndicatorSprite;

    // Corners
    public Vector3 topLeftCornerPosition = new Vector3(0f,0f,0f);
    public Vector3 bottomRightCornerPosition = new Vector3(0f,0f,0f);

    private char[] letters = {'A','B','C','D','E','F','G','H','I','J'};
    public (int, char) calculatePosition(Vector3 shipPosition)
    {

        //Calculate number position of the boat
        float boardWidth = Mathf.Abs(topLeftCornerPosition.x - bottomRightCornerPosition.x);
        float squareSize = boardWidth / 10;
        float distanceToCorner = Mathf.Abs(topLeftCornerPosition.x - shipPosition.x);

        int squareNumber = (int)Mathf.Ceil(distanceToCorner / squareSize);

        //Calculate letter position of the boat
        float boardHeight = Mathf.Abs(topLeftCornerPosition.z - bottomRightCornerPosition.z);
        squareSize = boardWidth / 10;
        distanceToCorner = Mathf.Abs(topLeftCornerPosition.z - shipPosition.z);

        int squareLetter = (int)Mathf.Floor(distanceToCorner / squareSize);

        if (squareLetter > 9)
        {
            squareLetter = 9;
        }
        else if (squareLetter < 0)
        {
            squareLetter = 0;
        }

        return (squareNumber, letters[squareLetter]);

    }

    public (int, char) getCurrentShipPosition(){
        return calculatePosition(currentShip.GetComponent<ShipHandler>().getPosition());
    }

    public void nextShip(){
        switch(currentShip.name){
            case "2x1Ship":
                currentShip = ship1x3A;
                break;
            case "3x1ShipA":
                currentShip = ship1x3B;
                break;
            case "3x1ShipB":
                currentShip = ship1x4;
                break;
            case "4x1Ship":
                currentShip = ship1x5;
                break;
            case "5x1Ship":
                break;
        }

    }

    public void highlightSquare((int,char) position, bool color){

        //Calculate coordinate of the square
        float boardWidth = Mathf.Abs(topLeftCornerPosition.x - bottomRightCornerPosition.x);
        float squareSize = boardWidth / 10;

        float xPosition = topLeftCornerPosition.x + ((position.Item1 - 1)* squareSize);
        float zPosition = 0;

        switch(position.Item2){
            case 'A':
                zPosition = topLeftCornerPosition.z;
                break;
            case 'B':
                zPosition = topLeftCornerPosition.z - squareSize;
                break;
            case 'C':
                zPosition = topLeftCornerPosition.z - (2* squareSize);
                break;
            case 'D':
                zPosition = topLeftCornerPosition.z - (3* squareSize);
                break;
            case 'E':
                zPosition = topLeftCornerPosition.z - (4* squareSize);
                break;
            case 'F':
                zPosition = topLeftCornerPosition.z - (5* squareSize);
                break;
            case 'G':
                zPosition = topLeftCornerPosition.z - (6* squareSize);
                break;
            case 'H':
                zPosition = topLeftCornerPosition.z - (7* squareSize);
                break;
            case 'I':
                zPosition = topLeftCornerPosition.z - (8* squareSize);
                break;
            case 'J':
                zPosition = topLeftCornerPosition.z - (9* squareSize);
                break;
            
        }

        var indicatorSquare = new GameObject();
        var spriteRenderer = indicatorSquare.AddComponent<SpriteRenderer>();
        if (color){
            spriteRenderer.sprite = indicatorSprite;
        } else {
            spriteRenderer.sprite = MissIndicatorSprite;
        }
        
        indicatorSquare.transform.position = new Vector3(xPosition + squareSize/2, topLeftCornerPosition.y - 0.001f, zPosition - squareSize/2);
        indicatorSquare.transform.localScale = new Vector3(0.0018f, 0.0018f, 0.0018f);
        indicatorSquare.transform.Rotate(90, 0, 0);
    }

    private int framesPassed = 0;
    // Update is called once per frame
    void Update()
    {
        // Wait a few frames between every update
        if(framesPassed < 10){
            framesPassed += 1;
        } else {
            textOutput.GetComponent<UnityEngine.UI.Text>().text = "Ship is at: " + getCurrentShipPosition();
            framesPassed = 0;
        }
    }

}
