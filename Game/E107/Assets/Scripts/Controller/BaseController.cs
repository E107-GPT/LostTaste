using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseController : MonoBehaviour
{
	protected Animator _animator;
	protected Rigidbody _rigidbody;
	protected NavMeshAgent _agent;
	// 공격자의 마지막 공격 시간을 저장하는 사전
	protected Dictionary<int, float> lastAttackTimes = new Dictionary<int, float>();
	protected float damageCooldown = 0.3f; // 피해를 다시 받기까지의 대기 시간(초)


	protected StateMachine _statemachine;

    public State CurState
    {
        get { return _statemachine.CurState; }
		set { CurState = value; }
    }

    private static long entity_ID = 0;
	private long id;
	[SerializeField]
	protected string entityName;
	private string personalColor;

	public long ID
	{
		set
		{
			id = value;
            entity_ID++;
		}
		get
		{
			return id;
		}
	}

	private void Awake()
	{
		_statemachine = new StateMachine();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();
		_agent = GetComponent<NavMeshAgent>();
		Init();
	}

    private void Start()
    {
		
    }
    void Update()
	{
		_statemachine.Execute();
	}

    //public abstract void Updated();
    public virtual void Setup(string name)
	{
		// id, 이름, 색상 설정
		ID = entity_ID;
        entityName = name;
		int color = Random.Range(0, 1000000);
		personalColor = $"#{color.ToString("X6")}";
	}
	public void PrintText(string text)
	{
		Debug.Log($"<color={personalColor}><b>{entityName}</b></color> : {text}");
	}


	public abstract void Init();

	public virtual void TakeDamage(int skillObjectId, int damage) {
		Debug.Log($"{gameObject.name} is damaged {damage} by {skillObjectId}");
	
	}

	// IDLE
	public virtual void EnterIdle() { }
	public virtual void ExcuteIdle() { }
	public virtual void ExitIdle() { }

	// DIE
	public virtual void EnterDie() { }
	public virtual void ExcuteDie() { }
	public virtual void ExitDie() { }

	// SKILL
	public virtual void EnterSkill() { }
	public virtual void ExcuteSkill() { }
	public virtual void ExitSkill() { }

	// MOVE
	public virtual void EnterMove() { }
	public virtual void ExcuteMove() { }
	public virtual void ExitMove() { }

	// DASH
	public virtual void EnterDash() { }
	public virtual void ExcuteDash() { }
	public virtual void ExitDash() { }

	// DrillDuck Slide
	public virtual void EnterSlide() { }
	public virtual void ExcuteSlide() { }
	public virtual void ExitSlide() { }
}
