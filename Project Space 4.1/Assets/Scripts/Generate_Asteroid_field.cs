using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Generate_Asteroid_field : MonoBehaviour
{
    //README:
    /*This script instantiates a ring of large asteroids at random points within a sphere around an empty 
     asteroid_gen game object. We use this to generate some asteroids that are actual 3D destructible objects 
    (rigidbodies, collisions). The smaller asteroids that fill out the scene and provide detail are handled by 
    a separate particle system.*/

    public Transform large_asteroidPrefab;
    public int scale_factor = 1;
    public int fieldRadius = 5000;
    public int large_astCount = 10;
    public float large_ast_minSize = 250f;
    public float large_ast_maxSize = 350f;
    public int seed = 34;
    void Start()
    {
        Random.InitState(seed);
        for (int loop = 0; loop < large_astCount * scale_factor; loop++)
        {
            Transform temp = Instantiate(large_asteroidPrefab, Random.insideUnitSphere * fieldRadius * scale_factor, Random.rotation);
            temp.localScale = temp.localScale * Random.Range(large_ast_minSize, large_ast_maxSize);
        }
    }
}
