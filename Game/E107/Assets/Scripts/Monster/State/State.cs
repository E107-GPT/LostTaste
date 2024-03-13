using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class
{
    // �ش� ���¸� ������ �� 1ȸ ȣ��
    public abstract void Enter(T entity);

    // �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
    public abstract void Execute(T entity);

    // �ش� ���¸� ������ �� 1ȸ ȣ��
    public abstract void Exit(T entity);
}
