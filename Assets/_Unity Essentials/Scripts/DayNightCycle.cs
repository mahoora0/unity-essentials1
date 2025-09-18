using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Inspector 창에서 하루의 길이를 초 단위로 설정할 수 있는 변수입니다.
    // 예를 들어, 60으로 설정하면 실제 시간 60초가 게임 속 하루가 됩니다.
    [Tooltip("하루의 길이를 초 단위로 설정합니다.")]
    public float dayDuration = 60f;

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime은 이전 프레임과 현재 프레임 사이의 시간 간격입니다.
        // dayDuration으로 나누어 회전 속도를 조절합니다.
        // 360을 곱해 전체 원(하루)을 회전시킵니다.
        // Vector3.right를 축으로 회전시켜 동쪽에서 서쪽으로 해가 지는 효과를 만듭니다.
        transform.Rotate(Vector3.right * (360 / dayDuration) * Time.deltaTime);
    }
}