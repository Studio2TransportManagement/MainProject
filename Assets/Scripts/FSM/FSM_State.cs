using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//T represents the owner of the FSM
public abstract class FSM_State<T> {

	abstract public void Begin(T obj);
	abstract public void Run(T obj);
	abstract public void End(T obj);
}
