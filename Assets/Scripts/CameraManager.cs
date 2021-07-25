using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

   

    [SerializeField] private float moveSpeedForRoomChange;

    public Room FocusedRoom;

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
        
    }

    private void Update()
    {
        UpdatePosition();
        // mb chnage to -> switch camera pos when enter/exit door
    }

    private void UpdatePosition()
    {
        if(FocusedRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();
   
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedForRoomChange);
    }

    private Vector3 GetCameraTargetPosition()
    {
        if (FocusedRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = FocusedRoom.GetRoomCentre();
        targetPos.z = transform.position.z;

        return targetPos;
    }

   public bool CheckIfSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
