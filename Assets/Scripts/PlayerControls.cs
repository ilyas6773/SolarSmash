using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public GameObject selectedAsteroid;
    public float throwForce = 10f;
    public GameObject asteroidPrefab;
    public float scaleFactor = 1f;
    public Button bigMeteorButton;
    public Button smallMeteorButton;


    // Start is called before the first frame update
    void Start()
    {
        SetSelectedAsteroid(smallMeteorButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll around the planet
        if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            transform.RotateAround(Vector3.zero, Vector3.up, h);
            transform.RotateAround(Vector3.zero, Vector3.right, v);
        }

        // Throw selected asteroid
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Tapped on planet");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Raycast hit: " + hit.collider.name);

                // Instantiate new asteroid
                Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.up;
                GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                newAsteroid.transform.localScale *= scaleFactor;
                newAsteroid.GetComponent<MeshRenderer>().enabled = true;
                newAsteroid.GetComponent<Rigidbody>().useGravity = true;
                newAsteroid.GetComponent<Meteor>().asteroidName = selectedAsteroid.name;

                // Throw asteroid towards planet
                Vector3 direction = (hit.point - newAsteroid.transform.position).normalized;
                newAsteroid.GetComponent<Rigidbody>().AddForce(direction * throwForce);
            }
            else
            {
                //Debug.Log("Raycast missed");
            }
        }

    }

    public void SetSelectedAsteroid(GameObject asteroid)
    {
        selectedAsteroid = asteroid;

        // Update button appearance
        if (selectedAsteroid == bigMeteorButton.gameObject)
        {
            bigMeteorButton.GetComponent<Image>().color = Color.green;
            smallMeteorButton.GetComponent<Image>().color = Color.white;
        }
        else if (selectedAsteroid == smallMeteorButton.gameObject)
        {
            bigMeteorButton.GetComponent<Image>().color = Color.white;
            smallMeteorButton.GetComponent<Image>().color = Color.green;
        }
    }
}
