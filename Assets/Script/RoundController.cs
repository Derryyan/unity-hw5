using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour {
	public int round = 1;
	public int trial = 0;
	public int getRound() {
		return round;
	}
	public void setRound() {
		trial++;
		if (trial == 10) {
			round++;
			trial = 0;
		}
	}
	public void reset() {
		trial = 0;
		round = 1;
	}
	public int getUFONum() {
		return round;
	}
}
