using DG.Tweening;
using MoreMountains.Feedbacks;
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
            material.DOFloat(3, "_GlitchAmount", .3f);
            material.DOFloat(0, "_GlitchAmount", .3f);
        }
    }
}
}