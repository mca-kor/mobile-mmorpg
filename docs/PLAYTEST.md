# 첫 시제품 실행과 검증

Unity 6.3 LTS `6000.3.27f1` 기준. 현재 코드는 클라우드에서 작성했으며 Unity 컴파일·실행·빌드는 미실행이다. Mac 검증 전에는 완료로 판단하지 않는다.

## 병합 전에 Mac에서 실행하기

1. Unity에서 Play 중이면 Play를 다시 눌러 멈춘다. 저장할 기존 변경이 있다면 저장·커밋하고 Unity를 닫는다.
2. GitHub Desktop에서 Fetch origin을 누른다. Current Branch에서 `feature/third-person-prototype`를 선택한다. 로컬 변경 충돌 안내가 나오면 강제로 덮어쓰지 않는다.
3. Unity Hub의 Projects에서 기존 `MobileMMORPG`를 연다. 스크립트 컴파일이 끝날 때까지 기다린다.
4. Unity의 Project 창에서 `Assets → Prototype → Scenes`를 열고 `ThirdPersonPrototype`을 더블 클릭한다. 기존 SampleScene은 변경하지 않는다.
5. 이 씬은 Play 전에 바닥과 캐릭터가 보이지 않는 것이 정상이다. Hierarchy에는 Third Person Prototype이 있고, Play를 누르면 전용 스크립트가 맵·캐릭터·카메라를 만든다.
6. 상단 Play를 누른 뒤 Game 화면을 클릭한다.
7. WASD로 이동하고 마우스 오른쪽 버튼을 누른 채 움직여 시점을 회전한다. 캐릭터 앞의 노란 표시로 바라보는 방향을 확인한다.
8. 오른쪽 버튼을 놓으면 포인터가 복원된다. Esc로도 해제할 수 있으며, 다시 회전하려면 오른쪽 버튼을 놓았다가 다시 누른다. 다른 앱으로 전환하거나 Play를 종료해도 포인터를 복원한다.
9. Window → General → Console에서 빨간 오류가 없는지 확인한다. 오류가 있으면 첫 오류 전체를 복사해 알려준다.
10. Play를 다시 눌러 종료한다. 실행 중 생성된 오브젝트가 사라지는 것은 정상이다.

PR 검증을 위해 브랜치를 바꾼 것이므로 먼저 병합할 필요는 없다. 검증과 사용자 검토 후 GitHub에서 Merge pull request → Confirm merge를 진행하고, GitHub Desktop에서 main으로 전환한 뒤 Fetch origin → Pull origin으로 받는다.

## 자동 테스트

Play를 종료한 뒤 Window → General → Test Runner를 연다. PlayMode 탭에서 Run All을 누른다. PrototypeMotorTests의 아래 4개 결과를 확인한다.

- 대각선 이동 속도가 직선과 같은지
- 카메라 기준 이동과 입력을 놓았을 때 정지하는지
- 벽·바닥을 통과하지 않는지
- 캐릭터가 이동할 때 카메라 거리와 회전이 유지되는지

테스트 코드는 포함했지만 클라우드에서 실행하지 못했다. 실패하면 테스트 이름과 오류를 PR에 남긴다. 오른쪽 마우스 버튼·Esc·포커스 전환은 수동으로도 확인해야 한다.

## 수동 검증

[기획 완료 기준](PROTOTYPE.md)의 항목을 하나씩 확인한다. 추가로 다음을 확인한다.

- 낮은 프레임에서도 장애물 앞에서 멈추는지
- 다른 앱으로 전환 후 돌아왔을 때 이동 키가 눌린 채 남지 않는지
- Esc, 오른쪽 버튼 해제, Play 종료 후 포인터를 정상 사용 가능한지
- 씬을 다시 Play해도 캐릭터·카메라가 한 개씩만 생성되는지

이동 속도 4, 회전 감도 0.15, 카메라 거리 6, 상하 각도 -10~65는 초기 조정값이다. 체감 검증 후 바꿀 수 있다. 장애물 뒤에서 카메라가 가려지는 문제·줌·모바일 터치는 이번 범위 밖이다. 처음에는 맵 중앙에서 검증한다.

## 결과 남기기

PR에 사용한 Mac, Unity 버전, Test Runner 결과, 수동 검증 성공·실패와 Console 오류를 기록한다. 확인하지 않은 기획 체크박스는 체크하지 않는다. docs/STATUS.md에도 검증 결과와 다음 할 일을 갱신한다.
