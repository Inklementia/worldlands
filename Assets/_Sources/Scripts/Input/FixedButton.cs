using UnityEngine;
using UnityEngine.EventSystems;

namespace _Sources.Scripts.Input
{
    public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        [HideInInspector]
        public bool Pressed;
        [HideInInspector]
        public bool Clicked;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
            Clicked = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked = true;
        }


    }
}
