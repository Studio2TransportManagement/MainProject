﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM_Core<T> {

	private T fsmOwner;
	private FSM_State<T> stateCurrent;
	private FSM_State<T> statePrevious;
	private FSM_State<T> stateGlobal;

	public void Init() {
		stateCurrent = null;
		statePrevious = null;
		stateGlobal = null;
	}

	public void Config(T owner, FSM_State<T> start_state) {
		fsmOwner = owner;
		ChangeState(start_state);
	}

	public void Update() {
		if (stateGlobal != null) {
			stateGlobal.Run(fsmOwner);
		}

		if (stateCurrent != null) {
			stateCurrent.Run(fsmOwner);
		}
	}

	public void ChangeState(FSM_State<T> new_state) {
		statePrevious = stateCurrent;

		if (stateCurrent != null) {
			stateCurrent.End(fsmOwner);
			stateCurrent = new_state;
			if (stateCurrent != null) {
				stateCurrent.Begin(fsmOwner);
			}
		}
	}

	public void ReturnToLastState() {
		if (statePrevious != null) {
			ChangeState(statePrevious);
		}
	}
}
