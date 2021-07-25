using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public List<Room> LoadedRooms = new List<Room>();

    private bool _isLoadingRoom;
    private bool _spawnedBossRoom;
    private bool _updatedRooms;

    private string _currentWorldName = "Basement";
    private RoomInfo _currentLoadRoomData;
    private Room _currentRoom;
    //public List<Room> _previousRooms = new List<Room>();
    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    [SerializeField] private DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, -1);
        //LoadRoom("Empty", 0, 1);

        Debug.Log(_currentLoadRoomData.X + ", " + _currentLoadRoomData.Y);
    }
    private void Update()
    {
        UpdateRoomQueue();

    }

    private void UpdateRoomQueue()
    {
        if (_isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!_spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (_spawnedBossRoom && !_updatedRooms)
            {
                foreach (Room room in LoadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                _updatedRooms = true;
            }
            return;
        }

        _currentLoadRoomData = loadRoomQueue.Dequeue();
        _isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(_currentLoadRoomData));
    }

    private IEnumerator SpawnBossRoom()
    {
        _spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            Room bossRoom = LoadedRooms[LoadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);

            var roomToRemove = LoadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            LoadedRooms.Remove(roomToRemove);

            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        //if room exist - skip
        if (CheckIfRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);

    }

    private IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = _currentWorldName + info.Name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!CheckIfRoomExist(_currentLoadRoomData.X, _currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3
            (
                _currentLoadRoomData.X * room.Width,
                _currentLoadRoomData.Y * room.Height,
                0
            );

            room.X = _currentLoadRoomData.X;
            room.Y = _currentLoadRoomData.Y;
            room.name = _currentWorldName + " - " + _currentLoadRoomData.Name + " (" + room.X + ", " + room.Y + ")";
            room.transform.parent = transform;

            Debug.Log(room.name);
            Debug.Log(_currentLoadRoomData.X * room.Width + ", " + _currentLoadRoomData.Y * room.Height);

            _isLoadingRoom = false;


            // if no room was loaded
            if (LoadedRooms.Count == 0)
            {
                CameraManager.instance.FocusedRoom = room;
            }

            LoadedRooms.Add(room);


            //room.RemoveUnconnectedDoors();

            //_currentRoom = room;
            // not start
            //if(roomCount != 0)
            //{
            //    _previousRooms.Add(LoadedRooms[roomCount - 1]);
            //    room.AddNecessaryDoorsToPreviousRooms(_previousRooms, _currentRoom);
            //}


            //roomCount++;
            //Debug.Log(dungeonGenerator.GeneratedRoomsCount + " rooms");
            //if (LoadedRooms.Count >= dungeonGenerator.GeneratedRoomsCount)
            //{
            //    Debug.Log("Configuring doors for " + dungeonGenerator.GeneratedRoomsCount + " rooms");
            //    ConfigureDoors();
            //}

        }
        else
        {
            Destroy(room.gameObject);
            _isLoadingRoom = false;

        }
    }

    //private void ConfigureDoors()
    //{
    //    foreach (Room room in LoadedRooms)
    //    {
    //        room.RemoveUnconnectedDoors(room);
    //    }
    //}

    public bool CheckIfRoomExist(int x, int y)
    {
        return LoadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }


    public RoomInfo GetCurrentRoomData()
    {
        return _currentLoadRoomData;
    }
    public Room FindRoom(int x, int y)
    {
        //Debug.Log("Exists: " + x + ", " + y);
        return LoadedRooms.Find(item => item.X == x && item.Y == y);
    }

    //mb event
    public void OnPlayerEnterRoom(Room room)
    {
        CameraManager.instance.FocusedRoom = room;
        _currentRoom = room;
    }

 
}
