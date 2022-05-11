using _Sources.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService

    {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 place);
    }
}