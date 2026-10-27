# mobile-mmorpg

프로그래밍 경험이 없는 게임 기획자가 ChatGPT와 Codex를 활용해 모바일 MMORPG를 만드는 프로젝트입니다. Windows PC, macOS, 스마트폰, 태블릿에서 같은 GitHub 저장소를 기준으로 작업을 이어가는 것이 첫 번째 목표입니다.

현재는 **개발 문서와 작업 규칙을 준비한 단계**입니다. Unity 프로젝트, 게임 코드, 서버는 아직 만들지 않았습니다.

## 기기별 역할

| 기기·도구 | 역할 |
| --- | --- |
| Apple M4 Mac mini | 메인 개발 컴퓨터. Unity 6 편집기 실행, 씬 편집, 실제 실행·빌드 검증 |
| Windows PC | 브라우저에서 ChatGPT·Codex Cloud로 작업 지시와 변경 검토 |
| 스마트폰·태블릿 | 브라우저 또는 지원 앱에서 기획 전달, 진행 확인, PR 검토 |
| GitHub | 확정된 문서·코드·중요 변경의 기준 |
| ChatGPT | 기획 정리와 초보자를 위한 설명 |
| Codex Cloud | 연결된 저장소의 문서를 읽고 작업하며 변경안을 제공 |

클라우드 작업이 Mac의 Unity 편집기를 자동 조작하거나 모든 기기의 미저장 파일을 동기화하는 것은 아닙니다. Unity 실행 결과는 Mac에서 별도로 확인합니다.

## 먼저 읽을 문서

1. [설치와 기기 간 작업 흐름](docs/SETUP.md)
2. [현재 상태와 다음 할 일](docs/STATUS.md)
3. [확정된 결정과 미정 사항](docs/DECISIONS.md)
4. [Codex 개발 규칙](AGENTS.md)

## 현재 구조

```text
mobile-mmorpg/
├── .gitattributes       # 기존 텍스트 줄바꿈 설정 보존
├── .gitignore          # Unity 임시 파일·빌드 결과·비밀정보 제외
├── README.md           # 프로젝트 입구
├── AGENTS.md           # Codex 작업 규칙
└── docs/
    ├── STATUS.md       # 진행 상황과 인계
    ├── DECISIONS.md    # 주요 결정 기록
    └── SETUP.md        # 기기별 준비와 GitHub 작업 방법
```

2026-10-27에 확인한 작업 사본은 `.gitattributes`만 추적하고 있었으며, 이 문서와 규칙을 추가했습니다. 저장소 주소: https://github.com/mca-kor/mobile-mmorpg

## 작업의 기본 순서

최신 GitHub 확인 → STATUS·DECISIONS 읽기 → 작은 작업 하나 지시 → 변경·검증 결과 확인 → PR 검토·병합 → 다음 기기에서 최신 내용 받기.

PR(Pull Request)은 변경을 본류에 합치기 전에 검토하는 제안서입니다. 브랜치는 작업을 분리하는 공간이고, 커밋은 변경을 기록한 저장 지점입니다. 자세한 절차는 [SETUP](docs/SETUP.md)을 따릅니다.

Unity 6의 정확한 버전과 프로젝트 경로, 서버 기술, 네트워크 방식은 아직 미정입니다. 결정 후 기록하고 진행합니다.
