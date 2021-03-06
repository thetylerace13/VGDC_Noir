﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour {
    public bool detectsPlayer = false;
    public bool detectsShadow = false;
    public List<GameObject> obstacles;

    // Use this for initialization
    void Start()
    {
        obstacles = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	      
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            obstacles.Add(other.gameObject);
        } // Detect if a wall is in the way
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (GetComponentInParent<Transform>().rotation == Quaternion.Euler(0, 0, 0))
        {
            if (obstacles.Count > 0)
            {
                GameObject obstacle = GetFarthest(obstacles, true);

                if (other.transform.position.x > obstacle.transform.position.x)
                {
                    if ((other.CompareTag("PlayerCharacter") && Lighting.playerInLight))
                    {
                        detectsPlayer = true;
                    }
                    if (other.CompareTag("Shadow"))
                    {
                        detectsShadow = true;
                    }
                }
            } // Don't detect player/shadow if no walls in way
            else
            {
                if ((other.CompareTag("PlayerCharacter") && Lighting.playerInLight))
                {
                    detectsPlayer = true;
                }
                if (other.CompareTag("Shadow"))
                {
                    detectsShadow = true;
                }
            } // Detect player/shadow
        }
        else
        {
            if (obstacles.Count > 0)
            {
                GameObject obstacle = GetFarthest(obstacles, false);

                if (other.transform.position.x < obstacle.transform.position.x)
                {
                    if ((other.CompareTag("PlayerCharacter") && Lighting.playerInLight))
                    {
                        detectsPlayer = true;
                    }
                    if (other.CompareTag("Shadow"))
                    {
                        detectsShadow = true;
                    }
                }
            } // Don't detect player/shadow if no walls in way
            else
            {
                if ((other.CompareTag("PlayerCharacter") && Lighting.playerInLight))
                {
                    detectsPlayer = true;
                }
                if (other.CompareTag("Shadow"))
                {
                    detectsShadow = true;
                }
            } // Detect player/shadow
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            detectsPlayer = false;
        }
        if (other.CompareTag("Shadow"))
        {
            detectsShadow = false;
        }
        if (other.CompareTag("Wall"))
        {
            obstacles.Remove(other.gameObject);
        }
    } // Lose sight of player

    GameObject GetFarthest(List<GameObject> objects, bool positive)
    {
        GameObject result = objects[0];

        foreach (GameObject obj in objects)
        {
            if ((positive && obj.transform.position.x > result.transform.position.x) ||
                (!positive && obj.transform.position.x < result.transform.position.x))
            {
                result = obj;
            }
        }

        return result;
    } // Get farthest wall in way of seeing player
}
