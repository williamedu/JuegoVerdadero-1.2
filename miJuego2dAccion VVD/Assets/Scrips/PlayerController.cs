using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

	//public float longIdleTime = 5f;
	public float speed = 4f;
	public float jumpForce = 2.5f;
	public float doubleJumpForce = 4.0f;

	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;

	// References
	private Rigidbody2D _rigidbody;
	private Animator _animator;

	// Long Idle
	//private float _longIdleTimer;

	// Movement
	public Joystick joystick;
	private Vector2 _movement;
	private bool _facingRight = true;
	private bool _isGrounded;
	private bool _canDoubleJump;
	public bool running;
	//Attacks
	public bool canReceiveInput;
	public bool inputReceived;
	public Transform AttackPoint;
	public float AttackRange = 0.5f ;
	public LayerMask enemyLayers;
	//Health
	public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;
	//some other external items 
	public float jumPadForceJump = 10f;



	// Attack
	public bool _isAttacking;
	public int attackDamage = 50;


	void Awake()
	{
		instance = this;
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

	public void TakeDamage (int damage)
    {
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
    }

	//daño de enemigos
	public void borracho()
	{
		TakeDamage(20);
		_animator.SetTrigger("Hurt");
	}

	public void Attack()
	{
		Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

		foreach (Collider2D enemy in hitenemies)
		{
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
		}
	}
	private void OnDrawGizmosSelected()
	{
		if (AttackPoint == null)
			return;
		Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
	}

	void Update()
	{
		
       
		if (_isAttacking == false)
		{
			// Movement
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			//float horizontalInput = joystick.Horizontal;        
			
			_movement = new Vector2(horizontalInput, 0f);


			// Flip character
			if (horizontalInput < 0f && _facingRight == true)
			{
				Flip();
			}
			else if (horizontalInput > 0f && _facingRight == false)
			{
				Flip();
			}
		}

		// Is Grounded?
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		// Is Jumping?
		if (_isGrounded)
		{
			_canDoubleJump = true;

		}

		if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
		{


			_rigidbody.velocity = Vector2.up * jumpForce;

		}
		else
		{
			if (Input.GetButtonDown("Jump") && (_canDoubleJump == true) && _isGrounded == false)
			{
				//_rigidbody.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);

				_rigidbody.velocity = Vector2.up * jumpForce;
				_canDoubleJump = false;
				_animator.SetBool("doubleJump", true);
			}
			else
			{
				_animator.SetBool("doubleJump", false);
			}
		}


		// Wanna Attack?
		
			if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false && running == false)
			{
			//Attack();
			StaminaBar.instance.UseStamina(15);
				inputReceived = true;
				canReceiveInput = false;

				_movement = Vector2.zero;
				_rigidbody.velocity = Vector2.zero;
				//_animator.SetTrigger("Attack");
				//Debug.Log("hola si funciona");
				//_animator.SetBool("isAttacking", true);
				//Debug.Log("hola este es un ataque");	


			}
		
			
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
			running = true;
        }
        else
        {
			running = false;
        }

			if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false && running == true)
		{;
			Debug.Log("wooo la velocidad se aumento en 5");
			speed = 5f;
			_animator.SetBool("RunAttack", true);
		}
        else
        {
			speed = 5f;
			_animator.SetBool("RunAttack", false);
		}
        
	}
	
	public void InputManager()
	{
		if (!canReceiveInput)
		{
			canReceiveInput = true;
		}
		else
		{
			canReceiveInput = false;
		}
	}

	void FixedUpdate()
	{
		if (_isAttacking == false) {
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
		}
	}

	void LateUpdate()
	{
		_animator.SetBool("Idle", _movement == Vector2.zero);
		_animator.SetBool("IsGrounded", _isGrounded);
		//_animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
		//_animator.SetBool("Jump", _isGrounded == false);

		// Animator
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			_movement = Vector2.zero;
			_isAttacking = true;

		}
		else {
			_isAttacking = false;
		}
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("RunAttack"))
		{
			
			_isAttacking = true;

		}
		else
		{
			_isAttacking = false;
		}

		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
		{

			running = true;

		}
		else
		{
			running = false;
		}

		// Long Idle
		//if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
		//_longIdleTimer += Time.deltaTime;

		//if (_longIdleTimer >= longIdleTime) {
		//_animator.SetTrigger("LongIdle");
		//}
		//} else {
		//	_longIdleTimer = 0f;
		//}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}


	//externernal codes for extra movement items etc
	public void jumPadForce()
    {
		_rigidbody.velocity = Vector2.up * jumPadForceJump;
	}
	
}
