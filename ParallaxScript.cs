using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour { 
    //length and startposition of sprites, we will also need camera
    private float length, startpos;
    public GameObject cam; 
    public float parallaxEffect;
    //this last variable helps with selecting how much parallax effect we want
    
    // Start is called before the first frame update
    void Start() {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        // We set the bounds for finding the length of the sprite
    }

    // Update is called once per frame
    void FixedUpdate() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        //How far you have moved relative to the camera
        //1- parallax effect since it is relavent to the camera
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) 
            startpos += length;
        else if (temp < startpos - length) 
            startpos -= length;

    }
}
