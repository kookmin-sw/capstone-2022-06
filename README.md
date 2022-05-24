# KAOS: Kookmin AOS - 실시간 멀티플레이어 AOS 게임

[팀 홈페이지(2022-06)](https://kookmin-sw.github.io/capstone-2022-06/)

## 프로젝트 소개

KAOS는 미지의 위협이 도사리는 전장에서 팀과의 협동을 통해 적을 물리치고 상대편의 본부를 부수는 게임입니다.

게임의 장르는 Real time 3rd person AOS이며 각 팀은 4명의 전투원과 1명의 지휘관으로 구성되어 상대를 무찌르게 됩니다.

플레이어는 전투원, 지휘관 둘 중 하나의 역할을 선택하여 전투에 임합니다. 전투원은 다양한 캐릭터 중 하나를 골라 캐릭터의 특색을 이용해 상대 전투원을 제압하고 본부를 격파해 게임을 승리로 이끌어야 하며, 지휘관은 자신에게 부여된 지원 능력을 활용하여 팀이 전장에서 유리한 상황을 점할 수 있도록 돕고 마찬가지로 게임에서 승리해야 합니다.

## 시연 영상
[![미리보기](https://img.youtube.com/vi/m6UGTNKuGWw/0.jpg)](https://youtu.be/m6UGTNKuGWw)

## 소개 영상

[![미리보기](https://img.youtube.com/vi/N5zG3Yk2-gw/0.jpg)](https://youtu.be/N5zG3Yk2-gw)

## 팀 소개

|이름|학번|역할|개인 깃허브|
|-|-|-|-|
|남경태|****1614|미니언, 몬스터, 타워, Data Management, Network |[링크](https://github.com/namgyeongtae)|
|이수연|****1661|Leader, UI, Store System, Inventory, ScoreBoard|[링크](https://github.com/2Baekgu)|
|우수빈|****1314|Playable 로직, Level design, Player, Camera, Map|[링크](https://github.com/wsb8618)|
|최호경|****1717|SFX, UI, Minimap, Fog of war, 지휘관, Sound, Server|[링크](https://github.com/nicotina04)|

## 실행 방법

### 1. 실행

    리포지토리를 다운로드 받아 KAOS.exe를 실행한다.
    
### 2. 조작

    지휘관 : 마우스를 화면 구석에 두면 해당 방향으로 이동할 수 있다. Q 버튼을 눌러 적을 공격하는 스킬을 발동할 수 있다. 
    W 버튼을 눌러 아군을 치유하는 스킬을 발동할 수 있다.
    챔피언: 마우스 오른쪽 클릭으로 원하는 지점으로 이동한다. 적을 오른쪽 클릭하면 일반 공격한다.
    챔피언 고유의 Q, W, E, R 스킬로 적을 공격하거나 아군을 돕는다.

### 3. 운영자 매뉴얼

    해당 소스와 Private에 필요한 에셋을 구한다. PUN2 네트워크 설정으로 적절한 App ID를 넣어 설정을 마친다.
    UI 패널에서 Test Game 버튼을 활성화 하여 테스트 기능에 접근한다.

## Tech Stack

|사용 스택|버전|
|-|-|
|Unity Engine|2021.2.1f1|
|Photon Server PUN2|2.40|

## Service Structure

### 서비스 구조도

![시스템 배치도-Page-1](https://user-images.githubusercontent.com/83545351/169989747-23ac273c-7383-45f8-b185-d00ee6cc0d38.png)

### 챔피언 로직

![시스템 배치도-Page-2 drawio (2)](https://user-images.githubusercontent.com/83545351/169989602-e7c6c569-75a0-4a00-b7d0-fedcf8199519.png)

### 게임 기능구조

![시스템 배치도-Page-3 drawio (2)](https://user-images.githubusercontent.com/83545351/169989377-dba0db2f-5596-4d34-b497-6d3ab7d7d8fe.png)