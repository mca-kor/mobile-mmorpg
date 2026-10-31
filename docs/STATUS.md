# 개발 진행 상황

최종 갱신: 2026-11-01 (한국 시간)

## 현재 단계

Mac PlayMode 테스트 16개 전체 통과 및 iPhone 주요 실기기 검증 3단계 정상 사용자 보고를 기록했다. USB 연결·설치 앱 조회·실행 명령은 도구로 확인했고, 화면·동시 조작·손가락 해제·가로 회전·앱 전환·잠금 복귀는 사용자 관찰을 근거로 한다. PR #7은 사용자 검토·병합 대기다.

## 최신 인계

### 2026-11-01 iPhone 실기기 검증 결과

- 사용자가 다음 실기기 검증 시작을 요청했다. GitHub와 `git fetch origin`으로 PR #7이 열려 있으며 병합되지 않았음을 확인했다. 작업 브랜치는 `codex/mac-local-iphone-settings`, 기준 커밋은 `3979842`다.
- 현재 Mac에서 확인한 Xcode는 `27.2` (`27B52`)다. 이전 검증의 `27.1` 기록과 구분하며 이번 작업에서 도구 업그레이드는 하지 않았다.
- 기존 Xcode 프로젝트와 실기기용 `MobileMMORPG.app` 빌드 산출물이 남아 있다. 앱 메타데이터의 Bundle Identifier는 `com.ahnmacmini.mobilemmorpg.prototype`, 버전은 `0.1.0`, 빌드 번호는 `0`이다. 이 산출물 정보만으로 현재 iPhone에 정확히 같은 빌드가 설치됐다고 확인할 수는 없다.
- 초기 기기 목록 자동 조회가 제한 시간 안에 완료되지 않아 연결·기기 모델·iOS 버전은 미확인이었다. iPhone을 Mac에 연결하고 잠금을 해제한 뒤 시제품 앱이 열리는지 사용자에게 확인을 요청했다.
- 사용자가 처음에는 iPhone 연결이 어렵다고 답했고, 이후 연결했다고 보고했다. 처음의 연결 불가 상태와 아래 직접 확인한 결과를 구분한다.
- Xcode의 다른 기기 조회 도구인 `xcdevice`로 **iPhone 15 Pro Max / iOS 27.2 (24B119) / USB / 사용 가능** 상태를 확인했다. 기기 식별자와 계정 정보는 기록에 포함하지 않는다.
- 해당 기기를 지정해 설치된 `com.ahnmacmini.mobilemmorpg.prototype` 앱을 조회했고 이름 `MobileMMORPG`, 버전 `0.1.0`을 확인했다. 이어 `devicectl`의 앱 실행 명령이 종료 코드 0·`success`를 반환했다. 앱 화면 표시나 실제 터치 성공을 관찰한 결과는 아니다.
- 기존 파일 108개를 로컬 `MobileMMORPG/Builds/Validation/2026-11-01_023738_KST_iphone/`에 백업했고, 준비 작업 후 기존 파일 내용이 같음을 확인했다. 이 폴더에 기기 조회·앱 실행의 원본 결과와 수동 검증 기록을 로컬로 보관한다. 원본 로그·기기 식별자·서명 정보는 GitHub 반영 대상에서 제외한다.
- 사용자가 1차 검증을 모두 정상으로 보고했다. 맵·캐릭터 표시와 분홍 재질 없음, MOVE와 시점의 동시 조작, 오른손 해제 시 이동 유지, 왼손 해제 시 시점 조작 유지를 사용자 보고로 통과 처리한다. 직접 화면·손가락 동작을 관찰한 것은 아니다.
- 사용자가 2차 검증도 모두 정상으로 보고했다. 양쪽 가로 방향에서 MOVE가 노치·홈 표시와 겹치지 않음, 회전 시 이전 입력이 멈춤, 손을 뗐다가 다시 누르면 정상 조작됨을 사용자 보고로 통과 처리한다.
- 사용자가 3차 검증도 모두 정상으로 보고했다. 이동 중 Home으로 나갔다 복귀하거나 화면을 잠갔다 해제한 뒤, 손을 대지 않으면 이동·회전이 멈추고 새 터치로 정상 조작되며 앱 종료·입력 불능이 없음을 사용자 보고로 통과 처리한다. 이번에 요청한 3단계 모두 정상 보고를 받았다.
- 새 빌드·서명·설치, 오른쪽 먼저 누르기·세 번째 손가락·화면 중앙 횡단, 실기기 충돌·카메라 상하 제한·Console 오류·성능은 이번 3단계에서 확인하지 않았다. Mac 실제 키보드·마우스와 일반적인 장치 상실·터치 취소도 별도 항목이다.
- 진행 순서: 앱 실행·분홍 재질 유무와 MOVE/LOOK 동시 입력 및 한 손씩 해제 → 양쪽 가로 방향의 안전 영역·입력 초기화 → Home·잠금 복귀 후 입력 잔류 확인. 물리 조작 결과는 사용자 관찰을 받아 기록한다.
- 기존 Xcode 프로젝트를 열어 설정과 실행 대상을 살펴봤으며 Unity·Xcode 새 빌드·앱 설치는 **미실행**이다. 기존 설치 앱의 실행 명령만 수행했다. 게임 코드·설정은 수정하지 않았고 기존 렌더링 버전 기록 한 줄과 미추적 설정 3개를 보존했다.
- 이번에는 기존 설치 앱 `0.1.0`을 검증했다. 이 앱과 특정 소스 커밋의 정확한 대응은 확인하지 않았으므로 새 Unity·Xcode 빌드 성공으로 표현하지 않는다.
- README·STATUS·IPHONE의 결과 문서 3개만 작업 브랜치에 커밋하고 연결된 GitHub 도구로 PR #7에 보완한다. 같은 내용의 로컬 커밋은 `codex/iphone-device-validation-local`에 보존하고 작업 브랜치를 원격 기록과 맞춘다. PR 병합·자동 병합은 진행하지 않는다.

