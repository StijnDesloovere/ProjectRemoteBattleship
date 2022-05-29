using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMarkerHandler : MonoBehaviour
{
    public GameObject cornerMarkerSouth;
    
    //Marker position
    private Vector3 markerPosition;
    private Vector3 southMarkerPosition;

    public GameObject textOutput;

    private int framesPassed = 0;

    private string[] letters = {"A","B","C","D","E","F","G","H","I","J"};

    public (int, string) calculatePosition(Vector3 shipPosition){
        if (cornerMarkerSouth != null)
        {
            float NorthMarkerX = markerPosition.x;
            float SouthMarkerX = southMarkerPosition.x;

            float NorthMarkerZ = markerPosition.z;
            float SouthMarkerZ = markerPosition.z;

            //Calculate number position of the boat
            float boardWidth = Mathf.Abs(NorthMarkerX - SouthMarkerX);
            float squareSize = boardWidth / 10;
            float distanceToCorner = Mathf.Abs(NorthMarkerX - shipPosition.x);

            int squareNumber = (int)Mathf.Floor(distanceToCorner / squareSize);

            //Calculate letter position of the boat
            float boardHeight = Mathf.Abs(NorthMarkerZ - SouthMarkerZ);
            squareSize = boardWidth / 10;
            distanceToCorner = Mathf.Abs(NorthMarkerZ - shipPosition.z);

            int squareLetter = (int)Mathf.Floor(distanceToCorner / squareSize);

            if(squareLetter > 9){
                squareLetter = 9;
            } else if (squareLetter < 0){
                squareLetter = 0;
            }

            return (squareNumber, letters[squareLetter]);
        } else {
            return(0,"A");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Wait a few frames between every update
        if(framesPassed < 10){
            framesPassed += 1;
        } else {
            markerPosition = transform.position;
            if(southMarkerPosition != null){
                southMarkerPosition = cornerMarkerSouth.transform.position;
            }
            framesPassed = 0;
        }
    }
}
