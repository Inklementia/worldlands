using UnityEngine;

namespace _Sources.Scripts.Helpers
{
    public class OpenUrl : MonoBehaviour
    {
        [TextArea(2,50)]
        [SerializeField] private string _myUrl;

        public void OpenMyUrl()
        {
            Application.OpenURL(_myUrl);
        }
    }
}