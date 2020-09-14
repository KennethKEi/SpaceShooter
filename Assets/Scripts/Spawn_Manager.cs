using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefabs;
    
    [SerializeField]
    private GameObject _powerUp;

    [SerializeField]
    private GameObject _speedBoostPowerup;

   [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUp_SpawnRoutine());
        StartCoroutine(SpeedBoost());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefabs, posToSpawn, Quaternion.identity);
            
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
        //yield return null; // wait 1 frame

    }

    IEnumerator PowerUp_SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 powerupspawnpos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_powerUp, powerupspawnpos, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpeedBoost()
    {
        while (_stopSpawning == false)
        {
            Vector3 speedboostpoweruppos = new Vector3(Random.Range(-8, 8f), 7, 0);
            Instantiate(_speedBoostPowerup, speedboostpoweruppos, Quaternion.identity);

           
            yield return new WaitForSeconds(5.0f);
                 
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
