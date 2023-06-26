using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    public GameObject explosionPrefab;
    public Slider hpSlider;

    void Start()
    {
        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = currentHP;

        if (currentHP <= 0)
        {
            // Instantiate explosion effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Hide planet
            GetComponent<Renderer>().enabled = false;

            // Reset planet after delay
            StartCoroutine(ResetPlanet(5f));
        }
    }

    IEnumerator ResetPlanet(float delay)
    {
        // Wait for delay
        yield return new WaitForSeconds(delay);

        // Reset HP
        currentHP = maxHP;
        hpSlider.value = currentHP;

        // Show planet
        GetComponent<Renderer>().enabled = true;
    }
}
