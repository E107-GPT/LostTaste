using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseController : MonoBehaviour
{
	protected Animator _animator;
	protected Rigidbody _rigidbody;
	protected NavMeshAgent _agent;
	


	[SerializeField]
    protected Define.State _state = Define.State.Idle;

	protected SMachine _statemachine;

	public State CurState
    {
        get { return _statemachine.CurState; }
		set { CurState = value; }
    }

    private static long enemy_ID = 0;
	private long id;
	[SerializeField]
	protected string enemyEntityName;
	private string personalColor;

	public long ID
	{
		set
		{
			id = value;
			enemy_ID++;
		}
		get
		{
			return id;
		}
	}

	private void Start()
	{
		_statemachine = new SMachine();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();
		_agent = GetComponent<NavMeshAgent>();
		Init();
	}
	void Update()
	{
		_statemachine.Update();
	}

	//public abstract void Updated();
	public virtual void Setup(string name)
	{
		// id, �̸�, ���� ����
		ID = enemy_ID;
		enemyEntityName = name;
		int color = Random.Range(0, 1000000);
		personalColor = $"#{color.ToString("X6")}";
	}
	public void PrintText(string text)
	{
		Debug.Log($"<color={personalColor}><b>{enemyEntityName}</b></color> : {text}");
	}

	public abstract void Init();

	public virtual void EnterIdle() { }

	public virtual void ExcuteIdle() { }

	public virtual void ExitIdle() { }

	public virtual void EnterDie() { }
	public virtual void ExcuteDie() { }

	public virtual void ExitDie() { }
	public virtual void EnterSkill() { }
	public virtual void ExcuteSkill() { }

	public virtual void ExitSkill() { }
	public virtual void EnterMove() { }
	public virtual void ExcuteMove() { }

	public virtual void ExitMove() { }
	public virtual void EnterDash() { }
	public virtual void ExcuteDash() { }

	public virtual void ExitDash() { }

}
