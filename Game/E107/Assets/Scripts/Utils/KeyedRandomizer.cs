using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Ű(key)�� ���� ������ ���� ������ �� �ִ� ���� ���� ������(pseudo-RNG)�Դϴ�.
/// </summary>
/// <remarks>
///     ȣ�� Ƚ���� �����ϴ� �������� RNG�� �޸�, Ű(key)�� ���� ������ �������� ���� ���ҿ� ������ �� �ֽ��ϴ�.<br />
/// 
///     �ڼ��� ������ <see href="https://www.notion.so/240308-86e96526f4214ee3a886db561be31ff8">Notion ����</see>�� �����ϼ���.
/// </remarks>
/// <seealso href="https://www.notion.so/240308-86e96526f4214ee3a886db561be31ff8"/>
public class KeyedRandomizer
{
    private const int BAG_SIZE = 32768;

    private readonly int[] _bag = new int[BAG_SIZE];

    /// <summary>
    /// Ű ��� RNG�� �����մϴ�.
    /// </summary>
    /// <param name="seed">RNG �õ尪</param>
    public KeyedRandomizer(int seed)
    {
        System.Random rng = new System.Random(seed);

        for (int i=0; i<BAG_SIZE; i++)
        {
            _bag[i] = rng.Next();
        }
    }

    /// <summary>
    /// 0 �̻� <see cref="int.MaxValue"></see> �̸� ���� ���� <c>int</c>�� ������ ����ϴ�.
    /// </summary>
    /// <param name="key">���� Ű</param>
    /// <returns>���� ���� ����</returns>
    public int GetRaw(int key)
    {
        if (key < 0 || key >= BAG_SIZE)
        {
            throw new ArgumentOutOfRangeException("key");
        }
        return _bag[key];
    }

    /// <summary>
    /// Ư�� ���� ���� <c>int</c>�� ������ ����ϴ�.
    /// </summary>
    /// <param name="key">���� Ű</param>
    /// <param name="startInclusive">������ ���� ���� (������ ����)</param>
    /// <param name="endExclusive">������ ���� �� (�������� ����)</param>
    /// <returns>���� ���� ����</returns>
    /// <exception cref="ArgumentOutOfRangeException">start�� end���� �� ŭ</exception>
    public int GetInt(int key, int startInclusive, int endExclusive)
    {
        if (startInclusive >= endExclusive)
        {
            throw new ArgumentOutOfRangeException();
        }

        return GetRaw(key) % (endExclusive - startInclusive) + startInclusive;
    }

    /// <summary>
    /// 0.0 �̻� 1.0 �̸� ���� ���� <c>double</c>�� ������ ����ϴ�.
    /// </summary>
    /// <param name="key">���� Ű</param>
    /// <returns>0.0 ~ 1.0 ���� ���� ����</returns>
    public double GetDouble(int key)
    {
        return (double)GetRaw(key) / int.MaxValue;
    }

    /// <summary>
    /// Ư�� ���� ���� <c>double</c>�� ������ ����ϴ�.
    /// </summary>
    /// <param name="key">���� Ű</param>
    /// <param name="start">������ ���� ����</param>
    /// <param name="end">������ ���� ��</param>
    /// <returns>���� ���� ����</returns>
    /// <exception cref="ArgumentOutOfRangeException">start�� end���� �� ŭ</exception>
    public double GetDouble(int key, double start, double end)
    {
        if (start > end)
        {
            throw new ArgumentOutOfRangeException();
        }
        return GetDouble(key) * (end - start) + start;
    }

    /// <remarks>
    ///     <see cref="ProbabilityTable{E}"></see>�� ����� ������ ���Ҹ� ���� �� �ֽ��ϴ�.<br />
    ///     ���� ���, ������ ���� �ڵ�� ���� 50%�� Ȯ���� "dead" �Ǵ� "alive"�� ����ϴ�.
    ///     <code>
    ///     var krng = new KeyedRandomizer(123);
    ///     var schrodingersBox = new Probabilitytable&lt;string>() {
    ///         "dead": 0.5,
    ///         "alive": 0.5,
    ///     };
    ///     string result = krng.GetFromTable(123, schrodingersBox);
    ///     </code>
    /// </remarks>
    /// <param name="key">���� Ű</param>
    /// <param name="table">Ȯ�� ���̺�</param>
    /// <seealso cref="ProbabilityTable{E}"/>
    public E GetFromTable<E>(int key, ProbabilityTable<E> table)
    {
        double[] weights = table.Values.ToArray();
        int len = weights.Length;
        if (len == 0)
        {
            return default(E);
        }

        // weights�� ������ (accs[i] = weights[0] + weights[1] + ... + weights[i])
        double[] accs = new double[len];
        accs[0] = weights[0];
        for (int i=1; i<len; i++)
        {
            accs[i] = accs[i - 1] + weights[i];
        }

        double sum = accs[len - 1];
        double number = GetDouble(key, 0.0, sum);

        for (int i=0; i<len; i++)
        {
            if (number < accs[i])
            {
                return table.Keys.ToArray()[i];
            }
        }
        throw new ArithmeticException("���������� �Ұ����� ��찡 �Ͼ���ϴ�.");
    }
}

/// <summary>
///     Ȯ�� ���̺��Դϴ�.
/// </summary>
/// <remarks>
///     � ���ҿ� �� ���Ҹ� ���� Ȯ��(����ġ)�� ���ν�Ų �ڷᱸ���Դϴ�.
/// </remarks>
/// <typeparam name="E">Ȯ�� ���̺��� ���õ� ������ Ÿ��</typeparam>
public class ProbabilityTable<E> : Dictionary<E, double> {
    ///     <summary>
    ///     Ȯ�� ���̺��� ����ϴ�. ����:
    ///     <code>
    ///     var schrodingersBox = new Probabilitytable&lt;string>() {
    ///         "dead": 0.5,
    ///         "alive": 0.5,
    ///     };
    ///     </code>
    ///     ����ġ�� ��� ������ ����ġ �տ� ���� ��� �ݿ��˴ϴ�.
    ///     </summary>
    public ProbabilityTable() : base() { }
}