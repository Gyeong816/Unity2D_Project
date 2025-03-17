using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float Attack_Rate = 0.5f;

    [SerializeField] GameObject firepoint;
    [SerializeField] GameObject bulletPrefab;  // 총알 프리팹
    [SerializeField] Transform firePoint;      // 총알이 나가는 위치
    [SerializeField] private float bulletSpeed = 10f;  // 총알 속도
    [SerializeField] private int Poolcount = 20;

    [SerializeField] private new AudioSource audio;
    private Queue<GameObject> bulletPool;
    private Vector3 poolPosition = new Vector3(0f, -40f, 0f);
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isShooting = false;
    private bool isDuckShooting = false;
    private PlayerMovement pM;
    private PlayerHp hp;

    private void Start()
    {
        firepoint.SetActive(false);
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < Poolcount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, poolPosition, Quaternion.identity);
            bullet.SetActive(false); // 비활성화하여 대기 상태로 둠
            bulletPool.Enqueue(bullet); // 큐에 추가
        }

        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        TryGetComponent(out pM);
        TryGetComponent(out hp);
        TryGetComponent(out audio);
    }


    void Update()
    {
        if (hp.isDie||hp.isTakingDamage)
        {
            firepoint.SetActive(false);
            return;
        }

        if (pM.isDuck&&!pM.isAim)
        {
            isShooting = false;  
            animator.SetBool("Shoot", false);
            animator.SetBool("UpAimShoot", false);
            if(Input.GetKey(KeyCode.X) && !isDuckShooting)
            {
                firepoint.SetActive(true);
                StartCoroutine(Duck_Shoot_co());
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                firepoint.SetActive(false);
            }
            return;
        }
        if (Input.GetKey(KeyCode.X) && !isShooting) 
        {
            firepoint.SetActive(true);
            StartCoroutine(Shoot_co());
        }
        if (Input.GetKeyUp(KeyCode.X)) 
        {
            firepoint.SetActive(false);
            animator.SetBool("UpAimShoot", false);
            animator.SetBool("Shoot", false);
            animator.SetBool("AimShoot", false);
            animator.SetBool("DuckShoot", false);
            audio.Stop();
        }
    }

    private IEnumerator Shoot_co()
    {
        
        isShooting = true;

        Shoot();

        yield return new WaitForSeconds(Attack_Rate);

        isShooting = false;
    }

    private IEnumerator Duck_Shoot_co()
    {
        isDuckShooting = true;

        DuckShoot();

        yield return new WaitForSeconds(Attack_Rate);

        isDuckShooting = false;
    }

    private void DuckShoot()
    {

        Vector2 R_direction = new Vector2(1, 0);
        Vector2 L_direction = new Vector2(-1, 0);

        GameObject bullet = GetBulletFromPool();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Bullet>().SetPlayerShooting(this);

        if (spriteRenderer.flipX == false)
        {
            firePoint.localPosition = R_direction * 1f;

            bullet.GetComponent<Rigidbody2D>().velocity = R_direction * bulletSpeed;
        }
        if (spriteRenderer.flipX == true)
        {
            firePoint.localPosition = L_direction.normalized * 1f;
            bullet.GetComponent<Rigidbody2D>().velocity = L_direction * bulletSpeed;
        }
    }

    private void Shoot()
    {
        audio.Play();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector2 direction = new Vector2(horizontal, vertical);
        Vector2 Up_direction = new Vector2(0, 1);
        Vector2 R_direction = new Vector2(1, 0);
        Vector2 L_direction = new Vector2(-1, 0);


        GameObject bullet = GetBulletFromPool(); 
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Bullet>().SetPlayerShooting(this);

        if (direction == Vector2.zero)
        {
            animator.SetBool("Shoot", true);

            if (spriteRenderer.flipX == false)
            {
                firePoint.localPosition = R_direction * 0.8f;

                bullet.GetComponent<Rigidbody2D>().velocity = R_direction * bulletSpeed;
            }
            else if (spriteRenderer.flipX == true)
            {
                firePoint.localPosition = L_direction * 0.8f;
                bullet.GetComponent<Rigidbody2D>().velocity = L_direction * bulletSpeed;
            }
        }
        else if (vertical == 1 && horizontal==0)
        {
            animator.SetBool("UpAimShoot", true);

            if (spriteRenderer.flipX == false)
            {
                firePoint.localPosition = R_direction * 0.3f + Up_direction * 1.1f; 

            }
            else if (spriteRenderer.flipX == true)
            {
                firePoint.localPosition = L_direction * 0.3f + Up_direction * 1.1f; 
            }
            //firePoint.localPosition = Up_direction * 1.1f;
            bullet.transform.right = Up_direction;
            bullet.GetComponent<Rigidbody2D>().velocity = Up_direction * bulletSpeed;
        }
        else
        {

            
            firePoint.localPosition = direction * 0.8f;
                      

            // Mathf.Atan2(y, x) → (x, y) 방향 벡터의 각도를 라디안 값으로 반환
            // Mathf.Rad2Deg → 라디안을 도(°) 단위로 변환
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        }
    }

    private GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0) // 풀에 총알이 남아있으면 가져오기
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true); // 활성화
            return bullet;
        }
        else // 만약 풀이 부족하면 새로 생성
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            return bullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = poolPosition; // 안 보이는 곳으로 이동
        bulletPool.Enqueue(bullet);
    }





}