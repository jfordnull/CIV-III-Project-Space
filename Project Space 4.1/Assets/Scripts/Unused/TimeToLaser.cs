using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    //public GameObject ParticlePrefab;
    // Start is called before the first frame update

    // Update is called once per frame

    public GameObject smallExplosionPrefab;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        //Instantiate(ParticlePrefab, this.transform.position, Quaternion.identity);
        GameObject boom = Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(boom, 1f);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}