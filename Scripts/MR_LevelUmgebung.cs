using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_LevelUmgebung : MonoBehaviour
{

    Mesh mesh;
    List<Vector2> uvs;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector3> normals;

    public int brickSize = 4;

    int index;
    public int LevelLength;

    public int Loch;
    public int Hinderniss;
    public int RoomSize = 10;

    int dist = 4;  //länge des geraden Flures nach dem ersten Raum
    int offset;

    int offset2;

    public int mobilePlatform;
    public int lengthOfPlatform;

    public int SizeX= 10;

    public int SizeY = 40;



     void Awake(){

        mesh = GetComponent<MeshFilter> ().mesh;
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        normals = new List<Vector3>();
       
    }
  
    void Start(){

        
        for(int i = 0; i<LevelLength; i++){

        linkeWand(i);
        rechteWand(i);

        if( i == Loch){

            GangMitLoch(i);
                    
        }else if( i>= mobilePlatform && i <= mobilePlatform + lengthOfPlatform){

                GangMitPlatform(i);

            
        }else {
            Gang(i);  //Gang enthällt auch die Decke!
        }

        
        

        if(i == Hinderniss){

            BuildHinderniss(i);
        }


        }


        createRoom(LevelLength, RoomSize);

        int currentDistance = (LevelLength+RoomSize)*brickSize;

        createHallway(currentDistance, dist); //enthält auch die decke! //dist = länge

        createFinalRoom(offset, offset2, SizeX, SizeY);
        

    CreateLevel();


    }

    void linkeWand(int z){

        int h = z*brickSize;


        Vector3 a = new Vector3 (-brickSize/2, brickSize, 0+h);  
        Vector3 b = new Vector3 (-brickSize/2, 0, 0+h); 
        Vector3 c = new Vector3 (-brickSize/2, 0, brickSize+h); 
        Vector3 d =new Vector3 (-brickSize/2, brickSize, brickSize+h); 

        Face(a,b,c,d);

    }

    void rechteWand(int z){
        
        int h = z*brickSize;
        Vector3 a = new Vector3 (brickSize/2, brickSize, brickSize+h);  
        Vector3 b = new Vector3 (brickSize/2, 0, brickSize+h); 
        Vector3 c = new Vector3 (brickSize/2, 0, 0+h); 
        Vector3 d = new Vector3 (brickSize/2, brickSize, 0+h); 

        Face(a,b,c,d);

    }

    void Gang(int z){

        int h = z*brickSize;


        Vector3 a = new Vector3 (-brickSize/2, 0, 0+h);
        Vector3 b = new Vector3 (brickSize/2, 0, 0+h); 
        Vector3 c = new Vector3 (brickSize/2, 0, brickSize+h); 
        Vector3 d =new Vector3 (-brickSize/2, 0, brickSize+h); 

        Vector3 r = new Vector3 (-brickSize/2, brickSize, 0+h);
        Vector3 s = new Vector3 (brickSize/2, brickSize, 0+h); 
        Vector3 t = new Vector3 (brickSize/2, brickSize, brickSize+h); 
        Vector3 u =new Vector3 (-brickSize/2, brickSize, brickSize+h); 

       // Face(r,s,t,u);
        Face(u,t,s,r);
        Face(a,b,c,d);

    }

        void GangMitPlatform(int z){  //braucht extra Wände

        int h = z*brickSize;


        Vector3 a = new Vector3 (-brickSize/2, -brickSize, 0+h);
        Vector3 b = new Vector3 (brickSize/2, -brickSize, 0+h); 
        Vector3 c = new Vector3 (brickSize/2, -brickSize, brickSize+h); 
        Vector3 d =new Vector3 (-brickSize/2, -brickSize, brickSize+h); 

        Vector3 r = new Vector3 (-brickSize/2, brickSize, 0+h);
        Vector3 s = new Vector3 (brickSize/2, brickSize, 0+h); 
        Vector3 t = new Vector3 (brickSize/2, brickSize, brickSize+h); 
        Vector3 u =new Vector3 (-brickSize/2, brickSize, brickSize+h); 

        Vector3 a1 = new Vector3 (-brickSize/2, 0, 0+h);
        Vector3 b2 = new Vector3 (brickSize/2, 0, 0+h); 
        Vector3 c3 = new Vector3 (brickSize/2, 0, brickSize+h); 
        Vector3 d4 =new Vector3 (-brickSize/2, 0, brickSize+h); 

       // Face(r,s,t,u);
        Face(u,t,s,r);
        Face(a,b,c,d);
        Face(a,d,d4,a1); //Wall leftside
        Face(b2,c3,c,b); //wall rightside

        if( z == mobilePlatform ){

            //wand am anfang

            Face(a1,b2,b,a);
        }else if( z == mobilePlatform+lengthOfPlatform){


            Face(c3,d4,d,c);

        }




    }
    void GangMitLoch(int z){

        int h = z*brickSize;


        Vector3 a = new Vector3 (-brickSize/2, 0, 0+h);
        Vector3 b = new Vector3 (brickSize/2, 0, 0+h); 
        Vector3 c = new Vector3 (brickSize/2, 0, brickSize/2+h); 
        Vector3 d =new Vector3 (-brickSize/2, 0, brickSize/2+h); 

        Vector3 e = new Vector3(-brickSize/2,-2,brickSize/2+h);  //Loch unten 
        Vector3 f = new Vector3(brickSize/2,-2,brickSize/2+h);
        Vector3 g = new Vector3(brickSize/2,-2,brickSize+h);
        Vector3 i = new Vector3(-brickSize/2, -2, brickSize+h);

        Vector3 j = new Vector3(-brickSize/2,0,brickSize/2+h);  //Loch oben 
        Vector3 k = new Vector3(brickSize/2,0,brickSize/2+h);
        Vector3 l = new Vector3(brickSize/2,0,brickSize+h);
        Vector3 m = new Vector3(-brickSize/2, 0, brickSize+h);


        Vector3 r = new Vector3 (-brickSize/2, brickSize, h);         //decke
        Vector3 s = new Vector3 (brickSize/2, brickSize, h); 
        Vector3 t = new Vector3 (brickSize/2, brickSize, brickSize+h); 
        Vector3 u =new Vector3 (-brickSize/2, brickSize, brickSize+h); 



        smallFace(u,t,s,r);

        smallFace(a,b,c,d);
        smallFace(e,f,g,i);
        smallFace(j,k,f,e);
        smallFace(l,m,i,g);

        smallFace(i,m,j,e);
        smallFace(f,k,l,g);  


      }


    private void BuildHinderniss(int z){

        int h = z*brickSize;

        Vector3 a =new Vector3(-brickSize/2,brickSize/2,0+h);
        Vector3 b =new Vector3(-brickSize/2,brickSize,0+h);
        Vector3 c = new Vector3(brickSize/2,brickSize/2,0+h);
        Vector3 d = new Vector3(brickSize/2,brickSize,0+h);     

        Vector3 e = new Vector3(-brickSize/2,brickSize/2,2+h);
        Vector3 f = new Vector3(-brickSize/2,brickSize,2+h);
        Vector3 g = new Vector3(brickSize/2,brickSize/2,2+h);
        Vector3 i = new Vector3(brickSize/2,brickSize,2+h);




        smallFace(c,d,b,a);  
        smallFace(e,f,i,g);
        smallFace(a,b,d,c);  
        smallFace(g,i,f,e);
        smallFace(e,g,c,a);
        smallFace(a,c,g,e);

    }




    void createRoom(int x, int Size){

        createFloorGunRoom(Size*2,Size, x);  //enthällt auch die decke
        createWallsGunRoom(Size*2,Size, x);

        

    }



    void createFloorGunRoom(int SizeX, int SizeY, int entf){  //und decke


            for( int i = 0; i< SizeY; i++){
                for(int k = 0; k < SizeX; k++){
                    
                    int e = entf*brickSize;
                    int x = k*brickSize;
                    int y = i *brickSize;

                    Vector3 a = new Vector3(-brickSize/2+x, 0, e+y);
                    Vector3 b = new Vector3(brickSize/2+x,0,e+0+y);
                    Vector3 c = new Vector3(brickSize/2+x,0,e+brickSize+y);
                    Vector3 d = new Vector3(-brickSize/2+x,0,e+brickSize+y);

                    Vector3 r = new Vector3(-brickSize/2+x, brickSize, e+y);  //decke
                    Vector3 s = new Vector3(brickSize/2+x,brickSize,e+0+y);
                    Vector3 t = new Vector3(brickSize/2+x,brickSize,e+brickSize+y);
                    Vector3 u = new Vector3(-brickSize/2+x,brickSize,e+brickSize+y);

                    Face(u,t,s,r);
                    Face(a,b,c,d);
                }

            }


    }

    void createWallsGunRoom(int SizeX,int SizeY, int entf){


        //Walls to the right
        for(int i = 0; i<SizeX-1; i++){ //-1 wegen der Tür!
            int x = i*brickSize;
            int e = brickSize*entf;


            //wand, wenn man reinkommt rechts entlang
            Vector3 a  = new Vector3(brickSize/2+x,0,e);
            Vector3 b = new Vector3(brickSize/2+x, brickSize, e);
            Vector3 c = new Vector3(brickSize/2+brickSize+x,0, e);
            Vector3 d = new Vector3(brickSize/2+brickSize+x, brickSize, e);
            //wand wenn man reinkommt gegenüber
            Vector3 f  = new Vector3(brickSize/2+x,0,e+SizeY*brickSize);
            Vector3 g = new Vector3(brickSize/2+x, brickSize, e+SizeY*brickSize);
            Vector3 h = new Vector3(brickSize/2+brickSize+x,0, e+SizeY*brickSize);
            Vector3 j = new Vector3(brickSize/2+brickSize+x, brickSize, e+SizeY*brickSize);
                       
            Face(a,b,d,c);
            Face(h,j,g,f);
        }
        
        
        for(int i = 0; i<SizeY; i++){  //neue For, da hier keine Tür!
            

            int x = i*brickSize;
            int e = brickSize*entf;
            //wand links
            Vector3 a = new Vector3(-brickSize/2,0,e+x);
            Vector3 b = new Vector3(-brickSize/2,0,e+brickSize+x);
            Vector3 c = new Vector3(-brickSize/2,brickSize,e+x);
            Vector3 d = new Vector3(-brickSize/2,brickSize,e+brickSize+x);

            Vector3 f = new Vector3(-brickSize/2+SizeX*brickSize,0,e+x);
            Vector3 g = new Vector3(-brickSize/2+SizeX*brickSize,0,e+brickSize+x);
            Vector3 h = new Vector3(-brickSize/2+SizeX*brickSize,brickSize,e+x);
            Vector3 j = new Vector3(-brickSize/2+SizeX*brickSize,brickSize,e+brickSize+x);


            Face(a,b,d,c);
            Face(h,j,g,f);
        }


    }

    private void createHallway(int entf, int dist){

        
        int nachLinks = 0;

        //floor (part1, to curve) + decke
        for(int i = 0; i<dist;i++){

            int x = i * brickSize;

            Vector3 a = new Vector3(-brickSize/2, 0, entf+x);
            Vector3 b = new Vector3(brickSize/2,0,entf+x);
            Vector3 c = new Vector3(brickSize/2,0,entf+brickSize+x);
            Vector3 d = new Vector3(-brickSize/2,0,entf+brickSize+x); 

            Vector3 r = new Vector3(-brickSize/2, brickSize, entf+x);
            Vector3 s = new Vector3(brickSize/2,brickSize,entf+x);
            Vector3 t = new Vector3(brickSize/2,brickSize,entf+brickSize+x);
            Vector3 u = new Vector3(-brickSize/2,brickSize,entf+brickSize+x); 

            Face(a,b,c,d);
            Face(u,t,s,r);

        }
            //floor part (nach Links) + Decke
        for(int i = 0; i < 20; i++){

         int currentDis =  entf + dist*brickSize;
         int x = i * brickSize;

        Vector3 a = new Vector3(-brickSize/2-x, 0, currentDis);
        Vector3 b = new Vector3(brickSize/2-x,0,currentDis);
        Vector3 c = new Vector3(brickSize/2-x,0,currentDis+brickSize);
        Vector3 d = new Vector3(-brickSize/2-x,0,currentDis + brickSize);   

        Vector3 r = new Vector3(-brickSize/2-x, brickSize, currentDis);
        Vector3 s = new Vector3(brickSize/2-x,brickSize,currentDis);
        Vector3 t = new Vector3(brickSize/2-x,brickSize,currentDis+brickSize);
        Vector3 u = new Vector3(-brickSize/2-x,brickSize,currentDis + brickSize);


        //right wall of that part
        Vector3 v = new Vector3(-brickSize/2-x, 0, currentDis+brickSize);
        Vector3 w = new Vector3(brickSize/2-x, 0, currentDis+brickSize);
        Vector3 y = new Vector3(brickSize/2-x, brickSize, currentDis+brickSize);
        Vector3 z = new Vector3(-brickSize/2-x, brickSize, currentDis+brickSize);

        nachLinks = x;     


        Face(a,b,c,d);
        Face(u,t,s,r);
        Face(v,w,y,z);

        }

        //floor part "zurück" + Decke
        for(int i = 0; i< 6; i++){

        int currentDis =  entf + dist*brickSize;
        int x = i * brickSize;

        Vector3 a = new Vector3(-brickSize/2-nachLinks, 0, currentDis-x);
        Vector3 b = new Vector3(brickSize/2-nachLinks,0,currentDis-x);
        Vector3 c = new Vector3(brickSize/2-nachLinks,0,currentDis+brickSize-x);
        Vector3 d = new Vector3(-brickSize/2-nachLinks,0,currentDis + brickSize-x);

        Vector3 ad = new Vector3(-brickSize/2-nachLinks, brickSize, currentDis-x);
        Vector3 bd = new Vector3(brickSize/2-nachLinks,brickSize,currentDis-x);
        Vector3 cd = new Vector3(brickSize/2-nachLinks,brickSize,currentDis+brickSize-x);
        Vector3 dd = new Vector3(-brickSize/2-nachLinks,brickSize,currentDis + brickSize-x);


            //wand rechts

        Vector3 v = new Vector3(brickSize/2-nachLinks-brickSize, 0, currentDis-x);
        Vector3 w = new Vector3(brickSize/2-nachLinks-brickSize, 0, currentDis+brickSize-x);
        Vector3 y = new Vector3(brickSize/2-nachLinks-brickSize, brickSize, currentDis-x);
        Vector3 z = new Vector3(brickSize/2-nachLinks-brickSize, brickSize, currentDis+brickSize-x);


        if(i < 5){  // Wand links wegen durchgang

        Vector3 r = new Vector3(brickSize/2-nachLinks, 0, currentDis-x);
        Vector3 s = new Vector3(brickSize/2-nachLinks,0, currentDis-brickSize-x);
        Vector3 t = new Vector3(brickSize/2-nachLinks, brickSize, currentDis-x);
        Vector3 u = new Vector3(brickSize/2-nachLinks, brickSize, currentDis-brickSize-x);

        Face(r,s,u,t);
        }

        offset = brickSize/2-nachLinks;
        offset2 = currentDis -x - brickSize;

        Face(dd,cd,bd,ad);
        Face(v,w,z,y);
        Face(a,b,c,d);
        }


        //wall right side

        for(int i = 0; i < dist+1; i++){  //man will die wand ja bis in die ecke

        int currentDis = entf;
        int x = i*brickSize;
        Vector3 a = new Vector3 (brickSize/2, brickSize, brickSize+x+entf);  
        Vector3 b = new Vector3 (brickSize/2, 0, brickSize+x+entf); 
        Vector3 c = new Vector3 (brickSize/2, 0, 0+x+entf); 
        Vector3 d = new Vector3 (brickSize/2, brickSize, 0+x+entf);

        Face(a,b,c,d);
        }

        //wall leftside, up to corner

        for( int i = 0; i < dist; i++){  //man will ja einen Weg lassen


        int currentDis = entf;
        int x = i*brickSize;
        Vector3 a = new Vector3 (-brickSize/2, brickSize, brickSize+x+entf);  
        Vector3 b = new Vector3 (-brickSize/2, 0, brickSize+x+entf); 
        Vector3 c = new Vector3 (-brickSize/2, 0, 0+x+entf); 
        Vector3 d = new Vector3 (-brickSize/2, brickSize, 0+x+entf);

        Face(d,c,b,a);


        }


        //wall leftside, after corner

        for( int i = 0; i <18; i++){

        //wall left side

        int currentDis = entf + dist*brickSize;
        int x = i* brickSize;

        Vector3 v = new Vector3(-brickSize/2-x-brickSize, 0, currentDis);
        Vector3 w = new Vector3(brickSize/2-x-brickSize, 0, currentDis);
        Vector3 y = new Vector3(brickSize/2-x-brickSize, brickSize, currentDis);
        Vector3 z = new Vector3(-brickSize/2-x-brickSize, brickSize, currentDis);

        //Face(v,w,y,z);
        Face(z,y,w,v);
        }


        }
        
        

        void createFinalRoom(int ver, int s, int RoomSizeX, int RoomSizeY){

            int r = ver - brickSize/2;
            int raumSize = RoomSizeY*brickSize;

            for(int i = 0; i > -RoomSizeY; i--){  //Boden und Decke

                for (int k  = -RoomSizeX; k<RoomSizeX; k++){

                    
                    int e = s;
                    int x = k*brickSize;
                    int y = i *brickSize;

                    Vector3 a = new Vector3(-brickSize/2+x+r, 0, e+y);
                    Vector3 b = new Vector3(brickSize/2+x+r,0,e+0+y);
                    Vector3 c = new Vector3(brickSize/2+x+r,0,e+brickSize+y);
                    Vector3 d = new Vector3(-brickSize/2+x+r,0,e+brickSize+y);

                    Vector3 l = new Vector3(-brickSize/2+x+r, brickSize, e+y);  //Decke
                    Vector3 m = new Vector3(brickSize/2+x+r,brickSize,e+0+y);
                    Vector3 n = new Vector3(brickSize/2+x+r,brickSize,e+brickSize+y);
                    Vector3 o = new Vector3(-brickSize/2+x+r,brickSize,e+brickSize+y);

                    Face(o,n,m,l);
                    Face(a,b,c,d);
                }
            }


            for(int i = -RoomSizeX; i<RoomSizeX; i++){  // Wände, paralell zur x achse  Insgesamt 4 Stück, Mittlere Wand = 2 

                int  x = i*brickSize;
                int e = s+brickSize;

            if( i == 0){  //EingangsTür

            //mittlere Wand soll dort aber keine Tür haben

            Vector3 t  = new Vector3(-brickSize/2+x+r,0,e-raumSize/2);
            Vector3 u = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize/2);
            Vector3 v = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize/2);
            Vector3 w = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize/2);

            Vector3 t1  = new Vector3(-brickSize/2+x+r,0,e-raumSize/2-brickSize);
            Vector3 u1 = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize/2-brickSize);
            Vector3 v1 = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize/2-brickSize);
            Vector3 w1 = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize/2-brickSize);

            Face(t,u,w,v);
            Face(v1,w1,u1,t1);

            }else if(i == 6){  //Tür in der Mittleren Wand

            Vector3 a  = new Vector3(-brickSize/2+x+r,0,e);
            Vector3 b = new Vector3(-brickSize/2+x+r, brickSize, e);
            Vector3 c = new Vector3(-brickSize/2+brickSize+x+r,0, e);
            Vector3 d = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e);


            Vector3 t2  = new Vector3(-brickSize/2+x+r,0,e-raumSize);                                      //Endwand seite 1
            Vector3 u2 = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize);
            Vector3 v2 = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize);
            Vector3 w2 = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize);


            Face(t2,u2,w2,v2);
            Face(c,d,b,a);


            }else if(i == -6){ //2. Tür in der Mittleren Wand

            Vector3 a  = new Vector3(-brickSize/2+x+r,0,e);
            Vector3 b = new Vector3(-brickSize/2+x+r, brickSize, e);
            Vector3 c = new Vector3(-brickSize/2+brickSize+x+r,0, e);
            Vector3 d = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e);

            
            Vector3 t2  = new Vector3(-brickSize/2+x+r,0,e-raumSize);                                      //Endwand seite 1
            Vector3 u2 = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize);
            Vector3 v2 = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize);
            Vector3 w2 = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize);



            Face(t2,u2,w2,v2);
            Face(c,d,b,a);

            } else{

            Vector3 a  = new Vector3(-brickSize/2+x+r,0,e);                                             //erste Wand
            Vector3 b = new Vector3(-brickSize/2+x+r, brickSize, e);
            Vector3 c = new Vector3(-brickSize/2+brickSize+x+r,0, e);
            Vector3 d = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e);



            Vector3 t  = new Vector3(-brickSize/2+x+r,0,e-raumSize/2);                                      //mittlere Wand seite 1
            Vector3 u = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize/2);
            Vector3 v = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize/2);
            Vector3 w = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize/2);

            
            Vector3 t1  = new Vector3(-brickSize/2+x+r,0,e-raumSize/2-brickSize);                           //mittlere Wand Seite 2
            Vector3 u1 = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize/2-brickSize);
            Vector3 v1 = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize/2-brickSize);
            Vector3 w1 = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize/2-brickSize);



            Vector3 t2  = new Vector3(-brickSize/2+x+r,0,e-raumSize);                                      //Endwand seite 1
            Vector3 u2 = new Vector3(-brickSize/2+x+r, brickSize, e-raumSize);
            Vector3 v2 = new Vector3(-brickSize/2+brickSize+x+r,0, e-raumSize);
            Vector3 w2 = new Vector3(-brickSize/2+brickSize+x+r, brickSize, e-raumSize);            


            Face(v1,w1,u1,t1);
            Face(t2,u2,w2,v2);
            //Face(t1,u1,w1,v1);
            Face(t,u,w,v);
            Face(c,d,b,a);
            

            }
            
            }



        for(int i = 0; i<SizeY; i++){

            int x = i*brickSize;
            int width = SizeX*brickSize - r;

            int test = r + SizeX*brickSize;
            //leftside

        Vector3 a = new Vector3 (-brickSize/2-width, brickSize, brickSize-x+s);  
        Vector3 b = new Vector3 (-brickSize/2-width, 0, brickSize-x+s); 
        Vector3 c = new Vector3 (-brickSize/2-width, 0, 0-x+s); 
        Vector3 d = new Vector3 (-brickSize/2-width, brickSize, 0-x+s);
            
            //rightside

        Vector3 a1 = new Vector3 (-brickSize/2+test, brickSize, brickSize-x+s);  
        Vector3 b1 = new Vector3 (-brickSize/2+test, 0, brickSize-x+s); 
        Vector3 c1 = new Vector3 (-brickSize/2+test, 0, 0-x+s); 
        Vector3 d1 = new Vector3 (-brickSize/2+test, brickSize, 0-x+s);

        Face(d,c,b,a);
        Face(a1,b1,c1,d1);

        }


    }



    private void Face(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
        

    Vector3 normal = GetNormal(a,b,c);

    vertices.Add(a); normals.Add(normal); uvs.Add(new Vector2(0f,0f));
    vertices.Add(b); normals.Add(normal); uvs.Add(new Vector2(1f,0f));
    vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f,1f));
    vertices.Add(d); normals.Add(normal); uvs.Add(new Vector2(0f,1f));

    vertices.Add(a); normals.Add(normal);uvs.Add(new Vector2(0f,0f));    
    vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f,1f));



    triangles.Add(index + 2);
    triangles.Add(index + 1);
    triangles.Add(index + 0);

    triangles.Add(index +4);
    triangles.Add(index +3);
    triangles.Add(index +5);

    index= index +6;
    



}

