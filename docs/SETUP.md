# 개발 환경과 기기 간 작업 안내

이 문서는 앞으로 따라 할 설정 절차입니다. 도구 설치나 계정 연결이 이미 완료되었다는 뜻은 아닙니다. 현재는 Unity 프로젝트를 생성하지 않습니다.

## 1. 공통 준비

1. 같은 GitHub 계정과 같은 ChatGPT 계정을 각 기기에서 사용합니다.
2. GitHub에서 https://github.com/mca-kor/mobile-mmorpg 에 접근할 수 있는지 확인합니다.
3. Codex Cloud의 저장소 연결 화면에서 GitHub 연결을 승인하고 이 저장소를 선택합니다. 가능하면 필요한 저장소만 접근하도록 지정합니다. 계정·플랜·조직 정책에 따라 사용 가능 여부와 화면 이름이 다를 수 있습니다.
4. 이 저장소를 대상으로 하는 클라우드 환경을 만들거나 선택합니다. 현재 문서 작업에는 별도 설치 스크립트나 Unity 설치가 필요 없습니다.
5. Codex에 “README.md, AGENTS.md, docs/STATUS.md, docs/DECISIONS.md를 읽고 현재 단계와 다음 작업을 한국어로 알려줘”라고 지시해 연결을 확인합니다.

대화만 시작했다고 저장소가 자동 연결되지는 않습니다. 작업 대상 저장소와 시작 브랜치를 매번 확인합니다. AGENTS.md는 Codex의 저장소 작업 지침이며, 일반 ChatGPT 대화에서는 필요한 문서를 직접 읽도록 요청합니다.

## 2. macOS: Apple M4 Mac mini

1. GitHub Desktop의 공식 macOS 버전을 설치하고 GitHub에 로그인합니다.
2. `File → Clone Repository`에서 저장소를 선택합니다. 복제(Clone)는 GitHub 저장소를 컴퓨터로 가져오는 작업입니다.
3. iCloud Drive·OneDrive·Dropbox 자동 동기화 폴더 밖의 로컬 개발 폴더를 선택합니다. Unity 캐시까지 파일 동기화하면 충돌이 생길 수 있습니다.
4. GitHub Desktop에서 `Fetch origin`으로 원격 변경을 확인하고, 받을 변경이 있으면 `Pull origin`으로 가져옵니다. 로컬 변경이 있다면 먼저 커밋하거나 별도로 보존합니다.
5. 공식 Unity Hub의 Apple Silicon 지원 버전을 설치합니다. Unity 계정으로 로그인하고 이용 조건·라이선스를 확인합니다.
6. 실제 Unity 프로젝트를 만들기로 한 다음 Unity 6의 정확한 버전을 결정합니다. 생성 후 `ProjectSettings/ProjectVersion.txt`에 기록된 동일 버전을 모든 편집 환경에서 사용합니다.
7. Android 개발 시 해당 편집기용 Android Build Support와 SDK·NDK·OpenJDK를 Hub에서 설치합니다. iOS 빌드는 macOS의 Xcode와 iOS 모듈이 필요하고, 배포에는 Apple의 계정·서명 요건을 별도로 확인합니다. 지금 모두 설치할 필요는 없습니다.

Unity 프로젝트를 생성하는 후속 작업에서는 Visible Meta Files와 Asset Serialization의 Force Text를 확인합니다. `Assets/`, `Packages/`, `ProjectSettings/`와 에셋의 `.meta` 파일을 Git에 기록합니다. Unity Hub로 열 폴더는 이 세 폴더가 있는 Unity 프로젝트 폴더입니다. 현재 저장소에는 아직 없습니다.

## 3. Windows PC: 클라우드 중심

1. 웹 브라우저에서 GitHub와 ChatGPT·Codex에 로그인합니다.
2. Codex Cloud에서 `mobile-mmorpg` 저장소와 최신 시작 브랜치를 선택합니다.
3. STATUS와 DECISIONS를 읽도록 요청한 뒤 작은 작업 하나를 지시합니다.
4. 변경 파일과 검증 결과를 확인하고 GitHub PR로 검토합니다.

