using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private GameObject SelectEnemy;
    [SerializeField]
    private float MovingLeft = -0.01f;
    [SerializeField]
    private float MovingRight = 0.01f;
    [SerializeField]
    private float MoveRange = 3.0f;

    bool isRight = true;
    bool isLeft = false;

    float moveCount = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            SelectEnemy.transform.Translate(MovingLeft, 0, 0);
            if (SelectEnemy.transform.position.x >= MoveRange)
            {
                isRight = false;
                isLeft = true;
            }
        }
        if (isLeft)
        {
            SelectEnemy.transform.Translate(MovingRight, 0, 0);
            if (SelectEnemy.transform.position.x <= -MoveRange)
            {
                isRight = true;
                isLeft = false;
            }

        }
    }
}
