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

 public int verticalorientation=1;
 float size;
 public bool pasthorizontal=true;
int pastvertical=1;
bool horizontal=true;
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
    private void Update() {
        if (sqor.GetComponent<SquareMove>().deathi) {
        Destroy(gameObject);
        }
    }

   public void Generate() {
    {
                 size =Inside_Tile.transform.localScale.x;
        int times =sqor.GetComponent<SquareMove>().timegened;
        times=Mathf.Min(times*times-times+1,25);
          height= Random.Range(15,10*times);
          width= Random.Range(15,10*times);
         cords= new int[height,width];
        int[,] newcords= new int[height,width];

        bool start=true;
        int x1 =0;
        int y1=0;
        int xs=0;
        int ys=0;
        int det1;
        int det2;
        while (((x1<width-1 && y1<height-1) && !(x1>5 && y1<1))|| start) {
            start=false;
            cords[y1,x1]=1;

             det1=Random.Range(0,2);
             det2=Random.Range(-1,2);

            det2=(2*Random.Range(0,2)-1);
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
        cords[y1,x1]=1;
        if (x1<width-1) {
            horizontal=false;
        } else {
            horizontal=true;
        }
        
   // gen=false;
/*for (int y=height-1; y>0;y--) {
    string p="{";
                for (int x=0; x<width;x++) {
                    p+="{"+cords[y,x].ToString()+"}";
                }
                Debug.Log(p+"}");
            }*/

            //intersection problem.
            //Solve enemies not spawning in negatives
            //make enemies equations stay
            // arrow towards next generation
        if (verticalorientation==-1) {

            for (int y=0; y<height;y++) {
                for (int x=0; x<width;x++) {
                    newcords[height-1-y,x]=cords[y,x];
                }
            }
            cords=newcords;
        }
       place();
       Vector2 tpos;
        tpos = pos(x1,y1);
        pastvertical=verticalorientation;
        if (verticalorientation==-1) {
            tpos = new Vector2(size*2.56f*(x1+1)+transform.position.x,(size*2.56f*(-1*y1)+transform.position.y));
            if (!pasthorizontal) {
                tpos = new Vector2(size*2.56f*(x1)+transform.position.x,(size*2.56f*(-1*y1-1)+transform.position.y));

            }
        }
        if (x1<width-1) {
            pasthorizontal=false;

        } else {
            pasthorizontal=true;

            verticalorientation=(2*Random.Range(0,2)-1);

        }
       
        Instantiate(Trigger, tpos, Quaternion.Euler(0f, 0f, 0f));
        sqor.GetComponent<SquareMove>().timegened+=1;
        Destroy(gameObject);    

    }

    
   }
   public Vector2 pos(float x,float y) {
    float xax;
    float yax;
        if (pasthorizontal) {
             xax=size*2.56f*(x+1)+transform.position.x;
             yax=(size*2.56f*(y)+transform.position.y);

        } else {
             xax=size*2.56f*(x)+transform.position.x;
             yax=(size*2.56f*(y+verticalorientation)+transform.position.y);
        }
        
        if (verticalorientation==-1) {
            yax-=size*2.56f*(height-1);
            if (pastvertical==-1) {
                xax-=size*2.56f;
                yax-=size*2.56f;
        }
        }
        return new Vector2(xax,yax);
    
   }
   public void place() {
       bool first=true;

     for (int y=0; y<height;y++) {
            for (int x=0; x<width;x++) {
            bool down= y-1>=0 && cords[y-1,x]==0;

            if (cords[y,x]==0) {
                bool doit=false;
                bool right= x+1<width-1 && cords[y,x+1]==1;
                bool left= x-1>=0 && cords[y,x-1]==1;
                bool up= y+1<height-1 && cords[y+1,x]==1;
                down= y-1>=0 && cords[y-1,x]==1;

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
                for (int lim1=0;lim1<=3;lim1+=1) {
                    for (int lim2=-3;lim2<=3;lim2+=1) {
                    right= x+lim1<width-1 && (y+lim2<height-1 && y+lim2>=0) && cords[y+lim2,x+lim1]==1;
                    left= x-lim1>=0 && (y+lim2<height-1 && y+lim2>=0) && cords[y+lim2,x-lim1]==1;
                    up= y+lim1<height-1 && (x+lim2<width-1 && x+lim2>=0) && cords[y+lim1,x+lim2]==1;
                    down= y-lim1>=0 && (x+lim2<width-1 && x+lim2>=0) && cords[y-lim1,x+lim2]==1;
                    if (right || left || up || down) {
                        doit=true;
                        break;
                    }
                }
                }
                
                if (doit || z!=0){
                currentt = Instantiate(Inside_Tile, pos(x,y), Quaternion.Euler(0f, 0f, 0f));
                currentt.GetComponent<SpriteRenderer>().sprite= sprites[z];
                }
                
                

            } else {
                if (down && (first || ((y-1>=0 && cords[y-1,x]==0) &&(((Random.Range(1,11)+Mathf.Min(Mathf.Sqrt(1.5f*sqor.GetComponent<SquareMove>().timegened+1)/10,3))*(Random.Range(1,11)+Mathf.Min(Mathf.Sqrt(1.5f*sqor.GetComponent<SquareMove>().timegened+1)/10,3)))>=threshold) ))) {
                    first=false;
                    GameObject currente = Instantiate(Enemy, pos(x,y),Quaternion.identity);
                    int value = Random.Range(0,10);
                    currente.GetComponent<SpriteRenderer>().sprite = esprites[value];
                    currente.GetComponent<Enemy_AI>().value=value;
                }

            }
        }
        }
   }

}
