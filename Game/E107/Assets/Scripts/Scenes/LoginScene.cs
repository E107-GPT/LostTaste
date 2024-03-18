using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        //Managers.Resource.Instantiate("UnityChan");

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    public override void Clear()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Login Scene Clear !");
    }
}
