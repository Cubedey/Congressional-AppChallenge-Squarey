using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Generate_Path : MonoBehaviour
{
      
    // Start is called before the first frame update
    [SerializeField] GameObject Inside_Tile;
    [SerializeField] GameObject Trigger;
        GameObject sqor;

    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject Enemy;
    [SerializeField] Sprite[] esprites;


public GameObject currentt;
 public int difficulty=0;
 public int threshold;
  public int height;
  public int width;
  public int[,] cords;
  void Awake() {
        sqor= GameObject.Find("Squarey McSquareFace");

  }
    void Start() 
    {
        threshold=60-Mathf.Min((difficulty+1)*(difficulty+1),60);

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name=="Squarey McSquareFace") {
        Generate();

    }
    }

   public void Generate() {
    {
                float size =Inside_Tile.transform.localScale.x;
        int times =sqor.GetComponent<SquareMove>().timegened;
        times=Mathf.Min(times*times-times+1,25);
          height= Random.Range(15,10*times);
          width= Random.Range(15,10*times);
         cords= new int[height,width];
        int x1 =0;
        int y1=0;
        int xs=0;
        int ys=0;
        int det1;
        int det2;
        while (x1<width-1 && y1<height-1) {
            cords[y1,x1]=1;

             det1=Random.Range(0,2);
             det2=Random.Range(-1,2);

            while (det2==0) {
            det2=Random.Range(-1,2);
            }
            if (det1==1 && x1+det2!=xs) {
                if (x1<3) {
                    x1++;
                } else {
                    x1+=det2;
                }
            } else if (y1+det2!=ys) {
                if (y1<3) {
                    y1++;
                } else {
                    y1+=det2;
                }
            }
            xs=x1;
            ys=y1;
            
        }
        Instantiate(Trigger, new Vector2(size*2.56f*(x1)+transform.position.x,size*2.56f*(y1)+transform.position.y), Quaternion.Euler(0f, 0f, 0f));

   // gen=false;
        for (int y=0; y<height-1;y++) {
            for (int x=0; x<width-1;x++)
            if (cords[y,x]==0) {
                currentt = Instantiate(Inside_Tile, new Vector2(size*2.56f*(x)+transform.position.x,size*2.56f*(y)+transform.position.y), Quaternion.Euler(0f, 0f, 0f));
                bool right= x+1<width-1 && cords[y,x+1]==1;
                bool left= x-1>=0 && cords[y,x-1]==1;
                bool up= y+1<height-1 && cords[y+1,x]==1;
                bool down= y-1>=0 && cords[y-1,x]==1;
                int z=0;
                if (left) {
                    z+=1;
                }
                    if (down) {
                    z+=2;
                }
                if (right) {
                    z+=4;
                }
                if (up) {
                    z+=8;
                }
                
                
                currentt.GetComponent<SpriteRenderer>().sprite= sprites[z];
            } else {

                if ((y-1>=0 && cords[y-1,x]==0) &&(((Random.Range(1,11)+Mathf.Min(Mathf.Sqrt(1.5f*sqor.GetComponent<SquareMove>().timegened+1)/10,3))*(Random.Range(1,11)+Mathf.Min(Mathf.Sqrt(1.5f*sqor.GetComponent<SquareMove>().timegened+1)/10,3)))>=threshold) ) {
                    GameObject currente = Instantiate(Enemy, new Vector2(size*2.56f*(x)+transform.position.x,size*2.56f*(y)+transform.position.y),Quaternion.identity);
                    int value = Random.Range(0,10);
                    currente.GetComponent<SpriteRenderer>().sprite = esprites[value];
                    currente.GetComponent<Enemy_AI>().value=value;
                }

            }
        }
        sqor.GetComponent<SquareMove>().timegened+=1;
        Destroy(gameObject);    

    }
   }

}
