using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Inspector â���� �Ϸ��� ���̸� �� ������ ������ �� �ִ� �����Դϴ�.
    // ���� ���, 60���� �����ϸ� ���� �ð� 60�ʰ� ���� �� �Ϸ簡 �˴ϴ�.
    [Tooltip("�Ϸ��� ���̸� �� ������ �����մϴ�.")]
    public float dayDuration = 60f;

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime�� ���� �����Ӱ� ���� ������ ������ �ð� �����Դϴ�.
        // dayDuration���� ������ ȸ�� �ӵ��� �����մϴ�.
        // 360�� ���� ��ü ��(�Ϸ�)�� ȸ����ŵ�ϴ�.
        // Vector3.right�� ������ ȸ������ ���ʿ��� �������� �ذ� ���� ȿ���� ����ϴ�.
        transform.Rotate(Vector3.right * (360 / dayDuration) * Time.deltaTime);
    }
}