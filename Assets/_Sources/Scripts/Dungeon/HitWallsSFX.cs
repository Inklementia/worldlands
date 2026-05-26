using DG.Tweening;
using UnityEngine;

namespace _Sources.Scripts.Dungeon 
{

public class HitWallsSFX : MonoBehaviour
{
    [SerializeField] private Tag triggerTag;
    [SerializeField] private Material material;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.HasTag(triggerTag))
        {
            //smth
        }
    }
}
}