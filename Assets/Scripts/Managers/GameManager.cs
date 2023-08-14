using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null; // 해당 스크립트를 변수로 받아옴

    // 싱글톤 프로퍼티
    public static GameManager Instance
    {
        get
        {
            if (instance == null) // instance가 없으면
            {
                return null; // null이면 null return
            }
            return instance; // instance가 있으면 return
        }
    }

    private void Awake()
    {
        if (instance == null) // null이면
        {
            instance = this; // 넣어주고
            
            DontDestroyOnLoad(this.gameObject); // 씬이 전환되어도 유지
        }
        else
        {
            Destroy(gameObject); // null이면 Destroy
        }
    }
}
