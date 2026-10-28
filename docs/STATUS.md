# 개발 진행 상황

최종 갱신: 2026-10-29 (한국 시간)

## 현재 단계

Mac 첫 이동 시제품 정상 작동 사용자 보고 및 PR #5 병합 완료. iPhone 터치 조작 구현 후 Mac·iPhone에서 정상 작동 사용자 보고. PR #6 검토·병합 대기.

## 최신 인계

### 2026-10-29 실기기 결과

- 사용자가 Mac에서 모바일 조작이 정상 작동한다고 보고한 뒤, iPhone에서도 잘 작동하며 이상이 없어 보인다고 보고했다.
- 첨부 스크린샷에서 Xcode의 실기기 실행 상태와 자동 서명 설정을 확인했다. 게임 조작 전체를 직접 관찰한 것은 아니다.
- Bundle Identifier는 Unity와 Xcode에 `com.ahnmacmini.mobilemmorpg.prototype`로 설정한 화면을 확인했다. Unity 기본 식별자의 등록 오류는 고유 식별자로 변경해 진행했다.
- PR #6은 열린 상태다. 검증 기록만 같은 작업 브랜치에 추가하며 병합은 사용자가 진행한다.
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

1. PR #6의 갱신된 검증 기록을 사용자 검토 후 병합한다.
2. Mac의 로컬 빌드 설정·Bundle Identifier 변경을 확인해 별도 커밋·PR로 보관한다. 내용 확인 없이 버리지 않는다.
3. main을 최신 상태로 받은 뒤, 미확인 테스트·기기 버전·Xcode 경고와 앱 전환·화면 회전을 추가 확인한다.
4. 다음 시제품 기능을 기획한다.

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
