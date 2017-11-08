using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MovementTests {

	[Test]
	public void movesAlongXAxisForHorizontalInput() {
//		Vector3 r = ();

		Assert.AreEqual (1, new Movement (1).calculate (0, 1, 1, r, f));
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator MovementTestsWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