### 2026-10-31 Mac 자동 테스트

- GitHub와 `git fetch origin`으로 PR #7이 열린 상태이며 병합되지 않았음을 확인했다. `codex/mac-local-iphone-settings`의 `d6292af`에서 검증을 진행했다. main은 `2356850`이며 병합·브랜치 전환은 하지 않았다.
- 작업 시작 시 `Mobile_RPAsset.asset`의 `k_AssetPreviousVersion` 12→13 한 줄이 로컬에 추가로 남아 있었다. URP의 버전 변환 완료 기록과 부합하며 렌더링 수치 변경은 없다. 이번 검증 문서 제출에서는 제외하고 보존한다.
- Unity와 Xcode가 종료된 상태를 확인하고, 기존 파일 107개를 `MobileMMORPG/Builds/Validation/2026-10-31_224616_KST/`에 사본·SHA-256 목록으로 백업했다. 기존 중복 설정 2개도 포함했다. 검증 출력과 백업은 Git에서 제외된다.
- 지정 버전 Unity `6000.3.27f1` (`672d562a42a7`)을 배치 모드(화면 조작 없이 실행하는 방식)로 실행했다. C# 컴파일 오류 없이 기존 PlayMode 테스트 16개가 모두 통과했다. 상세 범위와 결과는 아래 검증 기록을 따른다.
- 테스트 중 Unity가 빈 `ps4Passcode`를 비어 있지 않은 새 값으로 채웠고 `ProjectSettings/SceneTemplateSettings.json`을 생성했다. 값은 출력하지 않았다. 생성 직후 사본을 로컬에 보관하고, 다른 설정 변경이 없음을 확인한 뒤 `ProjectSettings.asset`을 테스트 전의 빈값 상태로 복원했다. 새 기본 설정 파일은 로컬 미추적 상태로 보존하고 제출에서 제외한다.
- 문서 갱신 전 기존 107개 파일이 테스트 전 백업과 내용이 같음을 확인했다. iPhone 식별자는 그대로 유지되며 게임 코드·테스트·씬·패키지·Unity 버전을 수정하지 않았다.
- 이번 단계에서는 검증 결과와 실행 안내 문서 5개만 PR #7에 보완한다. 기획·비용·서비스의 새 결정은 없으며 병합은 사용자가 진행한다.
- 직접 푸시 인증을 설정하는 대신 연결된 GitHub 도구로 문서 변경을 반영한다. 같은 내용의 로컬 기록은 `codex/mac-playmode-validation-local`에 보존하고 작업 브랜치는 PR #7 원격 기록과 맞춘다.

