using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject cop;
    public GameObject robber;
    public GameObject money;
    public GameObject gem;
    public GameObject wayPoints;

    [SerializeField] private int copsToSpawn = 5;
    [SerializeField] private float moneySpawnInterval = 5f;
    [SerializeField] private float gemSpawnInterval = 15f;
    [SerializeField] private float playerHeight = 1f;

    private GameObject[] _floor;

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        
        _floor = GameObject.FindGameObjectsWithTag("Floor");
        if (_floor.Length == 0)
        {
            Debug.LogError("No floor objects found. Initialization failed.");
            yield break;
        }
        
        // Once _floor is initialized, start spawning objects
        SpawnPlayer();
        SpawnCops();
        StartCoroutine(SpawnMoney());
        StartCoroutine(SpawnGems());
    }

    private Vector3 FindFloorToSpawn()
    {
        var randomFloor = _floor[Random.Range(0, _floor.Length)];
        var randomPositionX = randomFloor.transform.localScale.x / 2;
        var randomPositionZ = randomFloor.transform.localScale.z / 2;

        // Generate a random position on the floor object
        return randomFloor.transform.position + new Vector3(
            Random.Range(-randomPositionX, randomPositionX),
            playerHeight, 
            Random.Range(-randomPositionZ, randomPositionZ)
        );
    }

    private void SpawnPlayer()
    {
        var playerPosition = new Vector3(0, playerHeight, 0);
        var playerInstance = Instantiate(robber, playerPosition, Quaternion.identity);

        var cameraFollow = Camera.main?.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.target = playerInstance.transform;
        }
        else
        {
            Debug.LogWarning("CameraFollow component not found on the main camera.");
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SpawnCops()
    {
        for (var i = 0; i < copsToSpawn; i++)
        {
            var wayPointPosition = FindFloorToSpawn();

            var newWayPoints = Instantiate(wayPoints, wayPointPosition, Quaternion.identity);
            var newCop = Instantiate(cop, wayPointPosition, Quaternion.identity);

            var aiEnemy = newCop.GetComponent<AIEnemy>();
            if (aiEnemy != null)
            {
                aiEnemy.AssignWaypoints(newWayPoints.transform);
            }
            else
            {
                Debug.LogWarning("AIEnemy component not found on the cop GameObject.");
            }
        }
    }

    private IEnumerator SpawnMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(moneySpawnInterval);
            var randomPosition = FindFloorToSpawn();
            Instantiate(money, randomPosition, Quaternion.identity);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private IEnumerator SpawnGems()
    {
        while (true)
        {
            yield return new WaitForSeconds(gemSpawnInterval);
            var randomPosition = FindFloorToSpawn();
            Instantiate(gem, randomPosition, Quaternion.Euler(-90, 0, 0));
        }
        // ReSharper disable IteratorNeverReturns
    }
}
