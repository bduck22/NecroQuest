using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpiritMove : MonoBehaviour
{
    public Transform Target;

    //[SerializeField] float angle;

    public float Speed;
    //public float Height;
    //public float CurvePer;

    //Vector3 Direction;
    Vector3 OriginPos;
    public Transform P2;

    public float Val = 0;
    void Start()
    {
        OriginPos = transform.position;
        P2.transform.localPosition = new Vector3(Random.Range(-5.0f, -1.0f), Random.Range(-3.0f,3.0f), 0);
    }

    Vector3 TargetP;
    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            if (Target.gameObject.activeSelf)
            {
                TargetP = Target.position;
            }
            else
            {
                TargetP = Target.position;
                Target = null;
            }
        }

        transform.position = BezierTest(OriginPos, P2.position, TargetP, Val);
        if (Val < 1)
        {
            Val += Speed * Time.deltaTime;
        }
        else
        {
            transform.GetComponent<Animator>().enabled = true;
        }

        //if (angle <= 180)
        //{
            //Direction = new Vector3(Vector2.Distance(OriginPos, Target) * (angle / 180),
            //    (angle - (angle >= (180*(CurvePer/100f)) ? //커브 각도 이상이면
            //    (((angle - (180 * (CurvePer / 100f))) + (angle - (180 * (CurvePer / 100f))) * ((180 * (CurvePer / 100f))/(180- (180 * (CurvePer / 100f)))))) : 0)//현재각도 + a를 빼준다
            //    ) * 0.1f * Height, 0f); //높이 조절

            //Debug.Log("AA " + angle);
            //Debug.Log((angle >= (180 * (CurvePer / 100f)) ? (((angle - (180 * (CurvePer / 100f))) + (angle - (180 * (CurvePer / 100f))) * ((180 * (CurvePer / 100f)) / 180f))) : 0));
            //transform.Translate(Direction*Time.deltaTime);
            //angle += Time.deltaTime * Speed;
        //}
            //Vector2 direction = new Vector2(, );//코싸인 함수 Mathf.Cos(angle * Mathf.Deg2Rad)  싸인 함수Mathf.Sin(angle++ * Mathf.Deg2Rad)
            /* 2, 3 ~ 6, 7
             * y=2⋅sin( P/4(x−2))+5
             * y=A*sin(B(x-C))+D
             * A = (7-2)/2
             * D = (3+7)/2
             * B = P/(6 - 2)
             * C = 2
             * ((Target.y - OriginPos.x)/2f)*Mathf.Sin((angle * Mathf.Deg2Rad * (180/angle))/(Target.x - OriginPos.x) *(angle * Mathf.Deg2Rad-(OriginPos.x)))+((OriginPos.y + Target.y)/2f)
             * */

            ////transform.position = Direction;
            //transform.Translate(SpeedPerSecond/180f * Direction*Time.deltaTime);
            //Mathf.Sin((Mathf.PI / Vector2.Distance(OriginPos, Target)) * ((angle * Mathf.Dg2Rad) - OriginPos.x)) 0 ~ 30 ~ 150 30 - (0 + 0) 31 - (1 + 0.2)
    }
    public Vector3 BezierTest(Vector3 P_1, Vector3 P_2, Vector3 P_3, float value)
    {

        Vector3 A = Vector3.Lerp(P_1, P_2, value);
        Vector3 B = Vector3.Lerp(P_2, P_3, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        return D;
    }
    //IEnumerator Attack()
    //{
    //    for (; angle <= 180; angle+=Speed)
    //    {
    //        Direction = new Vector3(Vector2.Distance(OriginPos, Target) * (angle / 180), (angle - (angle >= 30 ? ((angle-30) + (angle - 30f)*0.2f):0))*0.1f*Height, 0f);
    //        transform.position = Direction;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //    //Direction = new Vector3(Vector2.Distance(OriginPos, Target), 0f, 0f);
    //}
}
