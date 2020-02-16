using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aim;
    float width;
    float height;
    void Start()
    {
        aim=GameObject.FindGameObjectWithTag("MainCamera");
        width=Screen.width;
        height=Screen.height;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.x<=width&&Input.mousePosition.y<=height&&Input.mousePosition.x>=0&&Input.mousePosition.y>=0){
            float x,y,z;
            y=(Input.mousePosition.x-width/2)/(width/2)*360;
            x=-(Input.mousePosition.y-height/2)/(height/2)*90;
            transform.eulerAngles = new Vector3(0, y, 0);
            aim.transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, 0);
        }
        if(transform.position.y<-70){
            transform.position=new Vector3(8,20,16);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-0.1f, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(0.1f, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, 0, 0.1f);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, 0, -0.1f);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            for(int x=1;x<=10;x++)
                transform.Translate(0, 0.1f, 0);

        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(Chunk.block<16)
                Chunk.block++;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(Chunk.block>1)
                Chunk.block--;
        }
        
    }


    
}
