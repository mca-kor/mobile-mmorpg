# iPhone 조작과 실기기 실행

Unity 6.3 LTS `6000.3.27f1`, 프로젝트 `MobileMMORPG/` 기준. iPhone을 첫 모바일 테스트 기기로 사용한다. 메뉴·버튼은 원문 이름으로 표시한다.

현재 상태 (2026-11-01): [PR #7](https://github.com/mca-kor/mobile-mmorpg/pull/7) 병합 커밋 `123e2eb` 기준 새 앱의 Unity·Xcode 빌드, 엄격한 서명 검증, iPhone 설치·실행이 성공했다. iPhone 15 Pro Max·iOS 27.2에서 새 앱 화면·기본 이동·시점이 정상이라는 사용자 보고도 받았다. 이전 Mac PlayMode 16개 통과와 빌드 전 기존 설치 앱의 3단계 검증 결과는 아래에서 구분한다. 사용자 진행 요청으로 `codex/ios-build-validation`의 결과 문서만 별도 PR로 제출하며 실제 반영 상태는 [STATUS](STATUS.md)를 따른다.

## 이번에 확인한 설정

- iPhone Bundle Identifier: `com.ahnmacmini.mobilemmorpg.prototype`. 이번 로컬 파일 검토에서 Xcode 빌드 출력에는 이 값이 있지만 Unity 원본에는 템플릿 식별자가 남아 있음을 확인했다. Unity의 `applicationIdentifier.iPhone`을 지정 값으로 맞췄다. 내보낸 Xcode 프로젝트는 수정하지 않았다.
- 이전 실기기 기록에서는 Xcode의 Automatically manage signing과 Personal Team을 사용해 실행했다.
- Unity 템플릿 기본 식별자로 Failed Registering Bundle Identifier가 나올 경우 고유 식별자로 변경한다. Unity와 이미 내보낸 Xcode 프로젝트 모두에 같은 값을 입력한다.
- 2026-11-01 현재 Mac의 Xcode는 `27.2` (`27B52`)이며, 2026-10-29 기록은 `27.1` (`27A9275`)이었다. 이번에 USB 기기 조회로 iPhone 15 Pro Max·iOS `27.2` (`24B119`), 설치 앱 조회로 `MobileMMORPG 0.1.0`을 확인했다. 기존 앱의 빌드 당시 도구 버전·소스 커밋까지 확인한 것은 아니다. 서명 인증서·계정 정보는 문서에 복사하지 않는다.

## 1. Mac에서 먼저 확인

1. Unity의 Play를 종료하고 저장하지 않은 작업을 보존한 뒤 Unity를 닫는다.
2. GitHub Desktop에서 Fetch origin을 누르고 Current Branch가 [STATUS](STATUS.md)의 작업 브랜치인지 확인한다. PR #6은 이미 main에 포함되어 있다. 로컬 수정은 보존하기 전 다른 브랜치로 옮기지 않는다.
3. Unity Hub에서 기존 MobileMMORPG를 열고 컴파일이 끝날 때까지 기다린다.
4. Project 창에서 Assets → Prototype → Scenes → ThirdPersonPrototype을 연다.
5. Play를 누르고 Game 화면을 클릭한다. 왼쪽 아래 MOVE 원과 오른쪽 LOOK 표시가 보인다.
6. 왼쪽 원을 왼쪽 마우스 버튼으로 누른 채 드래그하면 캐릭터가 이동한다. 버튼을 놓으면 정지한다.
7. 오른쪽 화면에서 왼쪽 마우스 버튼으로 드래그하면 시점이 회전한다. 첫 누름에서 시점이 갑자기 튀지 않아야 한다.
8. 기존 WASD 이동과 오른쪽 마우스 버튼 회전도 확인한다. Mac 마우스 시험은 한 번에 한 손가락만 흉내 낸다.
9. Play를 종료한 뒤 Window → General → Test Runner의 PlayMode → Run All을 실행한다. 기존 테스트 4개와 터치 상태 테스트 12개 사례를 확인한다. 2026-10-31에는 이 16개 사례가 Mac 배치 실행에서 통과했으며 수동 화면·실기기 조작 결과와 구분한다.
10. Window → General → Console에서 첫 빨간 오류가 있으면 전체 내용을 알려준다.

Editor에 가상 Touchscreen이 연결되어 있으면 마우스 모사 대신 해당 터치 입력을 사용한다. 실기기와 Editor는 같은 손가락 역할·좌표 규칙을 사용하지만, iOS의 실제 이벤트·화면·성능까지 Mac에서 검증할 수는 없다.

## 2. iPhone 빌드 준비

Play를 종료한다. 기존 씬과 로컬 수정은 먼저 저장한다.

1. Tools → Prototype → Prepare iPhone Build를 선택한다.
2. 안내를 읽고 Apply를 누른다. 공통 Scene List에 시제품 씬을 첫 번째로 등록하고, 기존 다른 씬은 보존한다. 가로 화면 양방향 회전만 허용한다. 이 작업은 로컬 ProjectSettings를 변경한다.
3. Tools → Prototype → Switch to iOS를 선택한다. 플랫폼 전환과 에셋 재임포트가 끝날 때까지 기다린다. iOS Build Support가 없으면 설치 안내가 나온다.
4. File → Build Profiles를 연다. 활성 플랫폼이 iOS인지 확인한다. 별도 프로필에서 Override Global Scene List를 사용 중이라면 그 프로필의 Scene List에도 ThirdPersonPrototype을 첫 번째 활성 씬으로 넣는다. 이번 프로젝트에서 별도 프로필이 없다면 공통 목록을 그대로 사용한다.
5. Edit → Project Settings → Player에서 iOS의 Other Settings → Identification → Bundle Identifier가 `com.ahnmacmini.mobilemmorpg.prototype`인지 확인한다. 수정이 잠겨 있으면 Override Default Bundle Identifier를 켠다. 내보낸 Xcode 프로젝트만 수정하면 다음 Unity 빌드에서 원래 값이 다시 적용될 수 있으므로 Unity 원본 설정에도 저장한다.
6. File → Build Profiles로 돌아가 Build를 누른다. 첫 검증은 Build And Run 대신 Build로 Xcode 프로젝트를 내보낸다.
7. 출력 위치는 Unity 프로젝트 안의 `Builds/` 아래 새 폴더로 지정한다. 첫 출력은 `Builds/iOS/`를 사용할 수 있지만 기존 출력이 있으면 덮어쓰지 않는다. 이번 검증은 `Builds/iOS-main-123e2eb-2026-11-01_032809_KST/`를 사용했다. 이 폴더는 .gitignore로 제외된다.

Unity에서는 Build가 끝나 Xcode 프로젝트가 생성되어야 한다. 실패하면 Console의 첫 오류를 전달한다. 생성된 Xcode 프로젝트, 인증서·프로비저닝 파일은 GitHub에 올리지 않는다.

## 3. Xcode에서 iPhone으로 실행

1. 데이터 전송이 가능한 케이블로 iPhone을 Mac에 연결하고 잠금을 해제한다. Trust This Computer? 안내가 나오면 본인 Mac인지 확인하고 Trust를 선택한다.
2. 생성 폴더에 `.xcworkspace`가 있으면 그것을, 없으면 `Unity-iPhone.xcodeproj`를 연다.
3. Xcode → Settings…의 Accounts 또는 Apple Accounts에서 자신의 Apple 계정을 추가한다. 버전에 따라 계정 화면 이름이 다를 수 있다.
4. 프로젝트 탐색기에서 Unity-iPhone을 선택하고 TARGETS의 Unity-iPhone → Signing & Capabilities를 연다.
5. Automatically manage signing을 켜고 Team에서 본인의 팀을 선택한다. 개인 실기기 시험에는 Personal Team을 사용할 수 있지만 설치 유효기간·기능 제한이 있다. 유료 가입부터 할 필요는 없다.
6. 상단 실행 대상에서 연결된 실제 iPhone을 선택한다. 시뮬레이터가 아닌지 확인하고 준비 작업이 끝날 때까지 기다린다.
7. 기기가 요구하면 iPhone의 Settings → Privacy & Security → Developer Mode를 켜고 재시작·확인을 완료한다. iOS 버전·표시 언어에 따라 이름이 다를 수 있다.
8. Xcode에서 Product → Run을 선택한다. 서명·기기 신뢰 관련 오류가 나오면 오류 원문을 전달한다. Apple 계정 비밀번호나 인증 코드는 채팅에 보내지 않는다.
9. 개발자 신뢰를 별도로 요구하는 경우 iPhone의 Settings → General → VPN & Device Management에서 본인 개발자 항목을 확인한다. 화면에 요구가 없으면 이 단계는 생략한다.

## 4. iPhone 조작

- 왼쪽 아래 MOVE 원 안에서 누른 채 드래그하면 이동한다. 손가락을 떼면 멈춘다. 원 밖의 왼쪽 화면에서 시작한 터치는 이동을 시작하지 않는다.
- 오른쪽 안전 영역에서 누른 채 드래그하면 시점이 회전한다. 손가락을 떼면 회전이 멈춘다.
- 두 손가락을 동시에 사용한다. 오른쪽을 먼저 누르거나 한쪽만 떼어도 다른 쪽 조작은 유지되어야 한다.
- 손가락 역할은 시작 위치에서 정해진다. 중앙을 넘어도 이동·시점 역할이 바뀌지 않는다. 세 번째 손가락은 사용 중인 조작을 빼앗지 않는다.
- 노치·홈 표시 영역은 시작 영역에서 제외한다. 가로 방향을 반대로 돌리면 입력을 초기화하므로 손가락을 떼었다가 다시 누른다.
- 앱 전환·잠금·터치 취소·입력 장치 상실 뒤에는 이동·회전이 눌린 채 남지 않아야 한다.

## 5. 검증 목록과 기록

- [ ] Mac에서 기존 WASD·오른쪽 버튼 조작 정상
- [ ] Mac 마우스 모사로 MOVE·LOOK을 각각 확인
- [x] PlayMode 테스트 결과 확인 (2026-10-31 Mac 배치 실행, 기존 4 + 터치 12 = 16개 통과)
- [x] 새 iPhone 빌드·엄격한 서명 검증·설치·실행 성공 (2026-11-01 도구 결과, main `123e2eb` 기준)
- [x] 새 앱의 맵·캐릭터 표시, 분홍 재질 없음, 기본 이동·시점 정상 (2026-11-01 사용자 보고)
- [x] 왼쪽 이동과 오른쪽 회전 동시 조작 정상 (2026-11-01 빌드 전 기존 앱 사용자 보고)
- [x] 한 손씩 떼면 해당 조작만 멈추고 다른 조작 유지 (2026-11-01 빌드 전 기존 앱 사용자 보고)
- [ ] 오른쪽 먼저 누르기·세 번째 손가락 시험 정상
- [ ] 화면 중앙을 넘어도 역할 유지, 손가락을 떼면 해당 조작 정지
- [x] 양쪽 가로 방향에서 노치·홈 표시와 조작 원이 겹치지 않음 (2026-11-01 빌드 전 기존 앱 사용자 보고)
- [x] 가로 방향 회전 시 이전 입력 정지, 새 터치로 정상 재개 (2026-11-01 빌드 전 기존 앱 사용자 보고)
- [x] 앱 전환·화면 잠금·복귀 후 입력이 남지 않고 새 터치로 정상 재개 (2026-11-01 빌드 전 기존 앱 사용자 보고)
- [ ] 충돌과 카메라 상하 제한 유지, 새 오류 없음

PR에 iPhone 모델, iOS·Xcode·Unity 버전, 성공·실패 항목과 첫 오류를 남긴다. 사용자 보고와 직접 실행 결과를 구분해 STATUS에 기록한다.

2026-11-01에는 기존 설치 앱 `0.1.0`으로 위 3단계 수동 검증을 진행했다. USB 연결·앱 조회·실행 명령 성공은 Mac 도구 결과이며, 화면과 실제 손가락 동작·회전·Home/잠금 복귀는 사용자 관찰 결과다. 소스 커밋과 기존 설치 앱의 정확한 대응은 미확인이다. 일반적인 장치 상실·터치 취소와 성능까지 통과한 것으로 확대하지 않는다.

이후 main `123e2eb` 기준으로 새 앱 `0.1.0` (빌드 `0`)을 만들고 설치·실행했다. Unity는 오류 0·경고 1, Xcode는 오류 0·경고 29였고 엄격한 서명 검증도 통과했다. 새 앱의 화면과 기본 이동·시점 정상은 사용자 보고다. 이전 3단계 전체를 새 앱에서 다시 확인한 것은 아니다. 새 앱에 붙은 FinderInfo 4개는 사본 보관 후 정리했으며 상세 절차·경고·한계는 [STATUS](STATUS.md)를 따른다.

## 6. GitHub에 반영

PR #6은 2026-10-29 (한국 시간), 설정·검증 기록의 [PR #7](https://github.com/mca-kor/mobile-mmorpg/pull/7)은 2026-11-01 03:21:45 (한국 시간)에 병합됐다. 이후 최신 main 기준의 새 빌드를 `codex/ios-build-validation`에서 검증했고 사용자 요청으로 결과 문서 3개를 별도 커밋·PR로 제출한다. 실제 반영 상태는 [STATUS](STATUS.md)를 따른다. 이후 새 변경은 작업 브랜치에서 검토·커밋·푸시하고 Create Pull Request로 제출하며 병합은 사용자가 진행한다. 인증·서명은 사용자 기기에서만 관리한다.

Prepare iPhone Build로 생긴 EditorBuildSettings·ProjectSettings 변경은 자동 푸시되지 않는다. 로컬 수정이 남아 브랜치 전환이 막히면 먼저 Changes를 확인하고 빌드 설정 변경을 별도 커밋·PR로 보관한다. 내용을 모른 채 Discard Changes를 누르지 않는다. Bundle Identifier는 공유 가능한 설정이지만 서명 키·인증서는 저장소에 넣지 않는다.

제출 점검에서는 기존 Git 이력부터 비어 있지 않았던 `ProjectSettings.asset`의 `ps4Passcode`를 비밀값으로 감지해 업로드가 거부됐다. 값은 출력하지 않고 제출 설정에서 빈값으로 바꿨다. 원본은 로컬 백업에 보존하며 이 변경으로 기존 Git 이력이 지워지는 것은 아니다. 자세한 반영 상태와 남은 확인 사항은 [STATUS](STATUS.md)를 따른다.

2026-10-31에는 Unity 테스트 실행 중 이 항목이 다시 채워졌다. 생성된 설정 사본은 Git에서 제외된 검증 폴더에 보관하고, 해당 항목만 실행 전의 빈값으로 복원했다. Unity를 열어 작업한 뒤에는 커밋 전에 값이 비어 있는지 확인하며 생성된 값·원본 로그는 업로드하지 않는다.

이번 작업 시작 시점의 수정 파일 8개와 미추적 설정 파일 2개는 로컬 `Builds/LocalStateBackups/2026-10-29_021356_KST/`에 사본·변경 패치·SHA-256(파일 내용 비교용 값) 목록으로 보관했다. 이 경로는 Unity 프로젝트 기준이며 Git에서 제외된다. `ShaderGraphSettings 2.asset`과 `URPProjectSettings 2.asset`은 각각 원본과 내용이 같지만 삭제하지 않았다. 백업·빌드 출력은 다른 기기에 자동 동기화되지 않는다. GitHub에 올릴 파일과 기존 자산 변경의 검토 결과는 [STATUS](STATUS.md)를 따른다.
