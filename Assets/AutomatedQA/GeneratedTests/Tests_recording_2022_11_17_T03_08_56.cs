using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.AutomatedQA;
using Unity.CloudTesting;

namespace GeneratedAutomationTests
{
    /// <summary>
    /// These tests were generated by Unity Automated QA for the recording from the Assets folder at "\Recordings\recording-2022-11-17-T03-08-56.json".
    /// You can regenerate this file from the Unity Editor Menu: Automated QA > Generate Recorded Tests
    /// </summary>
    public class Tests_recording_2022_11_17_T03_08_56 : AutomatedQATestsBase
    {
        /// Generated from recording file: "\Recordings\recording-2022-11-17-T03-08-56.json".
        [UnityTest]
        [CloudTest]
        public IEnumerator CanPlayToEnd()
        {
            yield return Driver.Perform.Click(Scene_MenuScene_PageObject.Clickable_MenuScene_PlayButton);
        }
        // Initialize test run data.
        protected override void SetUpTestRun()
        {
            Test.entryScene = Scene_MenuScene_PageObject.SceneName;
            Test.recordedAspectRatio = new Vector2(1920f,30f);
            Test.recordedResolution = new Vector2(1920f,1080f);
        }

    }
}

