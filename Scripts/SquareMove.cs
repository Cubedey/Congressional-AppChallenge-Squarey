using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 


public class SquareMove : MonoBehaviour
{
    public int score;
    public bool start;
    public Sprite[] s;
    public SpriteRenderer thisRenderer;

    public int playerNumber = 1;
    public TextMeshProUGUI pNumberDisplay;
    public GameObject screen;

public bool selectedenemy;

    private Rigidbody2D sqr;
    public GameObject Trigger;
    public GameObject texti;
    public GameObject btn;
    public bool deathi=false;
    public TMP_Text scorie;
    public TMP_Text death;


    public GameObject input;


    public bool canJump;
    public bool hasenemy;
    public GameObject target;
public int timegened;

    // Start is called before the first frame update
    void Awake() {
        sqr=GetComponent<Rigidbody2D>();
        Restart();
    }
    void Start()
    {
        score=0;
        input.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.IntegerNumber;
        pNumberDisplay.text = playerNumber.ToString();
    }

    // Update is called once per frame

    void Update()
    {
        input.GetComponent<TMP_InputField>().ActivateInputField();

        bool killq=true;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Brick")) {
            if (Vector3.Distance(transform.position, obj.transform.position)<=500f) {
                killq=false;
            }
        }
        if (killq) {
            Die();
        }
        List<float> elist = new List<float>();
        selectedenemy=false;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
            if (Mathf.Abs(obj.transform.position.x-transform.position.x)<=8 && Mathf.Abs(obj.transform.position.y-transform.position.y)<=4.5) {
                elist.Add(obj.GetComponent<Enemy_AI>().dist);
                
            }
            if (target!=null && target.GetComponent<Enemy_AI>().dist==obj.GetComponent<Enemy_AI>().dist) {
                selectedenemy=true;
            }
        }
        //Debug.Log(selectedenemy);
        if (elist.Count!=0) {
            hasenemy=true;
            if (!selectedenemy) {
            elist.Sort();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
                if (elist[0]==obj.GetComponent<Enemy_AI>().dist) {
                    target=obj;
                }
            }
                            
            }
        }
        else if (!selectedenemy) {
            hasenemy=false;
        }
float moveSpeed = 8f;
if (!start && Input.GetKey("space")) {
    start=true;
    screen.SetActive(false);

}
    if (!deathi && start) {
    if (Input.GetKey(KeyCode.A)) 
    {
        thisRenderer.sprite = s[0];
        sqr.velocity = new Vector2(-moveSpeed, sqr.velocity.y);
    }
    if (Input.GetKey(KeyCode.D))
    {   
        thisRenderer.sprite = s[1];     
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        sqr.velocity = new Vector2(moveSpeed, sqr.velocity.y);
    }
    if (canJump &&  Input.GetKey(KeyCode.W))
    {
        sqr.velocity = new Vector2(sqr.velocity.x,moveSpeed*1.25f);
        canJump=false;
        sqr.drag=0;

    }
    if (!canJump && Input.GetKey(KeyCode.S))
    {
        sqr.velocity = new Vector2(sqr.velocity.x,sqr.velocity.y-moveSpeed/8f);
    }


    }
}

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.name=="Enemy(Clone)") {
            Die();
    }
    canJump=true;
    sqr.drag=1;
}
private void OnCollisionStay2D(Collision2D collision)
{
    canJump=true;
}
private void OnCollisionExit2D(Collision2D collision)
{
    sqr.drag=.1f;
}
public void Restart() {
    playerNumber=1;
    thisRenderer.sprite = s[1]; 
    pNumberDisplay.text = playerNumber.ToString();
    timegened=0;
    score=0;
    scorie.GetComponent<TMP_Text>().text="Score: "+score.ToString();
    sqr.velocity =  new Vector2(0f,0f);

    transform.position =  new Vector2(-4.37f,-2f);
    Instantiate(Trigger, new Vector2(6f,-1.38f), Quaternion.Euler(0f, 0f, 0f));
    deathi=false;
    death.GetComponent<TMP_Text>().text="";
        btn.SetActive(false);
        input.SetActive(true);
        texti.SetActive(true);

}
 void Die() {
deathi=true;
        death.GetComponent<TMP_Text>().text="You Died!?";
        btn.SetActive(true);
        input.SetActive(false);
        texti.SetActive(false);


    }
    //fix before go off
     public void Kill() {
        if (input.GetComponent<TMP_InputField>().text==texti.GetComponent<Text>().answer.ToString() && (hasenemy && !deathi)) {
            score+=Random.Range(1,11)*(timegened+1);
            scorie.GetComponent<TMP_Text>().text="Score: "+score.ToString();
            GetComponent<Shooter>().Shoot(texti.GetComponent<Text>().chosen);
            hasenemy=false; 
            playerNumber = texti.GetComponent<Text>().prevval;
            pNumberDisplay.text = playerNumber.ToString();
            input.GetComponent<TMP_InputField>().text="";
        }
    }
}