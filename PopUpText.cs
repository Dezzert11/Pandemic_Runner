using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public Transform player;
    public float showOnDistance = 2;

    MeshRenderer textMesh;

    // Use this for initialization
    void Start()
    {
        textMesh = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < showOnDistance)
            textMesh.enabled = true;
        else
            textMesh.enabled = false;
    }
}
