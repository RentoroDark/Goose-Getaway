using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

public class Regenerate: MonoBehaviour 
{
    [SerializeField] GameObject[] prefabArray;
    [SerializeField] GameObject[] tunelsArray;
    [SerializeField] GameObject[] coins;
    [SerializeField] GameObject[] riftRouts;
    [SerializeField] GameObject[] boosts;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] PlayerData playerData;
    private ObjectPool<GameObject> tunnelPool;
    private ObjectPool<GameObject> coinPool;
    private ObjectPool<GameObject> wallPool;
    private ObjectPool<GameObject> stampPool;
    private ObjectPool<GameObject> trapPool;
    private GameObject objectBuffer;
    private int?[] cellTaken = new int?[21];
    private List<GameObject> buffer;
    public int lastPlatform = 0;
    private List<int> tunnelShuffle = new() { 0, 1, 2};
    private int platformCounter = 0;
    private Vector3 displacement;
    
    void Start() 
    {
        Array.Fill(cellTaken, null);
        GroundController.OnRegeneration += HandleRegeneration;
        StartCoroutine("InitializePools");
    }
    void OnDestroy()
    {
        GroundController.OnRegeneration -= HandleRegeneration;
    }
    void HandleRegeneration(object currentObject, GameObject manipulatedGameObject)
    {
        platformCounter += 1;
        Array.Fill(cellTaken, null);
        var script  = manipulatedGameObject.GetComponent<GroundController>();
        foreach (GameObject curObj in script.obstacleBufer)
        {
            
            StartCoroutine("ReturnToPool", curObj);
        }
        script.obstacleBufer.Clear();
        foreach (Transform child in manipulatedGameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        
        if (platformCounter == 20)
        {
            StartCoroutine(RiftGeneration(script));
            playerMoney.goldMultiplier += 1;
            platformCounter = 0;
            lastPlatform = 0;
        }
        else
        {
            switch (Random.Range(0, 10))
            {
                case 0:
                {
                    StartCoroutine(RandomGeneration(script));
                    break;
                }
                case 1 when lastPlatform != 1:
                {
                    StartCoroutine(XShape(script));
                    lastPlatform = 1;
                    break;
                }
                case 3 when lastPlatform != 3:
                {
                    StartCoroutine(TunelGeneration(script));
                    lastPlatform = 3;
                    break;
                }
                default:
                {
                    StartCoroutine(RandomGeneration(script));
                    lastPlatform = 0;
                    break;
                }
            }
        }
        
        script.gameObject.transform.position = script.previousPlatform.transform.position;
        
        
    }

    IEnumerator RandomGeneration(GroundController script)
    {
        yield return 0;
        script.platforTypes[0].SetActive(true);
        if (Random.Range(0, 10) == 0) //creats random tunnel
        {
            var cell = Random.Range(9, 12);
            script.obstacleBufer.Add(Instantiate(tunelsArray[Random.Range(0, 3)], script.placeHolders[cell].transform));
            for (int i = cell % 3; i < 21; i += 3)
            {
                cellTaken[i] = 2;
            }
        }
        for (int i = 0; i < Random.Range(5, 15); ++i)
        {
            var prefab = Random.Range(0, 10);
            var currentCell = Random.Range(3, script.placeHolders.Length - 3);
            if(cellTaken[currentCell] == null)
            {
                if (prefab < 4 && cellTaken[currentCell + 3] == null)
                {
                    prefab = 0; //Traps
                    for (int j = currentCell + 3 - currentCell % 3; j < currentCell + 5 - currentCell % 3; ++j)
                    {
                        if (cellTaken[j] == null)
                        {
                            PlaceObject(script, trapPool, currentCell);
                            cellTaken[currentCell] = prefab;
                            break;
                        }
                    }
                }
                else if (prefab < 8 && cellTaken[currentCell - 3] == null)
                {
                    prefab = 1; //Trees
                    for (int j = currentCell - currentCell % 3; j < currentCell + 3 - currentCell % 3; ++j)
                    {
                        if (currentCell != j && cellTaken[j] == null)
                        {
                            PlaceObject(script, wallPool, currentCell);
                            cellTaken[currentCell] = prefab;
                            break;
                        }
                    }
                }
                else if (prefab > 7)
                {
                    prefab = 2; //Stamps
                    PlaceObject(script, stampPool, currentCell);
                    cellTaken[currentCell] = prefab;
                    
                }

            }
            if (i%2 == 0)
            {
                yield return null;
            }

        }
        if (Random.Range(0, 10) == 0)
        {
            if(cellTaken[19] == null)
            {
                script.obstacleBufer.Add(Instantiate(boosts[Random.Range(0, boosts.Length)], script.placeHolders[19].transform));
            }
            
        }
                
    }
    IEnumerator XShape(GroundController script)
    {
        yield return 0;
        script.platforTypes[0].SetActive(true);
        for (int i = 0; i < 21; i += 2)
        {
            if (i < 10)
            {
                objectBuffer = wallPool.Get();
                objectBuffer.transform.SetPositionAndRotation(script.placeHolders[i].transform.position, Quaternion.identity);
                objectBuffer.transform.SetParent(script.placeHolders[i].transform);
                script.obstacleBufer.Add(objectBuffer);
            }
            if (i > 10)
            {
                objectBuffer = trapPool.Get();
                objectBuffer.transform.SetPositionAndRotation(script.placeHolders[i].transform.position, Quaternion.identity);
                objectBuffer.transform.SetParent(script.placeHolders[i].transform);
                script.obstacleBufer.Add(objectBuffer);                
            }
            if (i % 2 == 0) yield return null;
        }
    }
    IEnumerator RiftGeneration(GroundController script)
    {
        script.platforTypes[1].SetActive(true);
        if (Random.Range(0,2) == 0)
        {
            
            script.obstacleBufer.Add(Instantiate(riftRouts[0], script.transform.GetChild(1).GetChild(0)));
            script.obstacleBufer.Add(Instantiate(riftRouts[1], script.transform.GetChild(1).GetChild(1)));
        }
        else
        {
            script.obstacleBufer.Add(Instantiate(riftRouts[1], script.transform.GetChild(1).GetChild(0)));
            script.obstacleBufer.Add(Instantiate(riftRouts[0], script.transform.GetChild(1).GetChild(1)));
        }
        yield return 0;
    }
   
   IEnumerator TunelGeneration(GroundController script)
   {    
        yield return 0;
        script.platforTypes[0].SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 3);
            (tunnelShuffle[j], tunnelShuffle[i]) = (tunnelShuffle[i], tunnelShuffle[j]);
        }
        var k = 0;
        yield return 0;
        foreach (int i in tunnelShuffle)
        {
            script.obstacleBufer.Add(Instantiate(tunelsArray[i], script.placeHolders[9 + k].transform));
            k++;
        }
    }

    private void PlaceObject(GroundController script, ObjectPool<GameObject> obstacle, int cell)
    {
        
        displacement = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.2f, 0.2f));
        
        
        if (!Physics.CheckSphere(script.placeHolders[cell].transform.position + displacement, 0.5f, LayerMask.GetMask("Obstacle")))
        {
            objectBuffer = obstacle.Get();
            objectBuffer.transform.SetPositionAndRotation(script.placeHolders[cell].transform.position + displacement, Quaternion.identity);
            objectBuffer.transform.SetParent(script.placeHolders[cell].transform);
            script.obstacleBufer.Add(objectBuffer);
            
            if (objectBuffer.tag != "Wall" && Random.Range(0, 2) == 1)
            {
                objectBuffer = coinPool.Get();
                objectBuffer.transform.SetPositionAndRotation(script.placeHolders[cell].transform.position + displacement, Quaternion.identity);
                objectBuffer.transform.SetParent(script.placeHolders[cell].transform);
                script.obstacleBufer.Add(objectBuffer);
            }
        }
        else if (!Physics.CheckSphere(script.placeHolders[cell].transform.position - displacement, 0.5f, LayerMask.GetMask("Obstacle")))
        {
            objectBuffer = obstacle.Get();
            objectBuffer.transform.SetPositionAndRotation(script.placeHolders[cell].transform.position - displacement, Quaternion.identity);
            objectBuffer.transform.SetParent(script.placeHolders[cell].transform);
            script.obstacleBufer.Add(objectBuffer);
            if (objectBuffer.tag != "Wall" && Random.Range(0, 2) == 1)
            {
                objectBuffer = coinPool.Get();
                objectBuffer.transform.SetPositionAndRotation(script.placeHolders[cell].transform.position - displacement, Quaternion.identity);
                objectBuffer.transform.SetParent(script.placeHolders[cell].transform);
                script.obstacleBufer.Add(objectBuffer);
            }
        }
    }
    
    IEnumerator ReturnToPool(GameObject curObj)
    {
        if (curObj.CompareTag("Wall"))
        {
            wallPool.Release(curObj);
        }
        else if (curObj.CompareTag("Coin"))
        {
            coinPool.Release(curObj);
        }
        else if (curObj.CompareTag("Trap"))
        {
            trapPool.Release(curObj);
        }
        else if (curObj.CompareTag("Stamp"))
        {
            stampPool.Release(curObj);
        }
        else
        {
            Destroy(curObj);
        }
         
        yield return null;
    }

    IEnumerator InitializePools()
    {
        yield return null;
        coinPool = new 
        (() => 
            {return Instantiate(coins[Random.Range(0, 2)]);},
            coin => {coin.gameObject.SetActive(true);},
            coin => {foreach (Transform c in coin.transform){c.gameObject.SetActive(true);}; coin.transform.SetParent(null); coin.SetActive(false);},
            coin => {Destroy(coin.gameObject);},
            false, 30, 50
        );
        yield return null;
        wallPool = new 
        (() => 
            {return Instantiate(prefabArray[1]);},
            wall => {wall.gameObject.SetActive(true);},
            wall => {wall.transform.SetParent(null); wall.SetActive(false);},
            wall => {Destroy(wall.gameObject);},
            false, 30, 50
        );
        yield return null;
        stampPool = new 
        (() => 
            {return Instantiate(prefabArray[2]);},
            stamp => {stamp.gameObject.SetActive(true);},
            stamp => {stamp.transform.SetParent(null); stamp.SetActive(false);},
            stamp => {Destroy(stamp.gameObject);},
            false, 30, 50
        );
        yield return null;
        trapPool = new 
        (() => 
            {return Instantiate(prefabArray[0]);},
            trap => {trap.gameObject.SetActive(true);},
            trap => {trap.transform.SetParent(null); trap.SetActive(false);},
            trap => {Destroy(trap.gameObject);},
            false, 30, 50
        );
        yield return null;
    }

    
}
    