### 2026-10-29 GitHub 제출 점검

- 사용자가 로컬 검토 결과를 확인하고 다음 단계 진행을 요청했다. 설정·자산 8개와 문서 6개를 별도 작업 브랜치의 PR로 제출하며 병합은 사용자가 진행한다.
- 터미널의 GitHub 인증 정보가 없어 직접 `git push`는 실패했다. 연결된 GitHub 도구로 검토한 파일 내용을 반영했다. 원래 로컬 커밋 `ef6a3f4`·`3341501`은 `codex/mac-local-iphone-settings-local-backup` 브랜치에 보존하며 원격 제출 대상에서 제외한다. 인증 정보나 비밀번호를 출력·복사하지 않았다.
- 자동 승인 검토가 `ProjectSettings.asset`의 비어 있지 않은 `ps4Passcode`를 비밀값으로 판단해 첫 파일 업로드를 거부했다. 값은 출력하지 않았다. 이 항목은 기존 main과 최초 Unity 설정 커밋에도 같은 값으로 추적돼 있었으며 이번 작업에서 새로 추가한 값이 아니다.
- 안전한 제출을 위해 `ps4Passcode`를 빈값으로 바꿨다. 실제 사용 중인 자격증명인지는 미확인이며 기존 Git 이력의 값이 제거된 것은 아니다. 실제로 사용한 값이라면 별도 폐기·교체가 필요하다. 거부된 파일 내용은 재전송하지 않는다.
- 원래 로컬 변경 10개 중 9개는 백업과 내용이 같다. `ProjectSettings.asset`은 지정 iPhone 식별자와 `ps4Passcode` 비우기의 두 항목만 다르며 원래 사본은 로컬 백업에 보존한다. 그 외 설정·자산과 중복 설정 2개는 보존한다.
- GitHub 제출 결과: [PR #7](https://github.com/mca-kor/mobile-mmorpg/pull/7) 생성, 대상 `main`, 작업 브랜치 `codex/mac-local-iphone-settings`. 설정 커밋은 `109b75e4c836d5cf60615f6951d399c3b2772682`이며 제출 결과를 문서 커밋으로 보완한다. 로컬 작업 브랜치는 같은 이름의 원격 브랜치를 추적한다. 병합·자동 병합은 진행하지 않았다.
- 원격 설정 커밋의 파일 트리가 검토한 로컬 파일 트리와 일치함을 확인했다. 최종 제출 파일은 14개이며 중복 설정·백업·빌드 출력·서명 자료는 제외했다. 로컬 main은 별도로 갱신하지 않았다.
- Unity·Xcode 빌드·PlayMode 테스트·iPhone 재실행은 이번 제출 점검에서도 미실행이다.

### 2026-10-29 Mac 로컬 개발 재개

- 작업 경로가 Mac의 `mobile-mmorpg` 저장소임을 확인하고 AGENTS·README·STATUS·DECISIONS·SETUP·PROTOTYPE·IPHONE·PLAYTEST를 읽었다. GitHub의 main 문서·PR 기록과 대조했다.
- [PR #6](https://github.com/mca-kor/mobile-mmorpg/pull/6)은 한국 시간 2026-10-29 02:01:53에 병합됐다. 병합 커밋은 `2356850c4717d14a363d6637ada23d8de568ae98`이다. 이번 작업에서 병합한 것이 아니다.
- 시작 브랜치는 `feature/iphone-touch-controls`, 커밋은 `47aaa6b8ac6171eae20e5123efb0c4b62b84826b`였다. `git fetch origin` 후 원격의 실기기 보고 문서 커밋과 병합 기록을 확인했다.
- 작업 브랜치: `codex/mac-local-iphone-settings`. 검토 준비 시 `origin/main`의 `2356850`을 기준으로 만들고 기존 로컬 변경을 가져왔다. 이전 작업 브랜치는 보존했다. 생성 당시 새 브랜치는 원격 추적 연결이 없었으며, 로컬 `main` 자체를 갱신한 것은 아니다. 이후 제출 상태는 위 최신 기록을 따른다.
- 시작 시 수정 파일 8개와 미추적 파일 2개를 `MobileMMORPG/Builds/LocalStateBackups/2026-10-29_021356_KST/`에 보관했다. 원본 사본·변경 패치·SHA-256(파일 내용 비교용 값) 목록을 포함하며 Git에서 제외된다. 이 백업과 기존 Xcode 출력은 로컬에만 있다.
- 사용자가 지정한 Bundle Identifier `com.ahnmacmini.mobilemmorpg.prototype`은 내보낸 Xcode 프로젝트에는 있었지만 Unity 원본 설정에는 템플릿 값이 남아 있었다. Unity·Xcode가 종료된 상태에서 `ProjectSettings.asset`의 `applicationIdentifier.iPhone` 한 항목만 맞췄다. 다음 Unity 빌드에 지정 값을 유지하기 위한 변경이다.
- 아래 기존 설정·자산 변경을 보존했다. 표의 경로는 Unity 프로젝트 `MobileMMORPG/` 기준이다. 씬·코드·패키지·Unity 버전은 수정하지 않았다. README·PROTOTYPE·PLAYTEST·IPHONE의 진행 상태와 인계 절차, DECISIONS의 D-017도 갱신했다.

| 기존 로컬 변경 | 검토 결과·보존 내용 |
| --- | --- |
| `ProjectSettings/EditorBuildSettings.asset` | 시제품 씬이 첫 번째 활성 씬이며 기존 SampleScene 유지 |
| `ProjectSettings/ProjectSettings.asset` | 세로 회전 금지·가로 양방향 허용, 기존 Input Actions 사전 로딩, iPhone 배칭 설정 유지. 지정 iPhone 식별자 저장 및 제출 점검에서 `ps4Passcode` 비움 |
| `Assets/Prototype/Resources/PrototypeBase.mat` | 재질 버전 정보·속성 저장 형식 변경 유지 |
| `Assets/Settings/DefaultVolumeProfile.asset` | `worldOffset`·`filter` 추가와 컴포넌트 참조 2개 제외 유지 |
| `Assets/Settings/Mobile_RPAsset.asset` | 모바일 렌더링 자산 버전 12→13·설정 저장 형식 변경 유지 |
| `Assets/Settings/UniversalRenderPipelineGlobalSettings.asset` | 실행용 렌더링 설정 목록 추가 유지 |
| `ProjectSettings/ShaderGraphSettings.asset` | `overrideShaderVariantLimit: 0` 추가 유지 |
| `ProjectSettings/UnityConnectSettings.asset` | `m_Enabled` 0→1 유지. Ads·Analytics·Purchasing·클라우드 오류 보고는 비활성. 유료 서비스 도입 결정 없음 |
| `ProjectSettings/ShaderGraphSettings 2.asset`, `ProjectSettings/URPProjectSettings 2.asset` | 각각 원본과 내용이 동일한 미추적 파일. 생성 경위는 미확인이고 삭제하지 않음 |

재질·렌더링 변경은 Unity가 자산을 다시 저장한 흔적과 부합하지만 생성 경위를 확정하지 않는다. 기존 자산을 임의로 되돌리지 않았다. 중복 파일 2개는 이번 PR에서 제외하고 로컬에 보존했다.

- 로컬 검토 완료 당시에는 새 커밋·푸시·PR 생성·병합이 없었다. 이후 사용자가 결과를 확인하고 다음 단계 진행을 요청해, 검토한 설정·자산 8개와 문서 6개를 별도 커밋·푸시·PR로 제출한다. 병합은 사용자가 진행한다. 실제 제출 결과는 위 최신 기록을 따른다.
- 내용이 같은 `ShaderGraphSettings 2.asset`·`URPProjectSettings 2.asset`은 로컬과 백업에 남기고 제출 대상에서 제외한다. Xcode 출력·백업·서명 자료도 포함하지 않는다.

### 2026-10-29 실기기 결과

- 사용자가 Mac에서 모바일 조작이 정상 작동한다고 보고한 뒤, iPhone에서도 잘 작동하며 이상이 없어 보인다고 보고했다.
- 첨부 스크린샷에서 Xcode의 실기기 실행 상태와 자동 서명 설정을 확인했다. 게임 조작 전체를 직접 관찰한 것은 아니다.
- Bundle Identifier는 Unity와 Xcode에 `com.ahnmacmini.mobilemmorpg.prototype`로 설정한 화면을 확인했다. Unity 기본 식별자의 등록 오류는 고유 식별자로 변경해 진행했다.
- 이 결과 기록 당시 PR #6은 열린 상태였다. 이후 병합됐으며 최신 상태는 위 Mac 로컬 개발 재개 기록을 따른다.
- PlayMode 16개 사례의 실행 결과, 기기 모델·iOS·Xcode 정확한 버전, 양쪽 가로 회전·앱 전환 등 개별 항목은 별도 미확인이다. 체크리스트를 일괄 통과 처리하지 않는다.
- Xcode에 경고 표시가 있으나 내용은 아직 확인하지 않았다. 실행 중이라는 사실만으로 모든 경고가 해결된 것으로 보지 않는다.

### 구현 당시 기록

- PR #5가 main에 병합된 것을 확인했다. Mac 정상 작동은 사용자 보고이며 테스트 4개 전체 통과를 뜻하지 않는다.
- iPhone을 첫 테스트 기기로 선택했다. Xcode·iOS Build Support 설치 완료는 사용자 보고다.
- 작업 브랜치: `feature/iphone-touch-controls`. 조이스틱·드래그·동시 입력·안전 영역, 취소·앱 전환 초기화, Mac 마우스 모사 추가.
- 새 터치 상태 테스트 12개 사례와 iPhone 빌드 준비 메뉴, 재질 리소스·런타임 컴포넌트 보존 설정을 추가했다.
- [IPHONE](IPHONE.md)에 Mac 검증·빌드·Xcode 서명·실기기 실행 절차를 기록했다. Unity·Xcode·iPhone 실행은 이 환경에서 검증하지 못했다.
- 기존 씬·패키지와 저장소의 ProjectSettings는 보존한다. 새 메뉴를 Mac에서 실행하면 로컬 ProjectSettings가 바뀌므로 검토·기록이 필요하다.
- 이번 기능은 별도 PR에서 사용자 검토·병합한다. main에 직접 반영하지 않는다.

## 이전 인계: Mac 시제품 구현 당시

- [PR #2](https://github.com/mca-kor/mobile-mmorpg/pull/2) 병합을 GitHub에서 확인함. 병합 커밋: `db6ba5c8076cac27dd581eca386660422b55e5f0`.
- Unity 6.3 LTS `6000.3.27f1`, Universal 3D(URP), 경로 `MobileMMORPG/`.
- Mac에서 main 받기 완료 및 이전 편집기 Console 오류 없음은 사용자 보고다. 병합 후 재실행 여부는 별도 미확인.
- GitHub의 main 버전 파일 확인 완료. 앞선 파일 검토에서 Force Text, Visible Meta Files 및 캐시 제외를 확인했다.
- [PR #3](https://github.com/mca-kor/mobile-mmorpg/pull/3) 병합 및 main의 문서 갱신 확인. 사용자는 안내 절차 완료를 보고했다.
- 첫 시제품: [PROTOTYPE](PROTOTYPE.md). WASD 이동과 3인칭 카메라, 오른쪽 마우스 버튼을 누른 채 회전. 모바일 입력은 후속 단계다.
- 기획 PR #4가 main에 반영된 것을 확인했다. 사용자 요청으로 이동·충돌·카메라 첫 구현을 진행했다.
- 작업 브랜치: `feature/third-person-prototype`. 전용 씬·스크립트·PlayMode 테스트 4개와 실행 안내 추가.
- 기존 SampleScene·패키지·프로젝트 설정 보존. 새 씬은 Play 때 임시 맵을 생성한다.
- [PLAYTEST](PLAYTEST.md)에서 Mac 실행 방법과 검증 목록을 확인한다. 이번 PR은 사용자 검토 후 병합하며 실행 성공으로 표시하지 않는다.
- 클라우드에서 Unity 실행·빌드, 스마트폰·태블릿 인계 검증은 미실행.

아래 저장소 분석과 초기 검증은 최초 구축 당시의 기록이다.

## 저장소 분석

- 대상: https://github.com/mca-kor/mobile-mmorpg
- 확인 기준: 연결된 로컬 작업 사본의 `a97c244` (Initial commit), 작업 브랜치 `work`.
- 작업 전 추적 파일: `.gitattributes` 한 개. 내용은 `* text=auto` 기반 줄바꿈 정규화.
- 작업 전 수정 사항 없음. Unity 프로젝트, 서버, 빌드·테스트 설정 없음.
- PR 준비 시 GitHub에서 기본 브랜치 `main`과 최신 커밋 `a97c244ab1c87a6af5cd9116f10445a784758064`를 확인함. 로컬 기준 커밋과 일치.

## 이번에 준비한 내용

- [x] README와 Codex 규칙 작성
- [x] 상태·의사결정 기록 형식 마련
- [x] Windows·macOS 준비 절차와 모바일 검토 흐름 작성
- [x] GitHub·Codex Cloud 인계 절차 작성
- [x] Unity용 .gitignore 작성, 기존 .gitattributes 보존
- [x] 초기 문서와 빈 Unity 프로젝트의 PR을 사용자 검토 후 기본 브랜치에 병합
- [ ] Mac mini의 도구 설치와 계정 연결을 실제로 확인
- [ ] 기기를 바꿔 저장소를 이어 읽는 절차를 실제로 확인

## 검증 기록

2026-10-31 Mac에서 직접 실행한 PlayMode 테스트:

| 테스트 묶음 | 전체 | 통과 | 실패 | 건너뜀 |
| --- | --- | --- | --- | --- |
| PrototypeMotorTests (이동·충돌·카메라) | 4 | 4 | 0 | 0 |
| PrototypeTouchStateTests (터치 상태 처리) | 12 | 12 | 0 | 0 |
| 합계 | 16 | 16 | 0 | 0 |

- 실행 옵션: `-batchmode -nographics -runTests -testPlatform PlayMode -assemblyNames MobileMMORPG.Prototype.PlayModeTests`. 완료 후 Test Runner가 자체 종료하도록 `-quit`는 사용하지 않았다.
- Unity 프로세스 종료 코드 0. `playmode-results.xml`의 최종 결과 `Passed`, 전체 16·통과 16·실패 0·건너뜀 0을 파싱해 확인했다. 실행 로그에 C# 컴파일 오류가 없었다.
- 원본 결과 XML·로그·요약 JSON·실행 전후 사본은 위 로컬 검증 폴더에 있다. 기기별 경로·진단 정보를 포함할 수 있어 GitHub에 올리지 않는다.
- 화면 렌더링 없이 실행한 Editor 테스트다. Mac 화면 표시·실제 키보드·마우스, iPhone 렌더링·실제 동시 터치·양방향 가로 화면·앱 전환·잠금 복귀·성능은 이번 테스트에 포함되지 않는다. Xcode 빌드와 iPhone 재실행은 **미실행**이다.
- Unity 실행 뒤 `ps4Passcode`가 다시 채워질 수 있으므로 커밋 전 값의 비어 있음과 변경 범위를 확인한다. 생성된 값이나 로그를 출력·업로드하지 않는다. 이번에는 해당 항목만 실행 전 상태로 복원하고 기존 파일 보존을 확인했다.
- 기기 목록 조회가 제한 시간 안에 완료되지 않아 iPhone 모델·iOS 버전은 이번 작업에서도 확인하지 못했다. 앱 설치·실행이나 기기 설정 변경은 수행하지 않았다.
- 문서 상대 링크 28개·구조·공백 검사와 검증 폴더의 Git 제외를 확인했다. 실행 전 기존 파일은 이번에 갱신하는 문서 5개를 제외하고 모두 백업과 내용이 같다.

2026-10-29 Mac 로컬 설정·문서 초기 검사 (비밀 필드 제출 점검 전):

- `git fetch origin` 성공. GitHub의 기본 브랜치가 main이고 PR #6이 병합된 것을 연결 도구와 최신 로컬 원격 기록으로 확인했다.
- 원래 로컬 변경 10개를 백업과 비교해 보존을 확인했다. `ProjectSettings.asset`은 iPhone 식별자 한 줄만 다르고 나머지 9개 파일은 백업과 내용이 같다.
- iPhone 식별자·가로 회전·실기기 SDK·씬 경로와 GUID 연결, 변경 자산의 기존 .meta 대응을 확인했다. Unity 버전은 GitHub와 로컬 모두 `6000.3.27f1`이며 Visible Meta Files·Force Text 설정을 유지했다.
- 문서 상대 링크·구조, 이번 수정 파일의 공백 검사와 Git 무시 규칙을 확인했다. Xcode 출력·백업·캐시·서명 파일은 제외되고 Assets·.meta·Packages·ProjectSettings는 추적 대상이다.
- 전체 `git diff --check`에는 작업 시작부터 있던 `PrototypeBase.mat`의 끝 공백 2곳이 남아 있다. 기존 자산 보존을 위해 수정하지 않았으며 이번 문서·식별자 변경의 검사와 구분한다.
- 현재 Mac 도구 조회: macOS `27.0.1`, `arm64`, Xcode `27.1` (`27A9275`). 과거 iPhone 실행 당시 버전과 같다고 확정하지 않는다.
- 이번 작업의 Unity 편집기 실행·컴파일·PlayMode 16개 사례·Xcode 빌드·iPhone 재실행은 **미실행**이다. 설정 보존 검토를 수행했으며 앱 실행 성공은 사용자 보고다. 기기 모델·iOS·개별 터치·회전·앱 복귀 결과와 이전 Xcode 경고 내용은 미확인이다.

2026-10-28 iPhone 조작 확장의 클라우드 검사:

- 변경 파일 25개, 신규 .meta GUID 9개 중복 없음, 신규 자산·폴더와 메타데이터 대응 확인
- 재질 YAML·URP Lit 참조와 link.xml 구문, Editor 전용 asmdef·문서 링크·공백·소스 괄호 검사 통과
- 씬 파일·Packages·ProjectSettings를 제출 변경에 포함하지 않음을 확인
- 터치 상태 테스트 12개 사례 추가. 기존 PlayMode 4개를 포함한 테스트는 실행하지 못함
- Unity 컴파일·API 호환성·Xcode 빌드·iPhone 실행·안전 영역·실제 동시 터치는 미검증
- 첫 Mac 시제품 정상 작동은 사용자 보고이며 이번 터치 확장의 통과 결과가 아님

2026-10-28 첫 구현의 클라우드 정적 검사:

- 변경 파일 24개, 새 .meta의 GUID 12개 중복 없음
- 씬 YAML 문법과 내부 파일 참조, PrototypeWorld 스크립트 GUID 연결 확인
- Runtime·PlayMode 테스트의 asmdef 참조 및 문서 상대 링크·줄 끝 공백 검사 통과
- 기존 SampleScene·Packages·ProjectSettings를 변경 목록에서 제외한 것을 확인
- PlayMode 테스트 4개 작성. Unity와 C# 컴파일러가 없어 컴파일·테스트·실행·빌드는 미실행
- 위 검사는 Unity 임포트·컴파일·물리·렌더링 성공을 보장하지 않는다. Mac 검증이 필요하다.

2026-10-27 작업 환경에서 아래 검사를 통과했다.

- 필수 파일 6개 존재, UTF-8 읽기와 상대 문서 링크 정상
- `git check-ignore`로 캐시·빌드·비밀정보 경로 10개 제외 확인
- 소스·씬·.meta·패키지·프로젝트 설정·환경 예제 경로 9개 보존 확인
- 기존 `.gitattributes`의 HEAD 내용과 바이트 단위 일치
- 기존 추적 파일 변경 없음, `git diff --check` 및 새 파일 줄 끝 공백 검사 통과

최초 문서 구축 당시 Unity 편집기 실행, 게임 빌드, 실제 기기 테스트는 미실행이었다. 당시에는 Unity 프로젝트가 없었다. GitHub 연결과 저장소 접근을 확인했다. 작업 브랜치는 `docs/initial-setup`이며 PR을 통한 사용자 검토를 요청한다. 기본 브랜치 병합은 사용자가 진행한다. Mac·Windows·모바일의 개별 계정 연결과 기기 전환은 미확인이다.

## 다음 작업

1. [PR #7](https://github.com/mca-kor/mobile-mmorpg/pull/7)의 Files changed에서 설정·자산·문서, Mac 자동 테스트 16개 통과와 iPhone 3단계 정상 사용자 보고를 검토한다. PR 병합은 사용자가 진행한다.
2. 추가 검증이 필요하면 오른쪽 먼저 누르기·세 번째 손가락·화면 중앙 횡단·실기기 충돌·카메라 제한과 이전 Xcode 경고를 확인한다. 새 빌드·서명·설치 검증은 기존 설치 앱의 3단계 통과와 구분한다.
3. 로컬 변경을 보존하고 새 PR이 병합된 뒤 main으로 전환해 Fetch origin → Pull origin으로 갱신한다.
4. 로컬의 렌더링 버전 기록 변경·중복 설정 2개·Unity가 생성한 SceneTemplateSettings.json을 유지하고, 다음 시제품 기능·비용·서비스 도입은 사용자와 범위를 확정한 후 진행한다.

## 기기 변경 시 인계 양식

새 작업을 마칠 때 아래 항목을 갱신한다. 미반영 작업을 완료로 표시하지 않는다.

```text
날짜 / 작업자·기기:
작업 목표:
브랜치 / 커밋 / PR 링크:
완료한 내용:
검증한 내용과 결과:
실행하지 못한 검증과 이유:
GitHub 반영 상태 (로컬 / 푸시 / PR / 병합):
남은 문제:
다음 작업 한 가지:
```
