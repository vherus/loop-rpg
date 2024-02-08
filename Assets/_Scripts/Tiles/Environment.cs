using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public List<Tile> Tiles;

    public void InitiateSpawn()
    {
        Debug.Log("Environment is spawning enemies...");
        Tiles.ForEach(t => t.Spawn());
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
