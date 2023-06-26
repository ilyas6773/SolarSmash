using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSelector : MonoBehaviour
{
    public GameObject bigAsteroid;
    public GameObject smallAsteroid;
    public PlayerControls playerControls;
    public float bigScaleFactor = 2f;
    public float smallScaleFactor = 0.5f;

    public void SelectAsteroid(string asteroidName)
    {
        if (asteroidName == "AsteroidBig")
        {
            playerControls.SetSelectedAsteroid(bigAsteroid);
            playerControls.scaleFactor = bigScaleFactor;
        }
        else if (asteroidName == "AsteroidSmall")
        {
            playerControls.SetSelectedAsteroid(smallAsteroid);
            playerControls.scaleFactor = smallScaleFactor;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
