//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sends simple controller button events to UnityEvents
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;

namespace valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class InteractableButtonEvents : MonoBehaviour
	{
		public UnityEvent onTriggerDown;
		public UnityEvent onTriggerUp;
		public UnityEvent onGripDown;
		public UnityEvent onGripUp;
		public UnityEvent onTouchpadDown;
		public UnityEvent onTouchpadUp;
		public UnityEvent onTouchpadTouch;
		public UnityEvent onTouchpadRelease;

		//-------------------------------------------------
		void Update()
		{
			for ( int i = 0; i < Player.instance.handCount; i++ )
			{
				Hand hand = Player.instance.GetHand( i );

				if ( hand.controller != null )
				{
					if ( hand.controller.GetPressDown( valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
					{
						onTriggerDown.Invoke();
					}

					if ( hand.controller.GetPressUp( valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
					{
						onTriggerUp.Invoke();
					}

					if ( hand.controller.GetPressDown( valve.VR.EVRButtonId.k_EButton_Grip ) )
					{
						onGripDown.Invoke();
					}

					if ( hand.controller.GetPressUp( valve.VR.EVRButtonId.k_EButton_Grip ) )
					{
						onGripUp.Invoke();
					}

					if ( hand.controller.GetPressDown( valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadDown.Invoke();
					}

					if ( hand.controller.GetPressUp( valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadUp.Invoke();
					}

					if ( hand.controller.GetTouchDown( valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadTouch.Invoke();
					}

					if ( hand.controller.GetTouchUp( valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						onTouchpadRelease.Invoke();
					}
				}
			}

		}
	}
}
