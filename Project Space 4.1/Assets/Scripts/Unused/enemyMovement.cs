using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public GameObject target;
    public GameObject orbiter;
    //public GameObject smallExplosionPrefab;
    public AudioSource explosionSource;
    public AudioClip expls;

    public Transform shootPoint;
    Ray ray;
    RaycastHit hitInfo;
    public TrailRenderer laserEffect;


    //public float shootSpeed = 10f;
    public float reloadTime = 1.3f;
    //public float laserLifeTime = 2f;
    float originalTime;

    float dist;


    // Start is called before the first frame update
    void Start()
    {
        originalTime = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        float attackDist = 500;
        float sightDist = 500;
        dist = Vector3.Distance(target.transform.position, transform.position);
        if(dist > sightDist)
        {
            /*timeCounter += Time.deltaTime*speed;

            float x = Mathf.Cos(timeCounter)*width;
            float z = Mathf.Sin(timeCounter)*height;
            float y = 0;

            transform.LookAt(transform.position);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, z), 10f * Time.deltaTime);*/
            transform.RotateAround(orbiter.transform.position, Vector3.up, -100 * Time.deltaTime);
            //transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
        }
        if(dist <= sightDist && dist > attackDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
            transform.LookAt(target.transform);
        }
        if(dist <= attackDist)
        {
            transform.RotateAround(target.transform.position, Vector3.up, -25 * Time.deltaTime);
            transform.LookAt(target.transform);
            transform.Rotate(0, -90, 0);
            reloadTime -= Time.deltaTime;
            if(reloadTime < 0)
            {
                RaycastShoot();
                reloadTime = originalTime;
            }
        }
        //Maybe??? Still trying to make it look horizontally after it loses sight of player
        //this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }

    /*public void shoot()
    {
        GameObject currentBullet = Instantiate(laserBeam, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
        Destroy(currentBullet, 3f); //Deletes bullet if it doesnt collide with anything after Xf seconds
    }*/

    public void RaycastShoot()
    {
        ray.origin = shootPoint.position;
        ray.direction = shootPoint.forward;
        var tracer = Instantiate(laserEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
           
            tracer.transform.position = hitInfo.point;
            //Instantiate(smallExplosionPrefab, hitInfo.point, transform.rotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
    }
}
