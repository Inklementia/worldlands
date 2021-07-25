using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Width;
    public float Height;

    public int X;
    public int Y;


    [SerializeField] private BoxCollider2D roomCollider;

    [Header("Doors")]
    public Door LeftDoor;
    public Door RightDoor;
    public Door TopDoor;
    public Door BottomDoor;

    public List<Door> roomDoors = new List<Door>();  // hide

    private bool _udpatedDoors;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
    private void Awake()
    {
        Camera cam = Camera.main;
        Height = 2f * cam.orthographicSize;
        Width = Height * cam.aspect;
        roomCollider.size = new Vector2(Width, Height);
    
    }
    private void Start()
    {
        if(RoomManager.instance == null)
        {
            //wrong scene
            Debug.Log("You pressed PLAY in the wrong scene");
            return;
        }

     
        Door[] doors = GetComponentsInChildren<Door>();

        foreach(Door door in doors)
        {
            roomDoors.Add(door);
            switch (door.DoorType)
            {
                case DoorType.Left:
                    LeftDoor = door;
                    break;
                case DoorType.Right:
                    RightDoor = door;
                    break;
                case DoorType.Top:
                    TopDoor = door;
                    break;
                case DoorType.Bottom:
                    BottomDoor = door;
                    break;
            }
        }
        RoomManager.instance.RegisterRoom(this);
        
    }
    private void Update()
    {
        if(name.Contains("End") && !_udpatedDoors)
        {
            RemoveUnconnectedDoors();
            _udpatedDoors = true;

        }    
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in roomDoors)
        {
            switch (door.DoorType)
            {
                case DoorType.Left:

                    if (GetLeftRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Right:

                    if (GetRightRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Top:

                    if (GetTopRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Bottom:

                    if (GetBottomRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;

            }
        }
    }

    public void RemoveUnconnectedDoorsForRoom(Room room)
    {
        foreach (Door door in room.roomDoors)
        {
            switch (door.DoorType)
            {
                case DoorType.Left:

                    if (GetLeftRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Right:

                    if (GetRightRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Top:

                    if (GetTopRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;
                case DoorType.Bottom:

                    if (GetBottomRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }

                    break;

            }
        }
    }
    public void AddNecessaryDoorsToPreviousRooms(List<Room> prevRooms, Room currRoom)
    {
        foreach (Room pr in prevRooms)
        {
        
                if (currRoom.Y < pr.Y && RoomManager.instance.FindRoom(currRoom.X, currRoom.Y + 1) != null)
                {
                    pr.BottomDoor.gameObject.SetActive(true);
                }

                if (currRoom.Y > pr.Y && RoomManager.instance.FindRoom(currRoom.X, currRoom.Y - 1) != null)
                {
                    pr.TopDoor.gameObject.SetActive(true);
                }

                if (currRoom.X < pr.X && RoomManager.instance.FindRoom(currRoom.X + 1, currRoom.Y) != null)
                {
                    pr.LeftDoor.gameObject.SetActive(true);
                }

                if (currRoom.X > pr.X && RoomManager.instance.FindRoom(currRoom.X - 1, currRoom.Y) != null)
            {
                    pr.RightDoor.gameObject.SetActive(true);
                }
            
      
        }
        

    }
    public Room GetRightRoom()
    {
        if(RoomManager.instance.CheckIfRoomExist(X + 1, Y))
        {

            return RoomManager.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeftRoom()
    {
        if (RoomManager.instance.CheckIfRoomExist(X - 1, Y))
        {

            return RoomManager.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTopRoom()
    {
        if (RoomManager.instance.CheckIfRoomExist(X, Y + 1))
        {

            return RoomManager.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottomRoom()
    {
        if (RoomManager.instance.CheckIfRoomExist(X, Y - 1))
        {

            return RoomManager.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    //TODO: change to Tags
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            RoomManager.instance.OnPlayerEnterRoom(this);
        }
    }
}
