using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            BlockFall();
        }
    }

    private void BlockFall()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _collider.isTrigger = true;
        StartCoroutine(DestroyBlock());
        IEnumerator DestroyBlock()
        {
            while (Vector3.Distance(transform.position, new Vector2(0, -17f )) >= 0.01f)
            {
                yield return null;
            }

            Destroy(gameObject);
        }
    }


}

/*
 в языке программиования сишарп есть 3 цикла
1.цикл while - он позволяет выполнять логику(действия) внутри фигурных скобок пока
или до тех пор условие в круглых скобках true
while(true)
{
  действие;
}
2.for - он позволяет выполнять логику(действие) столько раз сколько указанно в круглых
скобках
for(int i = 0; i < length; i++)
{
   действие;
}
int i = 0 - это начало отсчета
i < length - это диапазон работы цикла, где length это значение до каторого будет
идти цикл.
i++ - это увеличение значения на еденицу то есть каждое повторение цикла начальное 
значение будет увеличено на еденицу.

 */
