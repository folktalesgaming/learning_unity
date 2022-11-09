﻿using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private int m_health = 100;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        //if (!m_grounded && m_groundSensor.State()) {
          //  m_grounded = true;
          //  m_animator.SetBool("Grounded", m_grounded);
        // }

        //Check if character just started falling
        //if(m_grounded && !m_groundSensor.State()) {
        //    m_grounded = false;
        //    m_animator.SetBool("Grounded", m_grounded);
        //}

        // -- Handle input and movement --
        //float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        //if (inputX > 0)
         //   transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //else if (inputX < 0)
        //    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        //m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
       // m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
         
        //Hurt
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Hurt");
            m_health -= 10;
        }
        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        // Death animation on health 0
        if (m_health == 0)
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_health = 100;
            m_isDead = !m_isDead;
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

        //Run
        // else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        //    m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        

        //Idle
        //  else
        //    m_animator.SetInteger("AnimState", 0);
    }
}
