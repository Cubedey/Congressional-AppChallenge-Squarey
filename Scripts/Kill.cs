using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject scr;
    void Start()
    {    scr= GameObject.Find("Squarey McSquareFace");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (scr.GetComponent<SquareMove>().deathi) {
            Destroy(gameObject);
        }
    }
}
