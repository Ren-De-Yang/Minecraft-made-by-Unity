using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk : MonoBehaviour
{
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        aim = GameObject.FindGameObjectWithTag("cube");
        Worldinit();
        Update();
    }
    void Update()
    {
        WorldCube();
        raycast();
        if (Input.GetMouseButtonDown(1))
        {
            ReplaceBlock();
        }
        if (Input.GetMouseButtonDown(0))
        {
            AddBlock();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private float tUnit = 0.25f;
    

    private Mesh mesh;
    private MeshCollider col;

    private int faceCount;

    void CubeTop(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        Cube(block,"Top");
    }
    void CubeNorth(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Cube(block,"North");
    }
    void CubeEast(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

        Cube(block,"East");
    }
    void CubeSouth(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));

        Cube(block,"South");
    }
    void CubeWest(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        Cube(block,"West");
    }
    void CubeBot(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));
       
        Cube(block,"Bot");
    }
    //1
    Vector2 tStone = new Vector2(1, 0);
    //2
    Vector2 tGrass = new Vector2(0, 1);
    //3
    Vector2 a = new Vector2(0, 3);
    //4
    Vector2 b = new Vector2(0, 2);
    Vector2 c = new Vector2(1, 3);    
    Vector2 d = new Vector2(1, 2);
    Vector2 e = new Vector2(1, 1);
    Vector2 f = new Vector2(0, 0);
    Vector2 g = new Vector2(2, 1);
    Vector2 h = new Vector2(2, 2);
    Vector2 i = new Vector2(2, 3);
    Vector2 j = new Vector2(2, 0);
    Vector2 k = new Vector2(3, 0);
    Vector2 l = new Vector2(3, 1);
    Vector2 m = new Vector2(3, 2);
    Vector2 n = new Vector2 (3, 3);

    Vector2 CheckTexture(byte block, string cubeface)
    {
        if (block == (byte)1)
        {
            return tStone;
        }
        if (block == (byte)2)
        {
            if(cubeface=="Top"){
                return tGrass;
            }
            else{
                return tStone;
            }
        }
        if (block == (byte)3)
        {
            return a;
        }
        if (block == (byte)4)
        {
            return b;
        }
        if (block == (byte)5)
        {
            return c;
        }
        if (block == (byte)6)
        {
            return d;
        }
        if (block == (byte)7)
        {
            return e;
        }
        if (block == (byte)8)
        {
            return f;
        }
        if (block == (byte)9)
        {
            return g;
        }
        if (block == (byte)10)
        {
            return h;
        }
        if (block == (byte)11)
        {
            return i;
        }
        if (block == (byte)12)
        {
            return j;
        }
        if (block == (byte)13)
        {
            return k;
        }
        if (block == (byte)14)
        {
            return l;
        }
        if (block == (byte)15)
        {
            return m;
        }
        if (block == (byte)16)
        {
            return n;
        }
        else
        {
            return tStone;
        }
    }
    void Cube(byte block, string cubeface)
    {
        Vector2 texture = CheckTexture(block,cubeface);
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 1); //2
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4 + 3); //4

        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y));
        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y));

        faceCount++;
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        col.sharedMesh = null;
        col.sharedMesh = mesh;

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();
        
        faceCount = 0;
    }

    public byte[,,] data;
    public int WorldX = 160;
    public int WorldY = 200;
    public int WorldZ = 160;

 
    void Worldinit()
    {
        data = new byte[WorldX, WorldY, WorldZ];
        for (int x = 0; x < WorldX; x++)
        {
            for (int y = 0; y < WorldY; y++)
            {
                for (int z = 0; z < WorldZ; z++)
                {
                    data[x, y, z] = (byte)0;
                }
            }
        }
        for (int x = 0; x < WorldX; x++)
        {
            for (int y = 0; y < WorldY; y++)
            {
                for (int z = 0; z < WorldZ; z++)
                {
                    if (y <= 7)
                    {
                        data[x, y, z] = (byte)1;
                    }
                    if (y == 8)
                    {
                        data[x, y, z] = (byte)2;
                    }
                }
            }
        }
        WorldCube();
    }

    public byte Block(int x, int y, int z)
    {
        if (x >= WorldX || x < 0 || y >= WorldY || y < 0 || z >= WorldZ || z < 0)
        {
            return (byte)0;
        }
        return data[x, y, z];
    }
    void WorldCube()
    {
        for (int x = 0; x < WorldX; x++)
        {
            for (int y = 0; y < WorldY; y++)
            {
                for (int z=0; z < WorldZ; z++)
                {
                    if (Vector3.Distance(campos,new Vector3(x,y,z))<=20f && Block(x, y, z) != 0  )
                    {
                        if (Block(x, y + 1, z) == (byte)0)
                        {
                            CubeTop(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y - 1, z) == (byte)0)
                        {
                            CubeBot(x, y, z, Block(x, y, z));
                        }
                        if (Block(x + 1, y, z) == (byte)0)
                        {
                            CubeEast(x, y, z, Block(x, y, z));
                        }
                        if (Block(x - 1, y, z) == (byte)0)
                        {
                            CubeWest(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y, z + 1) == (byte)0)
                        {
                            CubeNorth(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y, z - 1) == (byte)0)
                        {
                            CubeSouth(x, y, z, Block(x, y, z));
                        }
                    }
                }
            }
        }
        UpdateMesh();
    }
    

    public GameObject cam;
    public GameObject aim;
    Vector3 focus=new Vector3();
    Vector3 addpos=new Vector3();
    Vector3 campos;

    void raycast(){
        campos=cam.transform.position;
        float step=0.05f;
        float stepf=0.05f;
        while(step<range){
            Vector3 pos = campos + (cam.transform.forward * step);
            //Debug.Log(pos.ToString());
            //Debug.DrawLine(cam.transform.position, pos, Color.red, 0.1f, true);
            if(Block((Int32)Mathf.Floor(pos.x), (Int32)Mathf.Ceil(pos.y), (Int32)Mathf.Floor(pos.z))!=(byte)0){
                focus=new Vector3(Mathf.Floor(pos.x),Mathf.Ceil(pos.y),Mathf.Floor(pos.z));
                aim.transform.position=new Vector3(focus.x+0.5f,focus.y-0.5f,focus.z+0.5f);
                aim.SetActive(true);
                return;
            }else{
                aim.SetActive(false);
                addpos=focus;
                focus=new Vector3(Mathf.Floor(pos.x),Mathf.Ceil(pos.y),Mathf.Floor(pos.z));
                step=step+stepf;
            }
            //Debug.Log(addpos.ToString()+" "+focus.ToString());
        }
    }
    float range = 10.0f;

    void ReplaceBlock()
    {
        //Debug.DrawLine(cam.transform.position,focus, Color.red, 1f, true);

        data[(Int32)focus.x, (Int32)focus.y, (Int32)focus.z] = (byte)0;
        //Debug.Log("delete"+focus.x.ToString()+" "+ focus.y.ToString()+" "+ focus.z.ToString());
        WorldCube();
        raycast();
    }
    public static byte block = (byte)1;
    void AddBlock()
    {
        try{
            if(Block((Int32)addpos.x, (Int32)addpos.y, (Int32)addpos.z)==(byte)0&&aim.activeSelf==true){
                data[(Int32)addpos.x, (Int32)addpos.y, (Int32)addpos.z] = block;
                //Debug.Log("add"+addpos.x.ToString()+" "+ addpos.y.ToString()+" "+ addpos.z.ToString());
                WorldCube();
                raycast();
            }
        }catch{

        }
        
    }
}