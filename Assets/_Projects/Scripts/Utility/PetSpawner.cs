using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private List<PetObjectSO> petVariations = new List<PetObjectSO>();
    [SerializeField] private Counter_Checkin[] checkInCounters;
    [SerializeField] private float minSpawnInterval = 10f;
    [SerializeField] private float maxSpawnInterval = 20f;

    private void Start()
    {
        // Start the timer to periodically check and spawn pets.
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        InvokeRepeating("CheckAndSpawnPet", 0f, spawnInterval);
    }

    private void CheckAndSpawnPet()
    {
        if (GameManager.Instance != null)
        {
            // Check if the game is paused using the GameManager's isPaused state.
            if (!GameManager.Instance.IsPaused)
            {
                // Randomly select a pet variation from the available options.
                PetObjectSO selectedPet = petVariations[Random.Range(0, petVariations.Count)];

                // Loop through the check-in counters and check if any of them have a pet.
                foreach (var counter in checkInCounters)
                {
                    if (!counter.HasPetObject())
                    {
                        // If the counter doesn't have a pet, spawn a random pet variation.
                        counter.CheckInPet(selectedPet);
                        break; // Exit the loop after spawning one pet.
                    }
                }

                // Randomly choose the next spawn interval.
                float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                CancelInvoke("CheckAndSpawnPet"); // Cancel the previous InvokeRepeating.
                InvokeRepeating("CheckAndSpawnPet", spawnInterval, spawnInterval);
            }
        }
        else
        {
            Debug.LogWarning("GameManager has not been assigned.");
        }
        
    }
}
