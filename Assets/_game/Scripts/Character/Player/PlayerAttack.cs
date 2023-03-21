using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CharacterAnimation characterAnimation;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private Transform targetWeapon;
    [SerializeField] private TargetCircle targetCircle;
    [SerializeField] private WeaponPool weaponPool;
    private Character enemy;
    private Vector3 enemyPosition;
    private float attackRange = 9f;
    public bool canAttack;



    public void Start()
    {
        canAttack = true; // character dung yen tu dau game van co the attack enemy di vao circle
        player.onHandWeapon.transform.SetParent(rightHand.transform);
        player.onHandWeapon.transform.localPosition = Vector3.zero;
        DeactiveTargetCircle();
    }

    void Update()
    {
        if (player.isDead == true)
        {
            targetCircle.gameObject.SetActive(false);
            return;
        }
        if (player.isMoving)
        {
            canAttack = false;
        }
        // only correct for player, not for bots
        if (Input.GetMouseButton(0))
        {
            if (!player.onHandWeapon.activeSelf)
            {
                player.onHandWeapon.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canAttack = true;
        }

        if(player.enemyList.Count>0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }

        if (canAttack && player.isMoving==false && enemy!=null)
        {
            RotateToTarget();
            StartCoroutine(Attack());
            StartCoroutine(DelayAttack(1.1f));
        }

        if(player.enemyList.Count > 0)
        {
            ActiveTargetCircle();
        }
        else
        {
            DeactiveTargetCircle();
        }
    }

    public void FindNearestTarget()
    {
        enemy = null;
        if (player.enemyList.Count > 0)
        {
            float minDistance = 100f;
            for (int i = 0; i < player.enemyList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, player.enemyList[i].transform.position);
                if (distance < minDistance)
                {
                    enemy = player.enemyList[i];
                    minDistance = distance;
                }
            }
        }
    }

    public void RotateToTarget()
    {
        if (enemy != null)
        {
            Vector3 dir;
            dir = enemy.transform.position - this.transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public IEnumerator Attack()
    {
        if (enemy != null)
        {
            player.onHandWeapon.SetActive(true);// hien thi weapon tren tay
            characterAnimation.ChangeAnim(Constant.ATTACK);// vung tay trong 0.4s
            enemyPosition = enemy.transform.position;// luu vi tri cua enemy
            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {
                if (player.isMoving) // neu character di chuyen thi cancel vung tay, dong thoi cancel weapon fly
                {
                    goto label1;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }
            player.onHandWeapon.SetActive(false); // tat hien thi weapon tren tay
            GameObject obj = weaponPool.GetObject(); // lay weapon tu` pool
            obj.transform.position = rightHand.transform.position; // dat weapon vao tay phai character
            ReCaculateTargetWeapon(obj); // dam bao weapon bay qua center cua enemy
            StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.transform.position, 8f)); // fly weapon
        }
    label1:;
        yield return null;
    }

    public IEnumerator DelayAttack(float delayTime) // set canAttack = false trong 2s, sau do set canAttack = true (tranh attack lien tuc)
    {
        canAttack = false;
        float elapsedTime = 0f;
        float duration = delayTime;
        while (elapsedTime < duration)
        {
            if (player.isMoving) // neu di chuyen thi cancel coroutine, vi khi do canAttack == true
            {
                goto label;
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        canAttack = true;
        if (!player.isDead)
        {
            characterAnimation.ChangeAnim(Constant.IDLE);
        }
        label:;
    }

    public IEnumerator FlyWeaponToTarget(GameObject obj, Vector3 target, float speed)
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.1f && obj.activeSelf)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            obj.transform.Rotate(0, speed, 0);
            yield return null;
        }
        weaponPool.ReturnToPool(obj); // sau khi bay den target thi cat vao pool
        yield return null;
    }

    public void ReCaculateTargetWeapon(GameObject obj)
    {

            Vector3 dir = enemyPosition - obj.transform.position;
            dir.y = 0;
            dir = dir.normalized;
            targetWeapon.transform.position = obj.transform.position + dir * attackRange;
        
    }

    public void ActiveTargetCircle()
    {
        if (!targetCircle.gameObject.activeSelf)
        {
            targetCircle.gameObject.SetActive(true);
        }
        if(enemy!=null)
        {
            targetCircle.enemyTransform = enemy.transform;
        }
    }

    public void DeactiveTargetCircle()
    {
        if (targetCircle.gameObject.activeSelf)
        {
            targetCircle.gameObject.transform.position -= new Vector3(0,100,0); // fix loi targetCircle nhap nhay o enemy cu~
            targetCircle.gameObject.SetActive(false);
        }
    }
}