이 흐름에서는 Windows에 Unity나 Git을 설치할 필요가 없습니다. 문서를 로컬에서도 관리하려면 GitHub Desktop으로 복제하고 macOS와 같은 Fetch·Pull·커밋·Push 절차를 사용합니다. Windows에서 Unity 편집을 추가하려면 별도 설정과 동일 버전 확인이 필요합니다.

## 4. 스마트폰·태블릿

브라우저 또는 해당 기능을 지원하는 앱에서 Codex 작업을 지시하고, GitHub 웹이나 앱으로 PR의 설명과 `Files changed`를 확인합니다. Unity 씬 편집·실행·빌드 검증은 Mac mini에서 진행합니다. 모바일에서 변경 내용을 충분히 읽기 어려우면 검토를 남겨 두고 큰 화면에서 이어갑니다.

다른 기기의 대화가 이어지지 않아도 저장소의 STATUS·DECISIONS와 PR 링크를 주면 작업을 재개할 수 있습니다.

## 5. 매 작업의 공통 흐름

1. **시작:** GitHub의 기본 브랜치와 최신 STATUS를 확인합니다. 기본 브랜치 이름은 GitHub에서 확인하며, 아래 `main` 예시는 실제 이름으로 바꿉니다.
2. **분리:** `docs/initial-setup`, `feature/작업명`처럼 목적별 브랜치를 사용합니다. 같은 파일을 여러 기기·클라우드 작업에서 동시에 수정하지 않습니다.
3. **지시:** 목적, 범위, 하지 않을 일, 완료 기준을 한국어로 전달합니다.
4. **검증:** Codex가 바꾼 파일·이유·검증 결과를 확인합니다. Unity 검증이 필요하면 Mac에서 해당 작업 브랜치를 받아 확인합니다.
5. **인계:** STATUS에 완료·남은 작업·검증 한계를, DECISIONS에 확정된 결정을 기록합니다. PR 링크는 생성된 뒤 확인 가능한 곳에 보완합니다.
6. **저장:** 변경을 커밋하고 Push하여 GitHub의 작업 브랜치에 올립니다. 클라우드 화면에서 PR 생성 기능을 사용했다면 실제 GitHub에 브랜치와 변경이 있는지 확인합니다.
7. **검토:** PR의 Files changed, 설명, 검사 결과를 검토합니다. 승인한 변경만 기본 브랜치로 병합(Merge)합니다.
8. **기기 전환:** 다음 기기에서 Fetch·Pull하거나 최신 기본 브랜치로 새 Codex 작업을 시작합니다. 진행 중인 PR을 이어가려면 그 PR과 작업 브랜치를 명시합니다.

GitHub에 올리지 않은 로컬 파일과 진행 중인 클라우드 변경은 다른 기기에 자동으로 나타나지 않습니다. 기기를 바꾸기 전에 작업 브랜치에 저장하고 PR 링크·다음 작업을 남깁니다. 기획 확정도 채팅에만 두지 말고 문서 변경으로 반영합니다.

### 작업 지시 예시

```text
mobile-mmorpg 저장소의 최신 기본 브랜치를 기준으로 시작해 줘.
AGENTS.md와 docs/STATUS.md, docs/DECISIONS.md를 먼저 읽어 줘.
목표: [이번에 할 일 한 가지]
범위: [수정해도 되는 문서나 기능]
이번에는 하지 않을 일: [예: 서버 개발, Unity 버전 변경]
완료 기준: [확인할 결과]
변경과 검증 결과를 한국어로 보여 주고 STATUS를 갱신해 줘.
```

## 6. 이번 초기 문서를 GitHub에 반영하기

현재 작업 환경에 있는 변경은 PC의 GitHub Desktop에 자동으로 나타나지 않습니다. Codex에서 변경이 보이는 작업 환경을 기준으로 커밋·푸시·PR을 요청하거나, 변경 파일을 로컬 저장소로 가져온 뒤 아래 절차를 진행합니다. 이번 변경은 `docs/initial-setup` 작업 브랜치와 PR을 통해 검토합니다. 기본 브랜치 병합은 사용자가 검토한 뒤 진행합니다. 아래 절차는 이후 새 변경을 올릴 때도 참고할 수 있습니다.

