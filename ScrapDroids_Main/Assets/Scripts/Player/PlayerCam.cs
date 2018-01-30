<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<doc>
  <members>
    <assembly>
      <name>UnityEngine.TestRunner</name>
    </assembly>
    <member name="?:UnityEngine.TestTools.IMonoBehaviourTest">
      <summary>
        <para>A MonoBehaviour test needs to implement this interface.</para>
      </summary>
    </member>
    <member name="P:UnityEngine.TestTools.IMonoBehaviourTest.IsTestFinished">
      <summary>
        <para>Indicates when the test is considered finished.</para>
      </summary>
    </member>
    <member name="?:UnityEngine.TestTools.IPrebuildSetup">
      <summary>
        <para>Interface for the method that implements the prebuild step.</para>
      </summary>
    </member>
    <member name="M:UnityEngine.TestTools.IPrebuildSetup.Setup">
      <summary>
        <para>Setup method that is automatically called before the test run.</para>
      </summary>
    </member>
    <member name="T:UnityEngine.TestTools.LogAssert">
      <summary>
        <para>LogAssert allows you to expect Unity log messages that would normally cause the test to fail.</para>
      </summary>
    </member>
    <member name="P:UnityEngine.TestTools.LogAssert.ignoreFailingMessages">
      <summary>
        <para>Set this property to true to prevent unexpected error log messages from triggering an assertion. This property is set to false by default.</para>
      </summary>
    </member>
    <member name="M:UnityEngine.TestTools.LogAssert.Expect(UnityEngine.LogType,System.String)">
      <summary>
        <para>Expect a log message of a specfic type. If an error, assertion or exception log is expected, the test will not fail. The test will fail if a log message is expected but does not appear.</para>
      </summary>
      <param name="type">Log type.</param>
      <param name="message">Log message to expect.</param>
    