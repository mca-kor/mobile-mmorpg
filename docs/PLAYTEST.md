# 첫 시제품 실행과 검증

Unity 6.3 LTS `6000.3.27f1` 기준. Mac·iPhone 시제품은 사용자에게 정상 작동을 보고받았고 PR #6 병합을 확인했다. 2026-10-31 Mac 배치 실행에서 기존 PlayMode 테스트 16개가 모두 통과했다. 실제 화면·키보드·마우스와 iPhone 단계는 아래 수동 검증 및 [IPHONE](IPHONE.md)을 따른다.

## Mac에서 실행하기

1. Unity에서 Play 중이면 Play를 다시 눌러 멈춘다. 저장할 기존 변경이 있다면 저장·커밋하고 Unity를 닫는다.
2. GitHub Desktop에서 Fetch origin을 누르고 Current Branch가 [STATUS](STATUS.md)의 작업 브랜치인지 확인한다. 로컬 설정 변경을 보존하기 전에는 다른 브랜치로 전환하지 않는다. PR #6의 코드는 이미 main에 포함되어 있다.
3. Unity Hub의 Projects에서 기존 `MobileMMORPG`를 연다. 스크립트 컴파일이 끝날 때까지 기다린다.
4. Unity의 Project 창에서 `Assets → Prototype → Scenes`를 열고 `ThirdPersonPrototype`을 더블 클릭한다. 기존 SampleScene은 변경하지 않는다.
5. 이 씬은 Play 전에 바닥과 캐릭터가 보이지 않는 것이 정상이다. Hierarchy에는 Third Person Prototype이 있고, Play를 누르면 전용 스크립트가 맵·캐릭터·카메라를 만든다.
6. 상단 Play를 누른 뒤 Game 화면을 클릭한다.
7. WASD로 이동하고 마우스 오른쪽 버튼을 누른 채 움직여 시점을 회전한다. 캐릭터 앞의 노란 표시로 바라보는 방향을 확인한다.
8. 오른쪽 버튼을 놓으면 포인터가 복원된다. Esc로도 해제할 수 있으며, 다시 회전하려면 오른쪽 버튼을 놓았다가 다시 누른다. 다른 앱으로 전환하거나 Play를 종료해도 포인터를 복원한다.
9. Window → General → Console에서 빨간 오류가 없는지 확인한다. 오류가 있으면 첫 오류 전체를 복사해 알려준다.
10. Play를 다시 눌러 종료한다. 실행 중 생성된 오브젝트가 사라지는 것은 정상이다.

새 변경은 작업 브랜치에서 검증하고 별도 PR로 검토한다. 병합은 사용자가 진행한다. 로컬 변경을 커밋 등으로 보존한 뒤 main으로 전환하고 Fetch origin → Pull origin으로 받는다.

## 자동 테스트

Play를 종료한 뒤 Window → General → Test Runner를 연다. PlayMode 탭에서 Run All을 누른다. 기존 PrototypeMotorTests의 아래 4개 결과와 새 PrototypeTouchStateTests의 12개 사례를 확인한다. 터치 검증 항목은 [IPHONE](IPHONE.md)에 있다.

- 대각선 이동 속도가 직선과 같은지
- 카메라 기준 이동과 입력을 놓았을 때 정지하는지
- 벽·바닥을 통과하지 않는지
- 캐릭터가 이동할 때 카메라 거리와 회전이 유지되는지

2026-10-31 Mac에서 Unity `6000.3.27f1`의 `-batchmode -nographics`로 실행했고 Motor 4개·Touch 12개 모두 통과했다. 이 결과는 화면 렌더링 없이 실행한 Editor 테스트다. 결과 XML과 실행 로그는 로컬 `MobileMMORPG/Builds/Validation/2026-10-31_224616_KST/`에 보관하며 [STATUS](STATUS.md)에 범위·한계를 기록했다. 실패하면 테스트 이름과 오류를 PR에 남긴다. 오른쪽 마우스 버튼·Esc·포커스 전환은 수동으로도 확인해야 한다.

배치 실행은 `-runTests -testPlatform PlayMode -assemblyNames MobileMMORPG.Prototype.PlayModeTests`로 이 테스트 묶음만 지정하고 `-quit`는 함께 사용하지 않는다. Test Runner가 결과를 저장한 뒤 자체 종료한다. 실행 전 Unity를 닫고 기존 변경을 백업한다. Unity 실행으로 설정 파일이 다시 저장될 수 있으므로 실행 후 Git 변경을 확인한다. 이번 실행에서 다시 채워진 `ps4Passcode`는 값을 출력하지 않고 백업한 뒤 빈값으로 복원했다.

## 수동 검증

[기획 완료 기준](PROTOTYPE.md)의 항목을 하나씩 확인한다. 추가로 다음을 확인한다.

- 낮은 프레임에서도 장애물 앞에서 멈추는지
- 다른 앱으로 전환 후 돌아왔을 때 이동 키가 눌린 채 남지 않는지
- Esc, 오른쪽 버튼 해제, Play 종료 후 포인터를 정상 사용 가능한지
- 씬을 다시 Play해도 캐릭터·카메라가 한 개씩만 생성되는지

이동 속도 4, 회전 감도 0.15, 카메라 거리 6, 상하 각도 -10~65는 초기 조정값이다. 체감 검증 후 바꿀 수 있다. 장애물 뒤에서 카메라가 가려지는 문제·줌은 이번 범위 밖이다. 모바일 터치는 이번 후속 단계에서 추가했다. 처음에는 맵 중앙에서 검증한다.

## 결과 남기기

PR에 사용한 Mac, Unity 버전, Test Runner 결과, 수동 검증 성공·실패와 Console 오류를 기록한다. 확인하지 않은 기획 체크박스는 체크하지 않는다. docs/STATUS.md에도 검증 결과와 다음 할 일을 갱신한다.
