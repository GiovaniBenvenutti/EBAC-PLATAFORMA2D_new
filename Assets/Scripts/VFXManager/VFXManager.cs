using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        WALK,
        VFX2
    }

    public List<VFXManagerSetup> vfxSetups;

    public void PlayVfxByType(VFXType vfxType, Vector3 position)
    {
        foreach (var i in vfxSetups)
        {
            if (i.vfxType == vfxType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 2f);
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}