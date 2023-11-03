using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class Text : MonoBehaviour
{
    public GameObject sqir;
        public TMP_Text tex;
        public float counter;
        public int answer;
        public int prevval=-1;
        string[] chars={"+","-","×","÷"};
        string[] chars2={"+","-","*","/"};
        public int chosen;

    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sqir.GetComponent<SquareMove>().hasenemy && prevval!=sqir.GetComponent<SquareMove>().target.GetComponent<Enemy_AI>().value) {
            int val=sqir.GetComponent<SquareMove>().target.GetComponent<Enemy_AI>().value;
            prevval=val;
            int c;
            int other=sqir.GetComponent<SquareMove>().playerNumber;
            if(val==0) {
                 c=Random.Range(0,2);

            } else {
             c=Random.Range(0,chars.Length);

            }
            chosen=c;
            if (chars[c]=="÷") {
                answer=Random.Range(1,13);
                other=val*answer;
                tex.text=other.ToString()+" ÷ ? = "+val.ToString();

            } else {
            DataTable dt = new DataTable();
            var pla = dt.Compute(val+chars2[c]+other,"");
            answer=int.Parse(pla.ToString());
            tex.text=val.ToString()+" "+chars[c]+" "+other.ToString()+" = ?";
            }
           
        } else if (!sqir.GetComponent<SquareMove>().hasenemy) {
            prevval=-1;
            tex.text="";
        }
    }
}
