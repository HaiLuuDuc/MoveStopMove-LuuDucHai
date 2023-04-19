using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : CharacterAttack
{
    [SerializeField] private CharacterAnimation characterAnim;
    [SerializeField] private Bot bot;


    public void Update()
    {
        if (bot.enemyList.Count > 0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }
    }

    public override IEnumerator Attack()
    {
        Vector3 enemyPos = enemy.transform.position;

        bot.DisplayOnHandWeapon();

        characterAnim.ChangeAnim(Constant.ATTACK);

        RotateToTarget();

        yield return new WaitForSeconds(0.4f);//thời gian vung tay cho đến khi vũ khí rời bàn tay là 0.4s
        if (character.isDead)
        {
            yield break;
        }

        bot.UnDisplayOnHandWeapon(); // tat hien thi weapon tren tay
        GameObject obj = weaponPool.GetObject(); // lay weapon tu` pool
        obj.transform.position = rightHand.transform.position; // dat weapon vao tay phai character

        ReCaculateTargetWeapon(obj, enemyPos);// đảm bảo vũ khí bay từ lòng bàn tay chứ không phải từ bot.position
        
        WeaponType wpt = bot.onHandWeapon.GetComponent<Weapon>().weaponData.weaponType;
        if (wpt == WeaponType.Boomerang)
        {
            Vector3 target2 = this.transform.position;// vị trí cho boomerang quay về
            StartCoroutine(FlyBoomerangToTarget(obj, targetWeapon.position, target2, 10f));
        }
        else
        {
            StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.position, 10f));
        }
        //play sound
        /*if (AudioManager.instance.IsInDistance(this.transform))
        {
            AudioManager.instance.Play(SoundType.Throw);
        }*/
        yield return null;
    }
}
