using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH_LevelBuilder : MonoBehaviour
{
    public MH_Room StartRoomPrefab, EndRoomPrefab;
    public List<MH_Room> roomPrefabs = new List<MH_Room>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<MH_Doorway> availableDoorways = new List<MH_Doorway>();
    MH_Startroom startRoom;
    MH_Endroom endRoom;
    List<MH_Room> placedRooms = new List<MH_Room>();

    LayerMask roomLayerMask;

    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }
    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();
        yield return startup;

        //startraum wird erstellt
        PlaceStartRoom();
        yield return interval;

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for (int i = 0; i < iterations; i++)
        {
            //plaziert ein raum von der liste
            Debug.Log("placed random room from list");
            PlaceRoom();
            yield return interval;
        }
        Debug.Log("Placed end room");
        yield return interval;

        Debug.Log("level generation completted");
        //yield return new WaitForSeconds(3);
        //ResetLevel();
    }

    //startraum wird gesetzt/erstellt
    void PlaceStartRoom()
    {
        Debug.Log("Placed StartRoom");
        startRoom = Instantiate(StartRoomPrefab) as MH_Startroom;
        startRoom.transform.parent = this.transform;

        // Get Doorways and add them to List
        AddDoorwaysToList(startRoom, ref availableDoorways);

        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }
    void AddDoorwaysToList(MH_Room room, ref List<MH_Doorway> list)
    {
        foreach (MH_Doorway doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }
    //räume/levelfragmente werden erstellt --> Geht noch nicht ganz
    void PlaceRoom()
    {
        Debug.Log("Placed Room");
        MH_Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as MH_Room;
        currentRoom.transform.parent = this.transform;

        List<MH_Doorway> allAvailableDoorways = new List<MH_Doorway>(availableDoorways);
        List<MH_Doorway> currentRoomDoorways = new List<MH_Doorway>();
        AddDoorwaysToList(currentRoom, ref currentRoomDoorways);
        //bekommt alles doorways und fügt diese random zur liste der verfügbaren doorways hinzu
        AddDoorwaysToList(currentRoom, ref availableDoorways);
        bool roomPlaced = false;
        //geht alle verfügbaren doorways durch
        foreach (MH_Doorway availableDoorway in allAvailableDoorways)
        {
            //geht alle verfügbaren doorways im jetztigen raum durch
            foreach (MH_Doorway currentDoorway in currentRoomDoorways)
            {
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
                //prüft auf overlap
                if (CheckRoomOverlap(currentRoom))
                {
                    continue;
                }
                roomPlaced = true;
                //fügt raum zur liste hinzu
                placedRooms.Add(currentRoom);
                //entfernt entsprechende doorways
                currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);
                //bricht die schleife ab wenn der raum geplaced wurde
                break;
            }
            //bricht die schleife ab wenn der raum geplaced wurde
            if (roomPlaced)
            {
                break;
            }
        }
        //raum konnte nicht platziert werden, levelabschnitt wird zurück gesetzt und es wird erneut versucht.
        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevel();
        } 
    }

    //Berechenung für Rotation des Raums
    void PositionRoomAtDoorway(ref MH_Room room, MH_Doorway roomDoorway, MH_Doorway targetDoorway)
    {
        //reset für position und rotation
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;
        //rotiert den raum damit, er passend zum doorway ist
        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = room.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);
        //positionierung für den raum
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
        room.transform.position = targetDoorway.transform.position - roomPositionOffset;
    }

    //Es wird geschaut ob sich die Räume overlapen
    bool CheckRoomOverlap(MH_Room room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
        if (colliders.Length > 0)
        {
            //ignoriert collider mit jetzigen raum
            foreach (Collider c in colliders)
            {
                if (c.transform.parent.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    return true;
                }
            }
        }
        return false;
    }

    
    void PlaceEndRoom()
    {
        //Instatiate Room
        endRoom = Instantiate(EndRoomPrefab) as MH_Endroom;
        endRoom.transform.parent = this.transform;

        //Doorway Schleife zzm durchlaufen
        List<MH_Doorway> allAvailableDoorways = new List<MH_Doorway>(availableDoorways);
        MH_Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;
        //Versucht alle verfügbaren Doorways
        foreach (MH_Doorway availableDoorway in allAvailableDoorways)
        {
            MH_Room room = (MH_Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);
            //Prüft ob sich der Room overlapt
            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }
            roomPlaced = true;

            doorway.gameObject.SetActive(false);
            availableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);
            break;
        }
        if (!roomPlaced){        
            ResetLevel();
        } 
    }

    //Level wird zurücksetzt
    void ResetLevel()
    {
        Debug.LogError("Reset level generator");
        StopCoroutine("GenerateLevel");
        //startraum wird zerstört
        if (startRoom)
        {
            Destroy(startRoom.gameObject);
        }
        //endraum wird zerstört
        if (endRoom)
        {
            Destroy(endRoom.gameObject);
        }
        //räume werden zerstört
        foreach (MH_Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }
        //aufräumen der listen
        placedRooms.Clear();
        availableDoorways.Clear();
        //Startet generate Level
        StartCoroutine("GenerateLevel");
    }
}
