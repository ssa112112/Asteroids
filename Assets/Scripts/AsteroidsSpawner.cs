using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidsSpawner : MonoBehaviour
{
    //spawn it
    [SerializeField]
    GameObject AsteroidPrefab;

    private float newSpawnTime = 3.1f;
    private float FirstSpawnInCoroutine = 6f;


    //Support with spawn
    private float colliderHight;
    private float colliderWidth;

    void Start()
    {
        SaveColidersShape();

        Spawn(ScreenEdge.Bottom);
        Spawn(ScreenEdge.Left);
        Spawn(ScreenEdge.Top);
        Spawn(ScreenEdge.Right);

        //Eternal spawner
        StartCoroutine(Spawner(newSpawnTime,FirstSpawnInCoroutine));
    }
 
    /// <summary>
    /// Save the shape of an asteroid. Need to do it, before use "spawn"
    /// </summary>
    private void SaveColidersShape()
    {
        var currentObject = Instantiate(AsteroidPrefab);
        var currentScripteObject = currentObject.GetComponent<Asteroid>();
        colliderHight = currentScripteObject.GetColliderHight();
        colliderWidth = currentScripteObject.GetColliderWidth();
        Destroy(currentObject);
    }

    private void Spawn(ScreenEdge beyond)
    {
        Vector3 positionForInstaniate;
        Direction directionMove;

        //define a direction for move and a start position
        switch (beyond)
            {
            case ScreenEdge.Bottom:
                directionMove = Direction.Up;
                positionForInstaniate = new Vector3(0, ScreenUtils.ScreenBottom - 0.5f * colliderHight, 0);
                break;
            case ScreenEdge.Top:
                directionMove = Direction.Down;
                positionForInstaniate = new Vector3(0, ScreenUtils.ScreenTop + 0.5f * colliderHight, 0);
                break;
            case ScreenEdge.Left:
                directionMove = Direction.Right;
                positionForInstaniate = new Vector3(ScreenUtils.ScreenLeft - 0.5f * colliderWidth, 0, 0);
                break;
            case ScreenEdge.Right:
                directionMove = Direction.Left;
                positionForInstaniate = new Vector3(ScreenUtils.ScreenRight + 0.5f * colliderWidth, 0, 0);
                break;
            default:
                throw new KeyNotFoundException("Unknow ScreenEdge");
        }

        //Сreate new asteroid with desired direction and position
        var currentObject = Instantiate(AsteroidPrefab, positionForInstaniate, Quaternion.identity);
        var currentScripteObject = currentObject.GetComponent<Asteroid>();
        currentScripteObject.Initialize(directionMove);
    }

    /// <summary>
    /// Eternal spawner
    /// </summary>
    /// <param name="newSpawnTime"></param>
    /// <returns></returns>
    private IEnumerator Spawner (float newSpawnTime, float firstSpawn)
    {
        if (firstSpawn != 0f)
            yield return new WaitForSeconds(firstSpawn);
        while (true)
        {
            var randomeScreenEdge = (ScreenEdge)Random.Range(0, 3);
            Spawn(randomeScreenEdge);
            yield return new WaitForSeconds(newSpawnTime);
        }
    }
}
