using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private GameObject[] _effectPrefabs = new GameObject[(int)Define.Effect.MaxCount];
    private Quaternion[] _effectQuaternion = new Quaternion[(int)Define.Effect.MaxCount]; 
    public void Init()
    {

        string[] effectNames = System.Enum.GetNames(typeof(Define.Effect));
        for(int i = 0; i < effectNames.Length - 1; i++)
        {
            GameObject go = Managers.Resource.Load<GameObject>($"Prefabs/Effects/{effectNames[i]}");
            Managers.Pool.CreatePool(go);
          
            _effectPrefabs[i] = go;
            _effectQuaternion[i] = go.transform.rotation;
        }
        
    }

    
    public ParticleSystem Play(Define.Effect effect, Transform starter)
    {
        GameObject original = _effectPrefabs[(int)effect];
        GameObject go = Managers.Resource.Instantiate($"Effects/{original.name}");

        go.transform.position = starter.position;
        go.transform.rotation = starter.rotation * _effectQuaternion[(int)effect];
        ParticleSystem ps = go.GetComponent<ParticleSystem>();

        ps.Play();

        return ps;
    }

    public void Stop(ParticleSystem ps)
    {
        ps.Stop();
        Managers.Resource.Destroy(ps.gameObject);
    }


}
