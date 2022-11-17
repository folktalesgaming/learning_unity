using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 1.0f;
    // [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;    
    private Rigidbody2D         m_body2d;
    private GameObject         m_hero_body;
    private Sensor_Bandit       m_groundSensor;
    // private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private int m_health = 100;
    private float m_move_direction = 1f;
    private bool m_is_cRoutine_running;

    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_hero_body = GameObject.FindWithTag("Hero");
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_is_cRoutine_running = false;
    }
	
	void Update () {

        bool isHeroNear = Vector3.Distance(transform.position, m_hero_body.transform.position) < 2.0f;
        // bool isHeroFacingCorrectly = 
        //     ((transform.localScale.x == -1.0f && m_hero_body.transform.localScale.x == -1.0f) 
        //     || (transform.localScale.x == 1.0f && m_hero_body.transform.localScale.x == 1.0f));
       
        //if (!m_grounded && m_groundSensor.State()) {
        //  m_grounded = true;
        //  m_animator.SetBool("Grounded", m_grounded);
        // }

        //Check if character just started falling
        //if(m_grounded && !m_groundSensor.State()) {
        //    m_grounded = false;
        //    m_animator.SetBool("Grounded", m_grounded);
        //}

        // Move
        // m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        // m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Get Hurt when player attacks while near
        if (Input.GetMouseButtonDown(0) && isHeroNear)
        {
            m_animator.SetTrigger("Hurt");
            m_health -= 10;
        }

        // On "F" key pressed revive bandit
        if (Input.GetKeyDown("f"))
        {
            m_combatIdle = !m_combatIdle;
            m_health = 100;
            m_isDead = false;
            m_animator.SetTrigger("Recover");
        }
        
        // Check for hero and change the combat idle animation state
        if(isHeroNear)
        {
            m_combatIdle = true;
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_combatIdle = false;
        }

        // Flip the character to face the Hero character when not dead and hero is near
        if (!m_isDead && isHeroNear && m_hero_body.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(!m_isDead && isHeroNear && m_hero_body.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Death animation on health 0
        if (m_health <= 0)
        {
            m_isDead = true;
            m_animator.SetTrigger("Death");
        }

        //Attack
        //else if(Input.GetMouseButtonDown(0)) {
        //    m_animator.SetTrigger("Attack");
        //}

        //Jump
        // else if (Input.GetKeyDown("space") && m_grounded) {
        //   m_animator.SetTrigger("Jump");
        //   m_grounded = false;
        //   m_animator.SetBool("Grounded", m_grounded);
        //   m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        //   m_groundSensor.Disable(0.2f);
        // }
    }

    void FixedUpdate()
    {
        bool isHeroNear = Vector3.Distance(transform.position, m_hero_body.transform.position) < 2.0f;

        if(m_health <= 0 || isHeroNear) {
            StopCoroutine("MoveCharacterAuto");
            m_is_cRoutine_running = false;
        }else {
            if(!m_is_cRoutine_running) {
                StartCoroutine("MoveCharacterAuto");
                m_is_cRoutine_running = true;
            }
        }
    }

    IEnumerator MoveCharacterAuto ()
    {
        while(true) {
            yield return new WaitForSeconds(3f);
            
            m_move_direction = -1f;
            m_animator.SetInteger("AnimState", 2);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            m_body2d.velocity = new Vector2(m_move_direction * m_speed, m_body2d.velocity.y);
            
            yield return new WaitForSeconds(1f);
            m_animator.SetInteger("AnimState", 0);

            yield return new WaitForSeconds(2f);
            m_move_direction = 1f;
            m_animator.SetInteger("AnimState", 2);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            m_body2d.velocity = new Vector2(m_move_direction * m_speed, m_body2d.velocity.y);
            
            yield return new WaitForSeconds(1f);
            m_animator.SetInteger("AnimState", 0);
        }
    }
}
