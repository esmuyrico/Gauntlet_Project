using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Created 4/11
// Handles the movement and actions of a player

public class PlayerController : MonoBehaviour
{
    private float speed = 2;
    private float moveDist = 0.5f;

    private bool moving = false;
    private Vector3 moveForward = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Move(Vector3 direction)
    {
        Transform objectInWay = DetectInDirection(direction);
        if (objectInWay == null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + direction * moveDist;
            moving = true;
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(startPos, endPos);

            float percentComplete = 0;
            while (percentComplete < 1)
            {
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - startTime) * speed;
                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                percentComplete = fractionOfJourney;
                yield return null;
            }

            moving = false;
        }
    }

    
    private Transform DetectInDirection(Vector3 direction)
    {
        float dist = 1.1f;
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction, Color.blue, 5);
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            Debug.Log(hit.transform.name);
            return hit.transform;
        }
        return null;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("forwards") && !moving)
        {
            StartCoroutine(Move(moveForward));
        }
        if (GUILayout.Button("Left"))
        {
            StartCoroutine(Move(Vector3.left));
        }
        if (GUILayout.Button("right"))
        {
            StartCoroutine(Move(Vector3.right));
        }
        if (GUILayout.Button("back"))
        {
            StartCoroutine(Move(Vector3.back));
        }
    }
}
