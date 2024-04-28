using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SegSpawner : MonoBehaviour
{
    [SerializeField] private int _numberOfRoomsInSegment; //default is 3
    [SerializeField] private List<RoomType> _roomTypes = new();
    [SerializeField] private GameObject _corridorPrefab;
    private List<GameObject> _roomsInSegment = new();
    private List<GameObject> _corridorsInSegment = new();
    private List<GameObject> _connectingCorridors = new();
    private List<GameObject> _previousSegmentCorridors = new();
    private List<GameObject> _previousSegmentRooms = new();
    private List<GameObject> _previousConnectingCorridors = new();

    public static SegSpawner Instance;

    private Vector3 _finalUpperMovement = new();

    private int? _connectionIndex = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        SpawnSegment(true);
        SpawnSegment();
        Debug.Log(_roomsInSegment[0].transform.position);
        Debug.Log(_previousSegmentRooms[0].transform.position);
    }

    public void DisableColliders()
    {
        foreach (var room in _roomsInSegment)
        {
            room.GetComponent<Collider>().enabled = false;
        }
        
    }
    
    public void RemovePreviousSegment()
    {
        foreach (var room in _previousSegmentRooms)
        {
            Debug.Log("hi");
            Debug.Log(room.transform.position); 
            room.SetActive(false);
            
        }

        foreach (GameObject corridor in _previousSegmentCorridors) corridor.SetActive(false);

        foreach (GameObject corridor in _previousConnectingCorridors) corridor.SetActive(false);
        
        _previousConnectingCorridors = new(_connectingCorridors);
        _previousSegmentCorridors = new(_corridorsInSegment);
        _previousSegmentRooms = new(_roomsInSegment);
        _connectingCorridors.Clear();
        _corridorsInSegment.Clear();
        _roomsInSegment.Clear();
    }

    private GameObject FindDirectionDoor(string direction, GameObject roomToFindDoors)
    {
        // Up
        // Down
        // Left
        // Right
        GameObject directionDoor = null;

        foreach (Transform child in roomToFindDoors.transform)
        {
            if (child.CompareTag(direction + "Door"))
            {
                directionDoor = child.gameObject;
                break;
            }
        }
        return directionDoor;
    }

    public void SpawnSegment(bool isStarter = false)
    {
        // distance from center to door + length of corridor
        
        float roomsOffset = 200f;
        Vector3 roomMovement = new Vector3();
        Vector3 distanceToUpperDoor = new Vector3();
        Vector3 distanceToDoor = new Vector3();
        Vector3 distanceToLowerDoor = new Vector3();

        for (int roomIndex = 0; roomIndex < _numberOfRoomsInSegment; roomIndex++)
        {


            int randomNum = Random.Range(0, _roomTypes.Count);
            RoomType randomRoom = _roomTypes[randomNum];

            if (roomIndex == 0) roomMovement = Vector3.zero;
            else
            {
                float endOfCorridorX = _corridorsInSegment[roomIndex - 1].transform.position.x -
                                       _corridorsInSegment[roomIndex - 1].GetComponent<Collider>().bounds.size.x / 2;

                Vector3 endOfCorridor = new Vector3(endOfCorridorX, 0,
                    _corridorsInSegment[roomIndex - 1].transform.position.z);

                GameObject rightDoor = FindDirectionDoor("Right", randomRoom.RoomPrefab);

                Vector3 distanceToCenter = rightDoor.transform.position + randomRoom.RoomPrefab.transform.position;

                roomMovement = endOfCorridor - distanceToCenter;
            }

            //if the currentSegment is zero, the spawn it at 0 coordinates,
            //but if the currentSegment is something else, then move it far away so that the
            //rooms don't collide with each other when spawning the next segment, then change the transform to fit it
            //the roomsOffset values are arbitrary values
            

            GameObject room = Instantiate(randomRoom.RoomPrefab,
                //disgusting ternary please end my suffering
                new Vector3(roomMovement.x + (roomIndex == 0 ? (isStarter ? 0 : roomsOffset) : 0), 0, roomMovement.z),
                Quaternion.identity);
            _roomsInSegment.Add(room);

            // if we're spawning the last room
            if (roomIndex == _numberOfRoomsInSegment - 1) continue;
            GameObject leftDoor = FindDirectionDoor("Left", room);
            distanceToDoor = room.transform.localPosition + leftDoor.transform.localPosition;

            //set initial position to zero, then set it to new because we need the collider to calculate it
            GameObject corridor = Instantiate(_corridorPrefab, Vector3.zero, Quaternion.Euler(0, 90, 0));
            corridor.transform.position = new Vector3(
                distanceToDoor.x - corridor.GetComponent<Collider>().bounds.size.x / 2, 0,
                distanceToDoor.z);
            
            _corridorsInSegment.Add(corridor);
        }

        if (_connectionIndex != null)
        {
            GameObject wantedRoom = _roomsInSegment[(int) _connectionIndex];
            GameObject downDoor = FindDirectionDoor("Down", wantedRoom);
            distanceToLowerDoor = wantedRoom.transform.localPosition + downDoor.transform.localPosition;
            
            for (int roomIndex = 0; roomIndex < _roomsInSegment.Count; roomIndex++)
            {
                _roomsInSegment[roomIndex].transform.position = new Vector3(
                    _roomsInSegment[roomIndex].transform.position.x - distanceToLowerDoor.x + _finalUpperMovement.x, 0,
                    _roomsInSegment[roomIndex].transform.position.z - distanceToLowerDoor.z + _finalUpperMovement.z);
                
            }

             for (int corridorIndex = 0; corridorIndex < _corridorsInSegment.Count; corridorIndex++)
             {
                 _corridorsInSegment[corridorIndex].transform.position = new Vector3(
                     _corridorsInSegment[corridorIndex].transform.position.x - distanceToLowerDoor.x + _finalUpperMovement.x, 0,
                     _corridorsInSegment[corridorIndex].transform.position.z - distanceToLowerDoor.z + _finalUpperMovement.z);
             }
        }

        _connectionIndex = Random.Range(0, _roomsInSegment.Count);

        GameObject randRoomInSegment = _roomsInSegment[(int) _connectionIndex];

        GameObject upDoor = FindDirectionDoor("Up", randRoomInSegment);

        distanceToUpperDoor = randRoomInSegment.transform.localPosition + upDoor.transform.localPosition;

        for (int amountOfCorridors = 0; amountOfCorridors < 4; amountOfCorridors++)
        {
            GameObject upperCorridor = Instantiate(_corridorPrefab, new Vector3(-100, -100, -100), Quaternion.identity);
            _connectingCorridors.Add(upperCorridor);
            
            upperCorridor.transform.position =
                new Vector3(distanceToUpperDoor.x, 0,
                    distanceToUpperDoor.z + upperCorridor.GetComponent<Collider>().bounds.size.z / 2);
        
            distanceToUpperDoor.z += upperCorridor.GetComponent<Collider>().bounds.size.z;
        }

        _finalUpperMovement = distanceToUpperDoor;

        if (!isStarter) return;
        DisableColliders();
        _previousConnectingCorridors = new (_connectingCorridors);
        _previousSegmentCorridors = new (_corridorsInSegment);
        _previousSegmentRooms = new (_roomsInSegment);
        
        Debug.Log("in function " + _previousSegmentRooms[0].transform.position);
        
        _connectingCorridors.Clear();
        _corridorsInSegment.Clear();
        _roomsInSegment.Clear();
    }
}
