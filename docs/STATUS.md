# 개발 진행 상황

최종 갱신: 2026-10-28 (한국 시간)

## 현재 단계

초기 문서와 빈 Unity 프로젝트 구축 완료. 첫 시제품 이동·충돌·3인칭 카메라 코드와 전용 씬 준비. Unity 컴파일·실행 검증 대기.

## 최신 인계

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

1. Mac에서 `feature/third-person-prototype` 브랜치를 받아 PLAYTEST에 따라 컴파일·PlayMode 테스트·수동 조작을 확인한다.
2. 결과와 오류를 PR에 기록하고 필요한 수정을 진행한다. 확인 후 사용자 검토·병합한다.
3. Windows 또는 모바일에서 최신 STATUS를 읽어 인계를 확인한다.
4. Mac 기본 동작 검증 이후 모바일 입력과 Android·iOS 우선순위를 정한다.

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
