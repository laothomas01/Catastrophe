using System.Collections.Generic;
using UnityEngine;

public class TestWorldGenerate : MonoBehaviour
{
    
    /*
    Generating the floor/terrain of our world 
    */

//we take x and z because we need a plane in our 3d world 
    [SerializeField] private float worldSizeX = 1;
    [SerializeField] private float worldSizeZ = 1; 

    [SerializeField] private float gridOffSet = 1;

    [SerializeField] private float XPerlinNoise = 1;
    [SerializeField] private float ZPerlinNoise = 1;

    [SerializeField] private float heightScale = 1;

    [SerializeField] private float detailScale = 1;

    private List<Vector3> spawnPositions = new List<Vector3>();
    //the object used to create our plane 
    public GameObject blockGameObject;
    public GameObject spawnedObject;
    [SerializeField] private int spawnLimit = 1;
    void Start()
    {
        //let's generate our world
        for(int x = 0; x < worldSizeX; x++)
        {
            for(int z = 0; z < worldSizeZ; z++)
            {
                //no height for now. just a flat floor 
                Vector3 pos = new Vector3(x * gridOffSet, 0, z * gridOffSet);
                spawnPositions.Add(pos);
                GameObject tile = Instantiate(blockGameObject,pos,Quaternion.identity);
                tile.transform.SetParent(this.transform);
            }
        }
        for(int i = 0; i < spawnLimit; ++i)
        {
            GameObject spawn = Instantiate(spawnedObject, ObjectSpawnLocation(), Quaternion.identity);
            spawn.transform.SetParent(this.transform);
        }

    }
    float GeneratePerlinNoise(float x, float z, float detailScale)
    {
        // float xNoise =( x + this.transform.position.x)/detailScale;
        // float zNoise = (z + this.transform.position.z) / detailScale;
        // return Mathf.PerlinNoise(xNoise,zNoise);
        return Mathf.PerlinNoise((x)/detailScale,(z + this.transform.position.z)/detailScale);
    }
    
    Vector3 ObjectSpawnLocation()
    {
        int index = Random.Range(0,spawnPositions.Count);
        Vector3 newPos = new Vector3(spawnPositions[index].x,spawnPositions[index].y + 2, spawnPositions[index].z);
        spawnPositions.RemoveAt(index);
        return newPos;

    }








    // public GameObject blockGameObject; 
    // // Start is called before the first frame update
    // private int worldSizeX = 2;
    // private int worldSizeZ = 2;

    // [SerializeField] private int gridOffSet = 2; 

    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     for(int x = 0; x < worldSizeX; x++)
    //     {
    //         for(int z = 0; z < worldSizeZ; z++)
    //         {
    //             Vector3 pos = new Vector3(x * gridOffSet,generateNoise(x,z,8f) * 10,z * gridOffSet);
    //             GameObject block = Instantiate(blockGameObject,pos,Quaternion.identity);
    //             block.transform.SetParent(this.transform);
    //         }
    //     }
    // }

    // //let's call "x" the vertical coordinates
    // // let's call "z" the horizontal coordinates 
    // // detail scale = granularity of data
    // private float generateNoise(int x,int z, float detailScale)
    // {
    //     float xNoise = (x + this.transform.position.x) / detailScale;
    //     float zNoise = (z + this.transform.position.z) / detailScale;
    //     return Mathf.PerlinNoise(xNoise,zNoise);
    // }
}
