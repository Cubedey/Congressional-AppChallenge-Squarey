using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // Start is called before the first frame update
    public float dist;
    GameObject scr;
    void Start()
    {    scr= GameObject.Find("Squarey McSquareFace");

        
    }

    // Update is called once per frame
    void Update()
    {
        dist=Vector3.Distance(scr.transform.position, transform.position);

        if (scr.GetComponent<SquareMove>().deathi) {
            Destroy(gameObject);
        }
        if (-250f*2.56f>transform.position.x-scr.transform.position.x) {
        Destroy(gameObject);
        }
    }
}
