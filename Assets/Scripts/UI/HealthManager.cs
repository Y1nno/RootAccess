using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject fullHealth;
    

    public GameObject emptyHealth;

    public int maxHealth = 2;
    public int currentHealth = 1;


    private readonly List<GameObject> fullIcons = new List<GameObject>();
    private readonly List<GameObject> emptyIcons = new List<GameObject>();


    private void addIcons(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var icon = Instantiate(prefab, transform, false);

            if (prefab == fullHealth)
                fullIcons.Add(icon);
            else
                emptyIcons.Add(icon);
        }
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        int fullNeeded = currentHealth;
        int emptyNeeded = maxHealth - currentHealth;

        // Clear existing icons
        foreach (var icon in fullIcons)
            Destroy(icon);
        foreach (var icon in emptyIcons)
            Destroy(icon);

        fullIcons.Clear();
        emptyIcons.Clear();

        addIcons(fullHealth, fullNeeded);
        addIcons(emptyHealth, emptyNeeded);
    }

}
