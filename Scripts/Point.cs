using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject square;
    GameObject Trigger;
    float pans=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Trigger").Length!=0) {
        Trigger=GameObject.FindGameObjectsWithTag("Trigger")[0];
        float ty=Trigger.transform.position.y;
        float tx=Trigger.transform.position.x;
        float sy=square.transform.position.y;
        float sx=square.transform.position.x;

        transform.position=new Vector3(0,0,0);
        float xadd=Mathf.Sin(-1*(pans)*3.1415926f/180);
        float yadd=Mathf.Cos(-1*(pans)*3.1415926f/180);
        //Debug.Log(xadd);
        //if (yadd<0) {
        //    yadd=yadd*-1f+90f;
        //}
        transform.position+=new Vector3(sx+xadd,sy-yadd,0);
       // Debug.Log(tx);
        float start;
        if (tx>sx) {
            start=0;
        } else {
            start=0;
        }
        pans=(-1*Mathf.Atan((ty-sy)/(tx-sx))*180f/(3.1415926f)); 
        bool up=ty-sy>0f;
        bool right=tx-sx>0f;
        if (up && right) {
            pans+=270f;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Abs(Mathf.Atan((ty-sy)/(tx-sx))*180f/(3.1415926f)+270f));

        } 
        if (up && !right) {
            pans+=90f;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Abs(Mathf.Atan((ty-sy)/(tx-sx))*180f/(3.1415926f)+90f));

        } 
        if (!up && right) {
            pans+=270f;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Abs(Mathf.Atan((ty-sy)/(tx-sx))*180f/(3.1415926f)+270f));

        } 
        if (!up && !right) {
            pans+=90f;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Abs(Mathf.Atan((ty-sy)/(tx-sx))*180f/(3.1415926f)+90f));

        } 

        }
    }
}
