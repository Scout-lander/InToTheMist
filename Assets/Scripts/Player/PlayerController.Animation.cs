using UnityEngine;

namespace AlterunaFPS
{
	public partial class PlayerController
	{
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDJump;
		private int _animIDFreeFall;
		private int _animIDMotionSpeed;
		private int _animIDHeadLookX;
		private int _animIDHeadLookY;
		private int _animIDGunFire;
		private int _animIDGunReload;
		
		[SerializeField]private Animator _animator;
		[SerializeField] private Animator _firstPersonAnimator; // First-person arms animator
		
		private bool _hasAnimator;
		private bool _hasFirstPersonAnimator;

		private void InitialiseAnimations()
		{
			if (_animator == null || _firstPersonAnimator == null)
			{
				Debug.LogError("Animator or FirstPersonAnimator is not assigned!");
				return;
			}
			_firstPersonAnimator.Play("Idle");
			
			_hasAnimator = true;
			_hasFirstPersonAnimator = true;

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			// setup animation IDs
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDHeadLookX = Animator.StringToHash("HeadLookX");
			_animIDHeadLookY = Animator.StringToHash("HeadLookY");

			_animIDGunFire = Animator.StringToHash("Fire");
			_animIDGunReload = Animator.StringToHash("Reload");
		}

		// Sync animations for both body and arms
		private void SyncAnimators()
		{
			if (_hasAnimator && _hasFirstPersonAnimator)
			{
				_firstPersonAnimator.SetFloat(_animIDSpeed, _animator.GetFloat(_animIDSpeed));
				_firstPersonAnimator.SetBool(_animIDGrounded, _animator.GetBool(_animIDGrounded));
				_firstPersonAnimator.SetBool(_animIDJump, _animator.GetBool(_animIDJump));
				_firstPersonAnimator.SetBool(_animIDFreeFall, _animator.GetBool(_animIDFreeFall));
				_firstPersonAnimator.SetFloat(_animIDMotionSpeed, _animator.GetFloat(_animIDMotionSpeed));
				_firstPersonAnimator.SetFloat(_animIDHeadLookX, _animator.GetFloat(_animIDHeadLookX));
				_firstPersonAnimator.SetFloat(_animIDHeadLookY, _animator.GetFloat(_animIDHeadLookY));
			}
		}
		
		private void OnFootstep(AnimationEvent animationEvent)
		{
			if (animationEvent.animatorClipInfo.weight > 0.5f)
			{
				if (FootstepAudioClips.Length > 0)
				{
					var index = Random.Range(0, FootstepAudioClips.Length);
					AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
				}
			}
		}

		private void OnLand(AnimationEvent animationEvent)
		{
			if (animationEvent.animatorClipInfo.weight > 0.5f)
			{
				AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
			}
		}
	}
}