using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction { NORTH, EAST, SOUTH, WEST }

[System.Serializable]
public static class DirectionExtensions {

	public static Direction Opposite(this Direction dir) {
		if (dir < Direction.SOUTH)
			return dir + 2;
		else
			return dir - 2;
	}
}
