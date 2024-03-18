using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     키(key)를 통해 일정한 값을 재현할 수 있는 유사 난수 생성기(pseudo-RNG)입니다.
/// </summary>
/// <remarks>
///     호출 횟수에 의존하는 보편적인 RNG와 달리, 키(key)를 통해 정해진 난수열의 임의 원소에 접근할 수 있습니다.<br />
/// 
///     자세한 설명은 <see href="https://www.notion.so/240308-86e96526f4214ee3a886db561be31ff8">Notion 문서</see>를 참조하세요.
/// </remarks>
/// <seealso href="https://www.notion.so/240308-86e96526f4214ee3a886db561be31ff8"/>
public class KeyedRandomizer
{
    private const int BAG_SIZE = 32768;

    private readonly int[] _bag = new int[BAG_SIZE];

    /// <summary>
    /// 키 기반 RNG를 생성합니다.
    /// </summary>
    /// <param name="seed">RNG 시드값</param>
    public KeyedRandomizer(int seed)
    {
        System.Random rng = new System.Random(seed);

        for (int i=0; i<BAG_SIZE; i++)
        {
            _bag[i] = rng.Next();
        }
    }

    /// <summary>
    /// 0 이상 <see cref="int.MaxValue"></see> 미만 범위 내의 <c>int</c>형 난수를 얻습니다.
    /// </summary>
    /// <param name="key">랜덤 키</param>
    /// <returns>범위 내의 난수</returns>
    public int GetRaw(int key)
    {
        if (key < 0 || key >= BAG_SIZE)
        {
            throw new ArgumentOutOfRangeException("key");
        }
        return _bag[key];
    }

    /// <summary>
    /// 특정 범위 내의 <c>int</c>형 난수를 얻습니다.
    /// </summary>
    /// <param name="key">랜덤 키</param>
    /// <param name="startInclusive">난수의 범위 시작 (범위에 포함)</param>
    /// <param name="endExclusive">난수의 범위 끝 (범위에서 제외)</param>
    /// <returns>범위 내의 난수</returns>
    /// <exception cref="ArgumentOutOfRangeException">start가 end보다 더 큼</exception>
    public int GetInt(int key, int startInclusive, int endExclusive)
    {
        if (startInclusive >= endExclusive)
        {
            throw new ArgumentOutOfRangeException();
        }

        return GetRaw(key) % (endExclusive - startInclusive) + startInclusive;
    }

    /// <summary>
    /// 0.0 이상 1.0 미만 범위 내의 <c>double</c>형 난수를 얻습니다.
    /// </summary>
    /// <param name="key">랜덤 키</param>
    /// <returns>0.0 ~ 1.0 범위 내의 난수</returns>
    public double GetDouble(int key)
    {
        return (double)GetRaw(key) / int.MaxValue;
    }

    /// <summary>
    /// 특정 범위 내의 <c>double</c>형 난수를 얻습니다.
    /// </summary>
    /// <param name="key">랜덤 키</param>
    /// <param name="start">난수의 범위 시작</param>
    /// <param name="end">난수의 범위 끝</param>
    /// <returns>범위 내의 난수</returns>
    /// <exception cref="ArgumentOutOfRangeException">start가 end보다 더 큼</exception>
    public double GetDouble(int key, double start, double end)
    {
        if (start > end)
        {
            throw new ArgumentOutOfRangeException();
        }
        return GetDouble(key) * (end - start) + start;
    }

    /// <remarks>
    ///     <see cref="ProbabilityTable{E}"></see>에 기반해 랜덤한 원소를 뽑을 수 있습니다.<br />
    ///     예를 들면, 다음과 같은 코드는 각각 50%의 확률로 "dead" 또는 "alive"를 얻습니다.
    ///     <code>
    ///     var krng = new KeyedRandomizer(123);
    ///     var schrodingersBox = new Probabilitytable&lt;string>() {
    ///         "dead": 0.5,
    ///         "alive": 0.5,
    ///     };
    ///     string result = krng.GetFromTable(123, schrodingersBox);
    ///     </code>
    /// </remarks>
    /// <param name="key">랜덤 키</param>
    /// <param name="table">확률 테이블</param>
    /// <seealso cref="ProbabilityTable{E}"/>
    public E GetFromTable<E>(int key, ProbabilityTable<E> table)
    {
        double[] weights = table.Values.ToArray();
        int len = weights.Length;
        if (len == 0)
        {
            return default(E);
        }

        // weights의 누적합 (accs[i] = weights[0] + weights[1] + ... + weights[i])
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
        throw new ArithmeticException("수학적으로 불가능한 경우가 일어났습니다.");
    }
}

/// <summary>
///     확률 테이블입니다.
/// </summary>
/// <remarks>
///     어떤 원소와 그 원소를 얻을 확률(가중치)을 매핑시킨 자료구조입니다.
/// </remarks>
/// <typeparam name="E">확률 테이블에서 선택될 원소의 타입</typeparam>
public class ProbabilityTable<E> : Dictionary<E, double> {
    ///     <summary>
    ///     확률 테이블을 만듭니다. 예시:
    ///     <code>
    ///     var schrodingersBox = new Probabilitytable&lt;string>() {
    ///         "dead": 0.5,
    ///         "alive": 0.5,
    ///     };
    ///     </code>
    ///     가중치는 모든 원소의 가중치 합에 대한 비로 반영됩니다.
    ///     </summary>
    public ProbabilityTable() : base() { }
}