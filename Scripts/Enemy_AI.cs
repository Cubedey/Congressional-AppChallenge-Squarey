using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    // Start is called before the first frame update
        private Rigidbody2D enemy;
        public float dist;
int moveSpeed;
 [SerializeField] int chosen_times=0;
 [SerializeField] int chosen_last=0;

 double right=.01;
 double left;

 public int value;

 double jump=.998;
 int based=1000;
 GameObject sqor;
 int mult;
    void Start()
    {
        sqor= GameObject.Find("Squarey McSquareFace");
        enemy=GetComponent<Rigidbody2D>();
        left=1.0-right;

    }
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag=="Operator") {
            Kill();
    }
}

    // Update is called once per frame
    void Update()
    {

        dist= Vector3.Distance (sqor.transform.position, this.transform.position);

        
        mult=sqor.GetComponent<SquareMove>().timegened;
        mult=(mult/2)*(mult/2)-2;
        moveSpeed=3+Mathf.Min(mult,12);

        //makkes ure same as based
        int x =Random.Range(0,based);

        if (x<((based*right)+((chosen_last)*(15-(chosen_times*chosen_times))))) {
        if (chosen_last==1) {
            chosen_times++;
        } else {
            chosen_times=0;
        }
        chosen_last=1;


        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        enemy.velocity =  new Vector2(-moveSpeed, enemy.velocity.y);
        }
        else if (x>=((based*left)+((chosen_last)*(15-(chosen_times*chosen_times)))) && x<(based*jump)) {
            //bug here but dont matter cause iterations
        if (chosen_last==-1) {
            chosen_times++;
        } else {
            chosen_times=0;
        }
        chosen_last=-1;            
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        enemy.velocity =  new Vector2(moveSpeed, enemy.velocity.y);
        }
        if (x>=(based*jump)) {
        enemy.velocity =  new Vector2(enemy.velocity.x, 3*moveSpeed);
        }
    }
    void Kill() {
            Destroy(gameObject);

    }
}