GitHub Desktop에서 파일이 보이는 경우:

1. 현재 작업 브랜치를 확인하고 기본 브랜치라면 `Branch → New Branch`로 `docs/initial-setup`을 만듭니다. 이번 파일 변경을 새 브랜치로 가져옵니다.
2. Changes에서 새 파일 6개와 기존 `.gitattributes`가 변경되지 않았는지 확인합니다.
3. 커밋 제목을 `docs: 초기 개발 문서와 Unity Git 규칙 추가`로 작성하고 Commit합니다.
4. `Publish branch` 또는 `Push origin`으로 작업 브랜치를 GitHub에 올립니다.
5. `Create Pull Request`로 PR을 열고 변경 내용과 검증 결과를 적습니다.
6. GitHub에서 Files changed를 확인한 후 사용자 판단으로 Merge합니다.
7. Mac에서 기본 브랜치로 전환해 Fetch·Pull하고 문서를 다시 확인합니다.

명령어를 사용하는 경우, **변경 파일이 있는 저장소 폴더에서** 실행합니다. Git 설치와 GitHub 인증이 필요합니다. 브랜치 이름이 이미 있으면 새 이름을 선택합니다.

```sh
git status
git switch -c docs/initial-setup
git add README.md AGENTS.md docs/STATUS.md docs/DECISIONS.md docs/SETUP.md .gitignore
git diff --cached --check
git diff --cached
git commit -m "docs: 초기 개발 문서와 Unity Git 규칙 추가"
git push -u origin docs/initial-setup
```

그다음 GitHub에서 해당 브랜치의 PR을 만들고 검토·병합합니다. `git push`만으로 기본 브랜치에 합쳐지는 것은 아닙니다. 권한·인증 오류가 나면 계정과 저장소 접근 권한을 확인하고 비밀 토큰을 채팅에 붙여 넣지 않습니다.

## 7. 충돌과 보관 규칙

- 충돌은 같은 부분의 서로 다른 수정 중 무엇을 남길지 결정해야 하는 상태입니다. 작업을 멈추고 두 변경을 비교합니다. 이해하지 못한 충돌은 Codex에 해결안과 영향을 설명하도록 요청합니다.
- Unity 씬·프리팹은 한 번에 한 작업자가 수정하고, 실행 검증 후 병합합니다. GitHub의 줄 단위 해결만으로 정상 동작을 보장할 수 없습니다.
- `.gitignore`는 캐시·빌드 산출물·로컬 설정·비밀 파일을 제외합니다. 이미 Git에 들어간 파일에는 소급 적용되지 않습니다.
- 에셋·설정·.meta는 함께 보관합니다. 큰 바이너리 에셋을 올리기 전 Git LFS(큰 파일 별도 보관)의 필요성과 요금·용량을 결정합니다. 이번에는 LFS 설정을 추가하지 않았습니다.
- 토큰, 비밀번호, 인증서, 서명 키는 GitHub에 올리지 않습니다. 실수로 올렸다면 파일 삭제만 하지 말고 해당 자격증명을 폐기·재발급하고 기록 정리를 검토합니다.

## 8. 처음 한 번 확인할 목록

- [ ] 각 기기에서 같은 GitHub 저장소 접근 가능
- [ ] Codex Cloud에서 문서를 읽고 현재 단계를 설명할 수 있음
- [ ] 작업 브랜치 → PR → 검토 → 병합 흐름 확인
- [ ] Mac에서 병합된 문서를 Pull하여 확인
- [ ] 모바일·태블릿에서 동일 PR과 STATUS 열기
- [ ] Unity 프로젝트 생성 전 정확한 Unity 버전·경로 결정

기기 간 실제 확인 결과는 STATUS에 날짜와 함께 남깁니다.
