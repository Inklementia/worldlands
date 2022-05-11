using UnityEngine;
using UnityEngine.EventSystems;

namespace _Sources.Scripts.Input
{
    public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector]
        public Vector2 TouchDist;
        [HideInInspector]
        public Vector2 PointerOld;
        [HideInInspector]
        protected int PointerId;
        [HideInInspector]
        public bool Pressed;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Pressed)
            {
                if (PointerId >= 0 && PointerId < UnityEngine.Input.touches.Length)
                {
                    TouchDist = UnityEngine.Input.touches[PointerId].position - PointerOld;
                    PointerOld = UnityEngine.Input.touches[PointerId].position;
                }
                else
                {
                    TouchDist = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y) - PointerOld;
                    PointerOld = UnityEngine.Input.mousePosition;
                }
            }
            else
            {
                TouchDist = new Vector2();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            PointerId = eventData.pointerId;
            PointerOld = eventData.position;
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    
   
    }
}