private void smallFace(Vector3 a, Vector3 b, Vector3 c, Vector3 d){   //Für die Stellen andenen nur ein Halber block gebraucht wird -> Textur auch nur bis 0.5
        

    Vector3 normal = GetNormal(a,b,c);

    vertices.Add(a); normals.Add(normal); uvs.Add(new Vector2(0f,0f));
    vertices.Add(b); normals.Add(normal); uvs.Add(new Vector2(1f,0f));
    vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f,0.5f));
    vertices.Add(d); normals.Add(normal); uvs.Add(new Vector2(0f,0.5f));

    vertices.Add(a); normals.Add(normal);uvs.Add(new Vector2(0f,0f));    
    vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f,0.5f));



    triangles.Add(index + 2);
    triangles.Add(index + 1);
    triangles.Add(index + 0);

    triangles.Add(index +4);
    triangles.Add(index +3);
    triangles.Add(index +5);

    index= index +6;
    



}


private Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c){

        Vector3 E1 = c -a;
        Vector3 E2 = b-a;

        return Vector3.Cross(E1, E2).normalized;


    }



private void CreateLevel(){

    Debug.Log("CreateLevel wird ausgeführt!");
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
      //  mesh.normals = normals.ToArray();
        mesh.RecalculateNormals();
        mesh.Optimize();

        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = mesh;
}

}