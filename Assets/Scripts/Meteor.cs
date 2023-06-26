using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosionPrefab;
    public string asteroidName;

    void OnCollisionEnter(Collision collision)
    {

        // Damage planet
        Planet planet = collision.gameObject.GetComponent<Planet>();
        if (planet != null)
        {

            int damage = (asteroidName == "BigMeteor_Button") ? 2 : 1;
            planet.TakeDamage(damage);

            // Get collision point
            ContactPoint contact = collision.contacts[0];
            Vector3 collisionPoint = contact.point;

            // Use Raycast to get texture coordinate at collision point
            RaycastHit hit;
            if (Physics.Raycast(collisionPoint, Vector3.down, out hit))
            {
                Vector2 textureCoord = hit.textureCoord;

                // Modify texture of planet at textureCoord to show hit mark
                // Get the planet's mesh renderer
                MeshRenderer meshRenderer = planet.GetComponent<MeshRenderer>();

                // Get the planet's material
                Material material = meshRenderer.material;

                // Get the planet's main texture
                Texture2D mainTexture = (Texture2D)material.mainTexture;

                // Create a new texture with the hit mark
                Texture2D hitMarkTexture = new Texture2D(mainTexture.width, mainTexture.height);
                // Define hit mark shape and size
                int hitMarkWidth = 10;
                int hitMarkHeight = 10;

                // Define hit mark color
                Color hitMarkColor = Color.red;

                // Add hit mark to hitMarkTexture
                for (int x = 0; x < hitMarkWidth; x++)
                {
                    for (int y = 0; y < hitMarkHeight; y++)
                    {
                        hitMarkTexture.SetPixel(x, y, hitMarkColor);
                    }
                }

                // Apply changes to hitMarkTexture
                hitMarkTexture.Apply();



                // Create a new texture to store the blended texture
                Texture2D blendedTexture = new Texture2D(mainTexture.width, mainTexture.height);

                // Blend the main texture and hit mark texture at textureCoord
                Color[] mainTexturePixels = mainTexture.GetPixels();
                Color[] hitMarkTexturePixels = hitMarkTexture.GetPixels();
                for (int i = 0; i < mainTexturePixels.Length; i++)
                {
                    // Calculate the pixel coordinate
                    int x = i % mainTexture.width;
                    int y = i / mainTexture.width;

                    // Calculate hit mark position on texture
                    int hitMarkX = (int)(textureCoord.x * mainTexture.width) - hitMarkWidth / 2;
                    int hitMarkY = (int)(textureCoord.y * mainTexture.height) - hitMarkHeight / 2;

                    // Check if pixel is within hit mark area
                    if (x >= hitMarkX && x < hitMarkX + hitMarkWidth && y >= hitMarkY && y < hitMarkY + hitMarkHeight)
                    {
                        // Blend pixel from main texture and hit mark texture
                        Color blendedPixel = Color.Lerp(mainTexturePixels[i], hitMarkTexturePixels[i], 0.5f);
                        blendedTexture.SetPixel(x, y, blendedPixel);
                    }
                    else
                    {
                        // Use pixel from main texture
                        blendedTexture.SetPixel(x, y, mainTexturePixels[i]);
                    }
                }

                // Apply changes to blended texture
                blendedTexture.Apply();

                // Set the planet's main texture to the blended texture
                material.mainTexture = blendedTexture;

            }
        }

        // Instantiate explosion effect
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destroy meteor
        Destroy(gameObject);
    }


}

