using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MobileMMORPG.Prototype.Editor
{
    public static class PrototypeIPhoneTools
    {
        private const string ScenePath = "Assets/Prototype/Scenes/ThirdPersonPrototype.unity";

        [MenuItem("Tools/Prototype/Prepare iPhone Build")]
        public static void Prepare()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorUtility.DisplayDialog("iPhone 빌드 준비", "Play를 종료한 뒤 다시 실행하세요.", "OK");
                return;
            }
            if (!File.Exists(ScenePath))
            {
                Debug.LogError("ThirdPersonPrototype scene is missing: " + ScenePath);
                return;
            }
            if (!EditorUtility.DisplayDialog("iPhone 빌드 준비",
                "시제품 씬을 공통 Scene List의 첫 번째로 등록하고 가로 화면 자동 회전을 설정합니다. " +
                "기존 다른 씬은 목록에 보존합니다. 이 변경은 로컬 ProjectSettings에 저장됩니다. 진행할까요?",
                "Apply", "Cancel")) return;

            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>
            {
                new EditorBuildSettingsScene(ScenePath, true)
            };
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                if (scene.path != ScenePath) scenes.Add(scene);
            EditorBuildSettings.scenes = scenes.ToArray();
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
            PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("iPhone 빌드 준비 완료",
                "Tools → Prototype → Switch to iOS를 실행하세요. 이후 File → Build Profiles에서 " +
                "iOS와 Scene List를 확인하고 Build를 누르세요. " +
                "프로필이 별도 Scene List를 사용한다면 시제품 씬을 첫 번째로 넣어 주세요.", "OK");
        }

        [MenuItem("Tools/Prototype/Switch to iOS")]
        public static void SwitchToIOS()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (!BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.iOS, BuildTarget.iOS))
            {
                EditorUtility.DisplayDialog("iOS 모듈 확인",
                    "Unity Hub에서 이 편집기 버전의 iOS Build Support를 설치하세요.", "OK");
                return;
            }
            if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS))
                Debug.LogError("Could not switch to iOS. Check Window → General → Console.");
        }
    }
}
