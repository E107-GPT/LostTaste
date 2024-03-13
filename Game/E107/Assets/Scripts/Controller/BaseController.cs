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
	protected Vector3 _destPos;

	[SerializeField]
	protected Define.State _state = Define.State.Idle;

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

	public virtual Define.State State
	{
		get { return _state; }
		set
		{
			_state = value;

			Animator anim = GetComponent<Animator>();
			switch (_state)
			{
				case Define.State.Die:
					break;
				case Define.State.Idle:
					anim.CrossFade("WAIT", 0.1f);
					break;
				case Define.State.Moving:
					anim.CrossFade("RUN", 0.1f);
					break;
				case Define.State.Skill:
					anim.CrossFade("ATTACK", 0.1f, -1, 0);
					break;
				case Define.State.Dash:
					anim.CrossFade("DASH", 0.1f, -1, 0);
					break;

			}
		}
	}

	private void Start()
	{
		Init();
	}
	void Update()
	{

		//switch (State)
		//{
		//	case Define.State.Die:
		//		UpdateDie();
		//		break;
		//	case Define.State.Moving:
		//		UpdateMoving();
		//		break;
		//	case Define.State.Idle:
		//		UpdateIdle();
		//		break;
		//	case Define.State.Skill:
		//		UpdateSkill();
		//		break;
		//	case Define.State.Dash:
		//		UpdateDash();
		//		break;
		//}
		Updated();
	}
	public abstract void Updated();
	public virtual void Setup(string name)
	{
		// id, 이름, 색상 설정
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

	protected virtual void UpdateDie() { }
	protected virtual void UpdateMoving() { }
	protected virtual void UpdateIdle() { }
	protected virtual void UpdateSkill() { }
	protected virtual void UpdateDash() {}
}
